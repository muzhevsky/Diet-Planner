using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine.UI;
using System;

public class DBOperator
{
    string fileConnection;

    IDbConnection dbConnection;
    IDbCommand command;

    string DATABASE_NAME = "/someDB.bytes";
    public DBOperator()
    {
        string filepath = Application.dataPath + "/db" + DATABASE_NAME;
        fileConnection = "URI=file:" + filepath;
        dbConnection = new SqliteConnection(fileConnection);
    }
    public bool AddUserToDB(RegistrationInfo regInfo)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        try
        {
            command.CommandText = "INSERT INTO user(login, password, phone_number, name)" +
            " VALUES('" + regInfo.Login + "', '" + regInfo.Password + "', '" + regInfo.Phone + "', '"+regInfo.DisplayedName+"')";
            command.ExecuteScalar();

            command.CommandText = "SELECT id FROM user WHERE login = '" + regInfo.Login + "'";
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                PlayerPrefs.SetInt("user_id", reader.GetInt32(0));
                reader.Close();
                dbConnection.Close();
                return true;
            }

            dbConnection.Close();
            return true;
        }
        catch(SqliteException e)
        {
            Debug.Log(e.Message);
            dbConnection.Close();
            return false;
        }
    }
    public bool CheckLogin(LoginInfo logInfo)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        try
        {
            command.CommandText = "SELECT password, id FROM user WHERE login = '" + logInfo.Login + "'";
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetString(0) == logInfo.Password)
                {
                    PlayerPrefs.SetInt("user_id", reader.GetInt32(1));
                    dbConnection.Close();
                    return true;
                }
            }
            dbConnection.Close();
            return false;
        }
        catch(SqliteException ex)
        {
            Debug.Log(ex.Message);
            dbConnection.Close();
            return false;
        }
    }
    public void AddTestInfo(int userId, AnswersList answersList)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "INSERT INTO user_data(gender_id,goal_id,height,weight,desired_weight,eating_frequency_id, activity_level_id)" +
            " VALUES(" + (int)answersList.Gender + "," + (int)answersList.Goal + "," + answersList.Height + "," + answersList.Weight + "," + answersList.DesiredWeight + "," + (int)answersList.EatingFrequency + "," + (int)answersList.ActivityLevel + ")";
        command.ExecuteScalar();

        command.CommandText = "SELECT id FROM user_data ORDER BY id DESC LIMIT 1";
        var reader = command.ExecuteReader();
        int data_id = 0;
        if (reader.Read())
        {
            data_id = reader.GetInt32(0);
            reader.Close();
        }

        command.CommandText = "UPDATE user SET user_data_id=" + data_id+ " WHERE id=" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();

        foreach(Allergenes item in answersList.Allergenes)
        {
            command.CommandText = "INSERT INTO allergenes_links(user_id, allergen_id) VALUES(" + PlayerPrefs.GetInt("user_id") + "," + (int)item+")";
            command.ExecuteScalar();
        }
        dbConnection.Close();
    }

    public ProfileData GetUserViewData()
    {
        ProfileData userData = new ProfileData();
        int userDataId = 0;
        int dietId = 0;
        int goalId = 0;

        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT name,login,user_data_id FROM user WHERE id=" + PlayerPrefs.GetInt("user_id");

        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.Name = reader.GetString(0);
            userData.Login = reader.GetString(1);
            userDataId = reader.GetInt32(2);
            reader.Close();
        }

        command.CommandText = "SELECT weight,height,diet_id,goal_id FROM user_data WHERE id=" + userDataId.ToString();
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.Weight = reader.GetInt32(0);
            userData.Height = reader.GetInt32(1);
            //dietId = reader.GetInt32(2);
            goalId = reader.GetInt32(3);
            reader.Close();
        }

        command.CommandText = "SELECT name FROM goals WHERE id=" + goalId.ToString();
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.Goal = reader.GetString(0);
            reader.Close();
        }

        command.CommandText = "SELECT allergen_id FROM allergenes_links WHERE user_id=" + PlayerPrefs.GetInt("user_id")+" LIMIT 1";
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.Allergenes_id = reader.GetInt32(0);
            reader.Close();
        }

        dbConnection.Close();
        return userData;
    }

    public void AddProduct(Product product)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT amount_left FROM user_ingredients WHERE ingredient_id=" + product.Id;
        var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            reader.Close();
            command.CommandText = "INSERT INTO user_ingredients(ingredient_id,amount_left) VALUES('" + product.Id + "'," + product.Amount + "')";
            command.ExecuteScalar();
        }
        else
        {
            reader.Close();

            command.CommandText = "UPDATE user_ingredients SET amount_left=" + product.Amount +" WHERE ingredient_id="+product.Id;
            command.ExecuteScalar();
        }
        dbConnection.Close();
    }

    public void CompleteMeal(Meal meal)
    {
        foreach(Food food in meal.FoodList)
        {
            foreach(Product ingredient in food.Ingredients)
            {
                SpendIngredients(ingredient);
            }
        }
    }

    public void SpendIngredients(Product ingredient)
    {
        dbConnection.Open();
        int amount = 0;
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT amount_left FROM user_ingredients WHERE ingredient_id=" + ingredient.Id;
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            amount = reader.GetInt32(0)-ingredient.Amount;
        }
        reader.Close();
        if (amount < 0) amount = 0;

        command.CommandText = "UPDATE user_ingredients SET amount_left=" + amount + " WHERE ingredient_id=" + ingredient.Id;

        dbConnection.Close();
    }

    public void EditAdditionalUserInfo(Goal goal, List<Allergenes> allergenes)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT user_data_id FROM user WHERE id="+PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        int dataId = 0;
        if (reader.Read())
        {
            dataId = reader.GetInt32(0);
        }
        reader.Close();

        command.CommandText = "UPDATE user_data SET goal_id ="+ (int)goal+" WHERE id=" + dataId;
        command.ExecuteScalar();

        command.CommandText = "DELETE FROM allergenes_links WHERE user_id=" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();

        foreach (Allergenes item in allergenes)
        {
            command.CommandText = "INSERT INTO allergenes_links(user_id, allergen_id) VALUES(" + PlayerPrefs.GetInt("user_id") + "," + (int)item + ")";
            command.ExecuteScalar();
        }
        dbConnection.Close();
    }

    public void EditMainUserInfo(string email, int weight, int height)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();

        command.CommandText = "UPDATE user SET login ='" + email + "' WHERE id=" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();

        command.CommandText = "SELECT user_data_id FROM user WHERE id=" + PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        int dataId = 0;
        if (reader.Read())
        {
            dataId = reader.GetInt32(0);
        }
        reader.Close();

        command.CommandText = "UPDATE user_data SET weight =" + weight + " WHERE id=" + dataId;
        command.ExecuteScalar();

        command.CommandText = "UPDATE user_data SET height =" + height + " WHERE id=" + dataId;
        command.ExecuteScalar();

    }

    public List<Product> GetUserProducts()
    {
        List<Product> products = new List<Product>();
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT amount_left, ingredient_id FROM user_ingredients WHERE user_id="+PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Product productItem = new Product();
            productItem.Amount = reader.GetInt32(0);
            productItem.Id = reader.GetInt32(1);
            products.Add(productItem);
        }
        reader.Close();

        for(int i = 0; i < products.Count; i++)
        {
            command.CommandText = "SELECT name FROM ingredients WHERE id="+ products[i].Id;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                products[i].Name = reader.GetString(0);
            }
            reader.Close();
        }

        dbConnection.Close();
        return products;
    }

    public List<DietInfo> GetDiets(Goal goalId)
    {
        List<DietInfo> result = new List<DietInfo>();
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT id,name,description FROM diets WHERE goal_id="+(int)goalId;
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            DietInfo newDietInfo = new DietInfo();
            newDietInfo.Id = reader.GetInt32(0);
            newDietInfo.Name = reader.GetString(1);
            newDietInfo.Description = reader.GetString(2);
            result.Add(newDietInfo);
        }
        return result;
    }

    public void SetDiet(DietInfo diet)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT user_data_id FROM user WHERE id=" + PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        int dataId = 0;
        if (reader.Read())
        {
            dataId = reader.GetInt32(0);
        }

        command = dbConnection.CreateCommand();
        command.CommandText = "UPDATE user_data SET diet_id=" + diet.Id + " WHERE id=" + dataId;
        command.ExecuteScalar();

        dbConnection.Close();
    }

    public Meal GetMeal(UserData data)
    {
        Meal result = new Meal();
        int dailyMenuId = 0;
        int mealId = 0;
        int mealTypeId = 0;

        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT daily_menu_id FROM diet_link WHERE diet_id=" + data.DietId+" AND day="+(int)DateTime.Today.DayOfWeek;
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            dailyMenuId = reader.GetInt32(0);
        }
        reader.Close();

        double time = DateTime.Now.TimeOfDay.TotalHours;
        if (!data.HadBreakfastToday) { command.CommandText = "SELECT breakfast_id FROM daily_menu WHERE id=" + dailyMenuId; }
        else if (!data.HadLunchToday) { command.CommandText = "SELECT lunch_id FROM daily_menu WHERE id=" + dailyMenuId; }
        else if (!data.HadSupperToday) { command.CommandText = "SELECT supper_id FROM daily_menu WHERE id=" + dailyMenuId; }
        else return null;

        reader = command.ExecuteReader();
        if (reader.Read())
        {
            mealId = reader.GetInt32(0);
        }
        reader.Close();

        command.CommandText = "SELECT meal_type FROM meal WHERE id=" + mealId;
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            mealTypeId = reader.GetInt32(0); 
        }
        reader.Close();

        command.CommandText = "SELECT type FROM meal_types WHERE id=" + mealTypeId;
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            result.Type = reader.GetString(0);
        }
        reader.Close();

        command.CommandText = "SELECT food_id FROM meal_link WHERE meal_id=" + mealId;
        reader = command.ExecuteReader();
        result.FoodList = new List<Food>();
        while (reader.Read())
        {
            result.FoodList.Add(new Food());
            result.FoodList[result.FoodList.Count - 1].Id = reader.GetInt32(0);
        }
        reader.Close();

        foreach(Food item in result.FoodList)
        {
            command.CommandText = "SELECT name,calories,proteins,fats,carbohydrates,recipe FROM food WHERE id=" + item.Id;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                item.Name = reader.GetString(0);
                item.Calories = reader.GetInt32(1);
                item.Proteins = reader.GetFloat(2);
                item.Fats = reader.GetFloat(3);
                item.Carbohydrates = reader.GetFloat(4);
                item.Recipe = reader.GetString(5);
            }
            reader.Close();
        }

        
        foreach (Food item in result.FoodList)
        {
            item.Ingredients = new List<Product>();
            command.CommandText = "SELECT ingredient_id,amount FROM ingredients_link WHERE food_id=" + item.Id;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                item.Ingredients.Add(new Product());
                item.Ingredients[item.Ingredients.Count - 1].Id = reader.GetInt32(0);
                item.Ingredients[item.Ingredients.Count - 1].Amount = reader.GetInt32(1);
            }
            reader.Close();

            foreach(Product product in item.Ingredients)
            {
                command.CommandText = "SELECT name,measure FROM ingredients WHERE id=" + product.Id;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product.Name = reader.GetString(0);
                    product.Measure = reader.GetString(1);
                }
                reader.Close();
            }
        }
        if (result.Type == "Завтрак") data.HadBreakfastToday = true;
        if (result.Type == "Обед") data.HadLunchToday = true;
        if (result.Type == "Ужин") data.HadSupperToday = true;
        dbConnection.Close();
        return result;
    }
    public UserData GetUserData()
    {
        UserData userData = new UserData();
        int userDataId = 0;
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT id,name,login,phone_number,user_data_id FROM user WHERE id=" + PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.Id = reader.GetInt32(0);
            userData.Name = reader.GetString(1);
            userData.Login = reader.GetString(2);
            userData.PhoneNumber = reader.GetString(3);
            userDataId = reader.GetInt32(4);
        }
        reader.Close();

        command.CommandText = "SELECT gender_id,goal_id,weight,height,diet_id FROM user_data WHERE id="+userDataId;
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.GenderId = (Gender)reader.GetInt32(0);
            userData.GoalId = (Goal)reader.GetInt32(1);
            userData.Weight = reader.GetInt32(2);
            userData.Height = reader.GetInt32(3);
            userData.DietId = reader.GetInt32(4);
        }
        reader.Close();
        dbConnection.Close();

        return userData;
    }
}