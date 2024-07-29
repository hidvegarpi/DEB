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
    public partial class UserControl4 : UserControl
    {
        public UserControl4()
        {
            InitializeComponent();
        }

        string _message = "";
        public string message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                label1.Text = value;
            }
        }

        private void UserControl4_Load(object sender, EventArgs e)
        {

        }
    }
}
