using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Core;
using System.Configuration;
using System.Security.Policy;

namespace Vezénylés_szerkesztő
{
    public static class PublicParameters
    {
        public static int warningCardDateDays         { get { return Properties.Settings.Default.warningCardDateDays; } set { Properties.Settings.Default.warningCardDateDays = value; Properties.Settings.Default.Save(); } }
        public static int warningExamDateDays         { get { return Properties.Settings.Default.warningExamDateDays; } set { Properties.Settings.Default.warningExamDateDays = value; Properties.Settings.Default.Save(); } }

        public static TimeSpan minShiftTime { get { return TimeSpan.FromMinutes(minShiftTimeMins); } }
        public static int minShiftTimeMins            { get { return Properties.Settings.Default.minShiftTimeMins; } set { Properties.Settings.Default.minShiftTimeMins = value; Properties.Settings.Default.Save(); } }
        public static int avgMonthlyHours             { get { return Properties.Settings.Default.avgMonthlyHours; } set { Properties.Settings.Default.avgMonthlyHours = value; Properties.Settings.Default.Save(); } }
        public static int hoursPTO                    { get { return Properties.Settings.Default.hoursPTO; } set { Properties.Settings.Default.hoursPTO = value; Properties.Settings.Default.Save(); } }
        public static int hoursStandby                { get { return Properties.Settings.Default.hoursStandby; } set { Properties.Settings.Default.hoursStandby = value; Properties.Settings.Default.Save(); } }
        public static int hoursFreeDay                { get { return Properties.Settings.Default.hoursFreeDay; } set { Properties.Settings.Default.hoursFreeDay = value; Properties.Settings.Default.Save(); } }
        public static int multiplierKm                { get { return Properties.Settings.Default.multiplierKm; } set { Properties.Settings.Default.multiplierKm = value; Properties.Settings.Default.Save(); } }

        public static int employeesPerFlight          { get { return Properties.Settings.Default.employeesPerFlight; } set { Properties.Settings.Default.employeesPerFlight = value; Properties.Settings.Default.Save(); } }
        public static int employeesPerNightShift      { get { return Properties.Settings.Default.employeesPerNightShift; } set { Properties.Settings.Default.employeesPerNightShift = value; Properties.Settings.Default.Save(); } }
        public static int employeesPerDayShift        { get { return Properties.Settings.Default.employeesPerDayShift; } set { Properties.Settings.Default.employeesPerDayShift = value; Properties.Settings.Default.Save(); } }
        public static int employeesPerStandby         { get { return Properties.Settings.Default.employeesPerStandby; } set { Properties.Settings.Default.employeesPerStandby = value; Properties.Settings.Default.Save(); } }

        public static int shiftIndexDay               = 2;
        public static int shiftIndexStandby           = 3;
        public static int shiftIndexPTO               = 4;
        public static int shiftIndexFreeDay           = 5;
        public static int shiftIndexPTO_O             = 6;
        public static int shiftIndexPTO_OI            = 7;
        public static int shiftIndexFreeDay_O         = 8;
        public static int shiftIndexFreeDay_OI        = 9;
        public static int shiftIndexNight1_O          = 10;
        public static int shiftIndexNight2_O          = 11;
        public static int shiftNonDefaultStart        = 12;

