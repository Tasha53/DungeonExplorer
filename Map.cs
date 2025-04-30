using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Map
    {
        //map attributes
        private List<int> directions;

        static Random rnd = new Random();

        //using getters and setters for map attributes
        public List<int> Directions
        {
            get { return directions; }
            set { directions = value; }
        }

        //map constructor
        public Map(List<int> directions)
        {
            directions = Directions;
        }

        //function which returns list of randomly generated directions
        //(numerically - e.g. 1 = forward...)
        public List<int> GenerateOrder(int rooms, List<int> directions)
        {
            int count = 0;
            while (count < rooms)
            {
                int randomDirection = rnd.Next(1, 4);
                directions.Add(randomDirection);
                count++;
            }
            return directions;
        }

    }
}
