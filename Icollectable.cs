using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal interface Icollectable
    {
        //ensures all items can be collected
        void AddToMainInventory(string itemName, List<string> itemNames);
    }
}
