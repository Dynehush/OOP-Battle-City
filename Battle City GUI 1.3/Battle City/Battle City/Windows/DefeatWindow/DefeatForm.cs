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
    public partial class DefeatForm : Form
    {
        public DefeatForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Game.Level = 1;
            Game.Restart();
            Hide();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Game.Level = 1;
            MainMenu menu = new MainMenu();
            menu.Show();
            Hide();
        }

        private void LoadButton_Click_1(object sender, EventArgs e)
        {
            Game.GameState = Game.State.Load;
            Game.Load();
            Hide();
        }

        private void DefeatForm_Load(object sender, EventArgs e)
        {
            FormClosing += DefeatForm_FormClosing;
        }
        private void DefeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
