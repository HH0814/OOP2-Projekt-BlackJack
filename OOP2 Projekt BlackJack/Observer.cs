using Projekt;
using System;
using System.Collections.Generic;

//Tanken med denna klass är att skapa ett observerpattern som Anton Backe rekommenderade, där det som observeras är eventet där spelaren får blackjack

//Interfacet för observeraren
public interface IObserver<T>
{
    public void Update(T eventData);
}

//Klass för subjektet
public class Subject<T>
{
    List<IObserver<T>> observers = new List<IObserver<T>>();
    public void Attach(IObserver<T> observer)
    {
        observers.Add(observer);
    }
    public void Detach(IObserver<T> observer)
    {
        observers.Remove(observer);
    }
    public void NotifyObservers(T eventData)
    {
        foreach (var observer in observers)
        {
            observer.Update(eventData);
        }
    }
}

// Concrete Observer
public enum blackjackEventType {Blackjack, Bust, Stand}
public class blackjackEvent
{
    public Participant participant;
    public blackjackEventType EventType;
    public blackjackEvent(Participant participant, blackjackEventType EventType)
    {
        this.participant = participant;
        this.EventType = EventType;
    }
}