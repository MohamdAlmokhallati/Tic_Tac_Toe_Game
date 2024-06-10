using System;
using System.Windows.Forms;

namespace Tic_Tac_Toc_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameScreen GameForm = new GameScreen(GameScreen.enGameMode.e2Plyers);
            GameForm.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GameScreen GameForm = new GameScreen(GameScreen.enGameMode.evsComputer);
            GameForm.ShowDialog();
        }
    }
}
