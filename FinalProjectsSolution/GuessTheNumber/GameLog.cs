using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    internal class GameLog
    {
        public string userName { get; set; }
        public int score { get; set; }

        public GameLog(string userName)
        {
            this.userName = userName;
        }
        public GameLog(string userName, int score)
        {
            this.userName = userName;
            this.score = score;
        }
    }
}
