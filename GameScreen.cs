using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;






namespace Tic_Tac_Toc_Game
{
    public partial class GameScreen : Form
    {


        //true = player 1 x
        //fals = player 2 o
        bool PlyerTurn = true;

        public enum enGameMode
        {
            evsComputer = 1, e2Plyers =2
        }
        private enGameMode gameMode = enGameMode.evsComputer ;


        public GameScreen(enGameMode GameMode )
        {

            gameMode = GameMode;
            PlyerTurn = true;
            InitializeComponent();
        }


        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            Color White = Color.White;
            Pen myPen = new Pen(White,10);

            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;


            e.Graphics.DrawLine(myPen, 130, 10, 130, 350);
            e.Graphics.DrawLine(myPen, 270, 10, 270, 350);
            e.Graphics.DrawLine(myPen, 10, 100, 400, 100);
            e.Graphics.DrawLine(myPen, 10, 240, 400, 240);

        }


        private bool Draw()
        {
            if (lBox1.Text != "" && lBox2.Text != "" && lBox3.Text != "" &&
                lBox4.Text != "" && lBox5.Text != "" && lBox6.Text != "" &&
                lBox7.Text != "" && lBox8.Text != "" && lBox9.Text != "" )
            {
                return true;
            }
            return false;
        }

        private void WinEffect(Label button1, Label button2, Label button3)
        {
            Color myColor = Color.FromArgb(178, 171, 174);
            button1.BackColor = myColor;
            button2.BackColor = myColor;
            button3.BackColor = myColor;
        }

        private bool Check3Buttons(Label button1, Label button2, Label button3) {

            if (button1.Text == "" && button2.Text == "" && button3.Text == "")
                return false;

            if(button1.Text == button2.Text && button2.Text == button3.Text)
            {
                WinEffect(button1, button2, button3);
                return true;
            }
            return false;
        }




        private bool CheckAllButtons()
        {
            if(Check3Buttons(lBox1, lBox2, lBox3))
            {
                return true;
            }

            if (Check3Buttons(lBox4, lBox5, lBox6))
            {
                return true;
            }

            if (Check3Buttons(lBox7, lBox8, lBox9))
            {
                return true;
            }

            if (Check3Buttons(lBox1, lBox4, lBox7))
            {
                return true;
            }


            if (Check3Buttons(lBox2, lBox5, lBox8))
            {
                return true;
            }

            if (Check3Buttons(lBox3, lBox6, lBox9))
            {
                return true;
            }

            if (Check3Buttons(lBox1, lBox5, lBox9))
            {
                return true;
            }

            if (Check3Buttons(lBox3, lBox5, lBox7))
            {
                return true;
            }

            return false;


        }




        private bool CheckWiner()
        {
            if (CheckAllButtons())
            {
                return true;
            }


            return false;
        }

        private void AllBoxesUnDesabled(bool NewMode)
        {
            lBox1.Enabled = NewMode;
            lBox2.Enabled = NewMode;
            lBox3.Enabled = NewMode;
            lBox4.Enabled = NewMode;
            lBox5.Enabled = NewMode;
            lBox6.Enabled = NewMode;
            lBox7.Enabled = NewMode;
            lBox8.Enabled = NewMode;
            lBox9.Enabled = NewMode;

        }

        private void AllBoxesToNormalColor()
        {
            Color MyColor = Color.FromArgb(118, 171, 174);

            lBox1.BackColor = MyColor;
            lBox2.BackColor = MyColor;
            lBox3.BackColor = MyColor;
            lBox4.BackColor = MyColor;
            lBox5.BackColor = MyColor;
            lBox6.BackColor = MyColor;
            lBox7.BackColor = MyColor;
            lBox8.BackColor = MyColor;
            lBox9.BackColor = MyColor;
        }


        private void WinMessage(string message,bool draw = false)
        {
            string WinnerName = draw ? "no one": PlyerTurn ? "O" : "X";
            if (MessageBox.Show((WinnerName + " Won" + "\nDo You Want to play again"),message,MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetGame();
            }
            else
            {
                this.Close();
            }

        }

        private void PrintWiner()
        {
            if(CheckWiner())
            {
                lWiner.Text = PlyerTurn ? "O Won" : "X Won";
                AllBoxesUnDesabled(false);
                WinMessage("Game Over");
            }

            if(Draw())
            {
                AllBoxesUnDesabled(false);
                WinMessage("Game Over", true);

            }

        }

        private void PrintPlayerTurn()
        {
            if(PlyerTurn)
            {
                lPlayerTurn.Text = "X" + " Turn";    
            }
            else
            {
                lPlayerTurn.Text = "O"+ " Turn";

            }
            PrintWiner();
        }

        private void Put_XO_OnBox(object sender)
        {
            if (((Label)sender).Text != "")
            {
                return;
            }
            if (PlyerTurn)
            {
                ((Label)sender).Text = "X";
            }
            else
            {
                ((Label)sender).Text = "O";
            }
            PlyerTurn = !PlyerTurn;
            PrintPlayerTurn();
        }

        private List <Label> getAllAvailableMovements()
        {
            List<Label> Buttons = new List<Label>();

            if(lBox1.Text == "") Buttons.Add(lBox1);
            if(lBox2.Text == "") Buttons.Add(lBox2);
            if(lBox3.Text == "") Buttons.Add(lBox3);
            if(lBox4.Text == "") Buttons.Add(lBox4);
            if(lBox5.Text == "") Buttons.Add(lBox5);
            if(lBox6.Text == "") Buttons.Add(lBox6);
            if(lBox7.Text == "") Buttons.Add(lBox7);
            if(lBox8.Text == "") Buttons.Add(lBox8);
            if(lBox9.Text == "") Buttons.Add(lBox9);

            return Buttons;
        }

        private async void Computermove()
        {
            if (PlyerTurn) return;
            await Task.Delay(200);
            Random rnd = new Random();
            List<Label> Buttons = getAllAvailableMovements();

            Put_XO_OnBox(Buttons[rnd.Next(0, Buttons.Count)]);

        }


        private void label_Click(object sender, EventArgs e)
        {
            Put_XO_OnBox(sender);

            if(gameMode == enGameMode.evsComputer )
            {
                
                Computermove();
            }
        }


        private void ResetGame()
        {
            lPlayerTurn.Text = "Player Turn";
            PlyerTurn = true;
            lWiner.Text = "";
            lBox1.Text = "";
            lBox2.Text = "";
            lBox3.Text = "";
            lBox4.Text = "";
            lBox5.Text = "";
            lBox6.Text = "";
            lBox7.Text = "";
            lBox8.Text = "";
            lBox9.Text = "";
            AllBoxesUnDesabled(true);
            AllBoxesToNormalColor();

        }

        private void btnResetGame_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}