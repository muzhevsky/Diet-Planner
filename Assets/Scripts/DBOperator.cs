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

        command.CommandText = "SELECT id FROM user_data ORDER BY id DESC LIMIT 1;";
        var reader = command.ExecuteReader();
        int id = 0;
        while (reader.Read())
        {
            id = reader.GetInt32(0);
        }


        command.CommandText = "UPDATE user SET user_data_id=" + id;
        command.ExecuteScalar();
        dbConnection.Close();
    }
}