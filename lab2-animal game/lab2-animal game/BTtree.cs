using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace lab2_animal_game
{[Serializable]
    class BTtree
    {   private BTnode rootNode = new BTnode("human");
        public BTnode root { get { return rootNode; } }

        //public BTtree(string question, string yesAnswer, string noAnswer)
        //{
        //    root = new BTnode(question);
        //    root.setYesNode(new BTnode(yesAnswer));
        //    root.setNoNode(new BTnode(noAnswer));

        //}
        
        public void query()  //traverse the BT tree
        {
            root.Query();
        }

    }
}
