using System.Linq.Expressions;

namespace Projekt
{
    class GameRound : IObserver<blackjackEvent>
    {
        private List<Participant> players = new List<Participant>(); //Lista av Participant som ineh�ller b�de spelare och dealers
        private Participant dealer;
        public GameRound(List<Participant> players, Participant dealer) //En lista avplayers, loopa igenom alla i den h�r listan och attacha till observern
        {  
            this.players = players; 
            this.dealer = dealer;
            foreach (Participant p in this.players) 
            {
                p.Attach(this); //Subjekten player och dealer har f�tt GameRound som observer
            }
            dealer.Attach(this); //Attachar dealer till observer
        }

    public void StartRound()
        {
            List<Participant> participants = new List<Participant>();
            foreach (Participant p in this.players) //En lista av b�de players och dealern, d�r dealern �r l�ngst bak, loopa igenom alla i den h�r listan
            {
                participants.Add(p);
            }
            participants.Add(dealer);
            int roundNumber = 0;
            foreach (Participant p in participants)
            {
                p.DealCards();
                p.ShowHand(roundNumber);
            }
            //System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand()); //Test
            while (participants.Any(p => !p.done)) //Loopar s� l�nge ingen participant i listan standar eller bustar eller f�r Blackjack
            {
                roundNumber++;
                foreach (Participant p in participants)
                {
                    if(!p.done)
                    {
                        p.Hit();
                        p.ShowHand(roundNumber);
                    }
                }
            }
            RoundResult(); //L�gg kanske in hur m�nga rundor som spelades
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
                    System.Console.WriteLine("PLAYER " + (i + 1) + " BUST!");
                }
                else if(dealer.Bust())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + " WON!");
                }
                else if (player.hand.HandValue() > dealer.hand.HandValue())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + " WON!");
                }
                else if (player.hand.HandValue() < dealer.hand.HandValue())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + " LOST!");
                }
                else
                {
                    System.Console.WriteLine("TIE!" + " PLAYER " + (i + 1) + " PUSH");
                }
            }    
        }

        public static void EndRound()
        {
            System.Console.WriteLine("Round over");
            //System.Console.WriteLine("Do you want to play again? Y/n");
            Environment.Exit(2);
        }

        public void Update(blackjackEvent eventData)
        {
            switch (eventData.EventType)
            {
                case blackjackEventType.Blackjack:
                    Console.WriteLine("blackjack");
                    break;
                case blackjackEventType.Bust:
                    Console.WriteLine("bust");
                    if (players.All(p => p.Bust())) //N�r eventet att alla spelare har bustat sker s� betyder det att dealern �r klar
                    {
                        dealer.done = true;
                    }
                    break;
                case blackjackEventType.Stand:
                    Console.WriteLine("stand");
                    break;
            }
            eventData.participant.done = true; //s�tt en flagga f�r vad som har h�nt
            //Switchcase f�r vem som vinner typ, vilket leder till n�gonting
        }

        // list of players
        // pot
        // order
        // hands?
        // winner

    }
}
   
