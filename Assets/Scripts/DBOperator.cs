using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.Android;

public static class DBOperator
{
    static IDbConnection dbConnection;
    static IDbCommand command;

    static string DATABASE_NAME = "database.db";
    public static void Init()
    {
        string DBPath = GetDatabasePath();
        dbConnection = new SqliteConnection("Data Source=" + DBPath);
    }
    static string GetDatabasePath()
    {
#if UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, DATABASE_NAME);
#endif
#if UNITY_ANDROID
        string filePath = Path.Combine(Application.persistentDataPath, DATABASE_NAME);
        if (!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
#endif
    }
    static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, DATABASE_NAME);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }
    public static bool AddUserToDB(RegistrationInfo regInfo)
    {
        try
        {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        
            command.CommandText = "INSERT INTO user(login, password, phone_number, name)" +
            " VALUES('" + regInfo.Login + "', '" + regInfo.Password + "', '" + regInfo.Phone + "', '"+regInfo.DisplayedName+"')";
            command.ExecuteScalar();

            command.CommandText = "SELECT id FROM user WHERE login = '" + regInfo.Login + "'";
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                PlayerPrefs.SetInt("user_id", reader.GetInt32(0));
            }
            reader.Close();

            dbConnection.Close();
            return true;
        }
        catch(SqliteException e)
        {
            dbConnection.Close();
            return false;
        }
    }
    public static bool CheckLogin(LoginInfo logInfo)
    {
        try
        {
            dbConnection.Open();
        command = dbConnection.CreateCommand();
        
            command.CommandText = "SELECT password, id FROM user WHERE login = '" + logInfo.Login + "'";
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetString(0) == logInfo.Password)
                {
                    int id = reader.GetInt32(1);
                    PlayerPrefs.SetInt("user_id", id);
                    dbConnection.Close();
                    return true;
                }
            }
            reader.Close();
            dbConnection.Close();
            return false;
        }
        catch(SqliteException ex)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Text>().text = ex.Message;
            Debug.Log(ex.Message);
            dbConnection.Close();
            return false;
        }
    }
    public static void AddTestInfo(AnswersList answersList)
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
        }
        reader.Close();

        command.CommandText = "UPDATE user SET user_data_id=" + data_id+ " WHERE id=" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();
        GlobalController.UserData.Id = data_id;

        foreach(Allergenes item in answersList.Allergenes)
        {
            command.CommandText = "INSERT INTO allergenes_links(user_id, allergen_id) VALUES(" + PlayerPrefs.GetInt("user_id") + "," + (int)item+")";
            command.ExecuteScalar();
        }
        dbConnection.Close();

    }

    public static ProfileData GetUserViewData()
    {
        ProfileData profileData = new ProfileData();
        int userDataId = 0;
        int dietId = 0;
        int goalId = 0;

        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT name,login,user_data_id FROM user WHERE id=" + PlayerPrefs.GetInt("user_id");

        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            profileData.Name = reader.GetString(0);
            profileData.Login = reader.GetString(1);
            userDataId = reader.GetInt32(2);
        }
        reader.Close();

        command.CommandText = "SELECT weight,height,diet_id,goal_id FROM user_data WHERE id=" + userDataId.ToString();
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            profileData.Weight = reader.GetInt32(0);
            profileData.Height = reader.GetInt32(1);
            dietId = reader.GetInt32(2);
            goalId = reader.GetInt32(3);
        }
        reader.Close();

        command.CommandText = "SELECT name FROM goals WHERE id=" + goalId.ToString();
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            profileData.Goal = reader.GetString(0);
        }
        reader.Close();

        List<int> allergenesIds = new List<int>();
        command.CommandText = "SELECT allergen_id FROM allergenes_links WHERE user_id=" + PlayerPrefs.GetInt("user_id");
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            allergenesIds.Add(reader.GetInt32(0));
        }
        reader.Close();

        profileData.AllergenesNames = new List<string>();
        foreach(int id in allergenesIds)
        {
            command.CommandText = "SELECT name FROM allergenes WHERE id=" + id;
            reader = command.ExecuteReader();
            if (reader.Read()) profileData.AllergenesNames.Add(reader.GetString(0));
            reader.Close();
        }

        command.CommandText = "SELECT name FROM diets WHERE id=" + dietId.ToString();
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            profileData.Diet = reader.GetString(0);
        }
        reader.Close();

        dbConnection.Close();
        return profileData;
    }

    public static void AddProduct(Product product)
    {
        dbConnection.Open();
        int id = 0;
        int amountLeft;
        int userId = PlayerPrefs.GetInt("user_id");
        command = dbConnection.CreateCommand();

        command.CommandText = "SELECT id FROM ingredients WHERE name='" + product.Name+"'";
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            id = reader.GetInt32(0);
        }
        reader.Close();

        command.CommandText = "SELECT amount_left FROM user_ingredients WHERE ingredient_id=" + id + " AND user_id="+ userId;
        reader = command.ExecuteReader();
        if (!reader.Read())
        {
            reader.Close();
            command.CommandText = "INSERT INTO user_ingredients(user_id,ingredient_id,amount_left) VALUES(" +userId + "," + id + "," + product.Amount + ")";
            command.ExecuteScalar();
        }
        else
        {
            amountLeft = reader.GetInt32(0);
            reader.Close();

            int temp = product.Amount + amountLeft;
            command.CommandText = "UPDATE user_ingredients SET amount_left=" + temp + " WHERE ingredient_id="+id +" AND user_id="+userId;
            command.ExecuteScalar();
        }
        dbConnection.Close();
    }

    public static void CompleteMeal(Meal meal)
    {
        if (meal != null)
        {
            foreach (Food food in meal.FoodList)
            {
                foreach (Product ingredient in food.Ingredients)
                {
                    SpendIngredients(ingredient);
                }
            }
        }
    }

    public static void SpendIngredients(Product ingredient)
    {
        dbConnection.Open();
        int amount = 0;
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT amount_left FROM user_ingredients WHERE ingredient_id=" + ingredient.Id+" AND user_id="+PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            amount = reader.GetInt32(0)-ingredient.Amount;
        }
        reader.Close();
        if (amount < 0) amount = 0;

        command.CommandText = "UPDATE user_ingredients SET amount_left=" + amount + " WHERE ingredient_id=" + ingredient.Id + " AND user_id=" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();
        dbConnection.Close();
    }

    public static void UpdateUserInfo(UserData userData)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();

        if (userData.GoalId != 0)
        {
            command.CommandText = "UPDATE user_data SET goal_id =" + (int)userData.GoalId + " WHERE id=" + userData.Id;
            command.ExecuteScalar();
        }

        if (userData.Login != null)
        {
            command.CommandText = "UPDATE user SET login ='" + userData.Login + "' WHERE id=" + PlayerPrefs.GetInt("user_id");
            command.ExecuteScalar();
        }

        if (userData.Weight != 0)
        {
            command.CommandText = "UPDATE user_data SET weight =" + userData.Weight + " WHERE id=" + userData.Id;
            command.ExecuteScalar();
        }

        if (userData.Height != 0)
        {
            command.CommandText = "UPDATE user_data SET height =" + userData.Height + " WHERE id=" + userData.Id;
            command.ExecuteScalar();
        }

        if (userData.ActivityLevel != 0)
        {
            command.CommandText = "UPDATE user_data SET activity_level_id =" + (int)userData.ActivityLevel + " WHERE id=" + userData.Id;
            command.ExecuteScalar();
        }

        if (userData.EatingFrequency != 0)
        {
            command.CommandText = "UPDATE user_data SET eating_frequency_id =" + (int)userData.EatingFrequency + " WHERE id=" + userData.Id;
            command.ExecuteScalar();
        }

        command.CommandText = "DELETE FROM allergenes_links WHERE user_id=" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();

        foreach (Allergenes item in userData.Allergenes)
        {
            command.CommandText = "INSERT INTO allergenes_links(user_id, allergen_id) VALUES(" + PlayerPrefs.GetInt("user_id") + "," + (int)item + ")";
            command.ExecuteScalar();
        }

        dbConnection.Close();

        UpdateWeights(GlobalController.Month);
    }

    public static List<Product> GetUserProducts()
    {
        List<Product> products = new List<Product>();
        if (dbConnection.State == ConnectionState.Open) dbConnection.Close();
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT amount_left,ingredient_id FROM user_ingredients WHERE user_id="+PlayerPrefs.GetInt("user_id");
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
            command.CommandText = "SELECT name,measure FROM ingredients WHERE id="+ products[i].Id;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                products[i].Name = reader.GetString(0);
                products[i].Measure = reader.GetString(1);
            }
            reader.Close();
        }

        dbConnection.Close();
        return products;
    }

    public static List<DietInfo> GetDiets()
    {
        List<DietInfo> result = new List<DietInfo>();
        dbConnection.Open();
        command = dbConnection.CreateCommand();

        List<int> dietIds = new List<int>();

        command.CommandText = "SELECT id FROM diets WHERE goal_id=" + (int)GlobalController.UserData.GoalId;
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            dietIds.Add(reader.GetInt32(0));
        }
        reader.Close();

        for(int i = 0; i < dietIds.Count; i++)
        {
            bool flag = false;
            for(int j = 0; j < GlobalController.UserData.Allergenes.Count; j++)
            {
                if (flag) break;
                command.CommandText = "SELECT allergen_id FROM diets_and_allergenes WHERE diet_id=" + dietIds[i];
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(0) == (int)GlobalController.UserData.Allergenes[j])
                    {
                        dietIds.RemoveAt(i);
                        flag = true;
                        reader.Close();
                        break;
                    }
                }
                reader.Close();
            }
        }

        foreach(int id in dietIds)
        {
            command.CommandText = "SELECT name,description FROM diets WHERE id="+id;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                DietInfo newDietInfo = new DietInfo();
                newDietInfo.Id = id;
                newDietInfo.Name = reader.GetString(0);
                newDietInfo.Description = reader.GetString(1);
                result.Add(newDietInfo);
            }

            reader.Close();
        }
        dbConnection.Close();
        return result;
    }

    public static void SetDiet(DietInfo diet)
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
        reader.Close();
        command = dbConnection.CreateCommand();
        command.CommandText = "UPDATE user_data SET diet_id=" + diet.Id + " WHERE id=" + dataId;
        command.ExecuteScalar();

        dbConnection.Close();
    }

    public static Meal GetMeal(UserData data)
    {
        Meal result = new Meal();
        int dailyMenuId = 0;
        int mealId = 0;
        int mealTypeId = 0;

        if (dbConnection.State == ConnectionState.Open) dbConnection.Close();
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        if(DateTime.Today.DayOfWeek!=0)command.CommandText = "SELECT daily_menu_id FROM diet_link WHERE diet_id=" + data.DietId+" AND day="+(int)DateTime.Today.DayOfWeek;
        else command.CommandText = "SELECT daily_menu_id FROM diet_link WHERE diet_id=" + data.DietId + " AND day=7";
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            dailyMenuId = reader.GetInt32(0);
        }
        reader.Close();

        double time = DateTime.Now.TimeOfDay.TotalHours;
        if (PlayerPrefs.GetInt("HadBreakfastToday") == 0) { command.CommandText = "SELECT breakfast_id FROM daily_menu WHERE id=" + dailyMenuId; }
        else if (PlayerPrefs.GetInt("HadLunchToday") == 0) { command.CommandText = "SELECT lunch_id FROM daily_menu WHERE id=" + dailyMenuId; }
        else if (PlayerPrefs.GetInt("HadSupperToday") == 0) { command.CommandText = "SELECT supper_id FROM daily_menu WHERE id=" + dailyMenuId; }
        else
        {
            dbConnection.Close();
            return null;
        }
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
                float test = reader.GetFloat(2);
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
        dbConnection.Close();
        return result;
    }
    public static UserData GetUserData()
    {
        UserData userData = new UserData();
        int userDataId = 0;
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT user_data_id,name,login,phone_number,user_data_id FROM user WHERE id=" + PlayerPrefs.GetInt("user_id");
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

        command.CommandText = "SELECT gender_id,goal_id,weight,height,diet_id,activity_level_id,eating_frequency_id,desired_weight FROM user_data WHERE id="+userDataId;
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            userData.GenderId = (Gender)reader.GetInt32(0);
            userData.GoalId = (Goal)reader.GetInt32(1);
            userData.Weight = reader.GetInt32(2);
            userData.Height = reader.GetInt32(3);
            userData.DietId = reader.GetInt32(4);
            userData.ActivityLevel = (ActivityLevel)reader.GetInt32(5);
            userData.EatingFrequency = (EatingFrequency)reader.GetInt32(6);
            userData.DesiredWeight = reader.GetInt32(7);
        }
        reader.Close();


        userData.Allergenes = new List<Allergenes>();
        command.CommandText = "SELECT allergen_id FROM allergenes_links WHERE user_id=" + PlayerPrefs.GetInt("user_id");
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            userData.Allergenes.Add((Allergenes)reader.GetInt32(0));
        }
        dbConnection.Close();

        return userData;
    }

    public static List<Product> GetAllDietProducts(UserData userData)
    {
        List<Product> result = new List<Product>();

        dbConnection.Open();
        command = dbConnection.CreateCommand();

        List<int> mealIds = new List<int>();
        command.CommandText = "SELECT breakfast_id,lunch_id,supper_id FROM daily_menu WHERE diet_id=" + userData.DietId;

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            mealIds.Add(reader.GetInt32(0));
            mealIds.Add(reader.GetInt32(1));
            mealIds.Add(reader.GetInt32(2));
        }
        reader.Close();


        List<int> foodIds = new List<int>();
        foreach(int mealId in mealIds){
            command.CommandText = "SELECT food_id FROM meal_link WHERE meal_id=" + mealId;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                foodIds.Add(reader.GetInt32(0));
            }
            reader.Close();
        }

        List<int> ingredientsIds = new List<int>();
        foreach(int foodId in foodIds)
        {
            command.CommandText = "SELECT ingredient_id FROM ingredients_link WHERE food_id=" + foodId;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ingredientsIds.Add(reader.GetInt32(0));
            }
            reader.Close();
        }
        
        for(int i = 0; i < ingredientsIds.Count; i++)
        {
            for(int j = i+1; j < ingredientsIds.Count; j++)
            {
                if(ingredientsIds[j] == ingredientsIds[i])
                {
                    ingredientsIds.RemoveAt(j);
                }
            }
        }

        foreach(int ingredientId in ingredientsIds)
        {
            string measure = "";
            string name = "";

            command.CommandText = "SELECT name,measure FROM ingredients WHERE id=" + ingredientId;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader.GetString(0);
                measure = reader.GetString(1);
            }
            reader.Close();

            result.Add(new Product());
            result[result.Count - 1].Id = ingredientId;
            result[result.Count - 1].Name = name;
            result[result.Count - 1].Measure = measure;
        }

        dbConnection.Close();
        return result;
    }

    public static void UpdateWeights(int month)
    {
        dbConnection.Open();
        
        command.CommandText = "SELECT month"+month+" FROM user_weights WHERE user_id=" + PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            reader.Close();
            command.CommandText = "INSERT INTO user_weights(user_id,month1,month2,month3,month4,month5,month6,month7,month8,month9,month10,month11,month12) VALUES(" + PlayerPrefs.GetInt("user_id") + ",0,0,0,0,0,0,0,0,0,0,0,0)";
            command.ExecuteScalar();
        }
        reader.Close();
        command.CommandText = "UPDATE user_weights SET month" + month + "= " + GlobalController.UserData.Weight + " WHERE user_id =" + PlayerPrefs.GetInt("user_id");
        command.ExecuteScalar();
        dbConnection.Close();
    }

    public static int[] GetLastWeights()
    {
        int[] result = new int[6];
        dbConnection.Open();
        command.CommandText = "SELECT month1,month2,month3,month4,month5,month6,month7,month8,month9,month10,month11,month12 from user_weights WHERE user_id=" + PlayerPrefs.GetInt("user_id");
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            int month = GlobalController.Month;
            if (month < 6) month += 12;
            for(int i = 5; i >= 0; i--)
            {
                result[i] = reader.GetInt32(month%12-1);
                month--;
            }
        }
        reader.Close();
        dbConnection.Close();
        return result;
    }

    public static Product GetProductByCode(string code)
    {

        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT name,measure,default_quantity FROM ingredients WHERE barcode='" + code + "'";
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Product result = new Product();
            result.Name = reader.GetString(0);
            result.Measure = reader.GetString(1);
            result.Amount = reader.GetInt32(2);
            reader.Close();
            return result;
        }
        else
        {
            reader.Close();
            return null;
        }
    }
}