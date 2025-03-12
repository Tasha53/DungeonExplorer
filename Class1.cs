using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Test
{
    Player player1 { get; set; }
    Room room1 { get; set; }

    public Test(Player _player)
    {
        player1 = _player;
    }

    //testing player object
    public void TestPlayer()
    {
        Debug.Assert(player1 != null, "The player was not created");
        Debug.Assert(player1.Health != 0, "The player doesn't have the"+
            " right amount of health");
        Debug.Assert(player1.Inventory != null, "The player's inventory" +
            " was not created");
        Debug.Assert(player1.Name != null, "The player doesn't have " +
            "a name");
    }

    //testing room object
    public Test(Room _room)
    {
        room1 = _room;
    }
    public void TestRoom()
    {
        Debug.Assert(room1 != null, "The room was not created");
        Debug.Assert(room1.Monster != null, "The chance of a monster" +
            " being present is null");
        Debug.Assert(room1.Item == "small health potion" 
            || room1.Item == "regular health potion" 
            || room1.Item == "large health potion", 
            "The item is invalid");
    }

}
}
