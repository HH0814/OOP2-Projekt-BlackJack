using Projekt;

namespace projekt
{
    class Order
    {
        public List<Participant>? players;

        void AddPlayer(Participant player)
        {
            players.Add(player);
        }
        void RemovePlayer(Participant player)
        {
            players.Remove(player);
        }
        //Jag vet inte riktikgt hur vi ska lösa detta med att fa ut ordningen.
        public List<Participant> CreateOrder()
        {
            //Returnera en lista med spelare i ordning
            players = new List<Participant>();
            return players;
        }
    }
}   
