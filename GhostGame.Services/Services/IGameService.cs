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
        string getAlphabet();
        GameState processLetter(string word, string letter);
        GameState resetGame();
    }
}
