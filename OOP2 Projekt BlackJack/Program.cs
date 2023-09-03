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
            
            Console.WriteLine("Welcome to Blackjack!");

            var players = new List<Participant> {player};
            //var gameround = new GameRound(players, dealer);
            int roundNumber = 0;
            //gameround.StartGame(roundNumber);
            bool playAgain = true;
            while (playAgain)
            {
            var gameround = new GameRound(players, dealer);
            gameround.StartGame(roundNumber);
            playAgain = gameround.EndRound();
            roundNumber++;
            deck = new Deck();
            deck.ShuffleDeck();
            List<Participant> participants = new List<Participant>();
            foreach (Participant p in players) //En lista av både players och dealern, där dealern är längst bak
            {
                participants.Add(p);
            }
            participants.Add(dealer);
            foreach (Participant p in participants) //loopar igenom alla i den här listan och tömmer deras händer på kort
            {
                p.hand.RemoveAllCards();
                p.deck = deck;
                p.done = false;
                p.Detach(gameround);
            }
            }
            Environment.Exit(0); //Spelet avslutas

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