        public static Color colorShiftAMStart             { get { return Properties.Settings.Default.colorShiftAMStart; } set { Properties.Settings.Default.colorShiftAMStart = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftAMEndLong           { get { return Properties.Settings.Default.colorShiftAMEndLong; } set { Properties.Settings.Default.colorShiftAMEndLong = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftAMEndShort          { get { return Properties.Settings.Default.colorShiftAMEndShort; } set { Properties.Settings.Default.colorShiftAMEndShort = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftModDay              { get { return Properties.Settings.Default.colorShiftModDay; } set { Properties.Settings.Default.colorShiftModDay = value; Properties.Settings.Default.Save(); } }

        public static Color colorShiftPMStart             { get { return Properties.Settings.Default.colorShiftPMStart; } set { Properties.Settings.Default.colorShiftPMStart = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftPMEndLong           { get { return Properties.Settings.Default.colorShiftPMEndLong; } set { Properties.Settings.Default.colorShiftPMEndLong = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftPMEndShort          { get { return Properties.Settings.Default.colorShiftPMEndShort; } set { Properties.Settings.Default.colorShiftPMEndShort = value; Properties.Settings.Default.Save(); } }

        public static Color colorShiftGate3               { get { return Properties.Settings.Default.colorShiftGate3; } set { Properties.Settings.Default.colorShiftGate3 = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftNight               { get { return Properties.Settings.Default.colorShiftNight; } set { Properties.Settings.Default.colorShiftNight = value; Properties.Settings.Default.Save(); } }
        public static Color colorShiftStandby             { get { return Properties.Settings.Default.colorShiftStandby; } set { Properties.Settings.Default.colorShiftStandby = value; Properties.Settings.Default.Save(); } }
        public static Color colorFreeDay                  { get { return Properties.Settings.Default.colorFreeDay; } set { Properties.Settings.Default.colorFreeDay = value; Properties.Settings.Default.Save(); } }
        public static Color colorStandBy                  { get { return Properties.Settings.Default.colorStandBy; } set { Properties.Settings.Default.colorStandBy = value; Properties.Settings.Default.Save(); } }
        public static Color colorPaidTimeOff              { get { return Properties.Settings.Default.colorPaidTimeOff; } set { Properties.Settings.Default.colorPaidTimeOff = value; Properties.Settings.Default.Save(); } }

        public static Color colorOrderedFreeDay           { get { return Properties.Settings.Default.colorOrderedFreeDay; } set { Properties.Settings.Default.colorOrderedFreeDay = value; Properties.Settings.Default.Save(); } }
        public static Color colorOrderedFreeDayImportant  { get { return Properties.Settings.Default.colorOrderedFreeDayImportant; } set { Properties.Settings.Default.colorOrderedFreeDayImportant = value; Properties.Settings.Default.Save(); } }
        public static Color colorOrderedPTOImportant      { get { return Properties.Settings.Default.colorOrderedPTOImportant; } set { Properties.Settings.Default.colorOrderedPTOImportant = value; Properties.Settings.Default.Save(); } }
        public static Color colorOrderedNight             { get { return Properties.Settings.Default.colorOrderedNight; } set { Properties.Settings.Default.colorOrderedNight = value; Properties.Settings.Default.Save(); } }
    }

    public enum EmployeeType
    {
        Default,
        Supervisor
    }

    [Flags]
    public enum ExamType
    {
        VED1 = 0b_0000_0001,
        VED2 = 0b_0000_0010,
        VED3 = 0b_0000_0100,
        VED4 = 0b_0000_1000
    }

    [Flags]
    public enum ShiftType
    {
        Night   = 0b_0000_0001,
        Day     = 0b_0000_0010,
        Long    = 0b_0000_0100,
        Short   = 0b_0000_1000,
        Divided = 0b_0001_0000
    }

    public class Employee
    {
        public int id;
        public string name;
        public EmployeeType type;
        public ExamType exams;
        public DateTime examDate;
        public DateTime cardDate;
        public float distanceToWork;
        public bool male;
        public List<int> canGoWith = new List<int>();

        //maybe transport mode
        //      asked for more shifts

        [JsonConstructor]
        public Employee(int _id, string _name, EmployeeType _type, ExamType _exams, DateTime _examDate, DateTime _cardDate, bool _male, float _distanceToWork = 0, List<int> _canGoWith = null)
        {
            id = _id;
            name = _name;
            type = _type;
            exams = _exams;
            examDate = _examDate;
            cardDate = _cardDate;
            distanceToWork = _distanceToWork;
            male = _male;
            canGoWith = _canGoWith == null ? new List<int>() : _canGoWith;
        }

        public override string ToString()
        {
            return 
                "Name: " + name +
                "\nSex: " + (male ? "Male" : "Female") + 
                "\nID: " + id +
                "\nType: " + type +
                "\nExamDate: " + examDate.ToString("yyyy. MM. dd.") +
                "\nCardDate: " + cardDate.ToString("yyyy. MM. dd.") +
                "\nDistanceToWork: " + distanceToWork + " Km" +
                "\nExams: " +
                (exams.HasFlag(ExamType.VED1) ? "1" : "0") +
                (exams.HasFlag(ExamType.VED2) ? "1" : "0") +
                (exams.HasFlag(ExamType.VED3) ? "1" : "0") +
                (exams.HasFlag(ExamType.VED4) ? "1" : "0");
        }

        public void SaveToFile(string directory = "./Employees")
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(directory + "/" + id.ToString().PadLeft(2, '0'), json);
        }
    }

    public class Month
    {
        public DateTime startDate;
        public List<Day> daysOfMonth;

        //statistic read only parameters

        public Month(DateTime _startDate)
        {
            startDate = new DateTime(_startDate.Year, _startDate.Month, 1);
            daysOfMonth = new List<Day>();

            for (int i = 0; i < DateTime.DaysInMonth(startDate.Year, startDate.Month); i++)
                daysOfMonth.Add(new Day(i + 1, this));
        }

        [JsonConstructor]
        public Month(DateTime _startDate, List<Day> _days)
        {
            startDate = _startDate;
            daysOfMonth = _days;
        }
    }

    public class Day
    {
        [JsonIgnore] public Month month;
        public DateTime date;
        public int dayNumber;
        public List<Flight> flightList;
        public List<Shift> shiftList;
        public List<Shift> nonDefaultShifts
        {
            get
            {
                List<Shift> list = new List<Shift>();
                for (int i = 0; i < shiftList.Count; i++)
                    if (i >= PublicParameters.shiftNonDefaultStart)
                        list.Add(shiftList[i]);
                //foreach (Shift s in shiftList)
                //    if (s.requiredPeople >= PublicParameters.employeesPerFlight)
                //        list.Add(s);
                return list;
            }
        }

        //statistic read only parameters

        public Day(int _dayNumber, Month _month)
        {
            month = _month;
            dayNumber = _dayNumber;
            date = new DateTime(month.startDate.Year, month.startDate.Month, _dayNumber);
            
            flightList = new List<Flight>();
            shiftList = new List<Shift>();

            AddDefaultShifts();
        }

        [JsonConstructor]
        public Day(int _dayNumber, DateTime _date, List<Flight> _flightList, List<Shift> _shiftList)
        {
            dayNumber = _dayNumber;
            date = _date;
            flightList = _flightList;
            shiftList = _shiftList;
        }

        public void AddFlight(Flight f)
        {
            flightList.Add(f);
            DateTime start = f.checkInStart;
            DateTime end = new DateTime(f.checkInStart.Year, f.checkInStart.Month, f.checkInStart.Day, f.checkInStart.Hour + 2, f.checkInStart.Minute, f.checkInStart.Second);

            CreateShifts();
        }

        public void AddDefaultShifts()
        {
            /*  0 */shiftList.Add(new Shift(0, 7, ShiftType.Night | ShiftType.Long, this));
            /*  1 */shiftList.Add(new Shift(19, 24, ShiftType.Night | ShiftType.Long, this));
            /*  2 */shiftList.Add(new Shift(7, 19, ShiftType.Day | ShiftType.Long, this));
            
            /*  3 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, true)); //standby
            /*  4 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, false, true)); //pto
            /*  5 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, false, false, true)); //free
            
            /*  6 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, false, true, false, true)); //ordered pto
            /*  7 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, false, true, false, true, true)); //ordered important pto
            
            /*  8 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, false, false, true, true)); //ordered free
            /*  9 */shiftList.Add(new Shift(7, 19, ShiftType.Day, this, -1, false, false, true, true, true)); //ordered important free
            
            /* 10 */shiftList.Add(new Shift(0, 7, ShiftType.Night | ShiftType.Long, this, -1, false, false, false, true)); //ordered night 1
            /* 11 */shiftList.Add(new Shift(19, 24, ShiftType.Night | ShiftType.Long, this, -1, false, false, false, true)); //ordered night 2
        }

        public void CreateShifts()
        {
            CheckFlightsCovered();

            LOOP:
            List<int> shiftToDelete = new List<int>();
            for (int i = PublicParameters.shiftNonDefaultStart + 1; i < shiftList.Count; i++)
            {
                if (shiftList.Count <= PublicParameters.shiftNonDefaultStart) break;
                if (shiftList[i].shiftStart == shiftList[i - 1].shiftEnd)
                {
                    //MessageBox.Show("1");
                    shiftList[i - 1].shiftEnd = shiftList[i].shiftEnd;
                    shiftToDelete.Add(i);
                }
                if (shiftList[i - 1].shiftStart == shiftList[i].shiftEnd)
                {
                    //MessageBox.Show("2");
                    shiftList[i].shiftEnd = shiftList[i - 1].shiftEnd;
                    shiftToDelete.Add(i + 1);
                }

                if (shiftList[i].shiftStart - new TimeSpan(1, 30, 0) == shiftList[i - 1].shiftEnd)
                {
                    //MessageBox.Show("3");
                    //MessageBox.Show(shiftList[i - 1].ToString() + "\n\n" + shiftList[i].ToString());
                    shiftList[i - 1].shiftEnd = shiftList[i].shiftEnd - (shiftList[i].shiftStart - shiftList[i - 1].shiftEnd);
                    shiftToDelete.Add(i);
                }

                if (shiftList[i].shiftStart - new TimeSpan(0, 30, 0) == shiftList[i - 1].shiftEnd)
                {
                    //MessageBox.Show("4");
                    //MessageBox.Show(shiftList[i - 1].ToString() + "\n\n" + shiftList[i].ToString());
                    shiftList[i - 1].shiftEnd = shiftList[i].shiftEnd - (shiftList[i].shiftStart - shiftList[i - 1].shiftEnd) - new TimeSpan(1, 30, 0);
                    shiftToDelete.Add(i);
                }

                if (shiftList[i].shiftStart - new TimeSpan(1, 0, 0) == shiftList[i - 1].shiftEnd)
                {
                    //MessageBox.Show("5");
                    //MessageBox.Show(shiftList[i - 1].ToString() + "\n\n" + shiftList[i].ToString());
                    shiftList[i - 1].shiftEnd = shiftList[i].shiftEnd - (shiftList[i].shiftStart - shiftList[i - 1].shiftEnd) - new TimeSpan(1, 0, 0);
                    shiftToDelete.Add(i);
                }
            }

            CheckFlightsCovered();

            if (shiftList.Count == PublicParameters.shiftNonDefaultStart + 3 && shiftToDelete.Count == 0)
            {
                if (shiftList[PublicParameters.shiftNonDefaultStart].shiftStart.Hour == 4 && shiftList[PublicParameters.shiftNonDefaultStart].shiftEnd.Hour == 8 &&
                    shiftList[PublicParameters.shiftNonDefaultStart + 1].shiftStart.Hour == 10 && shiftList[PublicParameters.shiftNonDefaultStart + 1].shiftEnd.Hour == 14 && shiftList[PublicParameters.shiftNonDefaultStart + 1].shiftEnd.Minute == 30 &&
                    shiftList[PublicParameters.shiftNonDefaultStart + 2].shiftStart.Hour == 17 && shiftList[PublicParameters.shiftNonDefaultStart + 2].shiftEnd.Hour == 21)
                {
                    shiftList.RemoveAt(PublicParameters.shiftNonDefaultStart + 2);
                    shiftList.RemoveAt(PublicParameters.shiftNonDefaultStart + 1);
                    shiftList.RemoveAt(PublicParameters.shiftNonDefaultStart);

                    Shift s4_19 = new Shift(4, 19, ShiftType.Day | ShiftType.Long, this, 5);
                    Shift s4_14 = new Shift(4, 14, ShiftType.Day | ShiftType.Divided, this, 0);
                    Shift s17_21 = new Shift(17, 21, ShiftType.Day | ShiftType.Divided, this, 0);

                    shiftList[PublicParameters.shiftIndexDay] = s4_19;
                    shiftList.Add(s4_14);
                    shiftList.Add(s17_21);

                    //MessageBox.Show("Custom day");
                }
            }

            if (shiftToDelete.Count == 1)
            {
                shiftList.RemoveAt(shiftToDelete[0]);
            } else if (shiftToDelete.Count > 1)
            {
                shiftList.RemoveAt(shiftToDelete[0]);
                shiftToDelete.RemoveAt(0);
                goto LOOP;
            }

            if (shiftList.Count > PublicParameters.shiftNonDefaultStart + 2)
            {
                string s = "";
                for (int i = PublicParameters.shiftNonDefaultStart; i < shiftList.Count; i++)
                    s += shiftList[i].ToString() + "\n\n";
                MessageBox.Show(s);
            }

            if (shiftList.Count == PublicParameters.shiftNonDefaultStart + 2)
            {
                shiftList[PublicParameters.shiftNonDefaultStart].type = shiftList[PublicParameters.shiftNonDefaultStart].type | ShiftType.Divided;
                shiftList[PublicParameters.shiftNonDefaultStart + 1].type = shiftList[PublicParameters.shiftNonDefaultStart + 1].type | ShiftType.Divided;
            }

            CheckFlightsCovered();
        }

        void CheckFlightsCovered() 
        {
            foreach (Flight f in flightList)
            {
                bool isCovered = false;
                foreach (Shift s in shiftList)
                    if (s.CoversFlight(f))
                        isCovered = true;

                if (!isCovered)
                {
                    foreach (Shift s in shiftList)
                    {
                        if (s.FlightStartsMidShift(f) &&
                            s.requiredPeople >= PublicParameters.employeesPerFlight &&
                            !isCovered)
                        {
                            //expand shift
                            while (!s.CoversFlight(f))
                                s.AddTimeAtEnd(new TimeSpan(0, 30, 0));
                            isCovered = true;
                        }
                    }
                }

                if (!isCovered)
                {
                    //add new shift
                    DateTime start2 = f.checkInStart/* - new TimeSpan(0, 20, 0)*/;
                    DateTime shiftStart = new DateTime(start2.Year, start2.Month,
                            start2.Minute == 0 ? start2.Day : (start2.Minute > 30 ? start2.Day : (start2.Hour >= 1 ? start2.Day : (start2.Day - 1))),
                            start2.Minute == 0 ? start2.Hour : (start2.Hour >= 1 ? start2.Hour : (start2.Minute > 30 ? 0 : 23)),
                            start2.Minute >= 30 ? 30 : 0, start2.Second);
                    DateTime shiftEnd = shiftStart + PublicParameters.minShiftTime;
                    Shift newShift = new Shift(shiftStart, shiftEnd, ShiftType.Day);
                    shiftList.Add(newShift);
                    isCovered = true;
                }
            }
        }

        public List<Shift> GetShiftsPerEmployee(Employee e)
        {
            List<Shift> shifts = new List<Shift>();
            foreach (Shift s in shiftList)
                foreach (Employee em in s.employeeList)
                    if (em != null)
                        if (em.id == e.id && em.name == e.name)
                            shifts.Add(s);
            return shifts;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Flight
    {
        public string destination;
        public string airline;
        public DateTime checkInStart;
        public bool isCharter;
        public bool isMonthly;
        public bool isWeekly;

        public Flight(string dest, string airl, DateTime checkIn, bool chrt, bool mnthly, bool wkly)
        {
            destination = dest;
            airline = airl;
            checkInStart = checkIn;
            isCharter = chrt;
            isMonthly = mnthly;
            isWeekly = wkly;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class Shift
    {
        [JsonIgnore] public Day day;
        public DateTime shiftStart;
        public DateTime shiftEnd;
        public ShiftType type;
        public List<Employee> employeeList = new List<Employee>();
        public int requiredPeople;

        public bool isStandby = false;
        public bool isPto = false;
        public bool isFreeDay = false;

        public bool ordered = false;
        public bool important = false;

        public bool sickShift = false;

        public float hours
        {
            get
            {
                if (isPto) return PublicParameters.hoursPTO;
                if (isStandby) return PublicParameters.hoursStandby;
                if (isFreeDay) return PublicParameters.hoursFreeDay;
                if (sickShift) return 0;
                return (float)Math.Round((shiftEnd - shiftStart).TotalHours, 1, MidpointRounding.AwayFromZero);
            }
        }
        public bool isNow
        {
            get
            {
                if (shiftStart < DateTime.Now && shiftEnd > DateTime.Now) return true;
                else return false;
            }
        }

        public Shift(float _shiftStart, float _shiftEnd, ShiftType _type, Day _day, int _reqPeople = -1, bool _standby = false, bool _pto = false, bool _freeDay = false, bool _ordered = false, bool _important = false)
        {
            day = _day;
            type = _type;
            requiredPeople = _reqPeople;
            shiftStart = new DateTime(day.date.Year, day.date.Month, day.date.Day, (int)(_shiftStart - (_shiftStart % 1f)), (int)((_shiftStart % 1) * 60f), 0);
            shiftEnd = new DateTime(day.date.Year, day.date.Month, day.date.Day, (int)(_shiftEnd == 24 ? 23 : (_shiftEnd - (_shiftEnd % 1f))), (int)(_shiftEnd == 24 ? 59 : ((_shiftEnd % 1) * 60f)), 0);

            if (requiredPeople <= 0)
            {
                if (type.HasFlag(ShiftType.Night) && type.HasFlag(ShiftType.Long)) requiredPeople = PublicParameters.employeesPerNightShift;
                else if (type.HasFlag(ShiftType.Day) && type.HasFlag(ShiftType.Long)) requiredPeople = PublicParameters.employeesPerDayShift;
                else if (_standby) requiredPeople = PublicParameters.employeesPerStandby;
                else requiredPeople = PublicParameters.employeesPerFlight;
            }

            isStandby = _standby;
            isPto = _pto;
            isFreeDay = _freeDay;

            ordered = _ordered;
            important = _important;

            if (isPto || isFreeDay || sickShift) requiredPeople = 0;
        }

        public Shift(DateTime _shiftStart, DateTime _shiftEnd, ShiftType _type, Day _day, int _reqPeople = -1, bool _standby = false, bool _pto = false, bool _freeDay = false, bool _ordered = false, bool _important = false)
        {
            day = _day;
            type = _type;
            requiredPeople = _reqPeople;
            shiftStart = _shiftStart;
            shiftEnd = _shiftEnd;

            if (requiredPeople <= 0)
            {
                if (type.HasFlag(ShiftType.Night) && type.HasFlag(ShiftType.Long)) requiredPeople = PublicParameters.employeesPerNightShift;
                else if (type.HasFlag(ShiftType.Day) && type.HasFlag(ShiftType.Long)) requiredPeople = PublicParameters.employeesPerDayShift;
                else if (_standby) requiredPeople = PublicParameters.employeesPerStandby;
                else requiredPeople = PublicParameters.employeesPerFlight;
            }

            isStandby = _standby;
            isPto = _pto;
            isFreeDay = _freeDay;

            ordered = _ordered;
            important = _important;

            if (isPto || isFreeDay || sickShift) requiredPeople = 0;
        }

        [JsonConstructor]
        public Shift(DateTime _shiftStart, DateTime _shiftEnd, ShiftType _type, int _reqPeople = -1, bool _standby = false, bool _pto = false, bool _freeDay = false, bool _ordered = false, bool _important = false)
        {
            type = _type;
            requiredPeople = _reqPeople;
            shiftStart = _shiftStart;
            shiftEnd = _shiftEnd;

            if (requiredPeople <= 0)
            {
                if (type.HasFlag(ShiftType.Night) && type.HasFlag(ShiftType.Long)) requiredPeople = PublicParameters.employeesPerNightShift;
                else if (type.HasFlag(ShiftType.Day) && type.HasFlag(ShiftType.Long)) requiredPeople = PublicParameters.employeesPerDayShift;
                else if (_standby) requiredPeople = PublicParameters.employeesPerStandby;
                else requiredPeople = PublicParameters.employeesPerFlight;
            }

            isStandby = _standby;
            isPto = _pto;
            isFreeDay = _freeDay;

            ordered = _ordered;
            important = _important;

            if (isPto || isFreeDay || sickShift) requiredPeople = 0;
        }

        public bool CoversFlight(Flight f)
        {
            if (f.checkInStart /*- new TimeSpan(0, 20, 0)*/ >= shiftStart &&
                f.checkInStart + new TimeSpan(1, 50, 0) <= shiftEnd &&
                requiredPeople >= PublicParameters.employeesPerFlight) return true;
            return false;
        }

        public bool FlightStartsMidShift(Flight f)
        {
            if (f.checkInStart > shiftStart && f.checkInStart <= shiftEnd) return true;
            return false;
        }

        public bool ContainsEmployee(Employee e)
        {
            foreach (Employee em in employeeList)
                if (em != null)
                    if (em.id == e.id)
                        return true;
            return false;
        }

        public void AddEmployee(Employee e)
        {
            if (!ContainsEmployee(e))
                employeeList.Add(e);
        }

        public void RemoveEmployee(Employee e)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                if (e.id == employeeList[i].id && e.name == employeeList[i].name)
                {
                    employeeList.RemoveAt(i);
                    return;
                }
            }
        }

        public void AddTimeAtEnd(TimeSpan time)
        {
            shiftEnd += time;
        }

        public void RemoveTimeFromEnd(TimeSpan time)
        {
            if (shiftEnd - shiftStart > PublicParameters.minShiftTime) shiftEnd -= time;
            if (shiftEnd - shiftStart < PublicParameters.minShiftTime) shiftEnd = shiftStart + PublicParameters.minShiftTime;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class PaidTimeOff
    {
        public Employee employee;
        public DateTime date;
    }

    public class RequestedFreeDay
    {
        public Employee employee;
        public DateTime date;
        public bool isImportant;
    }
}
