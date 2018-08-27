using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame.Services.Models
{
    public class GameState
    {
        public int gameStatus { get; set; }
        public string word { get; set; }
        public string message { get; set; }

        public GameState()
        {
            gameStatus = (int) Enums.GameStatusEnum.HumanTurn;
            word = String.Empty;
            message = "Human plays...";
        }

        public void setNextTurn()
        {
            if (gameStatus == (int)Enums.GameStatusEnum.HumanTurn)
            {
                gameStatus = (int)Enums.GameStatusEnum.ComputerTurn;
            }
            else
            {
                gameStatus = (int)Enums.GameStatusEnum.HumanTurn;
            }
        }

        public void setComputerWin()
        {
            gameStatus = (int)Enums.GameStatusEnum.ComputerWin;
        }

        public void setHumanWin()
        {
            gameStatus = (int)Enums.GameStatusEnum.HumanWin;
        }
    }
}
