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
            gameStatus = (int) Enums.GameStatusEnum.GameInProgress;
            word = String.Empty;
            message = "Human plays...";
        }

        public void setGameInProgress()
        {
            gameStatus = (int)Enums.GameStatusEnum.GameInProgress;
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
