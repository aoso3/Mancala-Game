using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaGame
{
    class MancalaBoard
    {
        public int lastMove;
        public bool repeated;
        public Pit[] pits;
        public int playingPits = 6;
        public int totalPits = 2 * (/*playingPits*/ 6 + 1);
        public int stonesNumber;
        public MancalaBoard()
        {
            pits = new Pit[totalPits];
            for (int pitNum = 0; pitNum < totalPits; pitNum++)
                pits[pitNum] = new Pit();
        }

        public void setUpForPlay()
        {
            for (int pitNum = 0; pitNum < totalPits;
                  pitNum++)
                if (!isAMancala(pitNum))
                    pits[pitNum].addStones(stonesNumber);
        }

        public int stonesInMancala(int playerNum)
        {
            return pits[getMancala(playerNum)].getStones();
        }

        public int stonesInPit(int playerNum, int pitNum, bool towPlayer)
        {
            return pits[getPitNum(playerNum, pitNum, towPlayer)].getStones();
        }

        private int getPitNum(int playerNum, int pitNum, bool towPlayer)
        {
            if (towPlayer) playerNum = 0;
            return playerNum * (playingPits + 1) + pitNum;
        }

        private int getMancala(int playerNum)
        {
            return playerNum * (playingPits + 1);
        }

        private bool isAMancala(int pitNum)
        {
            return pitNum % (playingPits + 1) == 0;
        }

        public MancalaBoard makeACopy()
        {
            MancalaBoard newBoard = new MancalaBoard();
            for (int pitNum = 0; pitNum < totalPits;
                  pitNum++)
                newBoard.pits[pitNum].addStones(this.
                                      pits[pitNum].getStones());
            return newBoard;
        }

        public bool doTheMove(int currentPlayerNum, int chosenPitNum, bool towPlayer)
        {
            int pitNum = getPitNum(currentPlayerNum, chosenPitNum, towPlayer);
            int stones = pits[pitNum].removeStones();
            while (stones != 0)
            {
                pitNum--;
                if (pitNum < 0)
                    pitNum = totalPits - 1;
                if (pitNum !=
                getMancala(otherPlayerNum(currentPlayerNum)))
                {
                    pits[pitNum].addStones(1);
                    stones--;
                }
            }
            if (pitNum == getMancala(currentPlayerNum))
                return true;
            if (ownerOf(pitNum) == currentPlayerNum &&
                   pits[pitNum].getStones() == 1 && pits[oppositePitNum(pitNum)].stones > 0)
            {
                stones = pits[oppositePitNum(pitNum)].removeStones();
                stones += pits[pitNum].removeStones();
                pits[getMancala(currentPlayerNum)].addStones(stones);

            }
            return false;
        }

        private int ownerOf(int pitNum)
        {
            return pitNum / (playingPits + 1);
        }

        private int oppositePitNum(int pitNum)
        {
            return totalPits - pitNum;
        }

        private int otherPlayerNum(int playerNum)
        {
            if (playerNum == 0)
                return 1;
            else
                return 0;
        }

        public bool gameOver()
        {
            for (int player = 0; player < 2; player++)
            {
                int stones = 0;
                for (int pitNum = 1; pitNum <= playingPits; pitNum++)
                    stones += pits[getPitNum(player, pitNum, false)].getStones();
                if (stones == 0)
                    return true;
            }
            return false;
        }

        public void emptyStonesIntoMancalas()
        {
            for (int player = 0; player < 2; player++)
                for (int pitNum = 0; pitNum <= playingPits; pitNum++)
                {
                    int stones = pits[getPitNum(player,
                                      pitNum, false)].removeStones();
                    pits[getMancala(player)].
                    addStones(stones);
                }
        }

        public List<MancalaBoard> getNextPossibleBoard(int playerNum)
        {
            List<MancalaBoard> nextBoards = new List<MancalaBoard>();
            int pitNum = playerNum * (playingPits + 1) + 1;
            for (int i = 0; i < playingPits; i++)
            {
                int currentPit = i + pitNum;
                if (pits[currentPit].stones > 0)
                {
                    MancalaBoard board = this.makeACopy();
                    board.lastMove = currentPit;
                    board.repeated = board.doTheMove(playerNum, i + 1, false);
                    nextBoards.Add(board);
                }
            }
            return nextBoards;
        }


        public int evaluate(int playerNum)
        {
            return this.stonesInMancala(playerNum) - this.stonesInMancala(otherPlayerNum(playerNum));
        }

    }
}
