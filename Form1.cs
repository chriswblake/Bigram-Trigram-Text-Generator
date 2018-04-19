using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ch10___Bigram_Model
{
    public partial class Form1 : Form
    {
        //Fields
        SentenceGenerator sentenceGenerator = null;

        public Form1()
        {
            InitializeComponent();

            //Default to einstein quote generator
            string fileAddress = @"EinsteinQuotes.txt";
            tbFileAddress.Text = fileAddress;
            sentenceGenerator = new SentenceGenerator(fileAddress);
        }

        //Controls
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            //Get file from user
            OpenFileDialog fd = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt",
                RestoreDirectory = true
            };
            DialogResult dialogResults = fd.ShowDialog();
            string fileAddress = fd.FileName;

            //Edit file address box
            tbFileAddress.Text = fileAddress;
           
            //Load text
            sentenceGenerator = new SentenceGenerator(fileAddress);

            btnGenerate.Enabled = true;
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (sentenceGenerator != null)
            {
                if (rbtnBigram.Checked)
                    tbResults.Text = sentenceGenerator.makeSentence(SentenceGenerator.NetType.Bigram);
                else if (rbtnTrigram.Checked)
                    tbResults.Text = sentenceGenerator.makeSentence(SentenceGenerator.NetType.Trigram);
            }
                
        }

        private void tbFileAddress_TextChanged(object sender, EventArgs e)
        {
            tbFileAddress.Select(tbFileAddress.Text.Length - 1, 1);
            tbFileAddress.ScrollToCaret();
        }
    }

    //Classes
    public class SentenceGenerator
    {
        Random rand = new Random();
        List<Node> Words = new List<Node>();
        List<BigramConnection> BigramConnections = new List<BigramConnection>();
        List<TrigramConnection> TrigramConnections = new List<TrigramConnection>();

        //Constructor
        public SentenceGenerator(string fileAddress)
        {
            //Load text and convert into Bigram network.
            parseFile(fileAddress);
        }

        //Methods
        private void parseFile(string fileAddress)
        {
            //Reusable variable
            int index;

            //Load text file and convert to lower case
            string text = File.ReadAllText(fileAddress).ToLower();

            //Split into sentences
            string[] sentences = text.Split(new char[] {'.', '?', '!'});

            //Analyze each sentence
            foreach (string sentence in sentences)
            {
                #region Clean sentence
                string sentenceCleaned = sentence;
                //Remove commas
                sentenceCleaned = sentenceCleaned.Replace(",", "");

                //Remove white space
                sentenceCleaned = sentenceCleaned.Trim();
                #endregion

                //Split into words
                List<string> senWords = sentenceCleaned.Split(' ').ToList();

                //Check length
                if (senWords.Count < 2)
                    continue;

                #region  First word
                //Get first word from list
                string firstWord = senWords.First();

                //Check if it is in the "Words" list.
                index = Words.FindIndex(w => w.word == firstWord);
                if (index > -1)
                {
                    //Word was found. Increase the "Start" occurences
                    Words[index].occurencesStart++;
                }
                else
                {
                    //Word is new. Add it to the list
                    Words.Add(new Node(firstWord) {occurencesStart=1});
                }
                #endregion

                #region  Last word
                //Get last word from list
                string lastWord = senWords.Last();

                //Check if it is in the "Words" list.
                index = Words.FindIndex(w => w.word == lastWord);
                if (index > -1)
                {
                    //Word was found. Increase the "End" occurences
                    Words[index].occurencesEnd++;
                }
                else
                {
                    //Word is new. Add it to the list
                    Words.Add(new Node(lastWord) { occurencesEnd=1 });
                }
                #endregion

                #region Middle words
                //Cycle through words in the middle. Notice that it starts at 1 and ends at (count-1).
                for(int i=1; i< senWords.Count-1; i++)
                {
                    //Get the word
                    string word = senWords[i];

                    //Check if this word is in the "Words" list.
                    index = Words.FindIndex(w => w.word == word);
                    if (index > -1)
                    {
                        //Word was found. Increase the "Middle" occurence
                        Words[index].occurencesMiddle++;
                    }
                    else
                    {
                        //Word is new. Add it to the list
                        Words.Add(new Node(word) { occurencesMiddle = 1 });
                    }
                }
                #endregion

                #region Analyze words (Bigram)
                //Record word pairs. Notice that it stops at (count-1).
                for(int i = 0; i< senWords.Count-1; i++)
                {
                    //Get words
                    string wordA = senWords[i];
                    string wordB = senWords[i + 1];

                    //Find nodes
                    Node nodeA = Words.Find(w => w.word == wordA);
                    Node nodeB = Words.Find(w => w.word == wordB);

                    //Check if it is in the bigrams list.
                    index = BigramConnections.FindIndex(c => c.nodeA == nodeA && c.nodeB == nodeB);
                    if (index > -1)
                    {
                        //Connection was found. Increase the occurences.
                        BigramConnections[index].occurences++;
                    }
                    else
                    {
                        //Connection is new. Add it to the list
                        BigramConnections.Add(new BigramConnection(nodeA, nodeB) { occurences = 1 });
                    }
                }
                #endregion

                #region Analyze words (Trigram)
                //Record word triplets. Notice that it stops at (count-2).
                for (int i = 0; i < senWords.Count - 2; i++)
                {
                    //Get words
                    string wordA = senWords[i];
                    string wordB = senWords[i + 1];
                    string wordC = senWords[i + 2];

                    //Find nodes
                    Node nodeA = Words.Find(w => w.word == wordA);
                    Node nodeB = Words.Find(w => w.word == wordB);
                    Node nodeC = Words.Find(w => w.word == wordC);

                    //Check if it is in the trigram list.
                    index = TrigramConnections.FindIndex(c => c.nodeA == nodeA && c.nodeB == nodeB && c.nodeC == nodeC);
                    if (index > -1)
                    {
                        //Connection was found. Increase the occurences.
                        TrigramConnections[index].occurences++;
                    }
                    else
                    {
                        //Connection is new. Add it to the list
                        TrigramConnections.Add(new TrigramConnection(nodeA, nodeB, nodeC) { occurences = 1 });
                    }
                }
                #endregion
            }

        }
        private Node nextNodeBigram (Node currentNode)
        {
            //Get list of possible connections
            List<BigramConnection> possiblities = BigramConnections.FindAll(c => c.nodeA == currentNode);
            int nodeCount = possiblities.Count;

            //Get total of occurences (for generating selection percentage)
            int totalOccurences = possiblities.Sum(c => c.occurences);

            //Loop until a word is found
            while (true)
            {
                //Pick a random node
                int index = rand.Next(0, nodeCount);

                //Generate its percentage of being picked
                double percentChance = possiblities[index].occurences / (double) totalOccurences;

                //Test the percentage agains a random number
                if (rand.NextDouble() < percentChance)
                {
                    //Select this node
                    return possiblities[index].nodeB;
                }
            }
        }
        private Node nextNodeTrigram(Node prevNode, Node currentNode)
        {
            //Get list of possible connections
            List<TrigramConnection> possiblities = TrigramConnections.FindAll(c => c.nodeA == prevNode && c.nodeB == currentNode);
            int nodeCount = possiblities.Count;
            if (nodeCount == 0)
            {
                //Prev node is null, or there is no match in the trigram. 
                possiblities = TrigramConnections.FindAll(c => c.nodeA == currentNode || c.nodeB == currentNode);                   
                nodeCount = possiblities.Count;
            }       

            //Get total of occurences (for generating selection percentage)
            int totalOccurences = possiblities.Sum(c => c.occurences);

            //Loop until a word is found
            while (true)
            {
                //Pick a random node
                int index = rand.Next(0, nodeCount);

                //Generate its percentage of being picked
                double percentChance = possiblities[index].occurences / (double)totalOccurences;

                //Test the percentage against a random number
                if (rand.NextDouble() < percentChance)
                {
                    //Select this node
                    return possiblities[index].nodeC;
                }
            }
        }
        public string makeSentence(NetType netType)
        {
            //Store the sentence as a list of nodes
            List<Node> sentence = new List<Node>();

            //Pick a random start node
            List<Node> startNodes = Words.FindAll(n => n.occurencesStart > 0);
            Node startNode = startNodes[rand.Next(0, startNodes.Count)];
            sentence.Add(startNode);

            //Build a sentence. Add words until an "end node" is found.
            while (true)
            {
                //Get last word in current sentence.
                Node prevNode = null;
                if (sentence.Count > 1)
                    prevNode = sentence[sentence.Count - 2]; //Second to last word.
                Node curNode = sentence.Last(); //Last word.

                //Get next potential word.
                Node next = null;
                if(netType == NetType.Bigram)
                    next = nextNodeBigram(curNode);
                else  if(netType == NetType.Trigram)
                    next = nextNodeTrigram(prevNode, curNode);

                //Add to the sentence
                sentence.Add(next);

                //Check if this next word is an end word
                if (next.occurencesEnd > 0)
                {
                    //Decided randomly if it should end
                    int totalOccurence = next.occurencesStart + next.occurencesMiddle + next.occurencesEnd;
                    double percentChanceEnd = next.occurencesEnd / (double)totalOccurence;
                    if (rand.NextDouble() < percentChanceEnd)
                        break;                 
                }
            }

            //Convert node list to string
            string strSentence = "";
            foreach(Node node in sentence)
                strSentence += " " + node.word;

            //Remove padding
            strSentence = strSentence.Trim();

            //Capitalize first Word
            strSentence = char.ToUpper(strSentence[0]) + strSentence.Substring(1);

            //Put a period on the end.
            strSentence += ".";

            //Return results
            return strSentence;
        }

        //Classes
        public enum NetType
        {
            Bigram,
            Trigram
        }

    }  
    public class BigramConnection
    {
        public Node nodeA;
        public Node nodeB;
        public int occurences;

        //Constructors
        public BigramConnection(Node nodeA, Node nodeB)
        {
            this.nodeA = nodeA;
            this.nodeB = nodeB;
        }

        //Debug
        public override string ToString()
        {
            return occurences + ": " + nodeA.word + " " + nodeB.word;
        }
    }
    public class TrigramConnection
    {
        public Node nodeA;
        public Node nodeB;
        public Node nodeC;
        public int occurences;

        //Constructors
        public TrigramConnection(Node nodeA, Node nodeB, Node nodeC)
        {
            this.nodeA = nodeA;
            this.nodeB = nodeB;
            this.nodeC = nodeC;
        }

        //Debug
        public override string ToString()
        {
            return occurences + ": " + nodeA.word + " " + nodeB.word + " " + nodeC.word;
        }
    }
    public class Node
    {
        public string word;
        public int occurencesStart;
        public int occurencesMiddle;
        public int occurencesEnd;

        //Constructor
        public Node(string word)
        {
            this.word = word;
        }

        //Debug
        public override string ToString()
        {
            return word + " (" + occurencesStart + "," + occurencesMiddle + "," + occurencesEnd + ")";
        }
    }

}
