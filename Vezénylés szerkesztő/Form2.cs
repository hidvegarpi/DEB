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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string name = "NaN";
        public int id = -1;
        public EmployeeType type = EmployeeType.Default;
        public DateTime examDate;
        public DateTime cardDate;
        public ExamType exams;
        public float distanceToWork = 0;
        public bool male = true;
        public List<int> canGoWith = new List<int>();

        public int ID
        {
            set
            {
                id = value;
                numericUpDown1.Value = value;
            }
        }

        public Employee employee
        {
            set
            {
                textBox1.Text = value.name;
                numericUpDown1.Value = value.id;
                comboBox1.SelectedItem = value.type.ToString();
                dateTimePicker1.Value = value.examDate;
                dateTimePicker2.Value = value.cardDate;
                textBox2.Text = value.distanceToWork.ToString();

                if (value.male)
                {
                    radioButton2.Checked = false;
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                    radioButton1.Checked = false;
                }

                foreach (int i in value.canGoWith)
                    foreach (Employee e in ((Form1)Owner).employeeList)
                        if (i == e.id)
                            richTextBox1.Text += e.name + "\n";

                checkBox1.Checked = value.exams.HasFlag(ExamType.VED1);
                checkBox2.Checked = value.exams.HasFlag(ExamType.VED2);
                checkBox3.Checked = value.exams.HasFlag(ExamType.VED3);
                checkBox4.Checked = value.exams.HasFlag(ExamType.VED4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exams = checkBox1.Checked ? exams | ExamType.VED1 : exams | 0;
            exams = checkBox2.Checked ? exams | ExamType.VED2 : exams | 0;
            exams = checkBox3.Checked ? exams | ExamType.VED3 : exams | 0;
            exams = checkBox4.Checked ? exams | ExamType.VED4 : exams | 0;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) => name = textBox1.Text;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) => id = (int)numericUpDown1.Value;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => type =
                comboBox1.SelectedItem.ToString() == "Munkáltatott" ? EmployeeType.Default :
                comboBox1.SelectedItem.ToString() == "Supervisor" ? EmployeeType.Supervisor : EmployeeType.Default;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) => examDate = dateTimePicker1.Value;

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) => cardDate = dateTimePicker2.Value;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text)) distanceToWork = float.Parse(textBox2.Text.Replace('.', ','));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) => male = radioButton1.Checked;

        private void radioButton2_CheckedChanged(object sender, EventArgs e) => male = radioButton1.Checked;

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Owner = this;
            form.employeeList = ((Form1)Owner).employeeList;
            form.Show();
            form.FormClosing += CanGoWithFormClosing;
        }

        void CanGoWithFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Form5 form = (Form5)sender;
            if (form.DialogResult == DialogResult.OK)
            {
                richTextBox1.Text = "";
                foreach (Employee em in form.employeeList)
                {
                    canGoWith.Add(em.id);
                    richTextBox1.Text += em.name + "\n";

                    foreach (Employee emp in ((Form1)Owner).employeeList)
                    {
                        if (emp.id == em.id)
                        {
                            emp.canGoWith.Add(id);
                            emp.SaveToFile();
                        }
                    }
                }
            }
        }
    }
}
