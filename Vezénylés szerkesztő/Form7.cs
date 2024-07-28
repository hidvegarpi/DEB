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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        public bool orderedFreeDay = false;
        public bool orderedPTO = false;
        public bool orderedNight = false;
        public bool important = false;
        DateTime _date;
        public DateTime date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                dateTimePicker1.Value = value;
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            orderedFreeDay = radioButton1.Checked;
            orderedPTO = radioButton2.Checked;
            orderedNight = radioButton3.Checked;
            important = checkBox1.Checked;
            date = dateTimePicker1.Value;

            Close();
        }
    }
}
