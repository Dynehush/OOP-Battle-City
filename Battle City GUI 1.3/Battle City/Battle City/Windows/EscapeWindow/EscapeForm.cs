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

namespace Battle_City.Windows
{
    public partial class EscapeForm : Form
    {
        private bool _isButtonPressed = false;
        public EscapeForm()
        {
            InitializeComponent();
        }
        private void EscapeForm_Load(object sender, EventArgs e)
        {
            FormClosing += EscapeForm_FormClosing;
        }

        private void EscapeForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isButtonPressed)
            {
                e.Cancel = true;
                Hide();
                return;
            } else
            {
                Game.ReturnToGame();
                Hide();
            }
        }

        private void DenyButton_Click(object sender, EventArgs e)
        {
            _isButtonPressed = true;
            Game.ReturnToGame();
            Hide();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            _isButtonPressed = true;
            Game.Reset();
            GameForm.Instance.Hide();

            var menu = new MainMenu();
            menu.Show();
            Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
