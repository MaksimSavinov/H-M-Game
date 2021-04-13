using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW2
{
    public partial class Win : Form
    {
        public Win()
        {
            InitializeComponent();
        }
        /// <summary>
        /// делаем форму на весь экран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Win_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// для выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// для запуска новой игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Heroes_of_Might_and_Magic Again = new Heroes_of_Might_and_Magic();
            Again.Show();
        }
    }
}
