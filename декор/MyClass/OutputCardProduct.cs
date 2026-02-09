using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace декор.MyClass
{
    internal class OutputCardProduct
    {
        public static void CardListProduction(FlowLayoutPanel panel)
        {
            try
            {
                panel.Controls.Clear();
                mySqlCommand.CommandText = @"
                SELECT 
                    пр.артикул,
                    тп.название_т_п,
                    пр.наименование,
                    пр.мин_стоимость,
                    пр.размер,
                    (SELECT SUM(м.стоиомсть * прв.`Необход кол м.`)
                     FROM up_02_2_1.производство прв
                     JOIN up_02_2_1.материалы м ON прв.id_материалы = м.id_материалы
                     WHERE прв.артикул = пр.артикул) as 'стоимость_материалов'
                FROM 
                    up_02_2_1.продукция пр
                JOIN 
                    up_02_2_1.с_тип_продукции тп 
                    ON пр.id_тип_продукции = тп.id_тип_продукции
                ";
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string articule = reader["артикул"].ToString();
                        string type = reader["название_т_п"].ToString();
                        string name = reader["наименование"].ToString();
                        float minCost = reader["мин_стоимость"] == DBNull.Value ? 0f : Convert.ToSingle(reader["мин_стоимость"]);
                        float razmer = reader["размер"] == DBNull.Value ? 0f : Convert.ToSingle(reader["размер"]);
                        float cost = reader["стоимость_материалов"] == DBNull.Value ? 0f : Convert.ToSingle(reader["стоимость_материалов"]);

                        // Форматирование с 2 знаками после запятой
                        string minCostFormatted = minCost.ToString("F2");
                        string razmerFormatted = razmer.ToString("F2");
                        string costFormatted = cost.ToString("F2");





                        UserControl1 us1 = new UserControl1(
                            articule,
                            type,
                            name,
                            minCostFormatted,
                            razmerFormatted,
                            costFormatted
                            );



                        panel.Controls.Add(us1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не получилось загрузить данные. Ошибка: {ex.Message}");
            }
        }
        public static bool TipProductCombobox()
        {
            try
            {
                mySqlCommand.CommandText = @"SELECT * FROM up_02_2_1.с_тип_продукции;";
                dtTipProductCombobox.Clear();
                mySqlDataAdapter.Fill(dtTipProductCombobox);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SizePackagingCombobox()
        {
            try
            {
                mySqlCommand.CommandText = @"SELECT id_размер_упаковки, concat(длина, 'см ', ширина, 'см ', высота, 'см') as 'размер' FROM up_02_2_1.размер_упаковки;";
                dtSizePackagingCombobox.Clear();
                mySqlDataAdapter.Fill(dtSizePackagingCombobox);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
