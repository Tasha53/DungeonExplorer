using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    abstract class Creature //player, dragon, goblin and zombie inherit from this
    {
        //creature attributes
        private int health;

        //using getters and setters for creature attributes
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

        //method which can be overriden so different monsters can deal
        //different amounts of damage to player
        public abstract int DamagePlayer(int playerHealth);

        //function that allows creature to lose health when damaged
        public int takeDamage(int monsterHealth, int damageValue)
        {
            if (damageValue > 0)
            {
                monsterHealth = monsterHealth - damageValue;
            }
            else { }
            return monsterHealth;
        }

        //creature constructor
        public Creature(int health)
        {
            health = Health;
        }


    }
}
