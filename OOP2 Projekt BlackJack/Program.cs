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
            gameround.StartRound();
            //gameround.MakeMove();
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