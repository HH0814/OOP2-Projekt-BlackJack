﻿using Projekt;
using System;
using System.Collections.Generic;

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
    public void NotifyObserver(T eventData, IObserver<T> observer)
    {
        observer?.Update(eventData);
    }
}

public enum blackjackEventType { Blackjack, Bust, Stand }
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
public enum roundoverEventType { Win, Tie, Lose }
public class roundoverEvent
{
    public Participant participant;
    public roundoverEventType EventType;
    public roundoverEvent(Participant participant, roundoverEventType EventType)
    {
        this.participant = participant;
        this.EventType = EventType;
    }
}