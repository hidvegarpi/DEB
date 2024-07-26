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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        

        List<Control> employeeControlList = new List<Control>();
        List<Employee> _employeeList = new List<Employee>();
        public List<Employee> employeeList
        {
            get
            {
                List<Employee> employees = new List<Employee>();
                for (int i = 0; i < _employeeList.Count; i++)
                    if (((CheckBox)employeeControlList[i]).Checked)
                        employees.Add(_employeeList[i]);
                return employees;
            }
            set
            {
                _employeeList = value;
                foreach (Employee e in value)
                {
                    CheckBox c = new CheckBox();
                    c.Text = e.name;
                    employeeControlList.Add(c);
                    flowLayoutPanel1.Controls.Add(c);
                }
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
