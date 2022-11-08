using Projekt;

namespace projekt
{
    class Order
    {
        public List<Participant>? participants;

        void AddPlayer(Participant participant)
        {
            participants.Add(participant);
        }
        void RemovePlayer(Participant player)
        {
            participants.Remove(player);
        }
        //Jag vet inte riktikgt hur vi ska lösa detta med att fa ut ordningen.
        List<Participant> CreateOrder()
        {
            //Returnera en lista med spelare i ordning
            participants = new List<Participant>();
            return participants;
        }
    }
}   
