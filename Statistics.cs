using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DungeonExplorer
{
    internal class Statistics
    {
        //Statistics class attributes
        private int monstersDefeated;
        private int healthRemaining;
        private int roomsNavigated;
        private int potionsConsumed;
        private int weaponsUsed;
        private int strength;
        private List<string> inventory;

        //Encapsulation - using getters and setters for player class attributes
        public int MonstersDefeated
        {
            get { return monstersDefeated; }
            set { monstersDefeated = value; }
        }

        public int HealthRemaining
        {
            get { return healthRemaining; }
            set { healthRemaining = value; }
        }

        public int RoomsNavigated
        {
            get { return roomsNavigated; }
            set { roomsNavigated = value; }
        }

        public int PotionsConsumed
        {
            get { return potionsConsumed; }
            set { potionsConsumed = value; }
        }

        public int WeaponsUsed
        {
            get { return weaponsUsed; }
            set { weaponsUsed = value; }
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value;  }
        }

        //Statistics constructor for end of game
        public Statistics(int monstersDefeated, int healthRemaining, int roomsNavigated, int potionsConsumed, int weaponsUsed) 
        {
            monstersDefeated = MonstersDefeated;
            healthRemaining = HealthRemaining;
            roomsNavigated = RoomsNavigated;
            potionsConsumed = PotionsConsumed;
            weaponsUsed = WeaponsUsed;
        }

        //Statistics constructor for during game
        public Statistics(int healthRemaining, int roomsNavigated, int strength)
        {
            healthRemaining = HealthRemaining;
            roomsNavigated = RoomsNavigated;
            strength = Strength;
        }

        //Procedure to show the user the player stats at the end of the game
        public void EndPlayerStats()
        {
            Console.WriteLine($"Displayed below are your statistics from " +
                $"the game:\nNumber of monsters defeated:" +
                $" {monstersDefeated}\nTotal health points" +
                $" remaining: {healthRemaining}\nTotal number of " +
                $"rooms navigated: {roomsNavigated}\nTotal number of" +
                $" potions consumed: {potionsConsumed}\nTotal number of " +
                $"weapons used: {weaponsUsed}");
        }

        //procedure to display player's stats (health, strength etc...)
        public void CurrentPlayerStats(string playerName)
        {
            Console.WriteLine($"{playerName}'s health = {healthRemaining}\n{playerName}'s strength = {strength}\n{playerName} is in room number {roomsNavigated - 1}");
        }
    }
}
