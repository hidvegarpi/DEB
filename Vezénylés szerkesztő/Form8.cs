using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vezénylés_szerkesztő
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        List<string> _warningList = new List<string>();
        public List<string> warningList
        {
            get
            {
                return _warningList;
            }
            set
            {
                _warningList = value;
                Text = "Értesítések: " + _warningList.Count;
                foreach (string w in _warningList) 
                {
                    UserControl4 u = new UserControl4();
                    u.message = w;
                    flowLayoutPanel1.Controls.Add(u);
                }
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
