namespace Projekt{
    class Participant
    {
        private IBehaviour behaviour;

        //bor vi egentligen ha public participant participant; isallet for hand deck och chips. eller gar det inte pga deck?
        public Hand hand;
        public Deck deck;
        public Chips chipstack; //vi behover denna for att kunna gora split och double ar jag ratt saker pa. 
        public string name = "";
        public void SetName(string inputNameString){
            name = "PlayerName"; //inputNameString;
        }
        public Chips chipStack {get; set; }
        public int Wins { get; set; }
        public int RoundsCompleted { get; set; } = 1;
        public void Hit()
        {
            if (behaviour.HitPossible(hand))
            {
                Card newCard = deck.PopCard();
                hand.AddCard(newCard);
            }
            else
            {
                Stand();
            }
        }
        public bool Stand()
        {
            if (behaviour.StandPossible(hand))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DealCards()
        {
            for (int i = 0; i < 2; i++)
            {
                hand.AddCard(deck.PopCard()); 
            }
        }

        public bool Bust()
        {
            if (hand.HandValue() > 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Blackjack()
        {
            if (hand.HandSize() == 2 && hand.HandValue()== 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DoubleDown()
        {
            if (behaviour.DoubleDownPossible(hand, chipstack))
            {
                //chipStack -= bet;
                //bet *= 2;
                //Hit();
            }
            else
            {
                Stand();
            }
        }

        public Participant Split()
        {
            if (behaviour.SplitPossible(hand))
            {
                Participant splitplayer = new Participant(new PlayerBehaviour(), deck); //instansierar en ny player som spelar splithanden
                splitplayer.chipstack.bet = 1000; /*Har vill vi ha bet av spelaren, har ej fattat ratt anrop an*/
                splitplayer.chipstack.chipStack = 0; //bor antagligen vara 0
                splitplayer.hand.AddCard(hand.GetCard(1)); //Ger andra kortet i spelarhanden till splithanden
                hand.RemoveCard(1); //Detta bor ta bort korten fran orginalspelaren 
                splitplayer.DealCards(); //Detta dealar ut ett nytt kort till splithanden
                return splitplayer; //returnerr splithanden/splitspelaren
            }
            else
            {
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