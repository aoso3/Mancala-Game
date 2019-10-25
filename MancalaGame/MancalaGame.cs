using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaGame
{
    
    class MancalaGame
    {
        public int currentPlayer = 0;
        public MancalaBoard board;
        public Player[] players;
        int level ;
            
        bool towPlayer;

        public MancalaGame(String name0, String name1, int level, int stonesNumber, bool towPlayer)
        {
            board  =  new MancalaBoard();
            board.stonesNumber = stonesNumber;
            board.setUpForPlay();
            players  =  new Player[2];
            players[0]  =  new Player(name0,  0);
            players[1] = new Player(name1, 1);

            currentPlayer  =  0;

            this.towPlayer = towPlayer;
            this.level = level;

        }

       public bool humanPlay(int currentPlayer, int pitNum,bool towPlayer)
       {
           return board.doTheMove(currentPlayer, pitNum, towPlayer);
        }  
    
    
         public bool computerPlay() {
        
            if(!board.gameOver()){
                List<MancalaBoard> next = board.getNextPossibleBoard(1);
                int max = int.MinValue;
                foreach(MancalaBoard b in next){
                    int max_min = minMax(b, this.level);
                    if(max < max_min){
                        max = max_min;
                        board = b;
                    }
                }
            }
            return board.repeated;
        }
    
    
    
     
        public int minMax(MancalaBoard board,int level){
            if(board.gameOver() || level == 0)
                return board.evaluate(1);
            else {
                List<MancalaBoard> next = board.getNextPossibleBoard(0);
                int min = int.MaxValue;
                int max = int.MinValue;

                foreach(MancalaBoard b in next){
    //                if (b.repeated)
    //                    min = Integer.min(min, maxMin(b, level));
    //                
    //                else 
                        min = Math.Min(min, maxMin(b, level-1));
                    
                }
                return min;
            }
        }
    
         public int maxMin(MancalaBoard board,int level){
            if(board.gameOver()|| level == 0)
                return board.evaluate(1);
            else {
                List<MancalaBoard> next = board.getNextPossibleBoard(1);
                int max = int.MinValue;
                foreach(MancalaBoard b in next){
    //                if (b.repeated)
    //                    max = Integer.max(max, minMax(b, level));
    //                else                
                        max = Math.Max(max, minMax(b, level-1));
                }
                return max;
            }
        }
    
     
     
        public  void play()  {
            //displayBoard();
            //while  (!board.gameOver())  {
            //    bool goAgain = false;
            //    if (currentPlayer==0)
            //    {
            //        goAgain = humanPlay();
                
            //    }
            //    else 
            //        goAgain = computerPlay();
            
            
            //    displayBoard();
            //    if  (!goAgain)         // If the current player does not go again,
            //        if  (currentPlayer  ==  0)     // switch to the other player.
            //                currentPlayer  =  1;
            //        else
            //                currentPlayer  =  0;
            //    else
            //Console.WriteLine("Player  "  +  
            //                 currentPlayer  + "  goes  again");			
            //}
            //    board.emptyStonesIntoMancalas();    // Game is over                                        //      board empty stones,
            //    displayBoard();                     //      display final board,
            //    if  (board.stonesInMancala(0)  > 
            //                    board.stonesInMancala(1))	//      and announce winner.
            //            Console.WriteLine(players[0].getName()+"  wins");
            //    else if (board.stonesInMancala(0)  <  
            //                     board.stonesInMancala(1))
            //            Console.WriteLine(players[1].getName()+"  wins");
            //    else
            //            Console.WriteLine("Tie");
	    }

        //private  void  displayBoard()  {
        //    String  mancalaLineFiller  =  "";    // Used to properly 
        //                                         //      space the 
        //                                         //      mancala line
        //    Console.WriteLine("-----------------------");
        //                                         // Top border
        //    // Player 1's pit line
        //    Console.Write("      ");   // space  past  mancala  entry
        //    for  (int  i  =  1;  i  <=  board.playingPits;  i++)  {
        //       Console.Write(board.stonesInPit(1,  i)  +  
        //                  "    ");    // Print pit's contents and pit spacing.
        //       mancalaLineFiller  +=  "     ";   // Build mancala 
        //                                         //      spacing string.
        //    }
        //    displayPlayer(1);    // Player 1 info
        //    // Mancala line
        //    Console.Write(board.stonesInMancala(1) +  
        //               "    ");    // Print player 1's manacala and spacing.
        //    Console.Write(mancalaLineFiller);   // Space past pit 
        //                                           //      entries.
        //    Console.Write(board.stonesInMancala(0));
        //                                   // Print player 0's mancala.
        //    // Player 0's pit line
        //    Console.Write("      ");
        //    for  (int  i  =  board.playingPits;  
        //         i  >=  1;  i--)
        //        Console.Write(board.stonesInPit(0, i) +  
        //                   "    ");    // Print pit's contents and spacing.
        //    displayPlayer(0);    // Player  0  info
        //    Console.Write("-----------------------"); 
        //                                           // Bottom border
        //}

        private  void  displayPlayer(int  playerNum)  {
            // Turn indicator
            if  (currentPlayer  ==  playerNum)			// If it this player's turn,
                Console.Write("            -->");	//      display turn 
													    //      indicator,
            else
                Console.Write("                  "); //      or display equal number of spaces otherwise.

		    // player info
            Console.WriteLine("Player {0} "  +  playerNum  +  
				    "( {1} "  +
				    players[playerNum].getName()  +  ")");
        }







        }
}
