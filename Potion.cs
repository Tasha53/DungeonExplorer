using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Potion : Items, Icollectable //Potion class inherits from Items
                                       //class and uses Icollectable interface
    {
        //Potion class attributes
        private int addedHealth;

        //Getters and setters for potion class attributes
        public int AddedHealth
        {
            get { return addedHealth; }
        }

        //Potion constructor
        public Potion(string name, int addedHealth) : base(name)
        {
           this.addedHealth = addedHealth;
        }

        //Overriding procedure to add the names of potions to the main inventory
        public override void AddToMainInventory(string itemName, List<string> itemNames)
        {
            Console.WriteLine($"Consumable ({itemName}) has been added to inventory");
            itemNames.Add(itemName);
        }
    }
}
