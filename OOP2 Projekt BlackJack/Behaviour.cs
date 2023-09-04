using System.Numerics;

namespace Projekt 
{
    interface IBehaviour // Subtype polymorfism vi har ett interface (typ) IBehaviour och två subtyper playerbehaviour och dealerbehviour
    {
        public bool HitPossible(Participant p);
        public void ShowHand(Hand hand, int roundNumber, string playerName);
        public bool BetPossible(Chips Stack);
        public bool AnnounceBlackJack(int turnNr);
    }

    class PlayerBehaviour : IBehaviour
    {
        public bool HitPossible(Participant p) // Metod för spelarens val mellan hit och stand
        {
            int move;
            System.Console.WriteLine("\n" + p.name + ": Do you want to hit or stand?");
            System.Console.WriteLine("1 = HIT\n2 = STAND");
            var input = Console.ReadLine();
            int.TryParse(input, out move);
            return move == 1;
        }

        public void ShowHand(Hand hand, int roundNumber, string playerName)
        {
            System.Console.WriteLine("\n" + playerName + "s'" + " Hand: " + hand.PrintHand());
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
        public bool HitPossible(Participant p)
        {
            int handValue = p.hand.HandValue();
            return handValue < 17; //Måste hitta om handens värde är under 17
        }

        public void ShowHand(Hand hand, int turnNumber, string _playerName)
        {
            if (turnNumber == 1)
            {
                Hand firsthand = new Hand();
                firsthand.AddCard(hand.HandList[0]); // Jag gör detta så att dealern bara visar första kortet första "turnen" 
                System.Console.WriteLine("\n" + "Dealers Hand: " + firsthand.PrintHand());
            }
            else
            {
                System.Console.WriteLine("\n" + "Dealers Hand: " + hand.PrintHand());
            }
        }

        public bool BetPossible(Chips Stack)
        {
            return false;
        }

        public bool AnnounceBlackJack(int turnNr) // Dealern ska inte announca blackjack utan att ha visat båda sina kort
        {
            return turnNr > 1;
        }
    }        
}