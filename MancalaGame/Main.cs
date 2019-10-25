using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;

namespace MancalaGame
{


    public partial class Main : Telerik.WinControls.UI.RadForm
    {

        int level = 5;
        int stonesNumber = 4;
        private bool towPlayer = false;
        MancalaGame game ;//new MancalaGame("Player1", "Player2");

       

        public Main()
        {
            InitializeComponent();
            radLabel2.Text = "";
            l0.Text = "";
            l1.Text = "";
            l2.Text = "";
            l3.Text = "";
            l4.Text = "";
            l5.Text = "";
            l6.Text = "";
            l7.Text = "";
            l8.Text = "";
            l9.Text = "";
            l10.Text = "";
            l11.Text = "";
            l12.Text = "";
            l13.Text = "";
            radButton3.Hide();
        }

        private void radLabel2_Click(object sender, EventArgs e)
        {

        }

       

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void radLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            int index = 2;
            play(0, index);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            int index = 1;
            play(0, index);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int index = 3;
            play(0, index);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            int index = 4;
            play(0, index);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int index = 5;

            play(0, index);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int index = 6;
            play(0, index);

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            int index = 13;
                play(1, index);

            

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            int index = 12;
            play(1, index);

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int index = 9;
            play(1, index);

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            int index = 10;
            play(1, index);

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            int index = 11;
            play(1, index);

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int index = 8;
            play(1, index);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.stonesNumber = Convert.ToInt32( radTextBox1.Text);
            this.towPlayer = radioButton6.Checked;
            if (!towPlayer)
            {
                if (radioButton1.Checked)
                    this.level = 5;
                if (radioButton2.Checked)
                    this.level = 7;
                if (radioButton3.Checked)
                    this.level = 10;
                if (radioButton4.Checked)
                    this.level = 11;
            }
           

            if (towPlayer)
                game = new MancalaGame("Player 1","Player 2", 0, stonesNumber, towPlayer);
            else
                game = new MancalaGame("Player 1", "Coomputer", level, stonesNumber, towPlayer);

            updateBoard();
            radLabel2.Text = game.players[game.currentPlayer].getName();

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(radTextBox1.Text, out stonesNumber))
            {
                return;
            }

            
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
        }


        Thread t;


        public void play(int playerNum ,int index)
        {

            if (!game.board.gameOver())
            {
                if ((!towPlayer && playerNum == 1&& index!=-1 ))
                {
                    return;
                }
                if (game.currentPlayer != playerNum) {
                    MessageBox.Show("Is not your turn");
                    return;
                }
                if (game.board.stonesInPit(playerNum, index, towPlayer) == 0 && ((game.currentPlayer == 0 && !towPlayer )||(towPlayer)))
                    return;

                bool goAgain = false;
                radLabel2.Text = game.players[game.currentPlayer].getName();

                if (game.currentPlayer == playerNum)
                {

                    if (game.currentPlayer == 0)
                        goAgain = game.humanPlay(0,index,false);
                    else if (game.currentPlayer == 1 && towPlayer)
                        goAgain = game.humanPlay(1,index,true);
                    else
                    {
                        radLabel2.Text = game.players[game.currentPlayer].getName();
                           goAgain = game.computerPlay();
                    }
                    
                }

                if (!goAgain)
                {        // If the current player does not go again,

                    if (game.currentPlayer == 0)     // switch to the other player.
                    {
                        if (!towPlayer)
                            radButton3.Show();
                        game.currentPlayer = 1;
                    }
                    else
                    {
                        game.currentPlayer = 0;
                        radButton3.Hide();

                    }
                }


                if (game.board.gameOver())
                {
                    finishPlay();
                    return;
                }
                //if (game.currentPlayer == 1 && !towPlayer)
                //{
                //    radButton3.Show();
                //}


                radLabel2.Text = game.players[game.currentPlayer].getName();

                updateBoard();
            }
            else
            {
                finishPlay();
            }


        }




        public void finishPlay()
        {

            game.board.emptyStonesIntoMancalas();    // Game is over 



            if (game.board.stonesInMancala(0) > game.board.stonesInMancala(1))
                MessageBox.Show(game.players[0].getName() + "  wins");
            else if (game.board.stonesInMancala(0) < game.board.stonesInMancala(1))
                MessageBox.Show(game.players[1].getName() + "  wins");
            else
                MessageBox.Show("Tie"); 
            updateBoard();
        }
        private void updateBoard() { 

          
            Pit  [] pits = game.board.pits;

                l0.Text = pits[0].stones.ToString();
                l1.Text = pits[1].stones.ToString();
                l2.Text = pits[2].stones.ToString();
                l3.Text = pits[3].stones.ToString();
                l4.Text = pits[4].stones.ToString();
                l5.Text = pits[5].stones.ToString();
                l6.Text = pits[6].stones.ToString();
                l7.Text = pits[7].stones.ToString();
                l8.Text = pits[8].stones.ToString();
                l9.Text = pits[9].stones.ToString();
                l10.Text = pits[10].stones.ToString();
                l11.Text = pits[11].stones.ToString();
                l12.Text = pits[12].stones.ToString();
                l13.Text = pits[13].stones.ToString();
           
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

            radioButton1.Show();
            radioButton2.Show();
            radioButton3.Show();
            radioButton4.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            play(1, -1);

            //radButton3.Hide();
        }
   
    }
}
