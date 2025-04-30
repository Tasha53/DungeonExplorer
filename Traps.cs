using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Traps
    {
        //trap attributes
        private string name;
        private int damage;

        //using getters and setters for trap attributes
        public string Name
        {
            get { return name; }
        }

        public int Damage
        {
            get { return damage; }
        }

        //trap constructor
        public Traps(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }

    }
}
