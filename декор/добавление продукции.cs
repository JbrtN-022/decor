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
        private string relativeImagePath = string.Empty;
        private string imagesFolder = "Images";

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

            if (comboBoxTipProd.SelectedValue == null || comboBoxSize.SelectedValue == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(selectedPhotoPath))
            {
                return false;
            }

            return true;
        }

        private string SaveImageToProjectFolder(string sourcePath, string articul)
        {
            try
            {
                string imagesFolderPath = Path.Combine(Application.StartupPath, imagesFolder);
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string extension = Path.GetExtension(sourcePath);
                string fileName = $"{articul}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                string destPath = Path.Combine(imagesFolderPath, fileName);

                File.Copy(sourcePath, destPath, true);
                return Path.Combine(imagesFolder, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка на заполненность всех полей
                if (!AreAllFieldsFilled())
                {
                    MessageBox.Show("Пожалуйста, заполните все поля и выберите изображение!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Проверка артикула
                string articul = textBoxArt.Text.Trim();
                if (articul.Length > 7)
                {
                    MessageBox.Show("Артикул не может быть длиннее 7 символов!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Проверка комбобоксов
                if (comboBoxTipProd.SelectedValue == null || comboBoxSize.SelectedValue == null)
                {
                    MessageBox.Show("Пожалуйста, выберите тип продукции и размер упаковки!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Проверка числовых значений с поддержкой запятой
                if (!decimal.TryParse(textBoxCostPrice.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal sebestoimostValue))
                {
                    MessageBox.Show("Себестоимость должна быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxMinSt.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal minCostValue))
                {
                    MessageBox.Show("Минимальная стоимость должна быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxWeightBez.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal weightBezValue))
                {
                    MessageBox.Show("Вес без упаковки должен быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxWeightS.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal weightSValue))
                {
                    MessageBox.Show("Вес с упаковкой должен быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!float.TryParse(textBoxSize.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float sizeValue))
                {
                    MessageBox.Show("Размер должен быть числом с плавающей точкой!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5. Проверка: себестоимость не больше минимальной цены
                if (sebestoimostValue >= minCostValue)
                {
                    MessageBox.Show("Себестоимость не должна быть больше или равна минимальной стоимости!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6. Проверка: вес без упаковки меньше веса с упаковкой
                if (weightBezValue >= weightSValue)
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

                // Сохранение изображения
                string savedImagePath = SaveImageToProjectFolder(selectedPhotoPath, articul);

                // Форматирование чисел для SQL
                string minCostStr = minCostValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string sebestoimostStr = sebestoimostValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string weightBezStr = weightBezValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string weightSStr = weightSValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string sizeStr = sizeValue.ToString(System.Globalization.CultureInfo.InvariantCulture);

                // Вызов метода добавления продукта
                bool result = AddProduct.InsertCard(
                    articul,
                    typeProd,
                    name,
                    desc,
                    savedImagePath,
                    minCostStr,
                    sizeBox,
                    weightBezStr,
                    weightSStr,
                    sert,
                    stand,
                    izgTime,
                    sebestoimostStr,
                    NCech,
                    colpeop,
                    sizeStr
                );

                if (result)
                {
                    MessageBox.Show("Продукт успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Возвращаемся на главную форму
                    this.Owner?.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить продукт!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении продукта: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
