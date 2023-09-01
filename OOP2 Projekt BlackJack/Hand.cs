namespace Projekt 
{
public class Hand
{
    //List of Card
    //Method Sum list of Card Values
    private int handSize;

    public List<Card> HandList {get; set; }
    /*public int HandValue()
    {
        HandList.Sort();
        int handValue = 0;
        foreach(Card card in HandList)
        {
            handValue += card.CardValueTypeToInt();
            if (handValue < 21) 
            {
                int alwaysAce = HandList.Last().CardValueTypeToInt();
                alwaysAce = 1;
            }
        }
        return handValue;*/

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
        string PrintCards = "";
        foreach (Card cards in HandList)
        {
            PrintCards += cards.ToString();
            PrintCards += " ";
        }
        PrintCards += "(" + HandValue() + ")";
        return PrintCards;
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

