using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Vezénylés_szerkesztő
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Employee> employeeList;
        public Month currentMonth;
        string statusLabelDefault;
        string titleDefault;
        string versionString = "v0.240804";

        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = flowLayoutPanel1;

            employeeList = new List<Employee>();
            currentMonth = new Month(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            statusLabelDefault = toolStripStatusLabel1.Text;
            titleDefault = Text;

            flowLayoutPanel1.VerticalScroll.Visible = true;
            timer1.Enabled = true;

            LoadEmployees();
            LoadCurrentMonth();

            bool clearShiftData = false;

            if (clearShiftData)
            {
                currentMonth.ClearAllShifts(false);
                currentMonth.CreateShifts(false);
            }

            ShowTable(false, false, !clearShiftData);
            UpdateStatusBar();
            CheckDates();
            GenerateStatistics();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            ActiveControl = flowLayoutPanel1;
        }

        private void timer1_Tick(object sender, EventArgs e) => UpdateStatusBar();

        void LoadEmployees()
        {
            foreach (string f in Directory.GetFiles("./Employees"))
            {
                if (f.EndsWith(".emp"))
                {
                    string json = File.ReadAllText(f);
                    Employee e = JsonConvert.DeserializeObject<Employee>(json);
                    employeeList.Add(e);
                }
            }
        }

        void LoadCurrentMonth()
        {
            if (!Directory.Exists("./MonthData")) Directory.CreateDirectory("./MonthData");
            if (!File.Exists("./MonthData/" + DateTime.Now.ToString("yyyy-MM") + ".mnth")) return;
            //string data = File.ReadAllText("./MonthData/" + DateTime.Now.ToString("yyyy-MM") + ".mnth");
            string data = File.ReadAllText("./MonthData/2024-08.mnth");
            currentMonth = JsonConvert.DeserializeObject<Month>(data);

            foreach (Day d in currentMonth.daysOfMonth)
            {
                foreach (Shift s in d.shiftList)
                {
                    DEL:
                    for (int i = 0; i < s.employeeList.Count; i++)
                    {
                        if (s.employeeList[i] == null)
                        {
                            s.employeeList.RemoveAt(i);
                            goto DEL;
                        }
                    }
                }
            }

            Text = titleDefault + " " + versionString + " - " + currentMonth.startDate.ToString("MMMM", CultureInfo.CurrentCulture).FirstCharToUpper() + " " + employeeList.Count + " fő";
        }

        public void SaveCurrentMonth()
        {
            if (!Directory.Exists("./MonthData")) Directory.CreateDirectory("./MonthData");
            string data = JsonConvert.SerializeObject(currentMonth, Formatting.Indented);
            File.WriteAllText("./MonthData/" + DateTime.Now.ToString("yyyy-MM") + ".mnth", data);
        }

        private void hozzáadásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 addEmployeeForm = new Form2();
            addEmployeeForm.Owner = this;
            addEmployeeForm.Show();
            addEmployeeForm.FormClosing += AddEmployeeFormClosing;
            addEmployeeForm.ID = employeeList.Count + 1;
        }

        private void hozzáadásToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddFlight(DateTime.Now);
        }

        public void AddFlight(DateTime date)
        {
            Form3 addFlightForm = new Form3();
            addFlightForm.Owner = this;
            addFlightForm.Show();
            addFlightForm.FormClosing += AddFlightFormClosing;
            addFlightForm.checkInStart = date;
        }

        void AddEmployeeFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Form2 form = (Form2)sender;
            if (form.DialogResult == DialogResult.OK)
            {
                Employee n = new Employee(form.id, form.name, form.type, form.exams, form.examDate, form.cardDate, form.male, form.distanceToWork, form.canGoWith);
                employeeList.Add(n);
                n.SaveToFile();
                ShowTable();
            }
        }

        void AddFlightFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Form3 form = (Form3)sender;
            if (form.DialogResult == DialogResult.OK)
            {
                Flight f = new Flight(form.flightDestination, form.airline, form.checkInStart, form.charter, form.monthly, form.weekly);
                foreach (Day d in currentMonth.daysOfMonth)
                {
                    if (d.date.Year == f.checkInStart.Year && d.date.Month == f.checkInStart.Month && d.date.Day == f.checkInStart.Day)
                    {
                        d.AddFlight(f);
                        foreach (Control c in daysControlList)
                            if (((UserControl3)c).day == d.dayNumber)
                                ((UserControl3)c).data = d;
                    }

                    if (f.isWeekly && d.date.Year == f.checkInStart.Year && d.date.Month == f.checkInStart.Month && 
                        (d.date.Day == f.checkInStart.Day + 7 || d.date.Day == f.checkInStart.Day + 14 || 
                        d.date.Day == f.checkInStart.Day + 21 || d.date.Day == f.checkInStart.Day + 28))
                    {
                        d.AddFlight(f);
                        foreach (Control c in daysControlList)
                            if (((UserControl3)c).day == d.dayNumber)
                                ((UserControl3)c).data = d;
                    }
                }
            }
            SaveCurrentMonth();
        }

        void EmployeeEdited(Employee e)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].id == e.id)
                {
                    employeeList[i] = e;
                    ((UserControl2)employeeControlList[i]).employeeData = e;
                    ((UserControl2)employeeControlList[i]).id = e.id;
                    ((UserControl2)employeeControlList[i]).name = e.name;
                    ((UserControl2)employeeControlList[i]).isSupervisor = (e.type == EmployeeType.Supervisor ? true : false);
                }
            }
            GenerateStatistics();
        }

        public List<List<Control>> controlNestedList = new List<List<Control>>();
        public List<Control> employeeControlList = new List<Control>();
        List<Control> daysControlList = new List<Control>();
        List<string> warningList = new List<string>();

        void ShowTable(bool onlyTopRow = false, bool onlyUpdate = false, bool notModifyShifts = false)
        {
            if (!onlyTopRow && !onlyUpdate)
            {
                foreach (UserControl u in flowLayoutPanel1.Controls)
                {
                    try { ((UserControl1)u).Destroy(); } catch { }
                    try { ((UserControl2)u).Destroy(); } catch { }
                    try { ((UserControl3)u).Destroy(); } catch { }
                }
                flowLayoutPanel1.Controls.Clear();
                List<Control> f1 = new List<Control>();

                foreach (Employee e in employeeList)
                {
                    UserControl2 u = new UserControl2();
                    u.name = e.name;
                    u.id = e.id;
                    u.isSupervisor = e.type == EmployeeType.Supervisor ? true : false;
                    u.onEmployeeEdited += EmployeeEdited;
                    u.owner = this;
                    u.employeeData = e;
                    f1.Add(u);
                    //flowLayoutPanel1.Controls.Add(u);

                    employeeControlList.Add(u);

                    List<Control> controlList = new List<Control>();

                    for (int i = 0; i < currentMonth.daysOfMonth.Count; i++)
                    {
                        UserControl1 n = new UserControl1();
                        //n.isFreeDay = true; //temporary
                        List<Shift> sl = currentMonth.daysOfMonth[i].GetShiftsPerEmployee(e);
                        n.shifts = sl;
                        n.employeeData = e;
                        n.owner = this;
                        n.date = new DateTime(currentMonth.startDate.Year, currentMonth.startDate.Month, i + 1);

                        if (n.shifts.Count > 0)
                        {
                            if (n.shifts[0].isSickDay)
                            {
                                foreach (SickDay s in currentMonth.sickDays)
                                {
                                    if (s.date.Day == i + 1 && s.sickEmployee.id == e.id && s.sickEmployee.name == e.name)
                                    {
                                        n.substituteEmployee = s.substituteEmployee;
                                    }
                                }
                            }
                        }

                        if (!notModifyShifts) 
                        { 
                            #region TMP_ShiftTypes
                            if (e.id == 1)
                            {
                                n.isFreeDay = false;
                                List<Shift> divided = new List<Shift>();
                                foreach (Shift s in currentMonth.daysOfMonth[i].nonDefaultShifts)
                                {
                                    if (s.type.HasFlag(ShiftType.Divided))
                                    {
                                        divided.Add(s);
                                        s.AddEmployee(e);
                                    }
                                }

                                n.shifts = divided;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 2)
                            {
                                n.isFreeDay = false;
                                List<Shift> nonDivided = new List<Shift>();
                                foreach (Shift s in currentMonth.daysOfMonth[i].nonDefaultShifts)
                                {
                                    if (!s.type.HasFlag(ShiftType.Divided))
                                    {
                                        nonDivided.Add(s);
                                        s.AddEmployee(e);
                                    }
                                }

                                n.shifts = nonDivided;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 3)
                            {
                                n.isFreeDay = false;
                                List<Shift> modDefault = new List<Shift>();
                                if (currentMonth.daysOfMonth[i].shiftList[PublicParameters.shiftIndexDay].shiftStart.Hour != 7)
                                {
                                    modDefault.Add(currentMonth.daysOfMonth[i].shiftList[PublicParameters.shiftIndexDay]);
                                    currentMonth.daysOfMonth[i].shiftList[PublicParameters.shiftIndexDay].AddEmployee(e);
                                }

                                n.shifts = modDefault;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 4)
                            {
                                n.isFreeDay = false;
                                List<Shift> night = new List<Shift>();
                                if (i % 4 == 1)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[0]);
                                    currentMonth.daysOfMonth[i].shiftList[0].AddEmployee(e);
                                }
                                if (i % 4 == 0)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[1]);
                                    currentMonth.daysOfMonth[i].shiftList[1].AddEmployee(e);
                                }

                                n.shifts = night;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 5)
                            {
                                n.isFreeDay = false;
                                List<Shift> night = new List<Shift>();
                                if (i % 4 == 2)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[0]);
                                    currentMonth.daysOfMonth[i].shiftList[0].AddEmployee(e);
                                }
                                if (i % 4 == 1)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[1]);
                                    currentMonth.daysOfMonth[i].shiftList[1].AddEmployee(e);
                                }

                                n.shifts = night;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 6)
                            {
                                n.isFreeDay = false;
                                List<Shift> night = new List<Shift>();
                                if (i % 4 == 3)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[0]);
                                    currentMonth.daysOfMonth[i].shiftList[0].AddEmployee(e);
                                }
                                if (i % 4 == 2)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[1]);
                                    currentMonth.daysOfMonth[i].shiftList[1].AddEmployee(e);
                                }

                                n.shifts = night;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 7)
                            {
                                n.isFreeDay = false;
                                List<Shift> night = new List<Shift>();
                                if (i % 4 == 0)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[0]);
                                    currentMonth.daysOfMonth[i].shiftList[0].AddEmployee(e);
                                }
                                if (i % 4 == 3)
                                {
                                    night.Add(currentMonth.daysOfMonth[i].shiftList[1]);
                                    currentMonth.daysOfMonth[i].shiftList[1].AddEmployee(e);
                                }

                                n.shifts = night;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 8)
                            {
                                n.isFreeDay = false;
                                List<Shift> day = new List<Shift>();
                                if (i % 2 == 1 && currentMonth.daysOfMonth[i].shiftList[2].shiftStart.Hour == 7)
                                {
                                    day.Add(currentMonth.daysOfMonth[i].shiftList[2]);
                                    currentMonth.daysOfMonth[i].shiftList[2].AddEmployee(e);
                                }

                                n.shifts = day;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 9)
                            {
                                n.isFreeDay = false;
                                List<Shift> day = new List<Shift>();
                                if (i % 2 == 0 && currentMonth.daysOfMonth[i].shiftList[2].shiftStart.Hour == 7)
                                {
                                    day.Add(currentMonth.daysOfMonth[i].shiftList[2]);
                                    currentMonth.daysOfMonth[i].shiftList[2].AddEmployee(e);
                                }

                                n.isGate3 = true;
                                n.shifts = day;

                                if (n.shifts.Count == 0)
                                    n.isFreeDay = true;
                            }

                            if (e.id == 10)
                            {
                                n.isFreeDay = false;
                                n.isStandBy = true;
                                currentMonth.daysOfMonth[i].shiftList[PublicParameters.shiftIndexStandby].AddEmployee(e);
                            }

                            if (e.id == 11)
                            {
                                n.isFreeDay = false;
                                n.isPTO = true;
                                currentMonth.daysOfMonth[i].shiftList[PublicParameters.shiftIndexPTO].AddEmployee(e);
                            }
                        #endregion
                        }

                        if (sl.Count == 0) n.SetNull();
                        f1.Add(n);
                        //flowLayoutPanel1.Controls.Add(n);
                        controlList.Add(n);
                    }

                    controlNestedList.Add(controlList);
                }
                flowLayoutPanel1.Controls.AddRange(f1.ToArray());
            }

            if (!onlyUpdate)
            {
                List<Control> f2 = new List<Control>();
                foreach (UserControl u in flowLayoutPanel2.Controls)
                {
                    try { ((UserControl1)u).Destroy(); } catch { }
                    try { ((UserControl2)u).Destroy(); } catch { }
                    try { ((UserControl3)u).Destroy(); } catch { }
                }
                flowLayoutPanel2.Controls.Clear();

                UserControl2 u2 = new UserControl2();
                u2.name = "";
                u2.id = 0;
                f2.Add(u2);
                //flowLayoutPanel2.Controls.Add(u2);

                for (int i = 0; i < currentMonth.daysOfMonth.Count; i++)
                {
                    UserControl3 n = new UserControl3();
                    n.data = currentMonth.daysOfMonth[i];
                    n.day = i + 1;
                    n.owner = this;
                    f2.Add(n);
                    flowLayoutPanel2.Controls.Add(n);
                    daysControlList.Add(n);
                }

                flowLayoutPanel2.Controls.AddRange(f2.ToArray());
            }
        }

        void UpdateTable()
        {
            ClearShiftVisual();
            foreach (Employee e in employeeList)
                for (int d = 0; d < currentMonth.daysOfMonth.Count; d++)
                    SetShiftVisual(e.id, d + 1, currentMonth.daysOfMonth[d].GetShiftsPerEmployee(e));
            GenerateStatistics();
            Application.DoEvents();
        }

        void UpdateStatusBar() //add multiple flights to current flight
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("HH:mm");
            string statusLabel = statusLabelDefault;
            int cD = -1;
            int cI = -1;

            List<string> currentFlights = new List<string>();
            bool nextFlightFound = false;
            for (int i = 0; i < currentMonth.daysOfMonth[DateTime.Now.Day - 1].flightList.Count; i++)
            {
                int d = DateTime.Now.Day - 1;
                Flight f = currentMonth.daysOfMonth[d].flightList[i];

                if (f.checkInStart < DateTime.Now &&
                f.checkInStart + new TimeSpan(2, 0, 0) > DateTime.Now)
                {
                    currentFlights.Add(f.destination);
                    cD = d;
                    cI = i;
                }

                if (!nextFlightFound &&
                    ((DateTime.Now.Day == currentMonth.daysOfMonth[d].dayNumber && f.checkInStart > DateTime.Now) ||
                    (DateTime.Now.Day + 1 == currentMonth.daysOfMonth[d].dayNumber && i == 0)))
                {
                    statusLabel = statusLabel.Replace("#NAME", f.destination);
                    statusLabel = statusLabel.Replace("#CHECKIN", f.checkInStart.ToString("yyyy. MM. dd. HH:mm"));
                    nextFlightFound = true;
                }
            }

            int sup = 0;
            int emp = 0;
            foreach (Shift s in currentMonth.daysOfMonth[DateTime.Now.Day - 1].shiftList)
            {
                if (s.hours > 0 && !s.isPto)
                {
                    foreach (Employee e in s.employeeList)
                    {
                        if (e.type == EmployeeType.Supervisor && s.isNow) sup++;
                        else if (e.type == EmployeeType.Default && s.isNow) emp++;
                    }
                }
            }

            if (currentFlights.Count > 0)
            {
                string s = currentFlights[0];
                if (currentFlights.Count > 1)
                    for (int i = 1; i < currentFlights.Count; i++)
                        s += " - " + currentFlights[i];
                statusLabel = statusLabel.Replace("#CURRENT", s);
            }

            statusLabel = statusLabel.Replace("#CURRENT", "Nincs");
            statusLabel = statusLabel.Replace("#NAME", "Nincs");
            statusLabel = statusLabel.Replace("#CHECKIN", "NaN");
            statusLabel = statusLabel.Replace("#1", emp.ToString());
            statusLabel = statusLabel.Replace("#2", sup.ToString());
            toolStripStatusLabel1.Text = statusLabel;
        }

        void CheckDates()
        {
            warningList = new List<string>();

            bool hasWarning = false;
            foreach (Employee e in employeeList)
            {
                if (e.cardDate < DateTime.Now + new TimeSpan(PublicParameters.warningCardDateDays, 0, 0, 0))
                {
                    string w = e.name + " belépőkártyájának érvényessége " + PublicParameters.warningCardDateDays + " napon belül lejár.";
                    //notifyIcon1.ShowBalloonTip(10000,
                    //    "Belépő Kártya Lejárat",
                    //    w,
                    //    ToolTipIcon.Warning);
                    hasWarning = true;
                    warningList.Add(w);
                }

                if (e.examDate + new TimeSpan(365, 0, 0, 0) < DateTime.Now + new TimeSpan(PublicParameters.warningExamDateDays, 0, 0, 0))
                {
                    string w = e.name + " vizsgájának érvényessége " + PublicParameters.warningExamDateDays + " napon belül lejár.";
                    //notifyIcon1.ShowBalloonTip(10000,
                    //    "Vizsga Lejárat",
                    //    w,
                    //    ToolTipIcon.Warning);
                    hasWarning = true;
                    warningList.Add(w);
                }
            }
            toolStripSplitButton1.Visible = hasWarning;
        }

        void GenerateStatistics()
        {
            richTextBox1.Text = "";

            List<float> hours = new List<float>();
            List<int> days = new List<int>();
            List<int> shifts = new List<int>();
            List<int> PTO = new List<int>();
            List<int> sickDays = new List<int>();
            List<float> totalHours = new List<float>();
            List<float> overtime = new List<float>();

            int longestName = 0;

            for (int i = 0; i < employeeList.Count; i++)
            {
                if (employeeList[i].name.Length > longestName) longestName = employeeList[i].name.Length;

                float h = 0;
                int d = 0;
                int s = 0;
                int p = 0;
                int si = 0;
                float t = 0;
                float o = 0;
                foreach (Day day in currentMonth.daysOfMonth)
                {
                    bool addDay = false;
                    foreach (Shift shift in day.shiftList)
                    {
                        if (shift.ContainsEmployee(employeeList[i]))
                        {
                            if (shift.isPto) p++;
                            else if (!shift.isStandby && !shift.isPto && !shift.isFreeDay || !shift.isSickDay && (!shift.ordered || (!shift.ordered && !shift.type.HasFlag(ShiftType.Night))))
                            {
                                h += shift.hours;
                                if (!(shift.shiftStart.Hour == 0 && shift.shiftStart.Minute == 0 && day.dayNumber > 1))
                                {
                                    s++;
                                    addDay = true;
                                }
                            }
                            else if (shift.isSickDay) si++;
                        }
                    }
                    if (addDay) d++;
                }
                t = h + (PublicParameters.hoursPTO * p);
                o = t - PublicParameters.avgMonthlyHours;

                hours.Add(h);
                days.Add(d);
                shifts.Add(s);
                PTO.Add(p);
                sickDays.Add(si);
                totalHours.Add(t);
                overtime.Add(o);
            }

            richTextBox1.Text += "Név".PadRight(longestName, ' ') + "|       " +
                "Óra" + "|    " +
                "Nap" + "| " +
                "Műszak" + "|  " +
                "Szabi" + "|  " +
                "Beteg" + "|    " +
                "Összes" + "|     " +
                "Túlóra" + "|   " + 
                "Távolság" + "|  " +
                "Összes Km" + "|      " + 
                "Összeg" + "\n";
            richTextBox1.Text += MultiplyChars(longestName + 103, '#') + "\n";

            for (int i = 0; i < employeeList.Count; i++)
            {
                richTextBox1.Text += employeeList[i].name.PadRight(longestName, ' ') + "|     " +
                    hours[i].ToString("F1").PadLeft(5, ' ') + "|     " +
                    days[i].ToString().PadLeft(2, ' ') + "|     " +
                    shifts[i].ToString().PadLeft(2, ' ') + "|     " +
                    PTO[i].ToString().PadLeft(2, ' ') + "|     " +
                    sickDays[i].ToString().PadLeft(2, ' ') + "|     " +
                    totalHours[i].ToString("F1").PadLeft(5, ' ') + "|     " +
                    overtime[i].ToString("F1").PadLeft(6, ' ') + "|      " +
                    employeeList[i].distanceToWork.ToString("F1").PadLeft(5, ' ') + "|       " + 
                    (Math.Round(employeeList[i].distanceToWork) * 2 * shifts[i]).ToString().PadLeft(4, ' ') + "|    " +
                    (Math.Round(employeeList[i].distanceToWork) * 2 * shifts[i] * PublicParameters.multiplierKm).ToString().PadLeft(5, ' ') + " Ft" + "\n";
            }

            int allPTO = 0;
            int allSickDays = 0;
            int allOrderedFreeDay = 0;
            int allNonOrderedFreeDay = 0;
            int allHours = 0;
            int allHoursPlusOvertime = 0;
            int allOvertime = 0;

            foreach (int i in PTO) allPTO += i;
            foreach (int i in sickDays) allSickDays += i;
            foreach (int i in hours) allHours += i;
            foreach (int i in totalHours) allHoursPlusOvertime += i;
            foreach (int i in overtime) allOvertime += i;

            foreach (Day d in currentMonth.daysOfMonth)
            {
                foreach (Shift s in d.shiftList)
                {
                    if (s.ordered && s.isFreeDay) allOrderedFreeDay += s.employeeList.Count;
                    else if (!s.ordered && s.isFreeDay) allNonOrderedFreeDay += s.employeeList.Count;
                }
            }

            label1.Text = label1.Text.Replace("#1", allPTO.ToString());
            label1.Text = label1.Text.Replace("#2", allSickDays.ToString());
            label1.Text = label1.Text.Replace("#3", allOrderedFreeDay.ToString());
            label1.Text = label1.Text.Replace("#4", allNonOrderedFreeDay.ToString());
            label1.Text = label1.Text.Replace("#5", allHours.ToString());
            label1.Text = label1.Text.Replace("#6", allHoursPlusOvertime.ToString());
            label1.Text = label1.Text.Replace("#7", allOvertime.ToString());
        }

        public void SetShiftVisual(int employeeID, int dayOfMonth, List<Shift> shiftList, Employee substitute = null)
        {
            ((UserControl1)controlNestedList[employeeID - 1][dayOfMonth - 1]).shifts = shiftList;
            if (substitute != null) ((UserControl1)controlNestedList[employeeID - 1][dayOfMonth - 1]).substituteEmployee = substitute;
        }

        public void SetShiftVisual(int employeeID, int dayOfMonth, Shift shiftToAdd, Employee substitute = null)
        {
            List<Shift> shiftList = new List<Shift>();

            foreach (Shift s in ((UserControl1)controlNestedList[employeeID - 1][dayOfMonth - 1]).shifts)
                shiftList.Add(s);

            shiftList.Add(shiftToAdd);
            SetShiftVisual(employeeID, dayOfMonth, shiftList, substitute);
        }

        public void ClearShiftVisual()
        {
            foreach (Employee e in employeeList)
                foreach (Day d in currentMonth.daysOfMonth)
                    ((UserControl1)controlNestedList[e.id - 1][d.dayNumber - 1]).shifts = new List<Shift> { d.shiftList[PublicParameters.shiftIndexFreeDay] };
        }

        public void AddNotification(string s)
        {
            warningList.Add(s);
            toolStripSplitButton1.Visible = true;
        }

        public void AddSickDay(int employeeID, int dayOfMonth, Employee substitute = null)
        {
            List<Shift> sl1 = new List<Shift>();
            foreach (Shift s in currentMonth.daysOfMonth[dayOfMonth - 1].shiftList)
            {
                s.RemoveEmployee(substitute);
                if (s.ContainsEmployee(GetEmployee(employeeID)))
                {
                    s.AddEmployee(substitute);
                    sl1.Add(s);
                }
            }
            SetShiftVisual(substitute.id, dayOfMonth, sl1);

            List<int> substituteShiftIndexes = new List<int>();
            for (int i = 0; i < currentMonth.daysOfMonth[dayOfMonth - 1].shiftList.Count; i++)
            {
                if (currentMonth.daysOfMonth[dayOfMonth - 1].shiftList[i].ContainsEmployee(GetEmployee(employeeID)))
                    substituteShiftIndexes.Add(i);

                currentMonth.daysOfMonth[dayOfMonth - 1].shiftList[i].RemoveEmployee(GetEmployee(employeeID));
            }

            currentMonth.daysOfMonth[dayOfMonth - 1].shiftList[PublicParameters.shiftIndexSickDay].AddEmployee(GetEmployee(employeeID));
            List<Shift> sl2 = new List<Shift>();
            sl2.Add(currentMonth.daysOfMonth[dayOfMonth - 1].shiftList[PublicParameters.shiftIndexSickDay]);
            SetShiftVisual(employeeID, dayOfMonth, sl2, substitute);

            List<Shift> substituteShifts = new List<Shift>();
            foreach (int i in substituteShiftIndexes)
                substituteShifts.Add(currentMonth.daysOfMonth[dayOfMonth - 1].shiftList[i]);
            currentMonth.AddSickDay(GetEmployee(employeeID), substitute, dayOfMonth, substituteShifts);

            SaveCurrentMonth();
            GenerateStatistics();
        }

        public Employee GetEmployee(int employeeID)
        {
            foreach (Employee e in employeeList)
                if (e.id == employeeID)
                    return e;
            return null;
        }

        string MultiplyChars(int m, char c)
        {
            string s = "";
            for (int i = 0; i < m; i++)
                s += c;
            return s;
        }

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            MoveWindow(Handle, x, y, width, height, true);
        }

        private static Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //notifyIcon1.Visible = false;
            SaveCurrentMonth();
        }

        private void beoSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Vezénylés Terv " + DateTime.Now.ToString("yyyy MMMM", CultureInfo.CurrentCulture) + " " + employeeList.Count + " fő";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int h = this.Height;
                this.Height = 159 + (employeeList.Count * 54);
                using (Bitmap bmp = new Bitmap(this.Width, 159 + (employeeList.Count * 54))) //min height 159
                {
                    this.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                    //File.WriteAllText(@"C:\Desktop\sample.png", "");
                    Bitmap n = cropImage(bmp, new Rectangle(18, 81, currentMonth.daysOfMonth.Count * 37 + 188, employeeList.Count * 54 + 57));
                    n.Save(saveFileDialog1.FileName, ImageFormat.Png); // make sure path exists!
                }
                this.Height = h;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            BringToFront();
        }

        private void beállításokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.Owner = this;
            form.Show();
        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            //ShowTable(false, false, true);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.warningList = warningList;
            form.Show();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.warningList = warningList;
            form.Show();
        }

        private void értesítésekTörléseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            warningList = new List<string>();
            toolStripSplitButton1.Visible = false;
        }

        private void beosztásGenerálásaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentMonth.ClearAllShifts(false);
            currentMonth.CreateShifts(false);
            currentMonth.CalculateEmployeesForShifts();
            UpdateTable();
        }
    }
}
