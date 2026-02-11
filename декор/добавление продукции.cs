using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using декор.MyClass;

namespace декор
{
    public partial class добавление_продукции : Form
    {
        public добавление_продукции()
        {
            InitializeComponent();
        }
        public string fileImage = string.Empty;
        private string selectedPhotoPath = "";
        private void добавление_продукции_Load(object sender, EventArgs e)
        {
            OutputCardProduct.TipProductCombobox();
            OutputCardProduct.SizePackagingCombobox();

            comboBoxSize.DataSource = ConnectionBD.dtSizePackagingCombobox;
            comboBoxSize.DisplayMember = "размер";
            comboBoxSize.ValueMember = "id_размер_упаковки";
            comboBoxSize.Text = null;

            comboBoxTipProd.DataSource = ConnectionBD.dtTipProductCombobox;
            comboBoxTipProd.DisplayMember = "название_т_п";
            comboBoxTipProd.ValueMember = "id_тип_продукции";
            comboBoxTipProd.Text = null;
        }
        private string relativeImagePath = string.Empty;

        // Папка для хранения изображений внутри папки приложения
        private string imagesFolder = "Images";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedPhotoPath = ofd.FileName;

                    pictureBox1.Image = Image.FromFile(selectedPhotoPath);
                }
            }
        }
        private bool AreAllFieldsFilled()
        {
            // Проверка всех текстовых полей
            if (string.IsNullOrWhiteSpace(textBoxArt?.Text) ||
                string.IsNullOrWhiteSpace(textBoxName?.Text) ||
                string.IsNullOrWhiteSpace(textBoxDesc?.Text) ||
                string.IsNullOrWhiteSpace(textBoxMinSt?.Text) ||
                string.IsNullOrWhiteSpace(textBoxWeightBez?.Text) ||
                string.IsNullOrWhiteSpace(textBoxWeightS?.Text) ||
                string.IsNullOrWhiteSpace(textBoxSertificat?.Text) ||
                string.IsNullOrWhiteSpace(textBoxNomStand?.Text) ||
                string.IsNullOrWhiteSpace(textBoxTimeIzg?.Text) ||
                string.IsNullOrWhiteSpace(textBoxCostPrice?.Text) ||
                string.IsNullOrWhiteSpace(textBoxNumDec?.Text) ||
                string.IsNullOrWhiteSpace(textBoxKolPeop?.Text) ||
                string.IsNullOrWhiteSpace(textBoxSize?.Text))
            {
                return false;
            }

            // Проверка комбобоксов
            if (comboBoxTipProd.SelectedValue == null || comboBoxSize.SelectedValue == null)
            {
                return false;
            }

            // Проверка наличия изображения
            if (string.IsNullOrEmpty(selectedPhotoPath))
            {
                return false;
            }

            return true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //try
            //{
                // 1. Проверка на заполненность всех полей
                if (!AreAllFieldsFilled())
                {
                    MessageBox.Show("Пожалуйста, заполните все поля и выберите изображение!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Проверка артикула (не длиннее 6 символов)
                string articul = textBoxArt.Text.Trim(); // предполагаем, что у вас есть textBoxArticul
                if (articul.Length > 7)
                {
                    MessageBox.Show("Артикул не может быть длиннее 7 символов!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Проверка на выбор значений в комбобоксах
                if (comboBoxTipProd.SelectedValue == null || comboBoxSize.SelectedValue == null)
                {
                    MessageBox.Show("Пожалуйста, выберите тип продукции и размер упаковки!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Проверка наличия картинки
                if (string.IsNullOrEmpty(selectedPhotoPath))
                {
                    MessageBox.Show("Пожалуйста, выберите изображение для продукта!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5. Получение и проверка числовых значений
                if (!decimal.TryParse(textBoxCostPrice.Text.Trim(), out decimal sebestoimost))
                {
                    MessageBox.Show("Себестоимость должна быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxMinSt.Text.Trim(), out decimal minCost))
                {
                    MessageBox.Show("Минимальная стоимость должна быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxWeightBez.Text.Trim(), out decimal weightBez))
                {
                    MessageBox.Show("Вес без упаковки должен быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxWeightS.Text.Trim(), out decimal weightS))
                {
                    MessageBox.Show("Вес с упаковкой должен быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!float.TryParse(textBoxSize.Text.Trim(), out float size))
                {
                    MessageBox.Show("Размер должен быть числом с плавающей точкой!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6. Проверка: себестоимость не больше минимальной цены
                if (sebestoimost >= minCost)
                {
                    MessageBox.Show("Себестоимость не должна быть больше или равна минимальной стоимости!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 7. Проверка: вес без упаковки меньше веса с упаковкой
                if (weightBez >= weightS)
                {
                    MessageBox.Show("Вес без упаковки должен быть меньше веса с упаковкой!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получение значений
                string typeProd = comboBoxTipProd.SelectedValue.ToString();
                string name = textBoxName.Text.Trim();
                string desc = textBoxDesc.Text.Trim();
                string sizeBox = comboBoxSize.SelectedValue.ToString();
                string sert = textBoxSertificat.Text.Trim();
                string stand = textBoxNomStand.Text.Trim();
                string izgTime = textBoxTimeIzg.Text.Trim();
                string NCech = textBoxNumDec.Text.Trim();
                string colpeop = textBoxKolPeop.Text.Trim();

            
               

                // Вызов метода добавления продукта
                bool result = AddProduct.InsertCard(
                    articul,
                    typeProd,
                    name,
                    desc,
                    relativeImagePath,
                    minCost.ToString(),
                    sizeBox,
                    weightBez.ToString(),
                    weightS.ToString(),
                    sert,
                    stand,
                    izgTime,
                    sebestoimost.ToString(),
                    NCech,
                    colpeop,
                    size.ToString()
                );

                if (result)
                {
                    MessageBox.Show("Продукт успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить продукт!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка при добавлении продукта: {ex.Message}",
            //        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}



        }
    }
}
