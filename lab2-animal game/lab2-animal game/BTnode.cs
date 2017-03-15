using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_animal_game
{ [Serializable]
    class BTnode
    {
        string message;
        BTnode yesNode;
        BTnode noNode;
        public BTnode(string Message) //constructor for new node
        {
            message = Message;
            yesNode = null;          
            noNode = null;
        }
        public void setMessage(string nodeMessage) //set message for question or animal name
        {
            message = nodeMessage;
        }
        public string getMessage()
        {
            return message;
        }
        public void setNoNode(BTnode node)
        {
            noNode = node;
        }
        public BTnode getNoNode()
        {
            return noNode;
        }
        public void setYesNode(BTnode node)
        {
            yesNode = node;
        }
        public BTnode getYesNode()
        {
            return yesNode;
        }
        //medthod to get user's input YEs or no 
        public static string GetYesOrNo()
        {
            string input = null;
            bool error = false;
            do
            {
                error = false;
                input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "n":
                    case "y":
                        input = input.ToLower();
                        break;
                    default:
                        {
                            Console.WriteLine("Input Yes or No");
                            error = true;
                        }
                        break;
                }
            } while (error);
            return input;

        }
        public bool isQuestion()  //check if node is a question or animal 
        {
            if (noNode == null && yesNode == null)
                return false;
            else
                return true;
        }
        public void Query()
        {
            if (this.isQuestion()) //if node is the queston display the question 
            {
                Console.WriteLine(this.message);
                string input = GetYesOrNo();
                if (input == "y")   //point to yes or no node 
                {
                    this.yesNode.Query();
                }
                else this.noNode.Query();
            }
            else { this.onQueryObject(); } //if node is the animal 

        }
        public void onQueryObject()
        {
            Console.WriteLine("is it a(n)"+this.message+"?");
            string input = GetYesOrNo();
            if (input == "y")
            {
                Console.WriteLine("Hurray!");
            }
            else { updateTree(); }

        }
        public  void updateTree() //if final guess is wrong, create two new nodes(leaf)
        {
            Console.WriteLine("I give up. What type of animal were you thinking of?");
            string newAnimalInput = Console.ReadLine();
            Console.WriteLine("What is a yes/no question that would distinguish between a(n) {0}, and a(n) {1} ?",this.message,newAnimalInput);
            string questionInput = Console.ReadLine();
            Console.WriteLine("what answer would be correct for a {0} ?(Yes or No)",newAnimalInput);
            string answerInput = GetYesOrNo();
            if (answerInput == "y")     
            {
                this.noNode = new BTnode(this.message);
                this.yesNode = new BTnode(newAnimalInput);
            }
            else { this.noNode = new BTnode(newAnimalInput);
                this.yesNode = new BTnode(this.message);
            }
            this.setMessage(questionInput);  //change the node to question node (branch)
 
        }
    }
}
