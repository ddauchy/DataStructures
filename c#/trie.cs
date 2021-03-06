using System;
using System.Collections.Generic;
public class Trie {
    protected TrieNode root;

    public Trie() {
        this.root = new TrieNode(' ');
    }
    
    public void addWord(string word) {
        this.root.addLetters(word);
    }
    
    public void printAllData() {
        this.root.printAllData(0);
    }

    public bool findWord (string word, bool completeOnly) {
        return this.root.findWord (word, completeOnly);
    }

    public void printAllWords() {
        this.root.printAllCompleteWords ("");
    }
    
}

public class TrieNode {
    
    protected char character;
    protected IList<TrieNode> children;
    protected bool isCompleteWord;
    
    public TrieNode(char character) {
        this.character = character;
        this.isCompleteWord = false;
        this.children = new List<TrieNode>();
    }

    public bool findWord(string word, bool completeOnly) {

        if (string.IsNullOrEmpty (word)) {
            if(completeOnly) {
                return this.isCompleteWord;
            }
            else {
                return true;
            }
        }

        char firstChar = word [0];
        string remnant = word.Remove (0, 1);

        foreach (TrieNode child in this.children) {
            if (child.character == firstChar) {
                return child.findWord (remnant, completeOnly);
            }
        }

        return false;
    }

    public void addLetters(string word) {
        if(string.IsNullOrEmpty(word)) {
            this.isCompleteWord = true;
            return;
        }
        
        char firstChar = word[0];
        string remnant = word.Remove(0,1);
        TrieNode child = this.childHasChar(firstChar);
        if(child == null) {
            child = this.addChild(firstChar);
        }
        
        child.addLetters(remnant);
    }
    
    public void printAllCompleteWords(string prefix) {
        if (this.isCompleteWord) {
            Console.WriteLine (prefix + this.character);
        }

        foreach (TrieNode child in this.children) {
            child.printAllCompleteWords (prefix + this.character);
        }

    }

    public void printAllData(int level) {
        string output ="";
        
        for(int i=0;i<level;i++){
            output += ":::";
        }
        
        output += this.character;
        
        if(this.isCompleteWord) {
            output += "***";
        }
        
        Console.WriteLine(output);
        level++;
        
        if(this.children != null) {
            foreach(TrieNode child in this.children) {
                child.printAllData(level);
            }
        }
        
    }
    
    protected TrieNode childHasChar(char firstChar) {
        
        if(this.children == null) {
            return null;
        }
        
        foreach(TrieNode child in this.children) {
            if (child.character == firstChar) {
                return child;
            }
        }
        
        return null;
    }
    
    protected TrieNode addChild(char character) {
        TrieNode newChild = new TrieNode(character);
        this.children.Add(newChild);
        return newChild;
    }

}

class Start {  
    static void Main() {
    Trie myTrie = new Trie();
    myTrie.addWord("heytheredan");
    myTrie.addWord("hello");
    myTrie.addWord("hay");
    myTrie.addWord("tree");
    myTrie.addWord("try");
    myTrie.addWord("trymyluck");
    myTrie.addWord("trymy");
    Console.WriteLine(myTrie.findWord("try", true));
  }
}
