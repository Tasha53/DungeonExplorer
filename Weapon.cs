using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Weapon : Items, Icollectable //inherits from abstract class 'items'
                                       //and uses Icollectable interface
    {
        //weapon attributes
        private int damage;

        //using getters and setters for weapon attributes
        public int Damage
        {
            get { return damage; }
        }

        //weapon constructor
        public Weapon(string name, int damage)  : base(name)
        {
            this.damage = damage;
        }

        //procedure to add items to inventory - overriden so potions and weapons
        //can display a different message when added
        public override void AddToMainInventory(string itemName, List<string> itemNames)
        {
            Console.WriteLine($"Weapon ({itemName}) has been added to inventory");
            itemNames.Add(itemName);
        }
    }
}
