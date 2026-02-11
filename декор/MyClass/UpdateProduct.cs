using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace декор.MyClass
{
    internal class UpdateProduct
    {
        public static DataTable GetProductByArticul(string articul)
        {
            DataTable dt = new DataTable();
            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(ConnectionBD.connectionString);
                connection.Open();

                string query = "SELECT * FROM up_02_2_1.продукция WHERE артикул = @articul";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@articul", articul);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при получении данных продукта: {ex.Message}",
                    "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;
        }

        public static bool UpdateCard(
    string oldArticul,
    string newArticul,
    string typeProd,
    string name,
    string desc,
    string img,
    string mincost,
    string razmerup,
    string vesbezup,
    string vessup,
    string sertificat,
    string n_standart,
    string vremaizg,
    string sebestoim,
    string n_ceh,
    string colvo_peop,
    string razmer)
        {
            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(ConnectionBD.connectionString);
                connection.Open();

                string updateQuery = @"UPDATE up_02_2_1.продукция 
        SET 
            артикул = @newArticul,
            id_тип_продукции = @typeProd,
            наименование = @name,
            описание = @desc,
            изображение = @image,
            мин_стоимость = @mincost,
            id_размер_упаковки = @razmerUp,
            вес_без_уп = @VesBezUp,
            вес_с_уп = @VesSUp,
            сертефикат_качества = @sertificatKach,
            номер_стандарта = @NStand,
            время_изг = @TimeIzg,
            себестоимость = @costPrice,
            номер_цеха = @NCeha,
            `кол-во_чел` = @KolVoPeop,
            размер = @size
        WHERE артикул = @oldArticul";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@oldArticul", oldArticul);
                    updateCommand.Parameters.AddWithValue("@newArticul", newArticul);
                    updateCommand.Parameters.AddWithValue("@typeProd", typeProd);
                    updateCommand.Parameters.AddWithValue("@name", name);
                    updateCommand.Parameters.AddWithValue("@desc", desc);
                    updateCommand.Parameters.AddWithValue("@image", img);

                    if (decimal.TryParse(mincost.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal mincostDecimal))
                    {
                        updateCommand.Parameters.AddWithValue("@mincost", mincostDecimal);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@mincost", mincost);
                    }

                    updateCommand.Parameters.AddWithValue("@razmerUp", razmerup);

                   
                    if (decimal.TryParse(vesbezup.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal vesbezupDecimal))
                    {
                        updateCommand.Parameters.AddWithValue("@VesBezUp", vesbezupDecimal);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@VesBezUp", vesbezup);
                    }

                   
                    if (decimal.TryParse(vessup.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal vessupDecimal))
                    {
                        updateCommand.Parameters.AddWithValue("@VesSUp", vessupDecimal);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@VesSUp", vessup);
                    }

                    updateCommand.Parameters.AddWithValue("@sertificatKach", sertificat);
                    updateCommand.Parameters.AddWithValue("@NStand", n_standart);
                    updateCommand.Parameters.AddWithValue("@TimeIzg", vremaizg);

                  
                    if (decimal.TryParse(sebestoim.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out decimal sebestoimDecimal))
                    {
                        updateCommand.Parameters.AddWithValue("@costPrice", sebestoimDecimal);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@costPrice", sebestoim);
                    }

                    updateCommand.Parameters.AddWithValue("@NCeha", n_ceh);
                    updateCommand.Parameters.AddWithValue("@KolVoPeop", colvo_peop);

                   
                    if (float.TryParse(razmer.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out float razmerFloat))
                    {
                        updateCommand.Parameters.AddWithValue("@size", razmerFloat);
                    }
                    else
                    {
                        updateCommand.Parameters.AddWithValue("@size", razmer);
                    }

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при обновлении продукта в БД: {ex.Message}",
                    "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
