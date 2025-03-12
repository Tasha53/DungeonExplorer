using System;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    public class Room
    {
        //Room class' attributes
        private string description;
        private bool monster;
        private string item;
        static Random rnd = new Random();

        //Using getters and setters for the room class' attributes
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool Monster
        {
            get { return monster; }
            set { monster = value; }
        }

        public string Item
        {
            get { return item; }
            set { item = value; }
        }

            //Function which randomly generates an item for the room
            //game object
            public string ChooseItem()
        {

            int randomItem = rnd.Next(1, 4);
            string item = "";
            switch (randomItem)
            {
                case 1:
                    item = "small health potion";
                    break;
                case 2:
                    item = "regular health potion";
                    break;
                case 3:
                    item = "large health potion";
                    break;
            }
            return item;
        }

        //Room constructor
        public Room(string description, bool monster, string item)
        {
            description = Description;
            monster = Monster;
            item = ChooseItem();
        }

        //Function which randomly generates one of 4 room descriptions
        public string ChooseRoom()
        {
            int roomNum = rnd.Next(1, 5);
            string chosenRoom = "";
            switch (roomNum)
            {
                case 1:
                    chosenRoom = "You enter a small, dimly lit room";
                    break;
                case 2:
                    chosenRoom = "You enter a completely dark room. It is" +
                        " impossible to see anything. Your fumble around in" +
                        " the darkness searching for\n a door to the next" +
                        " room.";
                    break;
                case 3:
                    chosenRoom = "You enter a large, brightly lit room";
                    break;
                case 4:
                    chosenRoom = "You enter a cave-like room. There" +
                        " is an ominous shadow in the corner...";
                    break;
            }
            return chosenRoom;
        }
    }
}
