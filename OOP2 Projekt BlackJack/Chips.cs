using System.Collections.Generic;

namespace Projekt
{
    public class Chips
    {
        public int Stack { get; set; } = 1000;
        public int bet { get; set; }
        public bool PlaceBet()
        {
            Console.WriteLine("Select your bet amount:");
            Console.WriteLine("1. 100 credits");
            Console.WriteLine("2. 200 credits");
            Console.WriteLine("3. 500 credits");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        return TryPlaceBet(100);
                    case 2:
                        return TryPlaceBet(200);
                    case 3:
                        return TryPlaceBet(500);
                    default:
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

        private bool TryPlaceBet(int betAmount)
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

        public int emptyChipStack()
        {  //Ar denna ens nodvandig? Kanske for att tomma sjalva potten men borde ju hanteras med transferChips.
            return Stack = 0;
        }
        public void transferChips(Chips receivingChipStack, Chips givingChipStack, int input)
        {
            bet = input;
            receivingChipStack.addChips(input);
            givingChipStack.subtractChips(input);
        }
        public string printChipStack()
        {
            return Stack.ToString();
        }
        public string printBet()
        {
            return bet.ToString();
        }
        public int getBet()
        {
            return bet;
        }
        public int getChipstack()
        {
            return Stack;
        }
    }
}