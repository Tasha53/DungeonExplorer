using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Inventory
    {

        //inventory class' attributes
        private List<string> itemNames;
        private List<Potion> potionList;
        private List<Weapon> weaponList;
        private int space;

        //using getters and setters for inventory attributes
        public List<string> itemNames_
        {
            get { return itemNames; }
            set { itemNames = value; }
        }

        public List<Potion> potionList_
        {
            get { return potionList; }
            set { potionList = value; }
        }

        public List<Weapon> weaponList_
        {
            get { return weaponList; }
            set { weaponList = value; }
        }

        public int space_
        {
            get { return space; }
            set { space = value; }
        }

        //inventory constructor
        public Inventory(string name, List<string> itemNames, List<Potion> potionList, List<Weapon> weaponList, int space)
        {
            itemNames = itemNames_;
            potionList = potionList_;
            weaponList = weaponList_;
            space = space_;

        }

        //procedure to add potion to list of potions in inventory
        public void PickUpPotion(Potion potion)
        {
            potionList.Add(potion);
        }

        //procedure to add weapon to list of weapons in inventory
        public void PickUpWeapon(Weapon weapon)
        {
            weaponList.Add(weapon);
        }

        //function to display names of items in inventory
        public string InventoryContents()
        {
            return string.Join(Environment.NewLine, itemNames.Select((x, n)
                => $"{n + 1}. {x}"));
        }

        //function to display name of potions in potion list and how much health they replenish
        public void DisplayPotions()
        {
            foreach (Potion potion in potionList)
            { Console.WriteLine($"{potion.Name} adds {potion.AddedHealth} health points\n"); }
        }

        //function to display name of weapons in weapon list and how much strength they give the player
        public void DisplayWeapons()
        {
            foreach (Weapon weapon in weaponList)
            { Console.WriteLine($"{weapon.Name} adds {weapon.Damage} strength points\n"); }
        }

    }
}
