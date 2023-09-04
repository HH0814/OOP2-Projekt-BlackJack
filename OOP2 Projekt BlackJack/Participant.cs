using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Projekt{
    public class Participant : Subject<blackjackEvent>
    {
        private IBehaviour behaviour;
        public Hand hand;
        public Deck deck;
        public bool done { set; get; }
        public Chips chipstack = new Chips(1000); //Här får varje deltagare en egen chipstack 
        public string name = "";
        public string SetPlayerName(){
            string playerName;  
            do
            {
                Console.Write("Please enter your name: ");
                playerName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(playerName));
            name = playerName;
            return playerName;
        }
        public int Wins { get; set; } = 0;
        public int RoundsCompleted { get; set; } = 1;
        public void Hit(int _turnNumber)
        {
            if (behaviour.HitPossible(this))
            {
                Console.WriteLine("\n" + name + " Hits");
                Card newCard = deck.PopCard();
                hand.AddCard(newCard);
                ShowHand(_turnNumber);
                if (Bust())
                {
                    NotifyObservers(new blackjackEvent(this,blackjackEventType.Bust));
                }
            }
            else
            {
                NotifyObservers(new blackjackEvent(this, blackjackEventType.Stand));
            }
        }

        public void DealCards(int turnNumber)
        {
            for (int i = 0; i < 2; i++)
            {
                hand.AddCard(deck.PopCard()); 
            }
            ShowHand(turnNumber);
        }
        public void ShowHand(int turnNumber)
        {
            behaviour.ShowHand(hand, turnNumber, name);
            if (Blackjack() && behaviour.AnnounceBlackJack(turnNumber))
            {
                NotifyObservers(new blackjackEvent(this, blackjackEventType.Blackjack));
            }
        }

        public bool Bust()
        {
            return hand.HandValue() > 21;
        }

        public bool Blackjack()
        {
            return hand.HandSize() == 2 && hand.HandValue() == 21;

        }

        public void printChipStack()
        {
            string stackString = chipstack.Stack.ToString();
            Console.WriteLine("\n" + name + " Chipstack: " + stackString);
        }

        public static Participant Dealer(Deck deck)
        {   
            return new Participant(new DealerBehaviour(), deck);
        }
        
        public static Participant Player(Deck deck)
        {   
            return new Participant(new PlayerBehaviour(), deck);
        }

        private Participant(IBehaviour behaviour, Deck deck)
        {
            this.behaviour = behaviour;
            this.deck = deck;
            hand = new Hand();
        }

    }

    
}