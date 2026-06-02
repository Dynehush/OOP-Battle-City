using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Battle_City.Core.Entities.Actor;
using System.Runtime.CompilerServices;
using Battle_City.Core.Services;

namespace Battle_City
{
    public partial class GameForm : Form
    {
        public static GameForm Instance { get; private set; }
        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            Game.StartGame();
            KeyPreview = true;

            KeyDown += FormInput.GameForm_KeyDown;
            FormClosing += GameForm_FormClosing;

            Instance = this;
        }
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
