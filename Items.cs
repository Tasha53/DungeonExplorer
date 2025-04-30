using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    //Abstraction - uses an abstract class because item class doesn't need
    //to be instantiated but potion and weapon need to inherit from it
    abstract class Items : Icollectable //Uses Icollectable interface
                                        //to ensure all items can
                                        //be added to the player's inventory
    {

        //Items class attributes
        private string name;
        
        //getters and setters for items attributes
        public string Name
        {
            get { return name; }
        }

        public Items(string name)
        {
            this.name = name;
        }

        //items constructor
        public abstract void AddToMainInventory(string itemName, List<string> itemNames);
    }
}
