using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Battle_City.Core.Services;

namespace Battle_City
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }
        private void WinForm_Load(object sender, EventArgs e)
        {
            FormClosing += WinForm_FormClosing;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Game.Level = 1;
            Game.Restart();
            Hide();
        }
        private void WinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
