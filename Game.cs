using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using DungeonExplorer;

namespace DungeonExplorer
{

    internal class Game
    {
        private Player player1;
        private Room currentRoom;
        static Random rnd = new Random();
        private Dragon dragon1;
        private Goblin goblin1;
        private Zombie zombie1;
        private Map Map;
        private Inventory Inventory1;
        private Potion Potion;
        private Weapon Weapon;
        private Statistics endStatistics;
        private Statistics gameStats;
        public Game()
        {
            // Initialize the game with all the classes

            player1 = new Player("", 0, 0, new List<string>());
            currentRoom = new Room("");
            dragon1 = new Dragon(0, "Dragon");
            goblin1 = new Goblin(0, "Goblin");
            zombie1 = new Zombie(0, "Zombie");
            Map = new Map(new List<int>());
            Map.Directions = new List<int>();
            Inventory1 = new Inventory("inventory", new List<string>(), new List<Potion>(), new List<Weapon>(), 3);
            Weapon = new Weapon("placeholder", 0);
            Potion = new Potion("placeholder", 0);
            endStatistics = new Statistics(0, 0, 0, 0, 0);
            gameStats = new Statistics(0, 0, 0);
        }

        bool playing = false;
        int roomsPassed = 0;
        //Function which uses a switch statement to randomly generate the
        //amount of damage that a monster deals to the player, used on
        //line 
        public int PlayerTakeDamage()
        {
            int damageValue = 0;
            int randomDamage = rnd.Next(1, 6);
            switch (randomDamage)
            {
                case 1:
                    damageValue = 0;
                    break;
                case 2:
                    damageValue = 5;
                    break;
                case 3:
                    damageValue = 20;
                    break;
                case 4:
                    damageValue = 35;
                    break;
                case 5:
                    damageValue = 30;
                    break;
            }
            return damageValue;
        }

