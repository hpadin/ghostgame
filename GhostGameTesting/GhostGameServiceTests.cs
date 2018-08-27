using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostGame.Services;
using System.Text.RegularExpressions;
using GhostGame.Services.Models;
using System.Collections.Generic;
using GhostGame.Services.Enums;

namespace GhostGameTesting
{
    [TestClass]
    public class GhostGameServiceTests
    {
        [TestMethod]
        public void TestAlphabetContainsOnlyLetters()
        {
            GameService gameService = new GameService();
            string alphabet = gameService.getAlphabet();

            Assert.IsTrue(Regex.IsMatch(alphabet, @"^[a-zA-Z]+$"));
        }

        [TestMethod]
        public void TestResetGameLeadsToEmptyWord()
        {
            GameService gameService = new GameService();
            GameState gameState = gameService.resetGame();

            Assert.IsTrue(gameState.word.Length == 0);
            Assert.IsTrue(gameState.gameStatus == 1); //In Progress
        }

        [TestMethod]
        public void TestHumanWinningMove()
        {
            GameService gameService = new GameService();
            List<string> words = new List<string> { "pygobranchiate",
                                                    "pygofer",
                                                    "pygopagus",
                                                    "pygopod",
                                                    "pygopodine",
                                                    "pygopodous",
                                                    "pygostyle",
                                                    "pygostyled",
                                                    "pygostylous",
                                                    "pyic",
                                                    "pyin",
                                                    "pyins",
                                                    "pyjama",
                                                    "pyjamaed",
                                                    "pyjamas" };

            TrieSearch trie = new TrieSearch(words);

            gameService.setWordsTrie(trie);
            GameState gameState = gameService.processLetter("PYJA", "M");

            Assert.IsTrue(gameState.gameStatus == (int)GameStatusEnum.HumanWin);
        }

        [TestMethod]
        public void TestHumanLoosingMoveForNotExistingWord()
        {
            GameService gameService = new GameService();
            List<string> words = new List<string> { "pygobranchiate",
                                                    "pygofer",
                                                    "pygopagus",
                                                    "pygopod",
                                                    "pygopodine",
                                                    "pygopodous",
                                                    "pygostyle",
                                                    "pygostyled",
                                                    "pygostylous",
                                                    "pyic",
                                                    "pyin",
                                                    "pyins",
                                                    "pyjama",
                                                    "pyjamaed",
                                                    "pyjamas" };

            TrieSearch trie = new TrieSearch(words);

            gameService.setWordsTrie(trie);
            GameState gameState = gameService.processLetter("PYIC", "M");

            Assert.IsTrue(gameState.gameStatus == (int)GameStatusEnum.ComputerWin);
        }

        [TestMethod]
        public void TestHumanLoosingMoveByCompletingWord()
        {
            GameService gameService = new GameService();
            List<string> words = new List<string> { "pygobranchiate",
                                                    "pygofer",
                                                    "pygopagus",
                                                    "pygopod",
                                                    "pygopodine",
                                                    "pygopodous",
                                                    "pygostyle",
                                                    "pygostyled",
                                                    "pygostylous",
                                                    "pyic",
                                                    "pyin",
                                                    "pyins",
                                                    "pyjama",
                                                    "pyjamaed",
                                                    "pyjamas" };

            TrieSearch trie = new TrieSearch(words);

            gameService.setWordsTrie(trie);
            GameState gameState = gameService.processLetter("PYIN", "S");

            Assert.IsTrue(gameState.gameStatus == (int)GameStatusEnum.ComputerWin);
        }
    }
}
