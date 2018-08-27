using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostGame.Services;
using System.Text.RegularExpressions;

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
    }
}
