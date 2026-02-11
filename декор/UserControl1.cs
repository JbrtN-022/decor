using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace декор
{
    public partial class UserControl1 : UserControl
    {
        public string articul;
        public string type;
        public string names;
        public string minSt;
        public string raz;
        public string cost;
        public UserControl1(string artic, string typ, string name, string minCosts, string razmer, string costs)
        {
            InitializeComponent() ;
            articul = artic ;
            type = typ ;
            names = name ;
            minSt = minCosts ;
            raz = razmer ;
            cost =  $@"{costs} р.";

            labelarticule.Text = artic;
            labelCost.Text = costs;
            labelminCost.Text = minCosts ;
           
            labelType.Text = $@"{typ} | {name}";
            labelWeight.Text = razmer;
 
        }
        private void UserControl1_Click(object sender, EventArgs e)
        {

            изменение_подукции form1 = new изменение_подукции(articul);

            // Получаем родительскую форму
            Form parentForm = this.FindForm();

            if (parentForm != null)
            {
                form1.Owner = parentForm;
                parentForm.Hide();
                form1.ShowDialog();
            }
            else
            {
                form1.ShowDialog();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void UserControl1_DoubleClick(object sender, EventArgs e)
        {
            UserControl1_Click(sender, e);
        }
    }
}
