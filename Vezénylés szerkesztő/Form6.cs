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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            SetBtnColors();
        }

        void SetBtnColors()
        {
            button1.BackColor = PublicParameters.colorShiftNight;
            button2.BackColor = PublicParameters.colorShiftAMStart;
            button3.BackColor = PublicParameters.colorStandBy;
            button4.BackColor = PublicParameters.colorFreeDay;
            button5.BackColor = PublicParameters.colorPaidTimeOff;
            button6.BackColor = PublicParameters.colorShiftAMEndLong;
            button7.BackColor = PublicParameters.colorShiftPMStart;
            button8.BackColor = PublicParameters.colorShiftGate3;
            button11.BackColor = PublicParameters.colorShiftAMEndLong;
            button12.BackColor = PublicParameters.colorShiftAMEndShort;
            button14.BackColor = PublicParameters.colorShiftPMEndLong;
            button13.BackColor = PublicParameters.colorShiftPMEndShort;
            button15.BackColor = PublicParameters.colorShiftModDay;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button1.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button1.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button2.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button2.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button3.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button3.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDialog1.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button4.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button4.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button4.BackColor = colorDialog1.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button5.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button5.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDialog1.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button6.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button6.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button6.BackColor = colorDialog1.Color;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button7.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button7.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button7.BackColor = colorDialog1.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button8.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button8.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button8.BackColor = colorDialog1.Color;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button11.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button11.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button11.BackColor = colorDialog1.Color;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button12.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button12.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button12.BackColor = colorDialog1.Color;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button14.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button14.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button14.BackColor = colorDialog1.Color;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button13.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button13.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button13.BackColor = colorDialog1.Color;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button15.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button15.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button15.BackColor = colorDialog1.Color;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();

            SetBtnColors();
        }
    }
}
