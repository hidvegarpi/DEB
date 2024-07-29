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

namespace Vezénylés_szerkesztő
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            employeeList = new List<Employee>();
            currentMonth = new Month(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            statusLabelDefault = toolStripStatusLabel1.Text;

            flowLayoutPanel1.VerticalScroll.Visible = true;
            timer1.Enabled = true;

            LoadEmployees();
            LoadCurrentMonth();

            foreach (Day d in currentMonth.daysOfMonth)
            {
                //d.shiftList = new List<Shift>();
                //d.AddDefaultShifts();
                //d.CreateShifts();
            }

            ShowTable(false, false, true);
            UpdateStatusBar();
            CheckDates();
            GenerateStatistics();
        }

        private void timer1_Tick(object sender, EventArgs e) => UpdateStatusBar();

        void LoadEmployees()
        {
            foreach (string f in Directory.GetFiles("./Employees"))
            {
                string json = File.ReadAllText(f);
                Employee e = JsonConvert.DeserializeObject<Employee>(json);
                employeeList.Add(e);
            }
        }

        void LoadCurrentMonth()
        {
            if (!Directory.Exists("./MonthData")) Directory.CreateDirectory("./MonthData");
            if (!File.Exists("./MonthData/" + DateTime.Now.ToString("yyyy-MM") + ".mnth")) return;
            string data = File.ReadAllText("./MonthData/" + DateTime.Now.ToString("yyyy-MM") + ".mnth");
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
        }

        public void SaveCurrentMonth()
        {
            if (!Directory.Exists("./MonthData")) Directory.CreateDirectory("./MonthData");
            string data = JsonConvert.SerializeObject(currentMonth, Formatting.Indented);
            File.WriteAllText("./MonthData/" + DateTime.Now.ToString("yyyy-MM") + ".mnth", data);
        }

        public List<Employee> employeeList;
        public Month currentMonth;
        string statusLabelDefault;

        private void hozzáadásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 addEmployeeForm = new Form2();
            addEmployeeForm.Owner = this;
            addEmployeeForm.Show();
            addEmployeeForm.FormClosing += AddEmployeeFormClosing;
            addEmployeeForm.ID = employeeList.Count + 1;
            Hide();
        }

        private void hozzáadásToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 addFlightForm = new Form3();
            addFlightForm.Owner = this;
            addFlightForm.Show();
            addFlightForm.FormClosing += AddFlightFormClosing;
            Hide();
        }

        void AddEmployeeFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Show();
            BringToFront();
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
            Show();
            BringToFront();
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

                foreach (Employee e in employeeList)
                {
                    UserControl2 u = new UserControl2();
                    u.name = e.name;
                    u.id = e.id;
                    u.isSupervisor = e.type == EmployeeType.Supervisor ? true : false;
                    u.onEmployeeEdited += EmployeeEdited;
                    u.owner = this;
                    u.employeeData = e;
                    flowLayoutPanel1.Controls.Add(u);

                    employeeControlList.Add(u);

                    List<Control> controlList = new List<Control>();

                    for (int i = 0; i < currentMonth.daysOfMonth.Count; i++)
                    {
                        UserControl1 n = new UserControl1();
                        n.isFreeDay = true; //temporary
                        n.shifts = currentMonth.daysOfMonth[i].GetShiftsPerEmployee(e);
                        n.employeeData = e;
                        n.owner = this;
                        n.date = new DateTime(currentMonth.startDate.Year, currentMonth.startDate.Month, i + 1);

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

                        flowLayoutPanel1.Controls.Add(n);
                        controlList.Add(n);
                    }

                    controlNestedList.Add(controlList);
                }
            }

            if (!onlyUpdate)
            {
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
                flowLayoutPanel2.Controls.Add(u2);

                for (int i = 0; i < currentMonth.daysOfMonth.Count; i++)
                {
                    UserControl3 n = new UserControl3();
                    n.data = currentMonth.daysOfMonth[i];
                    n.day = i + 1;
                    flowLayoutPanel2.Controls.Add(n);
                    daysControlList.Add(n);
                }
            }
        }

        void UpdateStatusBar() //add multiple flights to current flight
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("HH:mm");
            string statusLabel = statusLabelDefault;
            int cD = -1;
            int cI = -1;

            bool nextFlightFound = false;
            for (int d = 0; d < currentMonth.daysOfMonth.Count; d++)
            {
                for (int i = 0; i < currentMonth.daysOfMonth[d].flightList.Count; i++)
                {
                    Flight f = currentMonth.daysOfMonth[d].flightList[i];

                    if (f.checkInStart < DateTime.Now &&
                    new DateTime(
                        f.checkInStart.Year,
                        f.checkInStart.Month,
                        f.checkInStart.Day,
                        f.checkInStart.Hour + 2,
                        f.checkInStart.Minute,
                        f.checkInStart.Second)
                    > DateTime.Now)
                    {
                        statusLabel = statusLabel.Replace("#CURRENT", f.destination);
                        cD = d;
                        cI = i;
                    }

                    if (!nextFlightFound && 
                        ((DateTime.Now.Day == currentMonth.daysOfMonth[d].dayNumber && f.checkInStart > DateTime.Now) || 
                        (DateTime.Now.Day + 1 == currentMonth.daysOfMonth[d].dayNumber && i == 0)))
                    {
                        statusLabel = statusLabel.Replace("#NAME", f.destination);
                        statusLabel = statusLabel.Replace("#CHECKIN", f.checkInStart.ToString("yyyy. MM. dd. HH:mm"));
                        //MessageBox.Show(f.ToString());
                        nextFlightFound = true;
                    }
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

            statusLabel = statusLabel.Replace("#CURRENT", "Nincs");
            statusLabel = statusLabel.Replace("#NAME", "Nincs");
            statusLabel = statusLabel.Replace("#CHECKIN", "NaN");
            statusLabel = statusLabel.Replace("#1", emp.ToString());
            statusLabel = statusLabel.Replace("#2", sup.ToString());
            toolStripStatusLabel1.Text = statusLabel;
        }

        void CheckDates()
        {
            foreach (Employee e in employeeList)
            {
                if (e.cardDate < DateTime.Now + new TimeSpan(PublicParameters.warningCardDateDays, 0, 0, 0))
                    notifyIcon1.ShowBalloonTip(10000,
                        "Belépő Kártya Lejárat",
                        e.name + " belépőkártyájának érvényessége " + PublicParameters.warningCardDateDays + " napon belül lejár.",
                        ToolTipIcon.Warning);

                if (e.examDate + new TimeSpan(365, 0, 0, 0) < DateTime.Now + new TimeSpan(PublicParameters.warningExamDateDays, 0, 0, 0))
                    notifyIcon1.ShowBalloonTip(10000,
                        "Vizsga Lejárat",
                        e.name + " vizsgájának érvényessége " + PublicParameters.warningExamDateDays + " napon belül lejár.",
                        ToolTipIcon.Warning);
            }
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
                            else if (!shift.isStandby)
                            {
                                h += shift.hours;
                                if (!(shift.shiftStart.Hour == 0 && shift.shiftStart.Minute == 0 && day.dayNumber > 1))
                                {
                                    s++;
                                    addDay = true;
                                }
                            }
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

            for (int i = 0; i < employeeList.Count; i++) //93
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
            notifyIcon1.Visible = false;
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
    }
}
