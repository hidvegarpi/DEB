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
            SetData();
            SetBtnColors();
        }

        void SetData()
        {
            numericUpDown1.Value = PublicParameters.warningCardDateDays;
            numericUpDown2.Value = PublicParameters.warningExamDateDays;
            numericUpDown3.Value = PublicParameters.employeesPerFlight;
            numericUpDown4.Value = PublicParameters.employeesPerNightShift;
            numericUpDown5.Value = PublicParameters.employeesPerDayShift;
            numericUpDown6.Value = PublicParameters.employeesPerStandby;
            numericUpDown7.Value = PublicParameters.avgMonthlyHours;
            numericUpDown8.Value = PublicParameters.hoursPTO;
            numericUpDown9.Value = PublicParameters.hoursStandby;
            numericUpDown10.Value = PublicParameters.hoursFreeDay;
            numericUpDown11.Value = PublicParameters.multiplierKm;
            numericUpDown12.Value = PublicParameters.minShiftTimeMins;

            checkBox1.Checked = PublicParameters.sickOnlyStandby;
            checkBox2.Checked = PublicParameters.generateByCanGoWith;
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
            button16.BackColor = PublicParameters.colorOrderedFreeDay;
            button17.BackColor = PublicParameters.colorOrderedFreeDayImportant;
            button18.BackColor = PublicParameters.colorOrderedNight;
            button19.BackColor = PublicParameters.colorSickDay;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PublicParameters.Reset();
            PublicParameters.Save();

            SetData();
            SetBtnColors();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PublicParameters.Save();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button1.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button1.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftNight = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button2.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button2.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftAMStart = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button3.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button3.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDialog1.Color;
                PublicParameters.colorStandBy = colorDialog1.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button4.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button4.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button4.BackColor = colorDialog1.Color;
                PublicParameters.colorFreeDay = colorDialog1.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button5.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button5.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDialog1.Color;
                PublicParameters.colorPaidTimeOff = colorDialog1.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button6.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button6.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button6.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftAMEndLong = colorDialog1.Color;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button7.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button7.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button7.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftPMStart = colorDialog1.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button8.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button8.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button8.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftGate3 = colorDialog1.Color;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button11.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button11.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button11.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftAMEndLong = colorDialog1.Color;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button12.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button12.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button12.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftAMEndShort = colorDialog1.Color;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button14.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button14.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button14.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftPMEndLong = colorDialog1.Color;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button13.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button13.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button13.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftPMEndShort = colorDialog1.Color;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button15.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button15.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button15.BackColor = colorDialog1.Color;
                PublicParameters.colorShiftModDay = colorDialog1.Color;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button16.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button16.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button16.BackColor = colorDialog1.Color;
                PublicParameters.colorOrderedFreeDay = colorDialog1.Color;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button17.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button17.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button17.BackColor = colorDialog1.Color;
                PublicParameters.colorOrderedFreeDayImportant = colorDialog1.Color;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button18.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button18.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button18.BackColor = colorDialog1.Color;
                PublicParameters.colorOrderedNight = colorDialog1.Color;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = button19.BackColor;
            colorDialog1.CustomColors = new int[] { ColorTranslator.ToOle(button19.BackColor) };
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button19.BackColor = colorDialog1.Color;
                PublicParameters.colorSickDay = colorDialog1.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) => PublicParameters.warningCardDateDays = (int)numericUpDown1.Value;

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) => PublicParameters.warningExamDateDays = (int)numericUpDown2.Value;

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) => PublicParameters.employeesPerFlight = (int)numericUpDown3.Value;

        private void numericUpDown4_ValueChanged(object sender, EventArgs e) => PublicParameters.employeesPerNightShift = (int)numericUpDown4.Value;

        private void numericUpDown5_ValueChanged(object sender, EventArgs e) => PublicParameters.employeesPerDayShift = (int)numericUpDown5.Value;

        private void numericUpDown6_ValueChanged(object sender, EventArgs e) => PublicParameters.employeesPerStandby = (int)numericUpDown6.Value;

        private void numericUpDown7_ValueChanged(object sender, EventArgs e) => PublicParameters.avgMonthlyHours = (int)numericUpDown7.Value;

        private void numericUpDown8_ValueChanged(object sender, EventArgs e) => PublicParameters.hoursPTO = (int)numericUpDown8.Value;

        private void numericUpDown9_ValueChanged(object sender, EventArgs e) => PublicParameters.hoursStandby = (int)numericUpDown9.Value;

        private void numericUpDown10_ValueChanged(object sender, EventArgs e) => PublicParameters.hoursFreeDay = (int)numericUpDown10.Value;

        private void numericUpDown11_ValueChanged(object sender, EventArgs e) => PublicParameters.multiplierKm = (int)numericUpDown11.Value;

        private void numericUpDown12_ValueChanged(object sender, EventArgs e) => PublicParameters.minShiftTimeMins = (int)numericUpDown12.Value;

        private void checkBox1_CheckedChanged(object sender, EventArgs e) => PublicParameters.sickOnlyStandby = checkBox1.Checked;

        private void checkBox2_CheckedChanged(object sender, EventArgs e) => PublicParameters.generateByCanGoWith = checkBox2.Checked;
    }
}
