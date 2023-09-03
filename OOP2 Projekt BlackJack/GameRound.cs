using System.Linq.Expressions;
using System.Numerics;
using static Projekt.Card;

namespace Projekt
{
    class GameRound : IObserver<blackjackEvent>
    {
        private List<Participant> players = new List<Participant>(); //Lista av Participant som ineh�ller b�de spelare och dealers
        private Participant dealer;
        public GameRound(List<Participant> players, Participant dealer) //En lista av players, loopa igenom alla i den h�r listan och attacha till observern
        {  
            this.players = players; 
            this.dealer = dealer;
            foreach (Participant p in this.players) 
            {
                p.Attach(this); //Subjekten player och dealer har f�tt GameRound som observer
            }
            dealer.Attach(this); //Attachar dealer till observer
        }

    public void StartGame(int roundNumber)
        {
            int turnNumber = 1;
            if (roundNumber == 0)
            {
                foreach (Participant p in players)
                {
                    p.SetPlayerName();
                }
                dealer.name = "Dealer"; //AUUUUUUGH
            }
            if (turnNumber == 1)
            {
                foreach (Participant p in players)
                {
                    p.printChipStack();
                    //p.chipstack.PlaceBet();
                }
            }
                foreach (Participant p in players)
            {
                p.DealCards(turnNumber);
                //p.ShowHand(turnNumber);
            }
            dealer.DealCards(turnNumber);
            //dealer.ShowHand(turnNumber);
            while (players.Any(p => !p.done) || !dealer.done) //Loopar s� l�nge ingen player i listan eller dealern standar eller bustar eller f�r Blackjack
            {
                turnNumber++;
                foreach (Participant p in players)
                {
                    if(!p.done)
                    {
                        p.Hit(turnNumber);
                    }
                    else
                    {
                        dealer.ShowHand(turnNumber);
                        dealer.Hit(turnNumber);
                    }
                }
            }
            RoundResult(roundNumber); //L�gg kanske in hur m�nga rundor som spelades
        } 

        //public void MakeMove()
        //{
        //    int move;
        //    do
        //    {
        //        //System.Console.WriteLine("Do you want to hit or stand?");
        //        //System.Console.WriteLine("1 = HIT\n2 = STAND");
        //        //move = int.Parse(Console.ReadLine());
        //        //var input = Console.ReadLine();
        //        //int.TryParse(input, out move);
        //        switch (move)
        //        {
        //            case 1:
        //            player.Hit();
        //            System.Console.WriteLine("Your Hand: " + player.hand.PrintHand());   
        //            System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand());
        //            if(dealer.Blackjack())
        //            {
        //                System.Console.WriteLine("DEALER HAS BLACKJACK! YOU LOSE!");
        //            }
        //            if(player.Bust())
        //            {
        //                System.Console.WriteLine("BUST! YOU LOSE!");
        //            } 
        //            break;
        //            case 2:
        //            player.Stand();
        //            do
        //            {
        //                System.Console.WriteLine("Dealer hits.");
        //                dealer.Hit();
        //                System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand());
        //                if(dealer.Bust())
        //                {
        //                    System.Console.WriteLine("DEALER BUST! YOU WIN!");
        //                } 
        //                else if(dealer.Stand())
        //                {
        //                   System.Console.WriteLine("Dealer stands.");
        //                   RoundResult(player, dealer);
        //                   break;
        //                }
        //            }
        //            while (!dealer.Stand() || !dealer.Bust());
        //            break;
        //            case 3:
        //            player.DoubleDown();
        //            {
        //                    player.DoubleDown();
        //                    System.Console.WriteLine("Your Hand: " + player.hand.PrintHand());
        //                    System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand());
        //                    if (dealer.Blackjack())
        //                    {
        //                        System.Console.WriteLine("DEALER HAS BLACKJACK! YOU LOSE!");
        //                    }
        //                    if (player.Bust())
        //                    {
        //                        System.Console.WriteLine("BUST! YOU LOSE!");
        //                    }
                            
        //                }
        //            break;
        //            default:
        //            Console.WriteLine("Invalid input\nPress any key to try again.");
        //            Console.ReadKey();
        //            break;
                    
        //        }

        //    } while (!move.Equals(2) && !player.Bust());
        //}
        public void RoundResult(int roundNumber)
        {
            Console.WriteLine("\n" + "Round " + roundNumber + " Results:");
            for (int i = 0; i < players.Count; i++)
            {
                Participant player = (Participant)players[i];
                if (player.Bust())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " BUST!");
                }
                else if(dealer.Bust())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " WON!");
                }
                else if (player.hand.HandValue() > dealer.hand.HandValue())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " WON!");
                }
                else if (player.hand.HandValue() < dealer.hand.HandValue())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " LOST!");
                }
                else
                {
                    System.Console.WriteLine("TIE!" + " PLAYER " + (i + 1) + ": " + player.name + " PUSH");
                }
            }
        }

        public bool EndRound()
        {
            System.Console.WriteLine("\n" + "Round over");
            Console.Write("\n" + "Do you want to play another round? (Y/n): ");
            string response = Console.ReadLine().Trim();

            if (string.Equals(response, "Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                return true; // The player wants to play again
            }
            else
            {
                return false; // The player does not want to play again
            }
        }

        public void Update(blackjackEvent eventData)
        {
            switch (eventData.EventType)
                {
                    case blackjackEventType.Blackjack:
                        Console.WriteLine(eventData.participant.name + " HAS BLACKJACK");
                        break;
                    case blackjackEventType.Bust:
                        Console.WriteLine(eventData.participant.name + " BUST");
                    if (players.All(p => p.Bust())) //N�r eventet att alla spelare har bustat sker, s� betyder det att dealern �r klar
                        {
                            dealer.done = true;
                        }
                        break;
                    case blackjackEventType.Stand:
                            Console.WriteLine(eventData.participant.name + " STANDS");
                        break;
                }
                eventData.participant.done = true; //s�tt en flagga f�r vad som har h�nt
        }

        // list of players
        // pot
        // order
        // hands?
        // winner

    }
}
   
