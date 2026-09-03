using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RBSRetail.Users;

// To do:
// Add a User
// View all Users
// Modify User account types

public enum AccountType
{
    Manager,
    Cashier
}

class User
{

    /// <summary>
    /// Adds a new user to the Users table in the SQLite database.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="account_type"></param>
    public static void AddUser(string username, string password, AccountType account_type)
    {
        string connectionString = "Data Source=RBS_Retail.db";

        string addUserSql = @"
            INSERT INTO Users (username, password, account_type)
            VALUES (@username, @password, @account_type);";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(addUserSql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@account_type", account_type);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a user from the Users table in the SQLite database based on the provided user_id.
    /// </summary>
    /// <param name="user_id"></param>
    public static void RemoveUser(int user_id)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string removeUserSql = "DELETE FROM Users WHERE user_id = @user_id";

        using (var connection = new SqliteConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (var command = new SqliteCommand(removeUserSql, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQLite Error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Changes the account type of a user in the Users table based on the provided user_id and new_account_type.
    /// </summary>
    /// <param name="user_id"></param>
    /// <param name="new_account_type"></param>
    public static void ChangeAccountType(int user_id, AccountType new_account_type)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string changeAccountTypeSql = "UPDATE Users SET account_type = @new_account_type WHERE user_id = @user_id   ";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(changeAccountTypeSql, connection))
                {
                    command.Parameters.AddWithValue("@new_account_type", new_account_type);
                    command.Parameters.AddWithValue("@user_id", user_id );
                    command.ExecuteNonQuery();
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"SQLite Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Displays all users from the Users table
    /// </summary>
    public static void ShowAllUsers()
    {
        string connectionString = $"Data Source=RBS_Retail.db";
        string showAllUsersSql = "SELECT * FROM Users";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(showAllUsersSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int user_id = reader.GetInt32(0);
                                string user_name = reader.GetString(1);
                                string account_type = reader.GetString(2);
                                Console.WriteLine($"User ID: {user_id}, Username: {user_name}, Account Type: {account_type}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No records found in the table.");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"SQLite Error: {ex.Message}");
        }
    }
    
}
