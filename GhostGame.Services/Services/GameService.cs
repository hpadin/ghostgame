using GhostGame.Services.Helpers;
using GhostGame.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GhostGame.Services
{
    public class GameService : IGameService
    {
        TrieSearch wordsTree;

        public GameService()
        {
            loadDictionary();
            resetGame();
        }

        public string getAlphabet()
        {
            return "abcdefghijklmnopqrstuvwxyz".ToUpper();
        }

        public GameState processLetter(string word, string letter)
        {
            GameState gameState = new GameState();
            gameState.word = word + letter;
            gameState.setGameInProgress();

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
                gameState.setComputerWin();
                return gameState;
            }
            else
            {
                //HGP: Letter entered is valid...Computer plays next letter

                //HGP: Ask for winning words to the trie
                List<string> winningWords = wordsTree.GetWinningWords(gameState.word);

                // it should play randomly among all its winning movements;
                if (winningWords.Count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(winningWords.Count);
                    string wordToFollow = winningWords[index];

                    gameState.word = gameState.word + wordToFollow[gameState.word.Length];
                    gameState.message = "Human plays...";

                    return gameState;
                }
                else
                {
                    List<string> loosingWords;
                    //Look for the words that doesnt contain an odd word inside so the game it doesnt finish earlier

                    //TODO HGP: This search can be optimized
                    List<string> loosingWordsWithoutOddWordsInside = resultWords.Where(s => !wordsTree.ContainsAnOddWordInsideWithLengthGreaterThan(s, 3)).ToList();
                    if (loosingWordsWithoutOddWordsInside.Count == 0)
                    {
                        loosingWords = resultWords.OrderByDescending(w => w.Length).ToList();
                    }
                    else
                    {
                        loosingWords = loosingWordsWithoutOddWordsInside.OrderByDescending(w => w.Length).ToList();
                    }

                    string loosingWord = loosingWords.First();
                    gameState.word = gameState.word + loosingWord[gameState.word.Length];
                    gameState.message = "Human plays...";

                    if (wordsTree.IsPrefixAWord(gameState.word) && gameState.word.Length >= 4)
                    {
                        //HGP: Letter entered leads to an existing word...Computer loses
                        gameState.setHumanWin();
                        gameState.message = "Human wins...";
                        gameState.setHumanWin();
                    }
                    return gameState;
                }
            }
        }

        public GameState resetGame()
        {
            return new GameState();
        }

        public void loadDictionary()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Resources\gosthGameDict.txt";
            filePath = filePath.Replace("GhostGame.Api", "GhostGame.Services");
            try
            { 
                var lines = File.ReadLines(filePath);
                wordsTree = new TrieSearch(lines);
            }
            catch (Exception e)
            {
                Logger.log(e.Message);
            }
        }

        public void setWordsTrie(TrieSearch trie)
        {
            wordsTree = trie;
        }
    }
}

