using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Dragon : Creature
    {

        //monster attributes
        private string variant;

        //encasulation - getter and setter for monster attribute
        public string Variant
        {
            get { return variant; }
            set { variant = value; }
        }


        //constructor
        public Dragon(int health, string variant): base(health) 
        {
            variant = Variant;
        }

        //Polymorphism - inherits DamagePlayer from creature and overrides
        //it to do a different amount of damage
        public override int DamagePlayer(int playerHealth)
        {
            playerHealth = playerHealth - 20;
            return playerHealth;
        }
    }
}
