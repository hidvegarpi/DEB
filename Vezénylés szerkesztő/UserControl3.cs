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
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
        }

        public void Destroy() => DestroyHandle();

        int _day;
        Day _data;

        public int day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
                label1.Text = label1.Text.Replace("#1", value.ToString().PadLeft(2, '0'));
            }
        }

        public Day data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                string flights = data.date.ToString("yyyy. MM. dd.");
                foreach (Flight f in data.flightList)
                    flights += "\n" + f.destination + " " + f.checkInStart.ToString("HH:mm");
                toolTip1.SetToolTip(this, flights);
                toolTip1.SetToolTip(label1, flights);

                string dayName = "";
                switch (_data.date.DayOfWeek)
                {
                    case DayOfWeek.Monday: dayName = " H"; break;
                    case DayOfWeek.Tuesday: dayName = " K"; break;
                    case DayOfWeek.Wednesday: dayName = "Sz"; break;
                    case DayOfWeek.Thursday: dayName = "Cs"; break;
                    case DayOfWeek.Friday: dayName = " P"; break;
                    case DayOfWeek.Saturday: dayName = "Sz"; break;
                    case DayOfWeek.Sunday: dayName = " V"; break;
                }
                label1.Text = label1.Text.Replace("#2", dayName);

                if (_data.date.DayOfWeek == DayOfWeek.Sunday) label1.ForeColor = Color.Red;
                this.BackColor = _data.date.DayOfWeek == DayOfWeek.Saturday || _data.date.DayOfWeek == DayOfWeek.Sunday ? Color.LightGray : Control.DefaultBackColor;
            }
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
            
        }

        private void UserControl3_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        private void UserControl3_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = _data.date.DayOfWeek == DayOfWeek.Saturday || _data.date.DayOfWeek == DayOfWeek.Sunday ? Color.LightGray : Control.DefaultBackColor;
        }
    }
}
