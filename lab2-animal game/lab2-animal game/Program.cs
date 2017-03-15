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
    // LAB2 animal game
    //Description: Progrom animal guess game using binary tree structure 
    //instructor: JD Sliver
    //student name: Xiao liu 
    //Date: March 13 2017




    class Program

    {
       
          static void Main(string[] args)
        { //check if bt tree.dat file exists and import the file
            BTtree tree=new BTtree();
            if (File.Exists("BTtree.dat")) //import the Bt tree
            {
                try
                {
                    FileStream fs = new FileStream("BTtree.dat", FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    tree = (BTtree)bf.Deserialize(fs);
                    fs.Close();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);


                }
                Console.WriteLine("BTtree has been imported from {0} LastModified time {1} ", Path.GetFullPath("BTtree.dat"),File.GetLastWriteTime(Path.GetFullPath("BTtree.dat")));
            }
           
            string wantToPlay = null;
          
            Console.WriteLine("do you want to play animals? Yes or no:");
            wantToPlay = BTnode.GetYesOrNo();
            if (wantToPlay=="y")
            {
                do
                {
                    tree.query();
                    Console.WriteLine("do you want to play animals? Yes or no:");
                    wantToPlay = BTnode.GetYesOrNo();

                } while (wantToPlay == "y");
            }
           
            if (wantToPlay=="n") //serilizae the BTtree exist the program
            {

                FileStream fs = new FileStream( "BTtree.dat", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, tree);
                fs.Close();
                Console.WriteLine("BT tree has benn saved ");


            }




          
        }
    }
}
