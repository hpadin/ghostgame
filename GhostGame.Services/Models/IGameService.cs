using GhostGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame.Services
{
    public interface IGameService
    {
        GameState processLetter(string letter);
        GameState getCurrentState();
        GameState resetGame();
    }
}
