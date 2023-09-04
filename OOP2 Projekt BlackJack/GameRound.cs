using System.Linq.Expressions;
using System.Numerics;
using static Projekt.Card;

namespace Projekt
{
    class GameRound : Subject<roundoverEvent>, IObserver<blackjackEvent> 
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
                //this.Attach(p.chipstack); //Subjektet (gameround) lägger till observern (Chips) som en observer
            }
            dealer.Attach(this); //Attachar dealer till observer
        }
        public void initGame()
        {
            foreach (Participant p in players)
            {
                p.SetPlayerName();
            }
            dealer.name = "Dealer"; //Lite fult men det funkar
        }
        public void StartGame(int roundNumber)
        {
            int turnNumber = 1;

            if (turnNumber == 1)
            {
                foreach (Participant p in players)
                {
                    p.printChipStack();
                    p.chipstack.PlaceBet(p.name);
                }
            }

            foreach (Participant p in players)
            {
                p.DealCards(turnNumber);
            }
            dealer.DealCards(turnNumber);

            while (players.Any(p => !p.done) || !dealer.done) //Loopar så länge ingen player i listan eller dealern standar eller bustar eller får Blackjack
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
            RoundResult(roundNumber); //Lägg kanske in hur många rundor som spelades
        } 

        public void RoundResult(int roundNumber)
        {
            Console.WriteLine("\n" + "Round " + (roundNumber+1) + " Results:");
            for (int i = 0; i < players.Count; i++)
            {
                Participant player = (Participant)players[i];
                if (player.Bust())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " BUST!");
                    this.NotifyObserver(new roundoverEvent(player, roundoverEventType.Lose), player.chipstack);
                }
                else if(dealer.Bust())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " WON!");
                    this.NotifyObserver(new roundoverEvent(player, roundoverEventType.Win), player.chipstack);
                }
                else if (player.hand.HandValue() > dealer.hand.HandValue())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " WON!");
                    this.NotifyObserver(new roundoverEvent(player, roundoverEventType.Win), player.chipstack);
                }
                else if (player.hand.HandValue() < dealer.hand.HandValue())
                {
                    System.Console.WriteLine("PLAYER " + (i + 1) + ": " + player.name + " LOST!");
                    this.NotifyObserver(new roundoverEvent(player, roundoverEventType.Lose), player.chipstack);
                }
                else
                {
                    System.Console.WriteLine("TIE!" + " PLAYER " + (i + 1) + ": " + player.name + " PUSH");
                    this.NotifyObserver(new roundoverEvent(player, roundoverEventType.Tie), player.chipstack);
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
                return true; //Spelaren vill spela igen
            }
            else
            {
                return false; //Spelaren vill inte spela igen
            }
        }

        public void Update(blackjackEvent eventData)
        {
            if (eventData.participant.done)
            {
                return; //Om participanten redan är klar så kommer inget att hända
            }  
            eventData.participant.done = true; //sätt en flagga för vad som har hänt
            switch (eventData.EventType)
            {
                case blackjackEventType.Blackjack:
                    Console.WriteLine(eventData.participant.name + " HAS BLACKJACK");
                    break;
                case blackjackEventType.Bust:
                    Console.WriteLine(eventData.participant.name + " BUST");
                if (players.All(p => p.Bust())) //När eventet att alla spelare har bustat sker, så betyder det att dealern är klar
                    {
                        dealer.done = true;
                    }
                    break;
                case blackjackEventType.Stand:
                    Console.WriteLine(eventData.participant.name + " STANDS");
                    break;
            }
        }

        // list of players
        // pot
        // order
        // hands?
        // winner

    }
}
   
