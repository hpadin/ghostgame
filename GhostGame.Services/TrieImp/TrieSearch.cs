using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// IMPORTANT: Trie implementation based on: http://aspdotnetcodebook.blogspot.com/2016/12/autocomplete-using-trie.html 
/// Summary description for Trie
/// </summary>
public class TrieSearch
{
    // Trie node class
    public class Node
    {
        public string Prefix { get; set; }
        public Dictionary<char, Node> Children { get; set; }

        // Does this node represent the last character in a word?
        public bool IsWord;

        public Node(String prefix)
        {
            this.Prefix = prefix;
            this.Children = new Dictionary<char, Node>();
        }
    }

    // The trie
    private Node trie;

    // Construct the trie from the dictionary
    public TrieSearch(IEnumerable<string> dict)
    {
        trie = new Node("");

        foreach (String s in dict)
        {
            string word = s.Replace(" ", "").Replace("'","").Replace("/","").ToUpper();
            
            if (!string.IsNullOrEmpty(word))
            {
                InsertWord(word);
            }
        }
    }

    // Insert a word into the trie
    private void InsertWord(String s)
    {
        // Iterate through each character in the string. If the character is not
        // already in the trie then add it
        Node curr = trie;
        for (int i = 0; i < s.Length; i++)
        {
            if (!curr.Children.ContainsKey(s[i]))
            {

                curr.Children.Add(s[i], new Node(s.Substring(0, i + 1)));
            }
            curr = curr.Children[s[i]];
            if (i == s.Length - 1)
                curr.IsWord = true;
        }
    }

    // Find all words in trie that start with prefix
    public List<String> GetWordsForPrefix(String pre)
    {
        List<String> results = new List<String>();

        // Iterate to the end of the prefix
        Node curr = trie;
        foreach (char c in pre.ToCharArray())
        {
            if (curr.Children.ContainsKey(c))
            {
                curr = curr.Children[c];
            }
            else
            {
                return results;
            }
        }

        // At the end of the prefix, find all child words
        FindAllChildWords(curr, results);
        return results;
    }

    //HGP: Added
    public List<String> GetWinningWords(string pre)
    {
        List<String> results = new List<String>();

        // Iterate to the end of the prefix
        Node curr = trie;
        foreach (char c in pre.ToCharArray())
        {
            if (curr.Children.ContainsKey(c))
            {
                curr = curr.Children[c];
            }
            else
            {
                return results;
            }
        }

        FindAllWinningWords(curr, results);
        return results;

    }

    // HGP: Added
    // Considered winning words the ones with an odd length
    private void FindAllWinningWords(Node n, List<String> results)
    {
        if (n.IsWord && n.Prefix.Length > 3 && !(n.Prefix.Length % 2 == 0))
            results.Add(n.Prefix);

        if (!(n.IsWord && n.Prefix.Length > 3 && (n.Prefix.Length % 2 == 0)))
        {
            foreach (var c in n.Children.Keys)
            {
                FindAllWinningWords(n.Children[c], results);
            }
        }
    }

    // Recursively find every child word
    private void FindAllChildWords(Node n, List<String> results)
    {
        if (n.IsWord)
            results.Add(n.Prefix);
        foreach (var c in n.Children.Keys)
        {
            FindAllChildWords(n.Children[c], results);
        }
    }

    // HGP: Added
    // Retrieves true if word is an existing word in the trie
    public bool IsPrefixAWord(string word)
    {
        Node curr = trie;
        foreach (char c in word.ToCharArray())
        {
            if (curr.Children.ContainsKey(c))
            {
                curr = curr.Children[c];
            }
            else
            {
                return false;
            }
        }

        return curr.IsWord;
    }

    // HGP: Added
    public bool ContainsAnOddWordInsideWithLengthGreaterThan(string word, int length)
    {
        Node curr = trie;
        foreach (char c in word.ToCharArray())
        {
            if (curr.Children.ContainsKey(c))
            {
                curr = curr.Children[c];
            }
            else
            {
                return false;
            }

            if (curr.IsWord && (curr.Prefix.Length%2 == 0) && curr.Prefix.Length > length && curr.Prefix != word)
            {
                return true;
            }
        }

        return false;
    }    
}