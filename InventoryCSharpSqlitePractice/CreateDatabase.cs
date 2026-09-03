using Microsoft.Data.Sqlite;

namespace RBSRetail.CreateDatabase;

class Database
{

    public string DatabaseName { get; set; }

    public Database(string databaseName)
    {
        DatabaseName = databaseName;
    }

    /// <summary>
    /// Create the database if it does not exist.
    /// </summary>
    public void CreateDatabase()
    {
        // Create the database
        string connectionString = $"Data Source={DatabaseName}";

        // Create each table 
        string createItemsTable = @"
           CREATE TABLE IF NOT EXISTS Items (
                item_id INTEGER PRIMARY KEY AUTOINCREMENT,
                item_name TEXT NOT NULL,
                uom TEXT NOT NULL,
                purchase_price INTEGER NOT NULL,
                selling_price INTEGER NOT NULL
            );";

        string createItemsPurchasesTable = @"
            CREATE TABLE IF NOT EXISTS ItemsPurchases (
                item_purchase_id INTEGER PRIMARY KEY AUTOINCREMENT,
                date TEXT NOT NULL,
                item_id INTEGER NOT NULL,
                quantity_bought INTEGER NOT NULL,
                total INTEGER NOT NULL,
                FOREIGN KEY (item_id) REFERENCES Items(item_id)
            );";

        string createSalesTable = @"
            CREATE TABLE IF NOT EXISTS Sales (
                sale_id INTEGER PRIMARY KEY AUTOINCREMENT,
                date TEXT NOT NULL,
                sale_by INTEGER NOT NULL,
                total_bill INTEGER NOT NULL,
                FOREIGN KEY (sale_by) REFERENCES Sales(sale_id)
            );";

        string createSalesItemsTable = @"
            CREATE TABLE IF NOT EXISTS SalesItems (
                sales_item_id INTEGER PRIMARY KEY AUTOINCREMENT,
                sale_id INTEGER NOT NULL,
                item_id INTEGER NOT NULL,
                quantity_sold INTEGER NOT NULL,
                total_per_item INTEGER NOT NULL,
                FOREIGN KEY (sale_id) REFERENCES Sales(sale_id),
                FOREIGN KEY (item_id) REFERENCES Items(item_id)
            );";

        string createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                user_id INTEGER PRIMARY KEY AUTOINCREMENT,
                username TEXT NOT NULL,
                password TEXT NOT NULL,
                account_type TEXT NOT NULL
            );";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(createItemsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createItemsPurchasesTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createSalesTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createSalesItemsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SqliteCommand(createUsersTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }
    }
}