        //Function which determines whether the player has lost all of their
        //health,it is used on line 212 to determine whether the game
        //continues - the game ends if player loses all health
        public bool PlayerDeath()
        {
            if (player1.Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Function to randomly determine whether or not an item is generated
        //in the room
        public Potion PotionGeneration(List<Potion> potions)
        {
            int randomType = rnd.Next(1, 3);
            var item1 = potions[0];
            int randomItem = rnd.Next(1, 4);
            switch (randomItem)
            {
                case 1: 
                    item1 = potions[randomItem - 1];
                    break;
                case 2:
                    item1 = potions[randomItem - 1];
                    break;
                case 3:
                    item1 = potions[randomItem - 1];
                    break;
            }
            return item1;
        }

        //Function that generates a random weapon
        public Weapon WeaponGeneration(List<Weapon> weapons)
        {
            int randomType = rnd.Next(1, 3);
            var item1 = weapons[0];
            int randomItem = rnd.Next(1, 4);
            switch (randomItem)
            {
                case 1:
                    item1 = weapons[randomItem - 1];
                    break;
                case 2:
                    item1 = weapons[randomItem - 1];
                    break;
                case 3:
                    item1 = weapons[randomItem - 1];
                    break;
            }
            return item1;
        }

        //Procedure which stops the game if the player dies
        public void GameEnd()
        {
            Console.WriteLine($"GAME OVER! {player1.Name.ToUpper()}" +
            $" has died...");
            playing = false;
        }


        //Procedure which stops the game if the player passes all of the rooms
        public void WinGame()
        {
            if (roomsPassed == Map.Directions.Count)
            {
                //game ends if player has passed all of the generated rooms
                Console.WriteLine($"CONGRATULATIONS " +
                    $"{player1.Name} YOU HAVE ESCAPED THE " +
                    $"DUNGEON! You had {player1.Health}" +
                    $" health points remaining");
                playing = false;
                
                //Setting the variables for the player's statistic to their
                //accurate values
                endStatistics.RoomsNavigated = roomsPassed;
                endStatistics.HealthRemaining = player1.Health;

                //Testing the statistics class
                Test testStatistics = new Test(endStatistics);
                testStatistics.TestStatistics();

                //Displaying the players statistics at the end of the game
                endStatistics.EndPlayerStats();
                Console.ReadKey(true);
            }
            else { }
        }

        //Procedure that checks if the player has died and ends the game accordingly
        public void CheckGameEnd()
        {
            if (PlayerDeath() == true)//game ends if player runs
                                      //out of health
            {
                //Setting the variables for the player's statistic to their
                //accurate values
                endStatistics.RoomsNavigated = roomsPassed;
                endStatistics.HealthRemaining = player1.Health;

                //Testing statistics class
                Test testStatistics = new Test(endStatistics);
                testStatistics.TestStatistics();

                //Displaying player's statistics at end of game
                endStatistics.EndPlayerStats();
                GameEnd();
            }
            else
            {
                roomsPassed++;

            }
            if (roomsPassed == Map.Directions.Count)
            {
                //game ends if player has passed 10 rooms
                WinGame();
            }
        }

        //Procedure that allows the player to use a potion
        public void UsePotion(Potion chosenPotion)
        {
            if (player1.Health + chosenPotion.AddedHealth <= 100)//checking player
                                                                 //has little
                                                                 //enough health
                                                                 //to use potion
            {
                player1.Health = player1.Health + chosenPotion.AddedHealth;
                Console.WriteLine($"You used a {chosenPotion.Name}\n Your health is now {player1.Health}");
                Inventory1.potionList_.Remove(chosenPotion);
                Inventory1.itemNames_.Remove(chosenPotion.Name); //removing used potion from inventory
                endStatistics.PotionsConsumed++; //updating statistics
            }
            else
            {
                Console.WriteLine($"{player1.Name}'s health is too high to use a {chosenPotion.Name}");
            }
        }

        //Function that hides key in random spot and keep asking player to search
        //for it until they find it
        public void KeySearch()
        {
            int locationNum = rnd.Next(1, 6);
            bool correctLocation = false;
            Console.WriteLine("You scan the room. It appears that there are" +
                " 5 possible locations where a key could possibly be hidden." +
                " You note that these are: a dest coated in at least three" +
                " inches of dust, a threadbare rug that is scrunched into" +
                " a heap in the centre of the room, a (fairly obvious) " +
                "'secret' compartment in the wall, a mess of plants " +
                "growing out of a damp spot in the left corner of the" +
                " room and a pile of various pieces of litter. " +
                "You stand and consider which to search first...");
            while (correctLocation == false)
            {
                Console.WriteLine($"Enter the number of the location you want" +
                    $" to search for the key: \n1) Dusty chest\n" +
                    $"2) Under Threadbare Rug\n" +
                    $"3) Secret Compartment in Wall" +
                    $"\n4) Plants\n" +
                    $"5) Pile of Discarded Litter");
                try
                {
                    int guessedLocation = Convert.ToInt32(Console.ReadLine());
                    if (guessedLocation == locationNum)
                    {
                        Console.WriteLine("Congratulations: you have found the key!" +
                            " You can now progress to the next room");
                        correctLocation = true;
                    }
                    else
                    {
                        //player loses 2hp if they pick the wrong hiding spot
                        player1.Health = player1.Health - 2;
                        Console.WriteLine("The key was not in that location");
                        Console.ReadKey(true);
                        Console.WriteLine("You lose 2hp due to exhaustion from searching");
                        Console.ReadKey(true);
                        Console.WriteLine($"Your health is now {player1.Health}");
                        Console.ReadKey(true);
                        Console.WriteLine("Try another location");
                        Console.ReadKey(true);
                        correctLocation = false;
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input - please enter either" +
                        " 1, 2, 3, 4 or 5 - whichever number corresponds " +
                        "to the location that you want to check for the key");
                    correctLocation = false;
                }
            }
        }

        //Procedure to allow player to use a weapon
        public void UseWeapon(Weapon chosenWeapon)
        {
            player1.Strength = 0;
            player1.Strength = player1.Strength + chosenWeapon.Damage;
            Console.WriteLine($"You used a {chosenWeapon.Name}\n Your strength " +
                $"is now {player1.Strength}");
            Inventory1.weaponList_.Remove(chosenWeapon);
            Inventory1.itemNames_.Remove(chosenWeapon.Name);
            endStatistics.WeaponsUsed++;
        }

        //Procedure to allow the player to choose one of the items in their
        //inventory to use
        public void UseItem2()
        {
            bool itemChoice = false;

            while (itemChoice == false)
            {
                Console.WriteLine("Enter the number of the item you want to" +
                        " use, or enter '0' if you wish to move on without" +
                        " using an item: "); ;
                string strItemUsed = Console.ReadLine();
                //Try-catch block to handle exceptions which may arise from
                //the user's input here
                try
                {
                    int itemUsed = Convert.ToInt32(strItemUsed) - 1;

                    //if -1 not use item
                    if (itemUsed == -1)
                    {
                        Console.WriteLine("You decide to continue without" +
                            " using an item");
                        itemChoice = true;
                        break;
                    }
                    //checking if chosen item is a potion (if its name starts with a 'P')
                    else if (Inventory1.itemNames_[itemUsed].StartsWith("P", StringComparison.OrdinalIgnoreCase))
                    {
                        string searchPotion = Inventory1.itemNames_[itemUsed];
                        Potion usedPotion = Inventory1.potionList_.Where(potion => potion.Name == searchPotion).FirstOrDefault();

                        //chosen item not in inventory
                        if (usedPotion == null)
                        {
                            Console.WriteLine("You do not have that item in your inventory");
                        }
                        else
                        {
                            UsePotion(usedPotion);
                            itemChoice = true;
                        }
                    }
                    else
                    {
                        //this runs if chosen item is not a potion (must be a weapon)
                        string searchWeapon = Inventory1.itemNames_[itemUsed];
                        Weapon usedWeapon = Inventory1.weaponList_.Where(weapon => weapon.Name == searchWeapon).FirstOrDefault();
                        if (usedWeapon == null)
                        {
                            Console.WriteLine("You do not have that item in your inventory");
                        }
                        else
                        {
                            UseWeapon(usedWeapon);
                            itemChoice = true;
                        }
                    }
                }
                //Handles exception if user enters value that is not a
                //number corresponding to an item in their inventory
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("You do not have that many items " +
                        "in your inventory,\n please input the number which" +
                        " corresponds to the item in your inventory that you" +
                        " wish to use");
                    itemChoice = true;
                }
                //Handles exception if user enters a value that can't be
                //converted to an integer
                catch (FormatException)
                {
                    Console.WriteLine("A valid number was not inputted, " +
                        "please input the number that corresponds to the " +
                        "item in your inventory that you wish to use");
                    itemChoice = true;
                }
                itemChoice = false;
            }
        }

        //Procedure to begin fight with 'dragon' monster
        public void DragonFight()
        {
            dragon1.Health = 25;
            Console.WriteLine($"The Dragon has {dragon1.Health} health");
            //Testing that the dragon game object is instantiated properly
            //and that its attributes have the correct values
            Test testDragon = new Test(dragon1);
            testDragon.TestDragon();

            bool invalidInput = true;
            bool flee = false;
            while (invalidInput || (dragon1.Health > 0 && PlayerDeath() == false))
            {
                try
                {
                    //dragon has chance to flee if its health is less than 10
                    if (dragon1.Health <= 10)
                    {
                        Console.WriteLine("The Dragon is weakening, it may flee!");
                    }
                    Console.WriteLine("You choose to fight the Dragon\n" +
                        "Your options are:\n1)Attack\n2)Use Item\n" +
                        "Please enter 1 or 2 to select your action");
                    int playerInput = Convert.ToInt32(Console.ReadLine());
                    if (playerInput == 1)//player has attacked 
                    {
                        invalidInput = false;
                        dragon1.Health = dragon1.takeDamage(dragon1.Health, player1.playerAttack());
                        Console.ReadKey(true);
                        Console.WriteLine("The Dragons health is " + dragon1.Health);
                        Console.ReadKey(true);

                        int randomAttack = rnd.Next(1, 5);

                        //dragon attacks player
                        if ((randomAttack == 1 || randomAttack == 4 )&& dragon1.Health > 0)
                        {
                            player1.Health = dragon1.DamagePlayer(player1.Health);
                            Console.WriteLine($"The Dragon attacked! Your health is now {player1.Health}");
                            Console.ReadKey(true);
                            if (PlayerDeath() == true)
                            {
                                GameEnd();
                            }
                        }
                        //dragon doesn't attack
                        else if (randomAttack == 2  && dragon1.Health > 0)
                        {
                            Console.WriteLine("The Dragon failed to attack");
                            Console.ReadKey(true);
                        }
                        //dragon attempts to flee
                        else if (randomAttack == 3 && dragon1.Health < 10)
                        {
                            Console.WriteLine("The Dragon attempted to flee but couldn't get away!");
                            Console.ReadKey(true);
                        }
                        //dragon flees - battle ends
                        else if (randomAttack == 3 && dragon1.Health <= 10 && dragon1.Health > 0)
                        {
                            Console.WriteLine("The Dragon decided to flee! It disappears into " +
                                "the depths of the dungeon...\nThe battle appears to have ended for" +
                                " now, so you move on");
                            flee = true;
                            dragon1.Health = 0;
                            endStatistics.MonstersDefeated++;
                            Console.ReadKey(true);
                        }
                        //ends battle if dragon dies before fleeing
                        if (player1.Health > 0 && dragon1.Health <= 0 && flee == false)
                        {
                            Console.WriteLine("You have defeated the Dragon! You run past it as it " +
                            "slumps to the ground");
                            endStatistics.MonstersDefeated++;
                        }
                        
                    }
                    //player uses item
                    else if (playerInput == 2)
                    {
                        invalidInput = false;
                        Console.WriteLine(DisplayInventory());
                        UseItem2();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        invalidInput = true;
                    }
                }
                catch (FormatException)
                {
                    invalidInput = true;
                    Console.WriteLine("Incorrect format - please enter either '1' or '2'");
                }
            }
        }

        //Procedure to begin fight with 'goblin' monster
        public void GoblinFight()
        {
            goblin1.Health = 35;
            Console.WriteLine($"The Goblin has {goblin1.Health} health");

            //Testing that the goblin game object is instantiated properly
            //and that its attributes have the correct values
            Test testGoblin = new Test(goblin1);
            testGoblin.TestGoblin();

            bool invalidInput = true;
            bool flee = false;
            while (invalidInput || (goblin1.Health > 0 && PlayerDeath() == false))
            {
                try
                {
                    //goblin has chance to flee if its health is less than 10
                    if (goblin1.Health <= 10)
                    {
                        Console.WriteLine("The Goblin is weakening, it may flee!");
                    }

                    Console.WriteLine("You choose to fight the goblin\nYour options " +
                        "are:\n1)Attack\n2)Use item\nPlease enter 1 or 2 to" +
                        " select your action");
                    int playerInput = Convert.ToInt32(Console.ReadLine());
                    if (playerInput == 1)//player has attacked
                    {
                        invalidInput = false;
                        goblin1.Health = goblin1.takeDamage(goblin1.Health, player1.playerAttack());
                        Console.ReadKey(true);
                        Console.WriteLine("The Goblins health is " + goblin1.Health);
                        Console.ReadKey(true);

                        int randomAttack = rnd.Next(1, 5);
                        //goblin attacks
                        if ((randomAttack == 1 || randomAttack == 4) && goblin1.Health > 0)
                        {
                            player1.Health = goblin1.DamagePlayer(player1.Health);
                            Console.WriteLine($"The Goblin attacked! Your health is now {player1.Health}");
                            Console.ReadKey(true);
                            if (PlayerDeath() == true)
                            {
                                GameEnd();
                            }
                        }
                        //goblin doesn't attack
                        else if (randomAttack == 2 && goblin1.Health > 0)
                        {
                            Console.WriteLine("The Goblin failed to attack");
                            Console.ReadKey(true);
                        }
                        //goblin attempts to flee
                        else if (randomAttack == 3 && goblin1.Health > 10)
                        {
                            Console.WriteLine("The Goblin attempted to flee but couldn't get away!");
                            Console.ReadKey(true);
                        }
                        //goblin flees - battle ends
                        else if (randomAttack == 3 && goblin1.Health <= 10 && goblin1.Health > 0)
                        {
                            Console.WriteLine("The Goblin decided to flee! It disappears into " +
                                "the depths of the dungeon...\nThe battle appears to have ended for" +
                                " now, so you move on");
                            flee = true;
                            goblin1.Health = 0;
                            endStatistics.MonstersDefeated++;
                            Console.ReadKey(true);
                        }
                        //if goblin dies before fleeing battle ends
                        if (player1.Health > 0 && goblin1.Health <= 0 && flee == false)
                        {
                            Console.WriteLine("You have defeated the Goblin! You run past it as it " +
                            "whithers away");
                            endStatistics.MonstersDefeated++;
                        }
                        
                    }
                    else if (playerInput == 2)
                        //player uses item
                    {
                        invalidInput = false;
                        Console.WriteLine(DisplayInventory());
                        UseItem2();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        invalidInput = true;
                    }
                }
                catch (FormatException)
                {
                    invalidInput = true;
                    Console.WriteLine("Incorrect format - please enter either '1' or '2'");
                }
            }
        }

        //Procedure to begin fight with 'zombie' monster
        public void ZombieFight()
        {
            zombie1.Health = 30;
            Console.WriteLine($"The Zombie has {zombie1.Health} health");

            //Testing that the zombie game object is instantiated properly
            //and that its attributes have the correct values
            Test testZombie = new Test(zombie1);
            testZombie.TestZombie();

            bool invalidInput = true;
            bool flee = false;
            while (invalidInput || (zombie1.Health > 0 && PlayerDeath() == false))
            {
                try
                {
                    //if zombies health is less than 10 it has the ability to flee
                    if (zombie1.Health <= 10)
                    {
                        Console.WriteLine("The Zombie is weakening, it may flee!");
                    }
                    Console.WriteLine("You choose to fight the zombie\nYour options are:" +
                        "\n1)Attack\n2)Use Item\nPlease enter 1 or 2 to select " +
                        "your action");
                    int playerInput = Convert.ToInt32(Console.ReadLine());
                    if (playerInput == 1)//player has attacked
                    {
                        invalidInput = false;
                        zombie1.Health = zombie1.takeDamage(zombie1.Health, player1.playerAttack());
                        Console.ReadKey(true);
                        Console.WriteLine("The zombies health is " + zombie1.Health);
                        Console.ReadKey(true);

                        int randomAttack = rnd.Next(1, 5);
                        //zombie attacks
                        if ((randomAttack == 1 || randomAttack == 5) && zombie1.Health > 0)
                        {
                            player1.Health = zombie1.DamagePlayer(player1.Health);
                            Console.WriteLine($"The Zombie attacked! Your health is now {player1.Health}");
                            Console.ReadKey(true);
                            if (PlayerDeath() == true)
                            {
                                GameEnd();
                            }
                        }
                        //zombie doesn't attack
                        else if (randomAttack == 2 && zombie1.Health > 0)
                        {
                            Console.WriteLine("The Zombie failed to attack");
                            Console.ReadKey(true);
                        }
                        //zombie attempts to flee
                        else if (randomAttack == 3 && zombie1.Health > 10)
                        {
                            Console.WriteLine("The Zombie attempted to flee but couldn't get away!");
                            Console.ReadKey(true);
                        }
                        //zombie flees - battle ends
                        else if (randomAttack == 3 && zombie1.Health <= 10 && zombie1.Health > 0)
                        {
                            Console.WriteLine("The Zombie decided to flee! It disappears into " +
                                "the depths of the dungeon...\nThe battle appears to have ended for" +
                                " now, so you move on");
                            flee = true;
                            zombie1.Health = 0;
                            endStatistics.MonstersDefeated++;
                            Console.ReadKey(true);
                        }
                        //if zombie dies before it can flee the battle ends
                        if (player1.Health > 0 && zombie1.Health <= 0 && flee == false)
                        {
                            Console.WriteLine("You have defeated the Zombie! You run past it as it " +
                                "melts into the floor");
                            endStatistics.MonstersDefeated++;
                        }
                    }
                    //player uses item
                    else if (playerInput == 2)
                    {
                        invalidInput = false;
                        Console.WriteLine(DisplayInventory());
                        UseItem2();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        invalidInput = true;
                    }
                }
                catch (FormatException)
                {
                    invalidInput = true;
                    Console.WriteLine("Incorrect format - please enter either '1' or '2'");
                }
            }
        }
        //procedure to choose random monster to be in the room
        public void chooseMonster()
        {
            int randomMonster = rnd.Next(1, 4);
            switch (randomMonster)
            {
                case 1:
                    Console.WriteLine("There is a Dragon in this room! Press any key to battle!");
                    Console.ReadKey(true);
                    DragonFight();
                    break;
                case 2:
                    Console.WriteLine("A Goblin emerges from the shadows! Press any key to battle!");
                    Console.ReadKey(true);
                    GoblinFight();
                    break;
                case 3:
                    Console.WriteLine("You hear a scraping sound in the corner of the room. You turn," +
                        " and a Zombie is shuffling towards you! Press any key to battle!");
                    Console.ReadKey(true);
                    ZombieFight();
                    break;
            }

        }

        //Function to display names of items in player's inventory
        public string DisplayInventory()
        {
            if (Inventory1.itemNames_.Count == 0)
            {
                return ($"{player1.Name}'s inventory is empty");
            }
            else
            {
                return ($"{player1.Name}'s inventory consists of: \n" +
                    $"{Inventory1.InventoryContents()}");
            }
        }

        //Procedure to discard an item and swap it with another
        public void DiscardItem()
        {
            bool itemChosen = false;
            while (itemChosen == false)
            {
                //Choosing item to discard
                Console.WriteLine("Please enter the number that corresponds to the item that you want to discard from your inventory: \n");
                Console.WriteLine(Inventory1.InventoryContents());
                string strDiscardedItem = Console.ReadLine();
                try
                {
                    int discardedItem = Convert.ToInt32(strDiscardedItem) - 1;
                    string itemToRemove = Inventory1.itemNames_[discardedItem];

                    //if item is potion removes it from the list of potions
                    if (Inventory1.itemNames_[discardedItem].StartsWith("P", 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        string searchPotion = Inventory1.itemNames_[discardedItem];
                        Potion discardedPotion = Inventory1.potionList_.Where(potion => potion.Name == searchPotion).FirstOrDefault();
                        if (discardedPotion == null)
                        {
                            Console.WriteLine("You do not have that item in your inventory");
                        }
                        else
                        {
                            Console.WriteLine($"Removed {discardedPotion.Name} from your inventory");
                            Inventory1.itemNames_.Remove(itemToRemove);//also removes item from main inventory
                            Inventory1.potionList_.Remove(discardedPotion);
                            itemChosen = true;
                        }
                    }
                    else
                    //if item is a weapon, removes it from the weapon inventory
                    {
                        string searchWeapon = Inventory1.itemNames_[discardedItem];
                        Weapon discardedWeapon = Inventory1.weaponList_.Where(weapon => weapon.Name == searchWeapon).FirstOrDefault();
                        if (discardedWeapon == null)
                        {
                            Console.WriteLine("You do not have that item in your inventory");
                        }
                        else
                        {
                            Console.WriteLine($"Removed {discardedWeapon.Name} from your inventory");
                            Inventory1.weaponList_.Remove(discardedWeapon);
                            Inventory1.itemNames_.Remove(itemToRemove);//also removes item from main inventory
                            itemChosen = true;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input - you must enter a number");
                    itemChosen = false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid input - that item does not exist " +
                        "in your inventory - please enter the number that" +
                        " corresponds to the item that you want to" +
                        " discard from your inventory: ");
                    itemChosen = false;
                }
            }
        }

        //Start of game

        public void Start()
        {
            //adding types of traps to 'traps' list
            List<Traps> traps = new List<Traps>();

            traps.Add(new Traps("Rolling Boulder", 5));
            traps.Add(new Traps("Hidden Spikes", 10));
            traps.Add(new Traps("Hidden Arrows", 15));

            //adding types of potion to 'potions' list
            List<Potion> potions = new List<Potion>();

            potions.Add(new Potion("Potion (Small)", 5));
            potions.Add(new Potion("Potion (Regular)", 10));
            potions.Add(new Potion("Potion (Large)", 20));

            //adding types of weapon to 'weapons' list
            List<Weapon> weapons = new List<Weapon>();

            weapons.Add(new Weapon("Weapon (Sword)", 2));
            weapons.Add(new Weapon("Weapon (Axe)", 5));
            weapons.Add(new Weapon("Weapon (Bow)", 10));

            bool creatingPlayer = true;
            playing = false;
            Map.Directions = Map.GenerateOrder(10, Map.Directions);

            while (creatingPlayer)
            {

                //Instantiating player object
                Console.WriteLine("Enter the player's name: ");
                player1.Name = Console.ReadLine();

                player1.Health = 100;
                player1.Strength = 0;

                Inventory1.itemNames_ = new List<string>();
                Inventory1.weaponList_ = new List<Weapon>();
                Inventory1.potionList_ = new List<Potion>();
                Inventory1.space_ = 2;

                //Calling test class to test the player creation
                Test playerTest = new Test(player1);
                Test inventoryTest = new Test(Inventory1);
                playerTest.TestPlayer(); //Testing that the player's
                                        //attributes are the desired values

                inventoryTest.TestInventory(); //Testing that the inventory's
                                               //attributes are the desired
                                               //values

                //setting up statistics class for end of game
                endStatistics.MonstersDefeated = 0;
                endStatistics.WeaponsUsed = 0;
                endStatistics.PotionsConsumed = 0;

                //Displaying the game rules
                Console.WriteLine("Game rules: \nYou have been stranded in" +
                    " a dungeon. To escape you must pass through at least 10" +
                    " \nrooms, fighting any monsters you may come across...\n" +
                    "There is a certain path that you must follow, each time" +
                    " you take a wrong turn the length of your dungeon journey" +
                    " will \nincrease by 1 room.\nThe maximum number of rooms " +
                    "in the dungeon is 15." +
                    " \nPress any key to continue\n");
                Console.ReadKey(true);

                Console.WriteLine("When you enter each room you will search" +
                    " it for monsters or items. \nYou will have to defeat any " +
                    "monsters before leaving each room, or attack them until" +
                    " they flee \nItems such as health potions will replenish " +
                    "your health when consumed and weapons will increase the damage" +
                    " that you do\nwhen attacking monsters\n");
                Console.ReadKey(true);

                Console.WriteLine("Potions: \nSmall health potion: grants" +
                    " 5 health points when consumed\nRegular health " +
                    "potion: grants 10 health points when consumed \nLarge " +
                    "health potion: grants 20 health points when consumed \n");
                Console.ReadKey(true);

                Console.WriteLine("Weapons: \nSword: grants 2 strength points\n" +
                    "Axe: grants 5 strength points\nBow: grants 10 strength points");

                Console.WriteLine($"Now, {player1.Name}, your adventure" +
                    $" begins. Good luck!\n");
                Console.ReadKey(true);

                Console.WriteLine($"Player's name is {player1.Name}" +
                    $" \n{player1.Name}'s health is: {player1.Health}" +
                    $" \nInventory is empty\nPress any key to begin" +
                    $" your adventure...\n");
                Console.ReadKey(true);

                creatingPlayer = false;
            }

            //Controls flow of game after player object is created
            playing = true;
            roomsPassed = 1;
            bool itemUsed = false;
            while (playing == true)
            {
                //At the start of each turn the user can either move into the
                //next room or view their player's status
                Console.WriteLine($"Do you want to view {player1.Name}'s" +
                    $" status or move on to the next room? \nType 'S' to " +
                    $"view player status or 'C' to continue: ");
                string playerChoice = Console.ReadLine().ToUpper();

                if (playerChoice == "S")
                {
                    //Setting up statistics class for during game
                    gameStats.HealthRemaining = player1.Health;
                    gameStats.RoomsNavigated = roomsPassed;
                    gameStats.Strength = player1.Strength;

                    //Displaying player's stats at this point in the game
                    gameStats.CurrentPlayerStats(player1.Name);

                    //Displaying player's inventory
                    Console.WriteLine(DisplayInventory());

                    //displays weapons and potions separately
                    Console.WriteLine("Do you wish to view the healing items and weapon items in your inventory separately? (y/n)");
                    string choice2 = Console.ReadLine().ToUpper();
                    if (choice2 == "Y")
                    {
                        Console.WriteLine($"Potions: \n");
                        Inventory1.DisplayPotions();
                        Console.ReadKey(true);
                        Console.WriteLine("\nWeapons: \n");
                        Inventory1.DisplayWeapons();
                        Console.ReadKey(true);
                    }
                    else if (choice2 == "N") { }
                    else 
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                else if (playerChoice == "C")
                {
                    //user is asked which direction they want to proceed in
                    bool directionChoice = false;
                    Test testMap = new Test(Map);
                    testMap.TestMap();
                    while (directionChoice == false)
                    {
                        try
                        {
                            Console.WriteLine("You scan the room looking for a door. " +
                                "There are 3. One to your left, one to your right " +
                                "and one directly in front of you.\nWhich door do you " +
                                "wish to take?\n(Enter 1, 2 or 3)\n1) Left\n" +
                                "2) Right\n3) Forward");
                            int playerDirection = Convert.ToInt32(Console.ReadLine());

                            //no action if player chooses right door
                            if (playerDirection == Map.Directions[roomsPassed - 1])
                            {
                                Console.WriteLine("The door opens easily as if this path is " +
                                    "often taken. " +
                                    "You have chosen the correct door.");
                                directionChoice = true;
                                Console.ReadKey(true);
                            }
                            //if player chooses wrong door another room is added to the dungeon -
                            //by adding another direction to the list of directions
                            else if ((playerDirection == 1 || playerDirection == 2 
                                || playerDirection == 3) 
                                && playerDirection != Map.Directions[roomsPassed - 1])
                            {
                                //until number of direcions in list reaches 15, new directions
                                //are added each time player makes wrong turn
                                if (Map.Directions.Count < 15)
                                {
                                    Console.WriteLine("You struggle to shove the door open - it is " +
                                        "clearly not often used.\n" +
                                        "You are on the wrong path, you will now have to pass " +
                                        "through an added " +
                                        "room to escape the dungeon.");
                                    Map.Directions = Map.GenerateOrder(1, Map.Directions);
                                    directionChoice = true;
                                    Console.ReadKey(true);
                                }
                                else
                                {
                                    //once number of directions gets to 15 no aditional rooms are added -
                                    //so player loses 2hp each time instead
                                    Console.WriteLine("You have chosen the wrong door, but" +
                                        " the dungeon doesn't go any deeper so it is the same" +
                                        " distance to the exit (no more rooms are added)\nHowever," +
                                        " because you chose the wrong door you lost 2 health" +
                                        " points from struggling to open the door.");
                                    player1.Health = player1.Health - 2;
                                    directionChoice = true;
                                    CheckGameEnd();

                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, please enter '1', '2' or '3'");
                                directionChoice = false;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input, please enter '1', '2' or '3'");
                        }
                    }
                    //If user continues to next room they are asked if they
                    //want to use any items
                    while (itemUsed == false)
                    {
                        Console.WriteLine("Would you like to use an item" +
                            " from your inventory? (Enter 'y' or 'n')");
                        string playerUseitem = Console.ReadLine().ToLower();
                        if (playerUseitem == "y")
                        {
                            Console.WriteLine(DisplayInventory());
                            UseItem2();
                            itemUsed = true;
                        }
                        else if (playerUseitem == "n")
                        {
                            Console.WriteLine("");
                            itemUsed = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection");
                            itemUsed= false;
                        }
                    }
                    itemUsed = false;

                    //Instantiating currentRoom game object
                    currentRoom.Description = currentRoom.ChooseRoom();
                    Console.WriteLine($"This is room number {roomsPassed}");
                    Console.ReadKey(true);
                    Console.WriteLine(currentRoom.Description);
                    Console.ReadKey(true);

                    //testing room game object
                    Test testRoom = new Test(currentRoom);
                    testRoom.TestRoom();
                    
                    //determining if an item is present
                    int itemPresent = rnd.Next(1,4);
                    if (itemPresent == 1 || itemPresent == 2)
                    {
                        //determining if item is potion or weapon
                        int itemType = rnd.Next(1, 3);
                        if (itemType == 1)
                        {
                            //generating random potion and adding it to player's
                            //inventory
                            var item = PotionGeneration(potions);
                            Console.WriteLine($"{player1.Name} searches the" +
                            $" room and finds a {item.Name}");
                            Console.ReadKey(true);

                            //if player's inventory is full, asks them if they want to swap two items
                            if (Inventory1.itemNames_.Count -1 >= Inventory1.space_)
                            {
                                bool playerDecided = false;
                                while (playerDecided == false)
                                {
                                    Console.WriteLine("Your inventory is full, do you " +
                                        "want to discard an item to replace it with this one?" +
                                        " Enter y/n");
                                    Console.WriteLine(Inventory1.InventoryContents());
                                    string discard = Console.ReadLine().ToLower();

                                    //discards item and picks up new potion
                                    if (discard == "y")
                                    {
                                        DiscardItem();
                                        Inventory1.PickUpPotion(item);
                                        Potion.AddToMainInventory(item.Name, Inventory1.itemNames_);
                                        playerDecided = true;
                                    }
                                    //doesn't discard item - moves on without collecting potion
                                    else if (discard == "n")
                                    {
                                        Console.WriteLine("You move on without collecting the item");
                                        playerDecided = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input, please enter 'y' or 'n'");
                                        playerDecided = false;
                                    }
                                }
                            }
                            else
                            {
                                //if inventory isn't full, picks up potion and moves on
                                Inventory1.PickUpPotion(item);
                                Potion.AddToMainInventory(item.Name, Inventory1.itemNames_);
                            }
                        }
                        //repeated for weapons
                        else if (itemType == 2)
                        {
                            var item = WeaponGeneration(weapons);

                            Console.WriteLine($"{player1.Name} searches the" +
                                $" room and finds a {item.Name}");
                            Console.ReadKey(true);

                            //checks if player inventory is full
                            if (Inventory1.itemNames_.Count -1 >= Inventory1.space_)
                            {
                                bool playerDecided = false;
                                while (playerDecided == false)
                                {
                                    //asks player if they want to discard an item if
                                    //their inventory is full
                                    Console.WriteLine("Your inventory is full, do you" +
                                        " want to discard an item to replace it with" +
                                        " this one? Enter y/n");
                                    Console.WriteLine(Inventory1.InventoryContents());
                                    string discard = Console.ReadLine().ToLower();
                                    if (discard == "y")
                                    {
                                        //discards item and swaps with newly found weapon
                                        DiscardItem();
                                        Weapon.AddToMainInventory(item.Name, Inventory1.itemNames_);
                                        Inventory1.PickUpWeapon(item);
                                        playerDecided = true;
                                    }
                                    else if (discard == "n")
                                        //leaves newly found weapon and moves on
                                    {
                                        Console.WriteLine("You move on without collecting the item");
                                        playerDecided = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input, please enter 'y' or 'n'");
                                        playerDecided = false;
                                    }
                                }
                            }
                            else
                            {
                                //adds weapon to main inventory and weapon inventory
                                Weapon.AddToMainInventory(item.Name, Inventory1.itemNames_);
                                Inventory1.PickUpWeapon(item);
                            }
                        }
                    }
                    else if (itemPresent == 3)
                    {
                        //Moves on if no item is generated
                        Console.WriteLine($"{player1.Name} searches" +
                            $" the room but finds no items \nPress any" +
                            $" key to continue \n");
                        Console.ReadKey(true);
                    }
                    else { Console.WriteLine("There was an error"); }


                    //generating the contents of the room (monsters/ traps or puzzles)
                    if (roomsPassed <= 5)
                    {
                        int roomContents = currentRoom.RoomContents();
                        if (roomContents == 1
                            || roomContents == 2
                            || roomContents == 3)
                        {
                            //monster is generated at random
                            Console.WriteLine("Suddenly, you fear you " +
                                "aren't alone in the room...");
                            Console.ReadKey(true);
                            chooseMonster();

                            CheckGameEnd(); //checking if player died in
                                            //monster battle
                        }
                        else if (roomContents == 4)
                        {
                            //trap is generated at random
                            int randomTrap = rnd.Next(0, 3);
                            player1.Health = player1.Health - traps[randomTrap].Damage;
                            Console.WriteLine($"This room has been armed " +
                                $"with a trap!");
                            Console.ReadKey(true);
                            Console.WriteLine($"You encounter a" +
                                $" {traps[randomTrap].Name} trap");
                            Console.ReadKey(true);
                            //trap damages player depending on which
                            //type of trap it is
                            Console.WriteLine($"You lose " +
                                $"{traps[randomTrap].Damage} health points" +
                                $" from the {traps[randomTrap].Name}." +
                                $"\nYour health is now {player1.Health}");

                            CheckGameEnd();//checks if trap killed player
                        }
                        else if (roomContents == 5)
                        {
                            //runs key search method - player must find key to move on
                            Console.WriteLine("You need to search for a key");
                            Console.ReadKey(true);
                            KeySearch();
                            CheckGameEnd();
                        }
                    }
                    else if (roomsPassed > 5)
                        //once player has passed 5 rooms there is a chance that
                        //two monsters may appear in each room
                    {
                        int roomContents = currentRoom.RoomContents();
                        if (roomContents == 1)
                        {
                            //one monster appears in this room
                            Console.WriteLine("Suddenly, you fear you " +
                            "aren't alone in the room...");
                            Console.ReadKey(true);
                            chooseMonster();

                            CheckGameEnd();
                        }
                        else if (roomContents == 2
                            || roomContents == 3)
                        {
                            //two monsters appear in this room
                            Console.WriteLine("Suddenly, you fear you " +
                            "aren't alone in the room...");
                            Console.ReadKey(true);
                            chooseMonster();
                            Console.WriteLine("As you recover from the " +
                                "battle you hear a faint grumbling behind" +
                                " you...you slowly turned and you are met" +
                                " with a large shadow inching towards you");
                            Console.ReadKey(true);
                            chooseMonster();

                            CheckGameEnd();
                        }
                        else if (roomContents == 4)
                        {
                            //a random trap is generated in this room
                            int randomTrap = rnd.Next(0, 3);
                            player1.Health = player1.Health - traps[randomTrap].Damage;
                            Console.WriteLine($"This room has been armed " +
                                $"with a trap! You encounter a" +
                                $" {traps[randomTrap].Name} trap\nYou lose " +
                                $"{traps[randomTrap].Damage} health points" +
                                $" from the {traps[randomTrap].Name}." +
                                $"\nYour health is now {player1.Health}");

                            CheckGameEnd();
                        }
                        else if (roomContents == 5)
                        {
                            //a key search puzzle is generated in this room
                            Console.WriteLine("You need to search for a key");
                            KeySearch();

                            CheckGameEnd();
                        }
                    }
                }
            }
        }
    }
}

