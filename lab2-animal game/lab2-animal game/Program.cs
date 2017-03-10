using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace lab2_animal_game
{
    class Program

    {[Serializable]
        public struct node
        {
            public string text;
            public int Yeslink;
            public int NoLink;
            public node(string t, int y, int n)
            {
                text = t;
                Yeslink = y;
                NoLink = n;
                
            }
            public override string ToString()
            {
                return string.Format("{0},{1},{2}", text, Yeslink, NoLink);
            }

        }




        static void Main(string[] args)
        {//iniate list for store node information 
            List<node> myNodeList = new List<node>();
      
            //check if local directory has data file, bring the nodes into list.          

            if (File.Exists(@"C:\Users\xliu43\Desktop\Data Structure\data-structure-lab2-Animal-Game\lab2-animal game\lab2-animal game\bin\Debug\animal.dat"))
            {
                try
                {
                    FileStream fs = new FileStream(@"C:\Users\xliu43\Desktop\Data Structure\data-structure-lab2-Animal-Game\lab2-animal game\lab2-animal game\bin\Debug\animal.dat",
                   FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    myNodeList = (List<node>)bf.Deserialize(fs);
                    fs.Close();
                    //debug
                    foreach (var node in myNodeList)
                          Debug.WriteLine(node);

                }

                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                node human = new node("human", 0, 0);
                myNodeList.Add(human);
                Debug.WriteLine(myNodeList);
            }

            string wantPlayOrNot = null;
            //game interface
            do
            {
                Console.Write("Do you want to play animals?(y/n)");
                wantPlayOrNot = Console.ReadLine().ToLower();
                if (wantPlayOrNot=="y")
                {
                    Console.WriteLine("Pick an animal,and I will guess it");
                }
                gamingprocess(myNodeList, 0);
            } while (true);
          













            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.ReadLine();
            }


        }
        public static void gamingprocess(List<node> mynodelist,int n)
        {
            Debug.WriteLine(n + mynodelist[n].ToString());
           
            if (mynodelist[n].text==null)
            {
                return;
            }
            
            if ((mynodelist[n].Yeslink==0&&mynodelist[n].NoLink==0)) //currently stays at animal node
            {
                string answer = null;
                string answerForNewQuestion = null;
                string question = null;
                string newanimal = null;
                node existingAnimal = mynodelist[n];
                Console.Write("Is it a(an) ?:"+existingAnimal.text);
                answer = Console.ReadLine();
                if (answer=="y")
                {
                    Console.WriteLine("Hurray!");
                    return;
                }
                else if (answer=="n") //answer wrong need to add a question and new animal 
                {
                    Console.WriteLine("I gave up. What type of animal werewr you thinking of?");
                    newanimal = Console.ReadLine();
                    Console.WriteLine("What is a yes/no question that would distinguish between a(n){0} and a(n){1}",newanimal,mynodelist[n].text);
                    question = Console.ReadLine();
                    Console.WriteLine("what answer would be correct for a {0} (Y or N)",newanimal);
                    answerForNewQuestion = Console.ReadLine();
                    node newAnimal = new node(newanimal, 0, 0);
                    
                    //insert newanimal after the original animal (mynodlist[n])
                    mynodelist.Insert((mynodelist.IndexOf(existingAnimal) + 1), newAnimal);

                    if (answerForNewQuestion=="y")
                    {
                        node newQuestion = new node();
                        mynodelist.Insert(mynodelist.IndexOf(existingAnimal), newQuestion);

                        //A List of structs cannot be updated like that - the reason is that structs are copied by value and 
                        //    are not a reference -so when you do .value you are changing a value on the copy which is then 
                        //    immediately discarded. If the items in the list were classes then this wouldn't be the case
                        //    because you would have a reference to the actual object. When working with structs y
                        //    ou need to get the contents of the list item - modify it and then put it back again.
                        //mynodelist[mynodelist.IndexOf(newQuestion)].text=question
                        int i = mynodelist.IndexOf(newQuestion);
                        newQuestion = mynodelist[i];
                        newQuestion.text = question;
                        newQuestion.Yeslink= mynodelist.IndexOf(newAnimal);
                        newQuestion.NoLink = mynodelist.IndexOf(existingAnimal);
                        mynodelist[i] = newQuestion;

                    }
                    else if (answerForNewQuestion=="n")
                    {
                        node newQuestion = new node();
                        mynodelist.Insert(mynodelist.IndexOf(existingAnimal), newQuestion);
                        int i = mynodelist.IndexOf(newQuestion);
                        newQuestion = mynodelist[i];
                        newQuestion.text = question;
                        newQuestion.Yeslink = mynodelist.IndexOf(existingAnimal);
                        newQuestion.NoLink = mynodelist.IndexOf(newAnimal);
                        mynodelist[i] = newQuestion;
                    }
                    //insert new question into mynodelist
                    
                    foreach (var node in mynodelist )
                    {
                        Debug.WriteLine(node);
                    }
                    
                }
            }
            else if (mynodelist[n].Yeslink!=0)
            { 
                Console.WriteLine("{0}",mynodelist[n].text);
                string answer = null;
                node question;
                question = mynodelist[n];
                answer = Console.ReadLine();
                if (answer=="y")
                {
                     gamingprocess(mynodelist, n+1);
                }
                else if (answer=="n")
                {
                    gamingprocess(mynodelist, n+2);
                }
            }

        }
    }
}
