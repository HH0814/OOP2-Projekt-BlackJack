using System.Text;
using static Projekt.Card;

namespace Projekt 
{
public class Hand
{
    private int handSize;

    public List<Card> HandList {get; set; }

    public int HandValue()
    {
        List<Card> orderedList = this.HandList.OrderBy(card => card.CardValueTypeToInt()).ToList(); //orderby tar in en funktion som frågar hur vi betraktar ett värde i listan. Vänster sorteras efter det som vi säger till höger.
        int sum = 0;

        for (int i = 0; i < orderedList.Count; i++)
        {
        Card card = orderedList[i];

        if (card.Value == Card.CardValueType.Ace && sum + card.CardValueTypeToInt() + (this.HandList.Count() - (i + 1) ) > 21)
        {
        sum += 1;
        }
        else
        {
        sum += card.CardValueTypeToInt();
        }
        }
        return sum;
    }
    
    public string PrintHand() 
    {
        Dictionary<SuitType, string> suitColors;
        suitColors = new Dictionary<SuitType, string>
        {
            { SuitType.Hearts, "\u001b[31m" }, // ANSI escape code for red
            { SuitType.Diamonds, "\u001b[34m" }, // ANSI escape code for blue
            { SuitType.Clubs, "\u001b[32m" }, // ANSI escape code for green
            { SuitType.Spades, "\u001b[37m" } // ANSI escape code for gray
        };

        string resetColor = "\u001b[0m"; // ANSI escape code to reset color
        StringBuilder PrintCards = new StringBuilder();

        foreach (Card cards in HandList)
        {
            PrintCards.Append(suitColors[cards.Suit]);
            PrintCards.Append(cards.ToString());
            PrintCards.Append(resetColor); // Reset color for the next card
            PrintCards.Append(" ");
        }
        PrintCards.Append("(" + HandValue() + ")");
        return PrintCards.ToString();
    }
    public int HandSize()
    {
        handSize = HandList.Count;
        return handSize;
    }
    public void AddCard(Card handCard)
    {
        HandList.Add(handCard);
    }
    public Card GetCard(int index)
    {
        Card card = HandList[index];
        return card;
    }
    public void RemoveCard(int index)
    {
        HandList.RemoveAt(index);
    }
    public void RemoveAllCards()
    {
        HandList.Clear();
    }
    public Hand()
    {   
        HandList = new List<Card>();
    }
} 
}  

