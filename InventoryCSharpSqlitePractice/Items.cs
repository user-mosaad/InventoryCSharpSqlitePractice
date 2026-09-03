using Microsoft.Data.Sqlite;

namespace RBSRetail.Items;

// To write:
// Add new Item
// Make an Item Purchase
// List down all Items
// Modify Item data

class Item
{
    /// <summary>
    /// Add a new item to the Items table in the database.
    /// </summary>
    /// <param name="item_name"></param>
    /// <param name="uom"></param>
    /// <param name="purchase_price"></param>
    /// <param name="selling_price"></param>
    public static void AddItem(string item_name, string uom, int purchase_price, int selling_price)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string addItemQuery = @"
            INSERT INTO Items (item_name, uom, purchase_price, selling_price)
            VALUES (@item_name, @uom, @purchase_price, @selling_price);";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqliteCommand(addItemQuery, connection))
            {
                command.Parameters.AddWithValue("@item_name", item_name);
                command.Parameters.AddWithValue("@uom", uom);
                command.Parameters.AddWithValue("@purchase_price", purchase_price);
                command.Parameters.AddWithValue("@selling_price", selling_price);

                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Remove an item from the Items table in the database based on the provided item_id.
    /// </summary>
    /// <param name="item_id"></param>
    public static void RemoveItem(int item_id) 
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string removeItemSql = "DELETE FROM Items WHERE item_id = @item_id";

        using (var connection = new SqliteConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (var command = new SqliteCommand(removeItemSql, connection))
                {
                    command.Parameters.AddWithValue("@item_id", item_id);

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
    /// Make an item purchase and record it in the ItemsPurchases table in the database.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="item_id"></param>
    /// <param name="quantity_bought"></param>
    /// <param name="total"></param>
    public static void MakeItemPurchase(string date, int item_id, int quantity_bought, int total)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string makeItemPurchaseSql = @"
            INSERT INTO ItemsPurchases (date, item_id, quantity_bought, total)
            VALUES (@date, @item_id, @quantity_bought, @total);";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqliteCommand(makeItemPurchaseSql, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@item_id", item_id);
                    command.Parameters.AddWithValue("@quantity_bought", quantity_bought);
                    command.Parameters.AddWithValue("@total", total);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"SQLite Error: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// List all items from the Items table in the database and display their details.
    /// </summary>
    public static void ListAllItems()
    {
        string connectionString = $"Data Source=RBS_Retail.db";
        string listAllItemsSql = "SELECT * FROM Items";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(listAllItemsSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int itemId = reader.GetInt32(0);
                                string itemName = reader.GetString(1);
                                string uom = reader.GetString(2);
                                int purchasePrice = reader.GetInt32(3);
                                int sellingPrice = reader.GetInt32(4);
                                Console.WriteLine($"Item ID: {itemId}, Name: {itemName}, UOM: {uom}, Purchase Price: {purchasePrice:N0}, Selling Price: {sellingPrice:N0}");
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

    /// <summary>
    /// Update the name of an item in the Items table based on the provided item_id and new_name.
    /// </summary>
    /// <param name="item_id"></param>
    /// <param name="new_name"></param>
    public static void UpdateItemName(int item_id, string new_name)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string updateItemNameSql = "UPDATE Items SET item_name = @new_name WHERE item_id = @item_id";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(updateItemNameSql, connection))
                {
                    command.Parameters.AddWithValue("@new_name", new_name);
                    command.Parameters.AddWithValue("@item_id", item_id);
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
    /// Update the unit of measurement (UOM) of an item in the Items table based on the provided item_id and new_uom.
    /// </summary>
    /// <param name="item_id"></param>
    /// <param name="new_uom"></param>
    public static void UpdateItemUOM(int item_id, string new_uom)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string updateItemUOMSql = "UPDATE Items SET uom = @new_uom WHERE item_id = @item_id";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(updateItemUOMSql, connection))
                {
                    command.Parameters.AddWithValue("@new_uom", new_uom);
                    command.Parameters.AddWithValue("@item_id", item_id);
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
    /// Update the purchase price of an item in the Items table based on the provided item_id and new_purchase_price.
    /// </summary>
    /// <param name="item_id"></param>
    /// <param name="new_purchase_price"></param>
    public static void UpdatePurchasePrice(int item_id, int new_purchase_price)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string updatePurchasePriceSql = "UPDATE Items SET purchase_price = @new_purchase_price WHERE item_id = @item_id";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(updatePurchasePriceSql, connection))
                {
                    command.Parameters.AddWithValue("@new_purchase_price", new_purchase_price);
                    command.Parameters.AddWithValue("@item_id", item_id);
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
    /// Update the selling price of an item in the Items table based on the provided item_id and new_selling_price.
    /// </summary>
    /// <param name="item_id"></param>
    /// <param name="new_selling_price"></param>
    public static void UpdateSellingPrice(int item_id, int new_selling_price)
    {
        string connectionString = $"Data Source=RBS_Retail.db";

        string updateSellingPriceSql = "UPDATE Items SET selling_price = @new_selling_price WHERE item_id = @item_id";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(updateSellingPriceSql, connection))
                {
                    command.Parameters.AddWithValue("@new_selling_price", new_selling_price);
                    command.Parameters.AddWithValue("@item_id", item_id);
                    command.ExecuteNonQuery();
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"SQLite Error: {ex.Message}");
        }
    }
}

