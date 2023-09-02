using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Projekt{
    public class Participant : Subject<blackjackEvent>
    {
        private IBehaviour behaviour;
        //bor vi egentligen ha public participant participant; isallet for hand deck och chips. eller gar det inte pga deck?
        public Hand hand;
        public Deck deck;
        public bool done { set; get; }
        public Chips chipstack { get; set; }//vi behover denna for att kunna gora split och double ar jag ratt saker pa. 
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
            if (behaviour.HitPossible(hand))
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
        public bool Stand()
        {
            return behaviour.StandPossible(hand);
        }

        public void DealCards(int turnNumber)
        {
            for (int i = 0; i < 2; i++)
            {
                hand.AddCard(deck.PopCard()); 
            }
            ShowHand(turnNumber);
            //if (Blackjack() && behaviour.AnnounceBlackJack(turnNumber))
            //{     
            //    NotifyObservers(new blackjackEvent(this, blackjackEventType.Blackjack));
            //}
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

        public void DoubleDown()
        {
            if (behaviour.DoubleDownPossible(hand, chipstack))
            {
                //jag tror dett ar ratt
                Player(deck).chipstack.Stack -= Player(deck).chipstack.bet;
                Player(deck).chipstack.bet *= 2;
                //Hit();
            }
            else
            {
                Stand();
            }
        }

        public Participant? Split()
        {
            if (behaviour.SplitPossible(hand))
            {
                Participant splitplayer = new Participant(new PlayerBehaviour(), deck); //instansierar en ny player som spelar splithanden
                splitplayer.chipstack.bet = Participant.Player(deck).chipstack.bet; /*Har vill vi ha bet av spelaren, har ej fattat ratt anrop an*/
                
                //remove chips from player nedan. Jag tror anropet ar fel dock...
                Player(deck).chipstack.Stack = Player(deck).chipstack.Stack - Player(deck).chipstack.bet;
                
                splitplayer.chipstack.Stack = 0; //bor antagligen vara 0
                splitplayer.hand.AddCard(hand.GetCard(1)); //Ger andra kortet i spelarhanden till splithanden
                hand.RemoveCard(1); //Detta bor ta bort korten fran orginalspelaren 
                //splitplayer.DealCards(); //Detta dealar ut ett nytt kort till splithanden
                return splitplayer; //returnerr splithanden/splitspelaren
            }
            else
            {
                Console.WriteLine("Something went wrong"); //Ersatt
                return null;
            }
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