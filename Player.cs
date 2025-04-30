using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    class Player: Creature, Idamage//Inherits from creature and uses damage interface
    {
        //Player class' attributes
        private string name;
        private int strength;
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

        public int Strength //Starts as 0, is increased as the player uses weapons
        {
            get { return strength; }
            set
            {
                if (value < 0) //checking that "strength" is given a valid value
                {
                    strength = 0;
                }
                else
                {
                    strength = value;
                }
            }
        }
        

        public List<string> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        //Player constructor
        public Player(string name, int health, int strength, List<string> inventory) : base (health)
        {

            name = Name;
            strength = Strength;
            inventory = Inventory;

        }

        //Procedure which adds item to player's inventory if item is found
        public void PickUpItem(string item)
        {
            inventory.Add(item);
        }

        //Function to randomly determine how much damage the player does
        public int playerAttack()
        {
            int damageNum = rnd.Next(1, 5);
            int damageValue = 0;
            switch (damageNum)
            {
                case 1:
                    damageValue = 0; //Adds more damage to damageValue if the player
                                                //has used an item to increase their strength
                    Console.WriteLine($"The attack fails! You dealt {damageValue} damage!");
                    break;
                case 2:
                    damageValue = 5 + Strength;
                    Console.WriteLine($"You attack feebly! You only deal {damageValue} damage");
                    break;
                case 3:
                    damageValue = 10 + Strength;
                    Console.WriteLine($"You attack the monster! You deal {damageValue} damage!");
                    break;
                case 4:
                    damageValue = 20 + Strength;
                    Console.WriteLine($"Your attack is really powerful! You deal {damageValue} damage!");
                    break;
            }
            return damageValue;
        }

        //Polymorphism - inherits DamagePlayer from creature and overrides
        //it so the player doesn't damage itself
        public override int DamagePlayer(int playerHealth)
        {
            return playerHealth;
        }
    }
}
