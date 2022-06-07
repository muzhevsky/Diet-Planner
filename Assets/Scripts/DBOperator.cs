using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DBOperator
{
    string fileConnection;

    IDbConnection dbConnection;
    IDbCommand command;

    string DATABASE_NAME = "/someDB.bytes";
    public DBOperator()
    {
        string filepath = Application.dataPath + "/StreamingAssets" + DATABASE_NAME;
        fileConnection = "URI=file:" + filepath;
        dbConnection = new SqliteConnection(fileConnection);
    }
    public bool AddUserToDB(RegistrationInfo regInfo)
    {
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        try
        {
            command.CommandText = "INSERT INTO User(login, password, phoneNumber)" +
            " VALUES('" + regInfo.Login + "', '" + regInfo.Password + "', '" + regInfo.Phone + "')";
            command.ExecuteScalar();
            dbConnection.Close();
            return true;
        }
        catch
        {
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
            command.CommandText = "SELECT password FROM User WHERE login = '" + logInfo.Login + "'";
            Debug.Log(command.CommandText);
            var reader = command.ExecuteReader();
            if (reader.GetString(0) == logInfo.Password)
            {
                dbConnection.Close();
                return true;
            }
            dbConnection.Close();
            return false;
        }
        catch
        {
            dbConnection.Close();
            return false;
        }
    }
}

public enum MyDBErrorCode
{

}