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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public string flightDestination = "NaN";
        public string airline = "NaN";
        DateTime _checkInStart;
        public bool charter = false;
        public bool monthly = false;
        public bool weekly = false;

        public DateTime checkInStart
        {
            get
            {
                return _checkInStart;
            }
            set
            {
                _checkInStart = value;
                dateTimePicker1.Value = value;
            }
        }

        private void button1_Click(object sender, EventArgs e) => Close();

        private void textBox1_TextChanged(object sender, EventArgs e) => flightDestination = textBox1.Text;

        private void textBox2_TextChanged(object sender, EventArgs e) => airline = textBox2.Text;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) => _checkInStart = dateTimePicker1.Value;

        private void radioButton1_CheckedChanged(object sender, EventArgs e) => charter = radioButton1.Checked;

        private void radioButton2_CheckedChanged(object sender, EventArgs e) => weekly = radioButton2.Checked;

        private void radioButton3_CheckedChanged(object sender, EventArgs e) => monthly = radioButton3.Checked;

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            charter = radioButton1.Checked;
            weekly = radioButton2.Checked;
            monthly = radioButton3.Checked;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
