using Newtonsoft.Json.Linq;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public Action<DateTime, Form4> onDateSelected;
        public DateTime sickDate;

        public List<Shift> shiftList = new List<Shift>();
        List<Employee> _employeeList = new List<Employee>();
        public List<Employee> employeeList
        {
            get
            {
                return _employeeList;
            }
            set
            {
                _employeeList = value;
                comboBox1.Items.Clear();
                foreach (Employee e in value)
                    comboBox1.Items.Add(e.name);
                if (value.Count > 0) comboBox1.SelectedIndex = 0;
                else comboBox1.SelectedItem = "";
            }
        }
        Employee _employeeData;
        public Employee employeeData
        {
            get
            {
                return _employeeData;
            }
            set
            {
                _employeeData = value;
                //label1.Text = label1.Text.Replace("#EMPLOYEE", value.id + "  -  " + value.name);
                label3.Text = /*employeeData.id + "  -  " + */employeeData.name;
            }
        }
        public Employee substituteEmployee
        {
            get
            {
                return employeeList[comboBox1.SelectedIndex];
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        public void SetDate(DateTime date) => dateTimePicker1.Value = date;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            onDateSelected(dateTimePicker1.Value, this);
            sickDate = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sickDate = dateTimePicker1.Value;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
