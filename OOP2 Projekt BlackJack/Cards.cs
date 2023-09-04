namespace Projekt
{
    public class Card
    {
        //Name
        //Value
        public enum CardValueType { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }
        //public enum CardValueType { Ace, Ten, Jack, Queen, King } //TEST
        public CardValueType Value { get; set; }
        public enum SuitType
        {
            Spades, Hearts, Clubs, Diamonds
        }
        //Suit
        public SuitType Suit { get; set; }
        public Card(SuitType suitType, CardValueType cardValueType) //Constructor
        {
            Suit = suitType;
            Value = cardValueType;
        }

        public Guid CardId {get; } = Guid.NewGuid();
 
        public override string ToString()
        {
            return $"{Value} of {Suit}.";
        }
        public int CardValueTypeToInt()
        {
            switch (Value)
            {
                case CardValueType.Ace: //Maste hanteras som ess alltsa 1 eller 10
                    return 11;
                case CardValueType.Two:
                    return 2;
                case CardValueType.Three:
                    return 3;
                case CardValueType.Four:
                    return 4;
                case CardValueType.Five:
                    return 5;
                case CardValueType.Six:
                    return 6;
                case CardValueType.Seven:
                    return 7;
                case CardValueType.Eight:
                    return 8;
                case CardValueType.Nine:
                    return 9;
                case CardValueType.Ten:
                    return 10;
                case CardValueType.Jack:
                    return 10;
                case CardValueType.Queen:
                    return 10;
                case CardValueType.King:
                    return 10;
                default:
                    return 0;
            }
        }
    }
 
}
