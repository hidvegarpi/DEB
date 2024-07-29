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
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        public void Destroy() => DestroyHandle();

        string _name;
        int _id;
        bool isSup;

        public Action<Employee> onEmployeeEdited;
        public Action<Employee, DateTime, Employee> sickEmployee;
        public Form owner;
        public Employee employeeData;

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                label2.Text = value;
                if (value == "") ContextMenuStrip = new ContextMenuStrip();
            }
        }
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                label1.Text = value == 0 ? "" : value.ToString().PadLeft(2, '0');
            }
        }
        public bool isSupervisor
        {
            get
            {
                return isSup;
            }
            set
            {
                isSup = value;
                label2.ForeColor = value ? Color.Red : Color.Black;
            }
        }


        private void UserControl2_Load(object sender, EventArgs e)
        {

        }

        private void szerkesztésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (name == "") return;

            Form2 addEmployeeForm = new Form2();
            addEmployeeForm.Owner = owner;
            addEmployeeForm.Text = "Munkáltatott szerkesztése";
            addEmployeeForm.employee = employeeData;

            addEmployeeForm.Show();
            addEmployeeForm.FormClosing += AddEmployeeFormClosing;
        }

        void AddEmployeeFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Form2 form = (Form2)sender;
            if (form.DialogResult == DialogResult.OK)
            {
                Employee n = new Employee(form.id, form.name, form.type, form.exams, form.examDate, form.cardDate, form.male, form.distanceToWork, form.canGoWith);
                n.SaveToFile();
                onEmployeeEdited(n);
                //MessageBox.Show(n.ToString());
            }
        }

        private void bejelentésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (name == "") return;
            Form4 sickEmployee = new Form4();
            sickEmployee.Owner = owner;
            sickEmployee.employeeData = employeeData;

            sickEmployee.Show();
            sickEmployee.FormClosing += SickEmployeeFormClosing;
            sickEmployee.onDateSelected += OnDateSelected;
        }

        void SickEmployeeFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        void OnDateSelected(DateTime date, Form4 form)
        {
            foreach (Day d in ((Form1)owner).currentMonth.daysOfMonth)
                if (d.date.Year == date.Year && d.date.Month == date.Month && d.date.Day == date.Day)
                    form.employeeList = d.shiftList[PublicParameters.shiftIndexStandby].employeeList.Count > 0 ? d.shiftList[PublicParameters.shiftIndexStandby].employeeList : new List<Employee>();
        }

        private void hozzáadásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (name == "") return;

            Form7 form = new Form7();
            form.Owner = owner;
            form.Text = form.Text += " : " + name;

            form.FormClosing += AddOderedShiftClosing;
            form.Show();
        }

        public void AddOderedShiftClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Form7 form = (Form7)sender;
            if (form.DialogResult == DialogResult.OK)
            {
                Form1 o = (Form1)owner;
                DateTime date = form.date;
                bool oFreeDay = form.orderedFreeDay;
                bool oPTO = form.orderedPTO;
                bool oNight = form.orderedNight;
                bool oImportant = form.important;
                Employee em = employeeData;

                foreach (Shift s in o.currentMonth.daysOfMonth[date.Day - 1].shiftList)
                {
                    if (s.ordered)
                    {
                        MessageBox.Show("Már létezik kérés a napra!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (false) // check for conficting request fo next day if requested night shift
                    {

                    }
                    else
                    {
                        s.RemoveEmployee(em);
                        break;
                    }
                }


                if (oPTO && oImportant)      o.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexPTO_OI].AddEmployee(em);
                if (oPTO && !oImportant)     o.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexPTO_O].AddEmployee(em);
                if (oFreeDay && oImportant)  o.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexFreeDay_OI].AddEmployee(em);
                if (oFreeDay && !oImportant) o.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexFreeDay_O].AddEmployee(em);
                if (oNight && oImportant)    o.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexNight2_O].AddEmployee(em);
                if (oNight && !oImportant)   o.currentMonth.daysOfMonth[date.Day - 1].shiftList[PublicParameters.shiftIndexNight2_O].AddEmployee(em);

                int shiftIndex = oImportant ? (oFreeDay ? PublicParameters.shiftIndexFreeDay_OI :
                    oPTO ? PublicParameters.shiftIndexPTO_OI : -1) : oFreeDay ?
                    PublicParameters.shiftIndexFreeDay_O : (oNight ? PublicParameters.shiftIndexNight2_O : PublicParameters.shiftIndexPTO_O);

                //o.currentMonth.daysOfMonth[date.Day - 1].shiftList[shiftIndex].employeeList.Add(em);
                List<Shift> sl = new List<Shift>();
                sl.Add(o.currentMonth.daysOfMonth[date.Day - 1].shiftList[shiftIndex]);

                if (oNight && date.Day < o.currentMonth.daysOfMonth.Count)
                {
                    o.currentMonth.daysOfMonth[date.Day].shiftList[PublicParameters.shiftIndexNight1_O].AddEmployee(em);
                    List<Shift> sl2 = new List<Shift>();
                    sl2.Add(o.currentMonth.daysOfMonth[date.Day].shiftList[PublicParameters.shiftIndexNight1_O]);
                    ((UserControl1)o.controlNestedList[em.id - 1][date.Day]).shifts = sl2;
                    ((UserControl1)o.controlNestedList[em.id - 1][date.Day]).EnableRemoveOrder();
                }
                
                ((UserControl1)o.controlNestedList[em.id - 1][date.Day - 1]).shifts = sl;
                ((UserControl1)o.controlNestedList[em.id - 1][date.Day - 1]).EnableRemoveOrder();

                o.SaveCurrentMonth();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
