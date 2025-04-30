using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class Test
    {
        //Setting up game objects to test
        Player player1 { get; set; }
        Room room1 { get; set; }
        Inventory inventory1 { get; set; }
        Dragon dragon1 { get; set; }
        Goblin goblin1 { get; set; }
        Zombie zombie1 { get; set; }
        Statistics statistics1 { get; set; }
        Map map1 { get; set; }



        public Test(Player _player)
        {
            player1 = _player;
        }

        public Test(Inventory _inventory)
        {
            inventory1 = _inventory;
        }

        public Test(Room _room)
        {
            room1 = _room;
        }

        public Test(Dragon _dragon)
        {
            dragon1 = _dragon;
        }

        public Test(Goblin _goblin)
        {
            goblin1 = _goblin;
        }

        public Test(Zombie _zombie)
        {
            zombie1 = _zombie;
        }

        public Test(Statistics _statistics)
        {
            statistics1 = _statistics;
        }

        public Test(Map _map)
        {
            map1 = _map;
        }


        //testing player object
        public void TestPlayer()
        {
            Debug.Assert(player1 != null, "The player was not created");
            Debug.Assert(player1.Health != 0, "The player doesn't have the" +
                " right amount of health");
            //Debug.Assert(player1.Inventory != null, "The player's inventory" +
            //" was not created");
            Debug.Assert(player1.Name != null, "The player doesn't have " +
                "a name");
        }

        //Testing inventory object
        public void TestInventory()
        {
            Debug.Assert(inventory1 != null, "The player's inventory wasn't created");
            Debug.Assert(inventory1.weaponList_ != null, "The player's weapon inventory wasn't created");
            Debug.Assert(inventory1.potionList_ != null, "The player's potion inventory wasn't created");
            Debug.Assert(inventory1.itemNames_ != null, "The player's merged inventory wasn't created");
        }

        //testing room object
        public void TestRoom()
        {
            Debug.Assert(room1 != null, "The room was not created");
        }

        //Testing Dragon object
        public void TestDragon()
        {
            Debug.Assert(dragon1 != null, "The 'Dragon' monster was not created");
            Debug.Assert(dragon1.Health != 0, "The 'Dragon' monster doesn't have the" +
                                                " right amount of health");
        }

        //Testing Goblin object
        public void TestGoblin()
        {
            Debug.Assert(goblin1 != null, "The 'Goblin' monster was not created");
            Debug.Assert(goblin1.Health != 0, "The 'Goblin' monster doesn't have the" +
                                    " right amount of health");
        }

        //Testing zombie object
        public void TestZombie()
        {
            Debug.Assert(zombie1 != null, "The 'Zombie' monster was not created");
            Debug.Assert(zombie1.Health != 0, "The 'Zombie' monster doesn't have the" +
                                    " right amount of health");
        }

        //Testing statistics object
        public void TestStatistics()
        {
            Debug.Assert(statistics1 != null, "The statistics class was not created");
        }

        //Testing map object
        public void TestMap()
        {
            Debug.Assert(map1 != null, "The map class was not created");
            Debug.Assert(map1.Directions != null, "The list of directions was not created");
        }


    }
}
