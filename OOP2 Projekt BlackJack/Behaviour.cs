namespace Projekt 
{
    interface IBehaviour // Subtype polymorfism vi har ett interface (typ) IBehaviour och två subtyper playerbehaviour och dealerbehviour
    {
        public bool HitPossible(Hand pHand);
        public bool StandPossible(Hand pHand);
        public bool DoubleDownPossible(Hand pHand);
        public bool SplitPossible(Hand pHand);

    }

    class PlayerBehaviour : IBehaviour
    {
        public bool HitPossible(Hand playerHand)
        {
            return true;
        }
        public bool StandPossible(Hand pHand)
        {
            return true;
        }
        public bool DoubleDownPossible(Hand pHand)
        {
            return true;
        }
        public bool SplitPossible(Hand pHand)
        {
            return true;
        }
    }
    class DealerBehaviour : IBehaviour
    {
        public bool HitPossible(Hand dealerHand)
        {
            int handValue = dealerHand.HandValue();
            if (handValue > 17)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool StandPossible(Hand dealerHand)
        {
            int handValue = dealerHand.HandValue();
            if (handValue > 17)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DoubleDownPossible(Hand dealerHand)
        {
            return false;
        }
        public bool SplitPossible(Hand dealerHand)
        {
            return false;
        }
        public bool DoubleDownPossible(Hand playerHand, Chips playerChipstack)
        {
            int handValue = playerHand.HandValue();
            int handSize = playerHand.HandSize();
            int chipStack = playerChipstack.chipStack;
            int bet = playerChipstack.bet;
            if (handSize == 2 && (handValue == 9 || handValue == 10 || handValue == 11) && chipStack > bet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SplitPossible(Hand playerHand)
        {
            int handValue = playerHand.HandValue();
            int handSize = playerHand.HandSize();
            Card card1 = playerHand.GetCard(0);
            Card card2 = playerHand.GetCard(1);
            if (handSize == 2 && card1 == card2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

        
}