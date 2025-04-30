using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Goblin : Creature, Idamage //inherits from creature and uses damage interface
    {
        //monster attributes
        private string variant;

        //using getters and setters for monster attributes
        public string Variant
        {
            get { return variant; }
            set { variant = value; }
        }


        //monster constructor
        public Goblin(int health, string variant) : base(health)
        {
            variant = Variant;
        }

        //Polymorphism - inherits DamagePlayer from creature and overrides
        //it to do a different amount of damage
        public override int DamagePlayer(int playerHealth)
        {
            playerHealth = playerHealth - 15;
            return playerHealth;
        }
    }
}
