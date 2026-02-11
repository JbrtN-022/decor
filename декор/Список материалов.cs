using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using декор.MyClass;

namespace декор
{
    public partial class Список_материалов : Form
    {
        private DataTable dtAvailableMaterials;
        private string currentArticul = "";
        private string selectedIdProduction = "";
        public Список_материалов()
        {
            InitializeComponent();
            MaterialInProduct.ComboBoxNameProduct();

            ConfigureDataGridView();
            ClearEditFields();

            comboBox1.DataSource = ConnectionBD.dtNameProductCombox;
            comboBox1.DisplayMember = "наименование";
            comboBox1.ValueMember = "артикул";
            comboBox1.SelectedIndex = -1; 
            comboBox1.Text = ""; 
            comboBoxMaterial.DataSource = null;
        }

        private void Список_материалов_Load(object sender, EventArgs e)
        {

        }
        private void LoadAvailableMaterials()
        {
            if (string.IsNullOrEmpty(currentArticul))
            {
                comboBoxMaterial.DataSource = null;
                return;
            }

            dtAvailableMaterials = MaterialInProduct.LoadAvailableMaterials(currentArticul);

            comboBoxMaterial.DataSource = null;
            comboBoxMaterial.DataSource = dtAvailableMaterials;
            comboBoxMaterial.DisplayMember = "наименование";
            comboBoxMaterial.ValueMember = "id_материалы";
            comboBoxMaterial.SelectedIndex = -1;
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            
                if (comboBox1.SelectedValue == null ||
                    comboBox1.SelectedIndex == -1 ||
                    comboBox1.SelectedValue is DBNull)
                {
                    dataGridView1.DataSource = null;
                    currentArticul = "";
                    comboBoxMaterial.DataSource = null;
                    return;
                }

                string articul = comboBox1.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(articul))
                {
                    currentArticul = articul; 

                   
                    MaterialInProduct.MaterialProduct(articul);
                    dataGridView1.DataSource = ConnectionBD.dtMaterialInProduct;

        
                    if (dataGridView1.Columns.Count > 0)
                    {
                        if (dataGridView1.Columns.Contains("id_производство"))
                            dataGridView1.Columns["id_производство"].Visible = false;
                        if (dataGridView1.Columns.Contains("id_материалы"))
                            dataGridView1.Columns["id_материалы"].Visible = false;
                        dataGridView1.Columns["наименование"].HeaderText = "Материал";
                        dataGridView1.Columns["стоиомсть"].HeaderText = "Стоимость";
                        dataGridView1.Columns["НеобходКолМат"].HeaderText = "Количество";
                    }

          
                    LoadAvailableMaterials();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
         
                if (string.IsNullOrEmpty(currentArticul))
                {
                    MessageBox.Show("Выберите продукцию!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxMaterial.SelectedValue == null || comboBoxMaterial.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите материал для добавления!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxAddKolVo.Text))
                {
                    MessageBox.Show("Введите количество материала!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (!decimal.TryParse(textBoxAddKolVo.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal quantityValue))
                {
                    MessageBox.Show("Количество должно быть числом!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (quantityValue <= 0)
                {
                    MessageBox.Show("Количество должно быть больше нуля!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idMaterial = comboBoxMaterial.SelectedValue.ToString();
                string quantity = quantityValue.ToString(System.Globalization.CultureInfo.InvariantCulture);

           
                bool result = MaterialInProduct.AddMaterial(currentArticul, idMaterial, quantity);

                if (result)
                {
                    MessageBox.Show("Материал успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                 
                    MaterialInProduct.LoadMaterials(currentArticul);
                    dataGridView1.Refresh();
                    LoadAvailableMaterials();

                    textBoxAddKolVo.Clear();
                    comboBoxMaterial.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении материала: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpp_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedIdProduction))
                {
                    MessageBox.Show("Выберите материал для редактирования!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxUppKolVo.Text))
                {
                    MessageBox.Show("Введите количество материала!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

    
                if (!decimal.TryParse(textBoxUppKolVo.Text.Trim().Replace(',', '.'),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal quantityValue))
                {
                    MessageBox.Show("Количество должно быть числом!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (quantityValue <= 0)
                {
                    MessageBox.Show("Количество должно быть больше нуля!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string quantity = quantityValue.ToString(System.Globalization.CultureInfo.InvariantCulture);

                bool result = MaterialInProduct.UpdateMaterial(selectedIdProduction, quantity);

                if (result)
                {
                    MessageBox.Show("Количество материала успешно обновлено!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                    MaterialInProduct.LoadMaterials(currentArticul);
                    dataGridView1.Refresh();
                    ClearEditFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении материала: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedIdProduction))
                {
                    MessageBox.Show("Выберите материал для удаления!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить этот материал?",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool deleteResult = MaterialInProduct.DeleteMaterial(selectedIdProduction, currentArticul);

                    if (deleteResult)
                    {
                        MessageBox.Show("Материал успешно удален!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        MaterialInProduct.LoadMaterials(currentArticul);
                        dataGridView1.Refresh();
                        LoadAvailableMaterials();
                        ClearEditFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении материала: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void ClearEditFields()
        {
            textBoxUppMat.Clear();
            textBoxUppKolVo.Clear();
            selectedIdProduction = "";
            buttonUpp.Enabled = false;
            buttonDel.Enabled = false;
        }
        private void ClearAllData()
        {
            currentArticul = "";
            dataGridView1.DataSource = null;
            ConnectionBD.dtMaterialInProduct.Clear();
            comboBox1.DataSource = null;
            textBoxAddKolVo.Clear();
            ClearEditFields();
        }

        private void textBoxUppKolVo_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Contains(","))
            {
                int selectionStart = textBox.SelectionStart;
                textBox.Text = textBox.Text.Replace(",", ".");
                textBox.SelectionStart = selectionStart;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                if (row.Cells["id_производство"].Value != null)
                {
                    selectedIdProduction = row.Cells["id_производство"].Value.ToString();
                    string materialName = row.Cells["наименование"].Value?.ToString() ?? "";
                    string quantity = row.Cells["НеобходКолМат"].Value?.ToString() ?? "";

                    
                    textBoxUppMat.Text = materialName;
                    textBoxUppKolVo.Text = quantity;

                   
                    buttonUpp.Enabled = true;
                    buttonDel.Enabled = true;
                }
            }
        }
    }
    }

