using System;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    public class Room
    {
        //Room class' attributes
        private string description;
        static Random rnd = new Random();

        //Using getters and setters for the room class' attributes
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //Room constructor
        public Room(string description)
        {
            description = Description;
        }

        //Function which randomly generates one of 4 room descriptions
        public string ChooseRoom()
        {
            int roomNum = rnd.Next(1, 5);
            string chosenRoom = "";
            switch (roomNum)
            {
                case 1:
                    chosenRoom = "You enter a small, dimly lit room. An " +
                        "eerie quietness descends...";
                    break;
                case 2:
                    chosenRoom = "You enter a completely dark room. It is" +
                        " impossible to see anything. You fumble around in" +
                        " the darkness searching for\n a door to the next" +
                        " room.";
                    break;
                case 3:
                    chosenRoom = "You enter a large, brightly lit room.";
                    break;
                case 4:
                    chosenRoom = "You enter a cave-like room. There" +
                        " is an ominous shadow in the corner...";
                    break;
            }
            return chosenRoom;
        }

        //function which randomly generates the contents of each
        //room (trap, monster, puzzle etc...)
        public int RoomContents()
        {
            int contentsNum = rnd.Next(1, 6);
            return contentsNum;
        }
    }
}
