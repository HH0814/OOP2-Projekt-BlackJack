using System.Collections.Generic;

namespace Projekt
{
    public class Chips : IObserver<roundoverEvent>
    {
        public int Stack { get; private set; }
        private List<int> bettingAmounts;
        public Chips(int startAmount)
        {
            Stack = startAmount;
            bettingAmounts = new List<int>() { 100, 200, 500};
        }
        private int bet { get; set; } = 0;
        public bool PlaceBet(string playerName)
        {
            Console.WriteLine(playerName + ": Select your bet amount:");
            for (int i = 0; i < bettingAmounts.Count; i++)
            {
                Console.WriteLine(i+1 + ". " + bettingAmounts[i] + " credits");
            }

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice)) //out
            {
                try
                {
                    if (TryPlaceBet(bettingAmounts[choice - 1]))
                    {
                        bet = bettingAmounts[choice - 1];
                        return true;
                    }
                    return false;
                }catch(IndexOutOfRangeException _e)
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                return false;
            }
        }
        public bool TryPlaceBet(int betAmount)
        {
            if (betAmount > Stack)
            {
                Console.WriteLine("You don't have enough credits to place this bet.");
                return false;
            }

            Stack -= betAmount;
            return true;
        }
        public int addChips(int input)
        {
            return Stack += input;
        }
        public int subtractChips(int input)
        {
            return Stack -= input;
        }

        public void Update(roundoverEvent eventData)
        {
            switch (eventData.EventType)
            {
            case roundoverEventType.Win:                   
                    addChips(bet*2);
                    Console.WriteLine(eventData.participant.name + " recieves " + (bet * 2) + " chips");
                    break;
            case roundoverEventType.Tie:
                    addChips(bet);
                    Console.WriteLine(eventData.participant.name + " chips are returned " + "(" + bet + ")" );    
                    break;
            case roundoverEventType.Lose:
                break;
            }
            bet = 0;
        }
    }
}