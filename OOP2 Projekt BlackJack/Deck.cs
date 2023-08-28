
namespace Projekt{
public class Deck
{
    private readonly List<Card> _deck = new();
    private IEnumerable<Card> GenerateDeck() //Skapar en kortlek, dvs ett kort av varje valör och färg  
    {
        for (var i = 0; i < Enum.GetValues(typeof(Card.SuitType)).Length; i++)
        {
            for (var j = 0; j < Enum.GetValues(typeof(Card.CardValueType)).Length; j++)
            {
                yield return new Card((Card.SuitType)i, (Card.CardValueType) j); //yield
            }
        }   
    }
    private Random rng = new Random(); 
    public void ShuffleDeck() //Modifierad shufflealgoritm från stackoverflow
    {
        int n = _deck.Count;
        while(n > 1) 
        {
            int k = rng.Next(n);
            n--;
            Card InDeck = _deck[k];
            _deck[k] = _deck[n];
            _deck[n] = InDeck;
        }

    }
    public Deck() //Konstruktor för deck
    {
        _deck = GenerateDeck().ToList();
    }

    public Card PopCard() { //Metod för att plocka ur det första kortet ur leken och returnera den.
        Card firstCard = _deck[0];
        _deck.RemoveAt(0);
        return firstCard; 
    }

    public string PrintDeck() //Metod i testsyfte för att printa ut hela leken   
    { 
        string PrintCard = "";
        foreach (Card cards in _deck)
        {
            PrintCard += cards.ToString();
            PrintCard += "\n";
        }
        return PrintCard;
    }
    
}
}
