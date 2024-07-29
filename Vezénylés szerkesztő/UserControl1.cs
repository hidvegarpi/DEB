using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
        public Employee employeeData;
        public Form1 owner;
        public DateTime date;

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
                label2.Text = " Szb  ";
                label3.Text = "";
                label4.Text = "";
                BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
                panel1.BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
                label1.BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
                label2.BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
                label3.BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
                label4.BackColor = !value ? DefaultBackColor : PublicParameters.colorPaidTimeOff;
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
                label1.Text = "";
                label2.Text = "   X  ";
                label3.Text = "   X  ";
                label4.Text = "";
                BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
                panel1.BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
                label1.BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
                label2.BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
                label3.BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
                label4.BackColor = !value ? DefaultBackColor : PublicParameters.colorFreeDay;
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
                label1.Text = "";
                label2.Text = "   X  ";
                label3.Text = "   X  ";
                label4.Text = "";
                BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
                panel1.BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
                label1.BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
                label2.BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
                label3.BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
                label4.BackColor = !value ? DefaultBackColor : PublicParameters.colorStandBy;
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
                    panel1.BackColor = DefaultBackColor;
                    BackColor = DefaultBackColor;
                    if (shiftData[0].ordered)
                    {
                        //MessageBox.Show(shiftData[0].ToString());
                        kérésTörléseToolStripMenuItem.Enabled = true;
                        kérésHozzáadásaToolStripMenuItem.Enabled = false;
                        if (shiftData[0].isStandby)
                        {
                            isStandBy = true;
                            panel1.BackColor = PublicParameters.colorStandBy;
                            BackColor = shiftData[0].important ? Color.Red : Color.Orange;
                        }
                        else if (shiftData[0].isFreeDay)
                        {
                            isFreeDay = true;
                            panel1.BackColor = PublicParameters.colorFreeDay;
                            BackColor = shiftData[0].important ? Color.Red : Color.Orange;
                        }
                        else if (shiftData[0].isPto)
                        {
                            isPTO = true;
                            panel1.BackColor = PublicParameters.colorPaidTimeOff;
                            BackColor = shiftData[0].important ? Color.Red : Color.Orange;
                        }
                        else if (shiftData[0].type.HasFlag(ShiftType.Night))
                        {
                            label1.Text = "";
                            label1.BackColor = Color.Transparent;
                            label2.Text = "";
                            label2.BackColor = Color.Transparent;
                            date3 = shiftData[0].shiftStart;
                            label3.BackColor = PublicParameters.colorShiftNight;
                            date4 = shiftData[0].shiftEnd;
                            label4.BackColor = PublicParameters.colorShiftNight;
                            BackColor = shiftData[0].important ? Color.Red : Color.Orange;
                        }
                    }
                    else if (shiftData[0].isStandby) isStandBy = true;
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
                        BackColor = DefaultBackColor;
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
                        BackColor = DefaultBackColor;
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
                        BackColor = DefaultBackColor;
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
                        BackColor = DefaultBackColor;
                    }
                }
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        public void EnableRemoveOrder() => kérésTörléseToolStripMenuItem.Enabled = true;

        private void kérésTörléseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveOrderedShift(true);
        }

        public void RemoveOrderedShift(bool removeNextNight = false)
        {
            //bool removed = false;
            foreach (Shift s in owner.currentMonth.daysOfMonth[date.Day - 1].shiftList)
            {
                if (s.ordered)
                {
                    s.RemoveEmployee(employeeData);
                    //removed = true;
                    //for (int i = 0; i < s.employeeList.Count; i++)
                    //{
                    //    if (s.employeeList[i].id == employeeData.id && s.employeeList[i].name == employeeData.name)
                    //    {
                    //        s.employeeList.RemoveAt(i);
                    //        removed = true;
                    //    }
                    //    if (removed) break;
                    //}
                }
                //if (removed) break;
            }

            if (removeNextNight && shiftData[0].shiftStart.Hour == 19)
                if (shiftData[0].type.HasFlag(ShiftType.Night) && date.Day < owner.currentMonth.daysOfMonth.Count)
                    ((UserControl1)owner.controlNestedList[employeeData.id - 1][date.Day]).RemoveOrderedShift();

            if (removeNextNight && shiftData[0].shiftStart.Hour == 0)
                if (shiftData[0].type.HasFlag(ShiftType.Night) && date.Day > 1)
                    ((UserControl1)owner.controlNestedList[employeeData.id - 1][date.Day - 2]).RemoveOrderedShift();

            List<Shift> sl = new List<Shift>();
            sl.Add(owner.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexFreeDay]);
            BackColor = PublicParameters.colorFreeDay;
            shiftData = sl;
            isFreeDay = true;

            kérésTörléseToolStripMenuItem.Enabled = false;
            kérésHozzáadásaToolStripMenuItem.Enabled = true;
            owner.SaveCurrentMonth();
        }

        private void kérésHozzáadásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeData.name == "") return;

            Form7 form = new Form7();
            form.Owner = owner;
            form.Text = form.Text += " : " + employeeData.name;
            form.date = date;

            form.FormClosing += ((UserControl2)owner.employeeControlList[employeeData.id - 1]).AddOderedShiftClosing;
            form.Show();

            kérésHozzáadásaToolStripMenuItem.Enabled = false;
            kérésTörléseToolStripMenuItem.Enabled = true;
            owner.SaveCurrentMonth();
        }
    }
}
