namespace Projekt
{
    class GameRound
    {
        public static void StartRound(Participant player, Participant dealer)
        {
            player.DealCards();
            System.Console.WriteLine("Your Hand: " + player.hand.PrintHand()); 
            dealer.DealCards();
            System.Console.WriteLine("Dealers Hand: " + dealer.hand.HandList[0] + " (" + dealer.hand.HandList[0].CardValueTypeToInt() + ")");
            //System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand()); //Test
            if (player.Blackjack())
            {
                System.Console.WriteLine("BLACKJACK! YOU WIN!");
            }


        } 

        public static void MakeMove(Participant player, Participant dealer)
        {
            int move;
            do
            {
                System.Console.WriteLine("Do you want to hit or stand?");
                System.Console.WriteLine("1 = HIT\n2 = STAND");
                //move = int.Parse(Console.ReadLine());
                var input = Console.ReadLine();
                int.TryParse(input, out move);
                switch (move)
                {
                    case 1:
                    player.Hit();
                    System.Console.WriteLine("Your Hand: " + player.hand.PrintHand());   
                    System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand());
                    if(dealer.Blackjack())
                    {
                        System.Console.WriteLine("DEALER HAS BLACKJACK! YOU LOSE!");
                    }
                    if(player.Bust())
                    {
                        System.Console.WriteLine("BUST! YOU LOSE!");
                    } 
                    break;
                    case 2:
                    player.Stand();
                    do
                    {
                        System.Console.WriteLine("Dealer hits.");
                        dealer.Hit();
                        System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand());
                        if(dealer.Bust())
                        {
                            System.Console.WriteLine("DEALER BUST! YOU WIN!");
                        } 
                        else if(dealer.Stand())
                        {
                           System.Console.WriteLine("Dealer stands.");
                           RoundResult(player, dealer);
                           break;
                        }
                    }
                    while (!dealer.Stand() || !dealer.Bust());
                    break;
                    case 3:
                    player.DoubleDown();
                    {
                            player.DoubleDown();
                            System.Console.WriteLine("Your Hand: " + player.hand.PrintHand());
                            System.Console.WriteLine("Dealers Hand: " + dealer.hand.PrintHand());
                            if (dealer.Blackjack())
                            {
                                System.Console.WriteLine("DEALER HAS BLACKJACK! YOU LOSE!");
                            }
                            if (player.Bust())
                            {
                                System.Console.WriteLine("BUST! YOU LOSE!");
                            }
                            
                        }
                    break;
                    default:
                    Console.WriteLine("Invalid input\nPress any key to try again.");
                    Console.ReadKey();
                    break;
                    
                }

            } while (!move.Equals(2) && !player.Bust());
        }
        public static void RoundResult(Participant player, Participant dealer)
        {
            if(player.hand.HandValue() > dealer.hand.HandValue())
            {
                System.Console.WriteLine("YOU WIN!");
            }
            else
            {
                System.Console.WriteLine("YOU LOSE!");
            }
        }

        public static void EndRound()
        {
            System.Console.WriteLine("Round over");
            Environment.Exit(2);
        }
 
        // list of players
        // pot
        // order
        // hands?
        // winner

    }
}
   
