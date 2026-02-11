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
    public partial class изменение_подукции : Form
    {
        public string articul;
        private string selectedPhotoPath = "";
        private string imagesFolder = "Images";
        private string currentImagePath = "";
        public изменение_подукции(string articul1)
        {
            InitializeComponent();
            articul = articul1;
        }
       
        private void изменение_подукции_Load(object sender, EventArgs e)
        {
            OutputCardProduct.TipProductCombobox();
            OutputCardProduct.SizePackagingCombobox();

            comboBoxSize.DataSource = ConnectionBD.dtSizePackagingCombobox;
            comboBoxSize.DisplayMember = "размер";
            comboBoxSize.ValueMember = "id_размер_упаковки";

            comboBoxTipProd.DataSource = ConnectionBD.dtTipProductCombobox;
            comboBoxTipProd.DisplayMember = "название_т_п";
            comboBoxTipProd.ValueMember = "id_тип_продукции";

            
            LoadProductData();
        }

        private void LoadProductData()
        {
            try
            {
                DataTable productData = UpdateProduct.GetProductByArticul(articul);

                if (productData.Rows.Count > 0)
                {
                    DataRow row = productData.Rows[0];

                    
                    textBoxArt.Text = row["артикул"].ToString();
                    textBoxName.Text = row["наименование"].ToString();
                    textBoxDesc.Text = row["описание"].ToString();
                    textBoxMinSt.Text = row["мин_стоимость"].ToString();
                    textBoxWeightBez.Text = row["вес_без_уп"].ToString();
                    textBoxWeightS.Text = row["вес_с_уп"].ToString();
                    textBoxSertificat.Text = row["сертефикат_качества"].ToString();
                    textBoxNomStand.Text = row["номер_стандарта"].ToString();
                    textBoxTimeIzg.Text = row["время_изг"].ToString();
                    textBoxCostPrice.Text = row["себестоимость"].ToString();
                    textBoxNumDec.Text = row["номер_цеха"].ToString();
                    textBoxKolPeop.Text = row["кол-во_чел"].ToString();
                    textBoxSize.Text = row["размер"].ToString();

                    
                    comboBoxTipProd.SelectedValue = row["id_тип_продукции"];
                    comboBoxSize.SelectedValue = row["id_размер_упаковки"];

                    
                    string imagePath = row["изображение"].ToString();
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        currentImagePath = imagePath;
                        string fullPath = Path.Combine(Application.StartupPath, imagePath);
                        if (File.Exists(fullPath))
                        {
                            pictureBox1.Image = Image.FromFile(fullPath);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных продукта: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!AreAllFieldsFilled())
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                string newArticul = textBoxArt.Text.Trim();
                if (newArticul.Length > 7)
                {
                    MessageBox.Show("Артикул не может быть длиннее 7 символов!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                if (comboBoxTipProd.SelectedValue == null || comboBoxSize.SelectedValue == null)
                {
                    MessageBox.Show("Пожалуйста, выберите тип продукции и размер упаковки!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                if (!decimal.TryParse(textBoxCostPrice.Text.Trim().Replace(',', '.'),
    System.Globalization.NumberStyles.Any,
    System.Globalization.CultureInfo.InvariantCulture,
    out decimal sebestoimost))
                {
                    MessageBox.Show("Себестоимость должна быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxMinSt.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal minCost))
                {
                    MessageBox.Show("Минимальная стоимость должна быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxWeightBez.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal weightBez))
                {
                    MessageBox.Show("Вес без упаковки должен быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(textBoxWeightS.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal weightS))
                {
                    MessageBox.Show("Вес с упаковкой должен быть числом!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!float.TryParse(textBoxSize.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out float size))
                {
                    MessageBox.Show("Размер должен быть числом с плавающей точкой!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                if (sebestoimost >= minCost)
                {
                    MessageBox.Show("Себестоимость не должна быть больше или равна минимальной стоимости!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                if (weightBez >= weightS)
                {
                    MessageBox.Show("Вес без упаковки должен быть меньше веса с упаковкой!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                string minCostStr = minCost.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string sebestoimostStr = sebestoimost.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string weightBezStr = weightBez.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string weightSStr = weightS.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string sizeStr = size.ToString(System.Globalization.CultureInfo.InvariantCulture);

                
                string typeProd = comboBoxTipProd.SelectedValue.ToString();
                string name = textBoxName.Text.Trim();
                string desc = textBoxDesc.Text.Trim();
                string sizeBox = comboBoxSize.SelectedValue.ToString();
                string sert = textBoxSertificat.Text.Trim();
                string stand = textBoxNomStand.Text.Trim();
                string izgTime = textBoxTimeIzg.Text.Trim();
                string NCech = textBoxNumDec.Text.Trim();
                string colpeop = textBoxKolPeop.Text.Trim();

                
                string savedImagePath = currentImagePath;
                if (!string.IsNullOrEmpty(selectedPhotoPath))
                {
                    savedImagePath = SaveImageToProjectFolder(selectedPhotoPath, newArticul);
                }

                
                bool result = UpdateProduct.UpdateCard(
                    articul, 
                    newArticul,
                    typeProd,
                    name,
                    desc,
                    savedImagePath,
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
                    MessageBox.Show("Продукт успешно обновлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Owner?.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось обновить продукт!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении продукта: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
