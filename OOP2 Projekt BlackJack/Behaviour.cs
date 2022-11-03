namespace Projekt 
{
    interface IBehaviour // Subtype polymorfism vi har ett interface (typ) IBehaviour och två subtyper playerbehaviour och dealerbehviour
    {
        public bool HitPossible(Hand pHand);
        public bool StandPossible(Hand pHand);
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
    }

        
}