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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        Shift _shiftData;
        Employee _employeeData;
        public Dictionary<int, ShiftNote> additionalData;

        string note = "NaN";
        int slot = 1;
        Color color = Color.DarkOrange;
        float hours = 0;

        public Shift shiftData
        {
            get
            {
                return _shiftData;
            }
            set
            {
                _shiftData = value;
                additionalData = _shiftData.additionalData;
            }
        }
        public Employee employeeData
        {
            get
            {
                return _employeeData;
            }
            set
            {
                _employeeData = value;
            }
        }
        public object owner;

        private void Form9_Load(object sender, EventArgs e)
        {
            button2.BackColor = color;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) => note = textBox1.Text;

        private void button1_Click(object sender, EventArgs e)
        {
            additionalData.Add(employeeData.id, new ShiftNote(note, slot, color, hours));
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) => slot = (int)numericUpDown1.Value;

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) => hours = (float)numericUpDown2.Value;

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = color;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(color) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
                color = colorDialog1.Color;
            }
        }

    }
}
