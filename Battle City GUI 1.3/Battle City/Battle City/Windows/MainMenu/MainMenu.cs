using static Battle_City.Core.Entities.Actor;
using Battle_City.Core.Services;

namespace Battle_City
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormClosing += MainMenu_FormClosing;
        }
        private void Start_Click(object sender, EventArgs e)
        {
            var game = new GameForm();
            game.Show();
            Hide();
        }
        private void Load_Click(object sender, EventArgs e)
        {
            Game.GameState = Game.State.Load;
            Game.Load();
            Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Instructions_Click(object sender, EventArgs e)
        {
            var instructions = new InstructionsForm();
            instructions.Show();
            Hide();
        }
        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
