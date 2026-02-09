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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string typeProd = (comboBoxTipProd.SelectedValue).ToString();
            string name = textBoxName.Text;
            string Desc = textBoxDesc.Text;
            string minCost = textBoxMinSt.Text;
            string sizeBox = ( comboBoxSize.SelectedValue).ToString();
            string weightBez = textBoxWeightBez.Text;
            string wieghtS = textBoxWeightS.Text;
            string sert = textBoxSertificat.Text;
            string stand = textBoxNomStand.Text;
            string izgTime  = textBoxTimeIzg.Text;
            string costPr = textBoxCostPrice.Text;
            string NCech = textBoxNumDec.Text;
            string colpeop = textBoxKolPeop.Text;
            string size = textBoxSize.Text;
        }
    }
}
