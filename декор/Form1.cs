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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MyClass.ConnectionBD.ConnectBd() == false){
                this.Close();   
            }
            OutputCardProduct.CardListProduction(flowLayoutPanel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            добавление_продукции form1 = new добавление_продукции();
            form1.Owner = this;
            this.Hide();
            form1.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Список_материалов form2 = new Список_материалов();
            form2.Owner = this;
            this.Hide();
            form2.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Список_материалов form2 = new Список_материалов();
            form2.Owner = this;
            this.Hide();
            form2.ShowDialog();
        }
    }
}
