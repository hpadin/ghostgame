using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GhostGameTesting
{
    [TestClass]
    public class TrieSearchTests
    {
        [TestMethod]
        public void TestLoadTrieAndCheckNumberOfLeafs()
        {
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
            List<string> resultWords = trie.GetWordsForPrefix("PY");

            Assert.AreEqual(words.Count, resultWords.Count);

            foreach(var w in resultWords)
            {
                Assert.IsTrue(trie.IsPrefixAWord(w));
            }
        }

        [TestMethod]
        public void TestLoadTrieWithAWordWithSpecialCharacter()
        {
            List<string> words = new List<string> { "pygobranchiate",
                                                    "/pygofer",
                                                    "pygopagus",
                                                    "pygopod",
                                                    "pygopodine",
                                                    "pygopodous",
                                                    "pygostyle",
                                                    "pygostyled",
                                                    "pygos/tylous",
                                                    "pyic",
                                                    "pyin",
                                                    "pyin/s",
                                                    "pyjama/",
                                                    "pyjamaed",
                                                    "pyjamas" };

            TrieSearch trie = new TrieSearch(words);
            List<string> resultWords = trie.GetWordsForPrefix("PY");
            Assert.AreEqual(words.Count, resultWords.Count);

            Assert.IsTrue(trie.IsPrefixAWord("pygofer".ToUpper()));
            Assert.IsTrue(trie.IsPrefixAWord("pygostylous".ToUpper()));
            Assert.IsTrue(trie.IsPrefixAWord("pyins".ToUpper()));
            Assert.IsTrue(trie.IsPrefixAWord("pyjama".ToUpper()));
        }

        [TestMethod]
        public void TestLoadTrieWithOneDuplicateWord()
        {
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
                                                    "pyjama",
                                                    "pyjamaed",
                                                    "pyjamas" };

            TrieSearch trie = new TrieSearch(words);
            List<string> resultWords = trie.GetWordsForPrefix("");
            Assert.AreEqual(words.Count - 1, resultWords.Count);
        }

        [TestMethod]
        public void TestingNoWinningWords()
        {
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
            List<string> resultWords = trie.GetWinningWords("PYJA");
            Assert.IsTrue(resultWords.Count == 0);
        }

        [TestMethod]
        public void TestWordContainsOddWord()
        {
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
            bool result = trie.ContainsAnOddWordInsideWithLengthGreaterThan("PYJAMAS", 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestReturnedWinningWords()
        {
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
            List<string> resultWords = trie.GetWinningWords("PYG");
            Assert.IsTrue(resultWords.Count == 5);
        }


    }
}
