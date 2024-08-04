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
        bool sickDay;
        List<Shift> shiftData = new List<Shift>();
        public Employee employeeData;
        public Form1 owner;
        public DateTime date;
        Employee _substitute;

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
                if (value)
                {
                    label1.Text = "";
                    label2.Text = " Szb  ";
                    label3.Text = "";
                    label4.Text = "";
                }
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
                if (value)
                {
                    label1.Text = "";
                    label2.Text = "   X  ";
                    label3.Text = "   X  ";
                    label4.Text = "";
                }
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
                if (value)
                {
                    label1.Text = "";
                    label2.Text = "   X  ";
                    label3.Text = "   X  ";
                    label4.Text = "";
                }
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
        public bool isSickDay
        {
            get
            {
                return sickDay;
            }
            set
            {
                sickDay = value;
                if (value)
                {
                    label1.Text = "";
                    label2.Text = "   B  ";
                    label3.Text = "";
                    label4.Text = "";
                }
                BackColor = !value ? DefaultBackColor : PublicParameters.colorSickDay;
                panel1.BackColor = !value ? DefaultBackColor : PublicParameters.colorSickDay;
                label1.BackColor = !value ? DefaultBackColor : PublicParameters.colorSickDay;
                label2.BackColor = !value ? DefaultBackColor : PublicParameters.colorSickDay;
                label3.BackColor = !value ? DefaultBackColor : PublicParameters.colorSickDay;
                label4.BackColor = !value ? DefaultBackColor : PublicParameters.colorSickDay;
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

                isPTO = false;
                isFreeDay = false;
                isStandBy = false;
                isGate3 = false;
                isSickDay = false;

                if (shiftData.Count == 0) SetNull();
                else if (shiftData.Count > 2) SetErr();
                else if (shiftData.Count == 2)
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
                else if (shiftData.Count == 1)
                {
                    panel1.BackColor = DefaultBackColor;
                    BackColor = DefaultBackColor;

                    if (shiftData[0].ordered)
                    {
                        //MessageBox.Show(shiftData[0].ToString());
                        toolStripMenuItem6.Enabled = true;
                        toolStripMenuItem5.Enabled = false;
                        if (shiftData[0].isFreeDay)
                        {
                            isFreeDay = true;
                            panel1.BackColor = PublicParameters.colorFreeDay;
                            BackColor = shiftData[0].important ? PublicParameters.colorOrderedFreeDayImportant : PublicParameters.colorOrderedFreeDay;
                        }
                        else if (shiftData[0].isPto)
                        {
                            isPTO = true;
                            panel1.BackColor = PublicParameters.colorPaidTimeOff;
                            BackColor = PublicParameters.colorOrderedPTOImportant;
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
                            BackColor = PublicParameters.colorOrderedNight;
                        }
                    }
                    else if (shiftData[0].isStandby) isStandBy = true;
                    else if (shiftData[0].isFreeDay) isFreeDay = true;
                    else if (shiftData[0].isPto) isPTO = true;
                    else if (shiftData[0].isSickDay) isSickDay = true;
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
        public Employee substituteEmployee
        {
            get
            {
                return _substitute;
            }
            set
            {
                _substitute = value;
                if (_substitute != null) label3.Text = "  " + _substitute.id;
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            if (shifts.Count == 0) SetNull();
        }

        public void EnableRemoveOrder() => toolStripMenuItem6.Enabled = true;

        public void SetNull()
        {
            isFreeDay = true;
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }

        public void SetErr()
        {
            label1.Text = "";
            label2.Text = "ERR";
            label3.Text = "";
            label4.Text = "";
            BackColor = Color.Red;
            panel1.BackColor = Color.Red;
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
            SetNull();

            //kérésTörléseToolStripMenuItem.Enabled = false;
            //kérésHozzáadásaToolStripMenuItem.Enabled = true;
            toolStripMenuItem6.Enabled = false;
            toolStripMenuItem5.Enabled = true;
            owner.SaveCurrentMonth();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) // sick add
        {
            if (employeeData.name == "") return;
            Form4 sickEmployee = new Form4();
            sickEmployee.Owner = owner;
            sickEmployee.employeeData = employeeData;

            sickEmployee.Show();
            sickEmployee.FormClosing += ((UserControl2)owner.employeeControlList[employeeData.id - 1]).SickEmployeeFormClosing;
            sickEmployee.onDateSelected += ((UserControl2)owner.employeeControlList[employeeData.id - 1]).OnDateSelected;
            
            sickEmployee.SetDate(date);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e) // order
        {
            if (employeeData.name == "") return;

            Form7 form = new Form7();
            form.Owner = owner;
            form.Text = form.Text += " : " + employeeData.name;
            form.date = date;

            form.FormClosing += ((UserControl2)owner.employeeControlList[employeeData.id - 1]).AddOderedShiftClosing;
            form.Show();

            //kérésHozzáadásaToolStripMenuItem.Enabled = false;
            //kérésTörléseToolStripMenuItem.Enabled = true;
            toolStripMenuItem5.Enabled = false; ;
            toolStripMenuItem6.Enabled = true;
            owner.SaveCurrentMonth();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e) // delete order
        {
            RemoveOrderedShift(true);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "Több óra a hónapban") owner.currentMonth.AddEmployeeForMoreHours(employeeData);
            if (toolStripComboBox1.Text == "Normál órák") owner.currentMonth.RemoveEmployeeFromMoreHours(employeeData);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < owner.currentMonth.sickDays.Count; i++)
            {
                int sickID = owner.currentMonth.sickDays[i].sickEmployee.id;
                int substID = owner.currentMonth.sickDays[i].substituteEmployee.id;

                if (owner.currentMonth.sickDays[i].date == date)
                {
                    foreach (Shift s1 in owner.currentMonth.daysOfMonth[date.Day - 1].shiftList)
                    {
                        foreach (Shift s2 in owner.currentMonth.sickDays[i].shiftList)
                        {
                            if (s1 == s2 && s1.ContainsEmployee(owner.GetEmployee(substID)))
                            {
                                s1.AddEmployee(owner.GetEmployee(sickID));
                                s1.RemoveEmployee(owner.GetEmployee(substID));
                            }
                        }
                    }

                    owner.currentMonth.daysOfMonth[
                        date.Day - 1
                        ].shiftList[
                        PublicParameters.shiftIndexStandby
                        ].AddEmployee(
                        owner.GetEmployee(substID)
                        );
                    owner.currentMonth.daysOfMonth[
                        date.Day - 1
                        ].shiftList[
                        PublicParameters.shiftIndexSickDay
                        ].RemoveEmployee(
                        owner.GetEmployee(sickID)
                        );

                    owner.SetShiftVisual(
                        sickID,
                        date.Day,
                        owner.currentMonth.daysOfMonth[date.Day - 1].GetShiftsPerEmployee(owner.GetEmployee(sickID)));
                    owner.SetShiftVisual(
                        substID,
                        date.Day,
                        owner.currentMonth.daysOfMonth[date.Day - 1].GetShiftsPerEmployee(owner.GetEmployee(substID)));

                    owner.currentMonth.sickDays.RemoveAt(i);
                    owner.SaveCurrentMonth();
                    break;
                }
            }
        }
    }
}
