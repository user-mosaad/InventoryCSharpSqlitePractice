using Microsoft.Data.Sqlite;

namespace RBSRetail.Sales;

// To do:
// Make a Sale
// Record Sale Items
// View Past Sales (all of them for now)

class Sale
{

    /// <summary>
    /// Add a sales record to the Sales table
    /// </summary>
    /// <param name="date"></param>
    /// <param name="sale_by"></param>
    /// <param name="total_bill"></param>
    public static void MakeSale(string date, int sale_by, int total_bill)
    {
        string connectionString = "Data Source=RBS_Retail.db";

        string makeSaleSql = @"
            INSERT INTO Sales (date, sale_by, total_bill)
            VALUES (@date, @sale_by, @total_bill);";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(makeSaleSql, connection))
                {
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@sale_by", sale_by);
                    command.Parameters.AddWithValue("@total_bill", total_bill);

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
    /// Add a sales item record to the SalesItems table
    /// </summary>
    /// <param name="sale_id"></param>
    /// <param name="item_id"></param>
    /// <param name="quantity_sold"></param>
    /// <param name="total_per_item"></param>
    public static void RecordSalesItem(int sale_id, int item_id, int quantity_sold, int total_per_item)
    {
        string connectionString = "Data Source=RBS_Retail.db";

        string recordSalesItemSql = @"
            INSERT INTO SalesItems (sale_id, item_id, quantity_sold, total_per_item)
            VALUES (@sale_id, @item_id, @quantity_sold, @total_per_item);";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(recordSalesItemSql, connection))
                {
                    command.Parameters.AddWithValue("@sale_id", sale_id);
                    command.Parameters.AddWithValue("@item_id", item_id);
                    command.Parameters.AddWithValue("@quantity_sold", quantity_sold);
                    command.Parameters.AddWithValue("@total_per_item", total_per_item);
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
    /// Show all past sales records from the Sales table
    /// </summary>
    public static void ShowPastSales()
    {
        string connectionString = "Data Source=RBS_Retail.db";

        string showPastSalesSql = @"
            SELECT * FROM Sales;";

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand(showPastSalesSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Sale ID: {reader["sale_id"]}, Date: {reader["date"]}, Sale By: {reader["sale_by"]}, Total Bill: {reader["total_bill"]}");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }
    }
}