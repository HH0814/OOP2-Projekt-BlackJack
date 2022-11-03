namespace Projekt{
    class Participant
    {
        private IBehaviour behaviour;
        public Hand hand;
        public Deck deck;
        public string name = "";
        public void SetName(string inputNameString){
            name = "PlayerName"; //inputNameString;
        }
        //public Chips chipStack {get; set; }
        //public int Bet {get; set; }
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