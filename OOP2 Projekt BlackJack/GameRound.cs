using System.Linq.Expressions;

namespace Projekt
{
    class GameRound : IObserver<blackjackEvent>
    {
        private List<Participant> players = new List<Participant>(); //Lista av Participant som inehåller både spelare och dealers
        private Participant dealer;
        public GameRound(List<Participant> players, Participant dealer) //En lista av players, loopa igenom alla i den här listan och attacha till observern
        {  
            this.players = players; 
            this.dealer = dealer;
            foreach (Participant p in this.players) 
            {
                p.Attach(this); //Subjekten player och dealer har fått GameRound som observer
            }
            dealer.Attach(this); //Attachar dealer till observer
        }

    public void StartGame()
        {
            //List<Participant> participants = new List<Participant>();
            //foreach (Participant p in this.players) //En lista av både players och dealern, där dealern är längst bak, loopa igenom alla i den här listan
            //{
            //    participants.Add(p);
            //}
            //participants.Add(dealer);
            int roundNumber = 0;
            if (roundNumber == 0)
            {


                foreach (Participant p in players)
                {
                    p.SetPlayerName();
                }
                dealer.name = "Dealer";
            }
            foreach (Participant p in players)
            {
                p.DealCards();
                p.ShowHand(roundNumber);
            }
            dealer.DealCards();
            dealer.ShowHand(roundNumber);
            while (players.Any(p => !p.done) || !dealer.done) //Loopar så länge ingen participant i listan standar eller bustar eller får Blackjack
            {
                roundNumber++;
                foreach (Participant p in players)
                {
                    if(!p.done)
                    {
                        p.Hit();
                        p.ShowHand(roundNumber);
                    }
                    else
                    {
                        dealer.Hit();
                        Console.WriteLine();
                        dealer.ShowHand(roundNumber);
                    }
                }
            }
            RoundResult(); //Lägg kanske in hur många rundor som spelades
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
        public void RoundResult()
        {
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

        public static bool EndRound()
        {
            System.Console.WriteLine("Round over");
            Console.Write("Do you want to play again? (Y/n): ");
            string response = Console.ReadLine().Trim();

            if (string.Equals(response, "Y", StringComparison.OrdinalIgnoreCase))
            {
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
                        //Console.WriteLine(player.name + " HAS BLACKJACK");
                        break;
                    case blackjackEventType.Bust:
                        //Console.WriteLine(player.name + " BUST");
                        if (players.All(p => p.Bust())) //När eventet att alla spelare har bustat sker, så betyder det att dealern är klar
                        {
                            dealer.done = true;
                        }
                        break;
                    case blackjackEventType.Stand:
                        //Console.WriteLine(player.name + " Stands");
                        break;
                }
                eventData.participant.done = true; //sätt en flagga för vad som har hänt
        }

        // list of players
        // pot
        // order
        // hands?
        // winner

    }
}
   
