using Projekt;
using System.Collections.Generic;

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

            var players = new List<Participant> {player};
            var gameround = new GameRound(players, dealer);
            gameround.StartGame();
        //gameround.MakeMove();
            bool playAgain = GameRound.EndRound();
            if (playAgain)
            {
            List<Participant> participants = new List<Participant>();
            foreach (Participant p in players) //En lista av både players och dealern, där dealern är längst bak
            {
                participants.Add(p);
            }
            participants.Add(dealer);
            foreach (Participant p in participants) //loopar igenom alla i den här listan och tömmer deras händer på kort
            {
                p.hand.RemoveAllCards();
            }
            deck.ShuffleDeck();
            gameround.StartGame();
            }
            else
            {
                // Exit the game
                Environment.Exit(0);
            }
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