using Projekt;
class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            deck.ShuffleDeck();
            var dealer = Participant.Dealer(deck);
            var player = Participant.Player(deck);
            //int playerMove;
            Console.WriteLine("Welcome to Blackjack!");
            GameRound.StartRound(player,dealer);
            GameRound.MakeMove(player,dealer);
            GameRound.EndRound();
            //System.Console.WriteLine("Input player name");
            //Console.WriteLine(deck.PrintDeck());
            //Console.WriteLine($"There are {dealer.GetDeck().Count} cards in the deck");
            //while (Console.ReadKey().Key != ConsoleKey.Escape)
            //{
                //try
                //{
                    
                //}
                //catch(Exception e)
                //{
                //    Console.WriteLine(e);
                //}
            //}
        }
    }