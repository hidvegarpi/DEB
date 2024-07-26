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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public void Destroy() => DestroyHandle();

        DateTime _date1;
        DateTime _date2;
        DateTime _date3;
        DateTime _date4;
        bool PTO;
        bool freeDay;
        bool standBy;
        bool gate3;
        List<Shift> shiftData = new List<Shift>();

        public DateTime date1
        {
            get
            {
                return _date1;
            }
            set
            {
                _date1 = value;
                label1.Text = _date1.ToString("HH:mm");
            }
        }
        public DateTime date2
        {
            get
            {
                return _date2;
            }
            set
            {
                _date2 = value;
                label2.Text = _date2.ToString("HH:mm");
            }
        }
        public DateTime date3
        {
            get
            {
                return _date3;
            }
            set
            {
                _date3 = value;
                label3.Text = _date3.ToString("HH:mm");
            }
        }
        public DateTime date4
        {
            get
            {
                return _date4;
            }
            set
            {
                _date4 = value;
                label4.Text = _date4.ToString("HH:mm");
            }
        }

        public bool isPTO
        {
            get
            {
                return PTO;
            }
            set
            {
                PTO = value;
                label1.Text = "";
                label2.Text = " Szb";
                label3.Text = "";
                label4.Text = "";
                BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
                label1.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label2.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label3.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label4.BackColor = !value ? DefaultBackColor : Color.Transparent;
            }
        }
        public bool isFreeDay
        {
            get
            {
                return freeDay;
            }
            set
            {
                freeDay = value;
                label1.Text = "   X";
                label2.Text = "   X";
                label3.Text = "   X";
                label4.Text = "   X";
                BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
                label1.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label2.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label3.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label4.BackColor = !value ? DefaultBackColor : Color.Transparent;
            }
        }
        public bool isStandBy
        {
            get
            {
                return standBy;
            }
            set
            {
                standBy = value;
                label1.Text = "   X";
                label2.Text = "   X";
                label3.Text = "   X";
                label4.Text = "   X";
                BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
                label1.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label2.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label3.BackColor = !value ? DefaultBackColor : Color.Transparent;
                label4.BackColor = !value ? DefaultBackColor : Color.Transparent;
            }
        }
        public bool isGate3
        {
            get
            {
                return gate3;
            }
            set
            {
                gate3 = value;
            }
        }
        public List<Shift> shifts
        {
            get
            {
                return shiftData;
            }
            set
            {
                shiftData = value;
                if (shiftData.Count == 2)
                {
                    date1 = shiftData[0].shiftStart;
                    label1.BackColor = PublicParameters.colorShiftAMStart;
                    date2 = shiftData[0].shiftEnd;
                    label2.BackColor = PublicParameters.colorShiftAMStart;
                    date3 = shiftData[1].shiftStart;
                    label3.BackColor = PublicParameters.colorShiftPMEndLong;
                    date4 = shiftData[1].shiftEnd;
                    label4.BackColor = PublicParameters.colorShiftPMEndLong;
                }
                if (shiftData.Count == 1)
                {
                    if (shiftData[0].isStandby) isStandBy = true;
                    else if (shiftData[0].isFreeDay) isFreeDay = true;
                    else if (shiftData[0].isPto) isPTO = true;
                    else if (shiftData[0].type == (ShiftType.Night | ShiftType.Long))
                    {
                        label1.Text = "";
                        label1.BackColor = Color.Transparent;
                        label2.Text = "";
                        label2.BackColor = Color.Transparent;
                        date3 = shiftData[0].shiftStart;
                        label3.BackColor = PublicParameters.colorShiftNight;
                        date4 = shiftData[0].shiftEnd;
                        label4.BackColor = PublicParameters.colorShiftNight;
                    }
                    else if (shiftData[0].type == (ShiftType.Day | ShiftType.Long) && shiftData[0].shiftStart.Hour == 4)
                    {
                        label1.Text = "";
                        label1.BackColor = Color.Transparent;
                        label2.Text = "";
                        label2.BackColor = Color.Transparent;
                        date3 = shiftData[0].shiftStart;
                        label3.BackColor = PublicParameters.colorShiftModDay;
                        date4 = shiftData[0].shiftEnd;
                        label4.BackColor = PublicParameters.colorShiftAMEndLong;
                    }
                    else if (shiftData[0].type == (ShiftType.Day | ShiftType.Long))
                    {
                        label1.Text = "";
                        label1.BackColor = Color.Transparent;
                        label2.Text = "";
                        label2.BackColor = Color.Transparent;
                        date3 = shiftData[0].shiftStart;
                        label3.BackColor = gate3 ? PublicParameters.colorShiftGate3 : PublicParameters.colorShiftAMStart;
                        date4 = shiftData[0].shiftEnd;
                        label4.BackColor = PublicParameters.colorShiftAMEndLong;
                    }
                    else
                    {
                        label1.Text = "";
                        label1.BackColor = Color.Transparent;
                        label2.Text = "";
                        label2.BackColor = Color.Transparent;
                        date3 = shiftData[0].shiftStart;
                        label3.BackColor = PublicParameters.colorShiftAMStart;
                        date4 = shiftData[0].shiftEnd;
                        label4.BackColor = PublicParameters.colorShiftAMEndLong;
                    }
                }
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
