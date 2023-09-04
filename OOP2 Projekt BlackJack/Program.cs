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
            int playerCount;
            while(true)
            {
            Console.WriteLine("How many players wants to play?");
            try 
            { 
                playerCount = int.Parse(Console.ReadLine());
                break;
            }
            catch { Console.WriteLine("Invalid input, try again with a number"); }
            }

            var players = new List<Participant>();
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(Participant.Player(deck));
            } 
            //var gameround = new GameRound(players, dealer);
            int roundNumber = 0;
            //gameround.StartGame(roundNumber);
            bool playAgain = true;
            while (playAgain && !players.Any(p=>p.chipstack.Stack == 0))
            {
                var gameround = new GameRound(players, dealer);
                //Om rou
                if (roundNumber == 0)
                {
                gameround.initGame();
                }
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