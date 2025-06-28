using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Battle_City
{
    public partial class InstructionsForm : Form
    {
        public InstructionsForm()
        {
            InitializeComponent();
        }

        private void InstructionsForm_KeyDown(object sender, EventArgs e)
        {
            var menu = new MainMenu();
            menu.Show();
            Hide();
        }
        private void InstructionsForm_Load(object sender, EventArgs e)
        {
            InstructionsText.ReadOnly = true;
            InstructionsText.TabStop = false;
            KeyPreview = true;

            InstructionsText.KeyDown += InstructionsText_KeyDown;
            InstructionsText.MouseDown += InstructionsText_MouseDown;
            InstructionsText.MouseMove += InstructionsText_MouseMove;
            InstructionsText.Enter += InstructionsText_Enter;

            KeyDown += InstructionsForm_KeyDown;
            FormClosing += InstructionsForm_FormClosing;
        }
        private void InstructionsText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.KeyCode == Keys.Insert || e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void InstructionsText_MouseDown(object sender, MouseEventArgs e)
        {
            InstructionsText.SelectionLength = 0;
            InstructionsText.SelectionStart = 0;
        }

        private void InstructionsText_MouseMove(object sender, MouseEventArgs e)
        {
            InstructionsText.SelectionLength = 0;
        }

        private void InstructionsText_Enter(object sender, EventArgs e)
        {
            ActiveControl = null;
        }
        private void InstructionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
