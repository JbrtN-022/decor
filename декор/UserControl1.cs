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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
