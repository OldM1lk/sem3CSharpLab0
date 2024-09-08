using System;
using System.Windows.Forms;

namespace Lab0
{
    public partial class Form1 : Form
    {
        public double v, t, L;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            v = Convert.ToDouble(textBox1.Text);
            t = Convert.ToDouble(textBox2.Text);
            L = Convert.ToDouble(textBox3.Text);

            MessageBox.Show("Скорость катера равна " + (L / t + v) + " м/с", "Результат", MessageBoxButtons.OK);
        }
    }
}
