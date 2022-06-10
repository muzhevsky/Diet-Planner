using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using UnityEngine.UI;

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

    public UserData GetUserData()
    {
        UserData userData = new UserData();
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
        //command.CommandText = "SELECT name FROM diets WHERE id=" + goalId.ToString();
        //reader = command.ExecuteReader();
        //if (reader.Read())
        //{
        //    userData.Diet = reader.GetString(0);
        //    reader.Close();
        //}

        dbConnection.Close();
        return userData;
    }

    public void AddProduct(Product product)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT amount FROM user_ingredients WHERE name=" + product.Name;
        var reader = command.ExecuteReader();
        while (reader.Read()) { }
        if (reader.IsDBNull(0))
        {
            reader.Close();
            command.CommandText = "INSERT INTO user_ingredients(name,amount) VALUES('" + product.Name + "'," + product.Amount + "')";
        }
        else
        {
            int amount = reader.GetInt32(0);
            reader.Close();

            amount += product.Amount;
            command.CommandText = "UPDATE user_ingredients SET amount=" + amount +" WHERE name="+product.Name;
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
        command.CommandText = "SELECT amount FROM user_ingredients WHERE name=" + ingredient.Name;
        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            amount = reader.GetInt32(0)-ingredient.Amount;
            reader.Close();
        }

        if (amount >= 0) command.CommandText = "UPDATE user_ingredients SET amount=" + amount + " WHERE name=" + ingredient.Name;
    }
}