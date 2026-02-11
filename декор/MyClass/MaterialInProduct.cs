using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace декор.MyClass
{
    internal static class MaterialInProduct
    {
      
        public static void ComboBoxNameProduct()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionBD.connectionString))
                {
                    string query = "SELECT артикул, наименование FROM up_02_2_1.продукция";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            ConnectionBD.dtNameProductCombox.Clear();
                            adapter.Fill(ConnectionBD.dtNameProductCombox);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка продукции: {ex.Message}");
            }
        }
        public static void MaterialProduct(string articul)
        {
            try
            {
                if (string.IsNullOrEmpty(articul))
                {
                    ConnectionBD.dtMaterialInProduct.Clear();
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(ConnectionBD.connectionString))
                {
                    string query = @"SELECT производство.id_производство, 
                                            материалы.id_материалы,
                                            материалы.наименование, 
                                            материалы.стоиомсть, 
                                            производство.НеобходКолМат
                                     FROM up_02_2_1.производство 
                                     INNER JOIN up_02_2_1.материалы ON производство.id_материалы = материалы.id_материалы
                                     WHERE производство.артикул = @articul
                                     ORDER BY материалы.наименование";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@articul", articul);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            ConnectionBD.dtMaterialInProduct.Clear();
                            adapter.Fill(ConnectionBD.dtMaterialInProduct);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке материалов: {ex.Message}");
            }
        }

        public static DataTable LoadAvailableMaterials(string articul)
        {
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionBD.connectionString))
                {
                    string query = @"SELECT id_материалы, наименование
                                     FROM up_02_2_1.материалы 
                                     WHERE id_материалы NOT IN (
                                         SELECT id_материалы 
                                         FROM up_02_2_1.производство 
                                         WHERE артикул = @articul
                                     )
                                     ORDER BY наименование";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@articul", articul);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке доступных материалов: {ex.Message}");
            }

            return dt;
        }

        
        public static bool AddMaterial(string articul, string idMaterial, string quantity)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionBD.connectionString))
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            /
                            string checkQuery = @"SELECT COUNT(*) FROM up_02_2_1.производство 
                                                 WHERE артикул = @articul AND id_материалы = @idMaterial";

                            using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@articul", articul);
                                checkCmd.Parameters.AddWithValue("@idMaterial", idMaterial);

                                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (exists > 0)
                                {
                                    MessageBox.Show("Этот материал уже добавлен к данной продукции!",
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }

                     
                            string insertQuery = @"INSERT INTO up_02_2_1.производство (артикул, id_материалы, НеобходКолМат) 
                                                  VALUES (@articul, @idMaterial, @quantity)";

                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@articul", articul);
                                insertCmd.Parameters.AddWithValue("@idMaterial", idMaterial);

                        
                                if (decimal.TryParse(quantity.Replace(',', '.'),
                                    System.Globalization.NumberStyles.Any,
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    out decimal quantityValue))
                                {
                                    insertCmd.Parameters.AddWithValue("@quantity", quantityValue);
                                }
                                else
                                {
                                    insertCmd.Parameters.AddWithValue("@quantity", quantity);
                                }

                                int rowsAffected = insertCmd.ExecuteNonQuery();
                                transaction.Commit();
                                return rowsAffected > 0;
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении материала: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        
        public static bool UpdateMaterial(string idProduction, string quantity)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionBD.connectionString))
                {
                    string query = @"UPDATE up_02_2_1.производство 
                                    SET НеобходКолМат = @quantity 
                                    WHERE id_производство = @idProduction";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@idProduction", idProduction);

                        if (decimal.TryParse(quantity.Replace(',', '.'),
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out decimal quantityValue))
                        {
                            cmd.Parameters.AddWithValue("@quantity", quantityValue);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@quantity", quantity);
                        }

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении количества: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool DeleteMaterial(string idProduction, string articul)
        {
            try
            {
            
                if (string.IsNullOrEmpty(articul))
                {
                    MessageBox.Show("Не выбран продукт!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (MySqlConnection connection = new MySqlConnection(ConnectionBD.connectionString))
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                
                            string countQuery = @"SELECT COUNT(*) FROM up_02_2_1.производство WHERE артикул = @articul";

                            using (MySqlCommand countCmd = new MySqlCommand(countQuery, connection, transaction))
                            {
                                countCmd.Parameters.AddWithValue("@articul", articul);
                                int materialCount = Convert.ToInt32(countCmd.ExecuteScalar());

                                if (materialCount <= 1)
                                {
                                    MessageBox.Show("Нельзя удалить последний материал у продукции! Продукция должна иметь хотя бы один материал.",
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }


                            string deleteQuery = @"DELETE FROM up_02_2_1.производство WHERE id_производство = @idProduction";

                            using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, connection, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@idProduction", idProduction);
                                int rowsAffected = deleteCmd.ExecuteNonQuery();
                                transaction.Commit();
                                return rowsAffected > 0;
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении материала: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void LoadMaterials(string articul)
        {
            MaterialProduct(articul);
        }
    }
}