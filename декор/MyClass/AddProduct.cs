using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;

namespace декор.MyClass
{
    internal class AddProduct
    {
        public static bool InsertCard(string articul, string typeProd, string name, string desc, string img, string mincost,
        string razmerup, string vesbezup, string vessup, string sertificat, string n_standart, string vremaizg,
        string sebestoim, string n_ceh, string colvo_peop, string razmer)
        {
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(ConnectionBD.connectionString);
                connection.Open();

                string insertQuery = @"INSERT INTO up_02_2_1.продукция 
                (`артикул`, `id_тип_продукции`, `наименование`, `описание`, `изображение`,
                `мин_стоимость`, `id_размер_упаковки`, `вес_без_уп`, `вес_с_уп`,
                `сертефикат_качества`, `номер_стандарта`, `время_изг`,
                `себестоимость`, `номер_цеха`, `кол-во_чел`, `размер`) 
                VALUES 
                (@articul, @typeProd, @name, @desc, @image, @mincost, @razmerUp, 
                @VesBezUp, @VesSUp, @sertificatKach, @NStand, @TimeIzg, 
                @costPrice, @NCeha, @KolVoPeop, @size);";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@articul", articul);
                    insertCommand.Parameters.AddWithValue("@typeProd", typeProd);
                    insertCommand.Parameters.AddWithValue("@name", name);
                    insertCommand.Parameters.AddWithValue("@desc", desc);
                    insertCommand.Parameters.AddWithValue("@image", img);
                    insertCommand.Parameters.AddWithValue("@mincost", mincost);
                    insertCommand.Parameters.AddWithValue("@razmerUp", razmerup);
                    insertCommand.Parameters.AddWithValue("@VesBezUp", vesbezup);
                    insertCommand.Parameters.AddWithValue("@VesSUp", vessup);
                    insertCommand.Parameters.AddWithValue("@sertificatKach", sertificat);
                    insertCommand.Parameters.AddWithValue("@NStand", n_standart);
                    insertCommand.Parameters.AddWithValue("@TimeIzg", vremaizg);
                    insertCommand.Parameters.AddWithValue("@costPrice", sebestoim);
                    insertCommand.Parameters.AddWithValue("@NCeha", n_ceh);
                    insertCommand.Parameters.AddWithValue("@KolVoPeop", colvo_peop);
                    insertCommand.Parameters.AddWithValue("@size", razmer);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при добавлении продукта в БД: {ex.Message}",
                    "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
