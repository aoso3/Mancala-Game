using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaGame
{
    class Player
    {
        String name;
        int playerNum;

        public Player(String name, int playerNum)
        {
            this.name = name;
            this.playerNum = playerNum;
        }

        public String getName()
        {
            if (name != null)
                return name;
            else
                return "Computer";
        }








    }
}
