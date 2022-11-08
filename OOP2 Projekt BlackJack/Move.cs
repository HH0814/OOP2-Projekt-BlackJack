namespace Projekt
{
    interface IMove
    {
        void MakeMove(bool isMoveLegal);
    }
    interface IIsMoveLegal
    {
        bool IsMoveLegal()
        {
            if (Participant.Player().hand.HandValue() > 21)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
    class Move
    {
    }
    class Stand : IMove, IIsMoveLegal
    {
        //method Is move allowed?
        // method execute move, add zero cards to hand remove zero cards from deck/cardstack
        bool IIsMoveLegal.IsMoveLegal()
        {
            if (Participant.Player().hand.HandValue() > 21)
            {
                Console.WriteLine("You are busted!");
                return false;
                //Har bor vi kanske kasta ut spelaren ur kon
            }
            else
            {
                return true;
            }
        }
        void IMove.MakeMove(bool isMoveLegal)
        {
            if (isMoveLegal)
            {
                Console.WriteLine("You stand!");
                //Do nothing esentially
            }
        }
    }
    class Hit : IMove, IIsMoveLegal
    {
        //method Is move allowed?
        // method execute move, add one card to hand remove one card from deck/cardstack
        bool IIsMoveLegal.IsMoveLegal()
        {
            if (Participant.Player().hand.HandValue() > 21)
            {
                Console.WriteLine("You are busted!");
                return false;
                //Har bor vi kanske kasta ut spelaren ur kon
            }
            else
            {
                return true;
            }
        }
        void IMove.MakeMove(bool isMoveLegal)
        {
            if (isMoveLegal)
            {
                Console.WriteLine("You hit!");
                Participant.Player().hand.AddCard(Participant.Player().deck.PopCard());
            }
        }
    }
    class Double : IMove, IIsMoveLegal
    {
        // method Is move allowed? if hand value is 9 || 10 || 11 
        // method execute move, add one card nonvisible, remove one card from deck/cardstack
        // method increase pot value
        bool IIsMoveLegal.IsMoveLegal()
        {
            if (Participant.Player().hand.HandValue() !> 21)
            {
                if (Participant.Player().hand.HandValue() == 9 || Participant.Player().hand.HandValue() == 10 || Participant.Player().hand.HandValue() == 11 && Participant.Player().chipStack.chipStack > Participant.Player().chipStack.bet)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("You can't double down!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You are busted!");
                return false;
            }
        }

        }
        void IMove.MakeMove(bool isMoveLegal)
        {
            if (isMoveLegal)
            {
                Console.WriteLine("You double down!");
                Participant.Player().hand.AddCard(Participant.Player().deck.PopCard());
                Participant.Player().chipStack.transferChips("placeholder", Participant.Player().chipStack, Participant.Player().chipStack.getBet());
                //jag vet inte riktigt vad jag maste ha som forsta arguemnt i transferChips for att saga att jag vill ha reciever som den allmanna potten.
            }
        }
    }
    class Split : IMove, IIsMoveLegal
    {
        // method Is move allowed?
        // method execute move
        // method add to order
        // method increase pot value
        bool IIsMoveLegal.IsMoveLegal()
        {
            // if player has 21 or more, return false

            // if player hand with 2 cards with the same values, return true
            // else return false

            if (Participant.Player().hand.HandValue() > 21)
            {
                Console.WriteLine("You are busted!");
                return false;
                //Har bor vi kasta ut spelaren ur kon
            }
            else if (Participant.Player().hand.HandList[0].Value == Participant.Player().hand.HandList[1].Value)
            {
                return true;
            }
            else
            {
                System.Console.WriteLine("You can't split!");
                return false;
            }

        }
        void IMove.MakeMove(bool isMoveLegal)
        {
            if (isMoveLegal)
            {
                //Add new player to order
                GameOrder.players.Add(new Player());
                //Add new hand to player
                GameOrder.players[GameOrder.players.Count - 1].Hand.AddCard(Player.Hand.handList[1]);
                //Add new card to new hand
                GameOrder.players[GameOrder.players.Count - 1].hand.deck.PopCard();
                //Remove card from old hand
                Player.Hand.handList.RemoveAt(1);
                //Increase pot value
                //vet ej hur man ska fixa detta annu
                System.Console.WriteLine("You split!");
            }
        }
    }
}

