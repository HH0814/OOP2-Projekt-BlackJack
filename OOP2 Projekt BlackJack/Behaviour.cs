using System.Numerics;

namespace Projekt 
{
    interface IBehaviour // Subtype polymorfism vi har ett interface (typ) IBehaviour och två subtyper playerbehaviour och dealerbehviour
    {
        public bool HitPossible(Hand pHand);
        public bool StandPossible(Hand pHand);
        public bool DoubleDownPossible(Hand pHand, Chips Stack);
        public bool SplitPossible(Hand pHand);
        public void ShowHand(Hand hand, int roundNumber, string playerName);
        public bool BetPossible(Chips Stack);
        public bool AnnounceBlackJack(int turnNr);

    }

    class PlayerBehaviour : IBehaviour
    {
        public bool HitPossible(Hand playerHand) // Metod för spelarens val mellan hit och stand
        {
            int move;
            System.Console.WriteLine("Do you want to hit or stand?");
            System.Console.WriteLine("1 = HIT\n2 = STAND");
            var input = Console.ReadLine();
            int.TryParse(input, out move);
            return move == 1;
        }

        public bool StandPossible(Hand pHand)
        {
            return true;
        }

        public bool DoubleDownPossible(Hand playerHand, Chips playerChipstack)
        {
            int handValue = playerHand.HandValue();
            int handSize = playerHand.HandSize();
            int chipStack = playerChipstack.Stack;
            //int bet = playerChipstack.bet;
            //if (handSize == 2 && (handValue == 9 || handValue == 10 || handValue == 11) && chipStack > bet)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            //return handSize == 2 && (handValue == 9 || handValue == 10 || handValue == 11) && chipStack > bet;
            return handValue == chipStack;
        }

        public bool SplitPossible(Hand playerHand) //Används inte just nu
        {
            int handValue = playerHand.HandValue();
            int handSize = playerHand.HandSize();
            Card card1 = playerHand.GetCard(0);
            Card card2 = playerHand.GetCard(1);
            return handSize == 2 && card1 == card2; //Samma som if else
        }

        public void ShowHand(Hand hand, int roundNumber, string playerName)
        {
            System.Console.WriteLine(playerName + "s'" + " Hand: " + hand.PrintHand());
        }

        public bool BetPossible(Chips Stack)
        {
            throw new NotImplementedException();
        }

        public bool AnnounceBlackJack(int turnNr)
        {
            return turnNr == 1;
        }
    }
    class DealerBehaviour : IBehaviour
    {
        public bool HitPossible(Hand dealerHand)
        {
            int handValue = dealerHand.HandValue();
            return handValue < 17; //Måste hitta om handens värde är under 17
        }

        public bool StandPossible(Hand dealerHand)
        {
            int handValue = dealerHand.HandValue();
            return handValue >= 17; //Måste standa om handens värde är 17 eller över 
        }

        public bool DoubleDownPossible(Hand dealerHand, Chips dealerChipstack)
        {
            return false;
        }

        public bool SplitPossible(Hand dealerHand)
        {
            return false;
        }

        public void ShowHand(Hand hand, int turnNumber, string _playerName)
        {
            if (turnNumber == 1)
            {
                Hand firsthand = new Hand();
                firsthand.AddCard(hand.HandList[0]); // Jag gör detta så att dealern bara visar första kortet första "turnen" 
                System.Console.WriteLine("Dealers Hand: " + firsthand.PrintHand());//hand.HandList[0].pr + " (" + hand.HandList[0].CardValueTypeToInt() + ")");
            }
            else
            {
                System.Console.WriteLine("Dealers Hand: " + hand.PrintHand());
            }
        }

        public bool BetPossible(Chips Stack)
        {
            return false;
        }

        public bool AnnounceBlackJack(int turnNr) //vi vill inte att dealern ska announca blackjack utan att ha visat båda sina kort
        {
            return turnNr > 1;
        }
    }        
}