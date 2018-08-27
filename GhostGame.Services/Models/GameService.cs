using GhostGame.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GhostGame.Services
{
    public class GameService : IGameService
    {
        GameState gameState;
        TrieSearch wordsTree;

        public GameService()
        {
            loadDictionary();
            resetGame();
        }

        public GameState processLetter(string letter)
        {
            gameState.word = gameState.word + letter;
            gameState.setNextTurn();

            if (gameState.word.Length >= 4 && wordsTree.IsPrefixAWord(gameState.word))
            {
                //HGP: Letter entered leads to an existing word...Human loses
                gameState.message = "Computer wins: " + gameState.word + " is an existing word";
                gameState.setComputerWin();
                return gameState;
            }

            List<string> resultWords = wordsTree.GetWordsForPrefix(gameState.word);

            if (resultWords.Count == 0)
            {
                //HGP: Letter entered doesnt have a word result...Human loses
                gameState.setComputerWin();
                gameState.message = "Computer wins: it doesn't exist a word starting by: " + gameState.word;
                return gameState;
            }
            else
            {
                //HGP: Letter entered is valid...Computer plays next letter

                //HGP: Winning words would be the ones with an odd length...
                List<string> winningWords = wordsTree.GetWinningWords(gameState.word);

                // it should play randomly among all its winning movements;
                if (winningWords.Count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(winningWords.Count);
                    string wordToFollow = winningWords[index];

                    gameState.word = gameState.word + wordToFollow[gameState.word.Length];
                    gameState.message = "Human plays...";
                    gameState.setNextTurn();

                    return gameState;
                }
                else
                {
                    List<string> loosingWords = resultWords.OrderByDescending(w => w.Length).ToList();
                    List<string> loosingWordsFiltered = loosingWords.FindAll(s => (s.Length > gameState.word.Length) && !resultWords.Contains(gameState.word + s[gameState.word.Length]));

                    string loosingWord;
                    if (loosingWordsFiltered.Count == 0)
                    {
                        loosingWord = loosingWords.First();
                    }
                    else
                    {
                        loosingWord = loosingWordsFiltered.First();
                    }
                   
                    gameState.word = gameState.word + loosingWord[gameState.word.Length];
                    gameState.setNextTurn();
                    gameState.message = "Human plays...";

                    if (resultWords.Contains(gameState.word) && gameState.word.Length >= 4)
                    {
                        //HGP: Letter entered leads to an existing word...Computer loses
                        gameState.setHumanWin();
                        gameState.message = "Human wins...";
                    }
                    return gameState;
                }
            }
        }

        public GameState getCurrentState()
        {
            return gameState;
        }

        public GameState resetGame()
        {
            gameState = new GameState();
            return gameState;
        }

        public void loadDictionary()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Resources\gosthGameDict.txt";
            filePath = filePath.Replace("GhostGame.Api", "GhostGame.Services");
            var lines = File.ReadLines(filePath);

            wordsTree = new TrieSearch(lines);
        }
    }
}

