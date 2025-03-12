using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Player
    {
        //Player class' attributes
        private string name;
        private int health;
        private List<string> inventory;

        static Random rnd = new Random();

        //Using getters and setters for player class' attributes
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) //Checking player
                    //gives valid input - if not, "name" is assigned a default
                    //value
                {
                    Console.WriteLine("Invalid input, player name" +
                        " defaulted to 'Player1'");
                    name = "Player1";
                }
                else
                {
                    name = value;
                }
            }
        }

        public int Health
        {
            get { return health; }
            set
            {
                if (value < 0) //checking that "health" is given a valid value
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
            }
        }

        public List<string> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        //Player constructor
        public Player(string name, int health, List<string> inventory) 
        {

            name = Name;
            health = Health;
            inventory = Inventory;

        }

        //Procedure which adds item to player's inventory if item is found
        public void PickUpItem(string item)
        {
            inventory.Add(item);
        }

        //Function which displays contents of player's inventory
        public string InventoryContents()
        {
            return string.Join(Environment.NewLine, inventory.Select((x, n)
                => $"{n+1}. {x}"));
        }
    }
}
