using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaGame
{
    class Pit
    {
        public int stones;
        public Pit() { this.stones = 0; }
        public int getStones() { return stones; }
        public void addStones(int stones) { this.stones += stones; }
        public bool isEmpty() { return stones == 0; }
        public int removeStones()
        {
            int stones = this.stones;
            this.stones = 0;
            return stones;
        }

    }
}
