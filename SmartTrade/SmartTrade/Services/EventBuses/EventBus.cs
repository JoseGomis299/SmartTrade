using System;
using System.Collections.Generic;
using System.Linq;

public class EventBus
{
    private static Dictionary<object, List<string>> _subscriptions = new();
    private static Dictionary<string, List<Event>> _events = new();


    public static void Subscribe(object subscriber, string eventName, Action onEventTriggered)
    {
        if (!_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event not found!");
            return;
        }

        if (!_subscriptions.ContainsKey(subscriber))
        {
            _subscriptions.Add(subscriber, new List<string>());
        }

        _subscriptions[subscriber].Add(eventName);
        _events[eventName].Add(new Event(onEventTriggered));
    }

    public static void Subscribe<T>(object subscriber, string eventName, Action<T> onEventTriggered)
    {
        if (!_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event not found!");
            return;
        }

        if (!_subscriptions.ContainsKey(subscriber))
        {
            _subscriptions.Add(subscriber, new List<string>());
        }

        _subscriptions[subscriber].Add(eventName);
        _events[eventName].Add(new Event(onEventTriggered));
    }

    public static void Unsubscribe(object subscriber, string eventName, Action onEventTriggered)
    {
        if (!_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event not found!");
            return;
        }

        if (!_subscriptions.ContainsKey(subscriber))
        {
            Console.WriteLine("Subscriber not found!");
            return;
        }

        if (!_subscriptions[subscriber].Contains(eventName))
        {
            Console.WriteLine("Subscriber not subscribed to event!");
            return;
        }

        _subscriptions[subscriber].Remove(eventName);
        _events[eventName].FindAll(x => x.Delegate == (Delegate?)onEventTriggered).First().ShouldBeDeleted = true;
    }

    public static void Unsubscribe<T>(object subscriber, string eventName, Action<T> onEventTriggered)
    {
        if (!_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event not found!");
            return;
        }

        if (!_subscriptions.ContainsKey(subscriber))
        {
            Console.WriteLine("Subscriber not found!");
            return;
        }

        if (!_subscriptions[subscriber].Contains(eventName))
        {
            Console.WriteLine("Subscriber not subscribed to event!");
            return;
        }

        _subscriptions[subscriber].Remove(eventName);
        _events[eventName].FindAll(x => x.Delegate == (Delegate?)onEventTriggered).First().ShouldBeDeleted = true;
    }

    public static void UnsubscribeFromAllEvents(object subscriber)
    {
        if (!_subscriptions.ContainsKey(subscriber))
        {
            Console.WriteLine("Subscriber not found!");
            return;
        }

        foreach (var eventName in _subscriptions[subscriber].Distinct())
        {
            //Mark all actions for removal
            for (var i = 0; i < _events[eventName].Count; i++)
            {
                if (_events[eventName][i].Delegate.Target == subscriber)
                {
                    _events[eventName][i].ShouldBeDeleted = true;
                }
            }
        }

        _subscriptions.Remove(subscriber);
    }

    public static void Publish(string eventName)
    {
        if (!_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event not found!");
            return;
        }

        for (var i = 0; i < _events[eventName].Count; i++)
        {
            var Event = _events[eventName][i];
            if (Event.ShouldBeDeleted || Event.Delegate is not Action action) continue;

            action?.Invoke();
        }

        DeleteMarkedEvents(eventName);
    }

    public static void Publish<T>(string eventName, T arg)
    {
        if (!_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event not found!");
            return;
        }

        for (var i = 0; i < _events[eventName].Count; i++)
        {
            var Event = _events[eventName][i];
            if (Event.ShouldBeDeleted || Event.Delegate is not Action<T> action) continue;

            action.DynamicInvoke(arg);
        }

        DeleteMarkedEvents(eventName);
    }

    private static void DeleteMarkedEvents(string eventName)
    {
        for (var i = 0; i < _events[eventName].Count; i++)
        {
            if (_events[eventName][i].ShouldBeDeleted)
            {
                _events[eventName].RemoveAt(i);
                i--;
            }
        }
    }

    public static void RegisterEvent(string eventName)
    {
        if (_events.ContainsKey(eventName))
        {
            Console.WriteLine("Event already exists!");
            return;
        }

        _events.Add(eventName, new List<Event>());
    }
}

public class Event
{
    public bool ShouldBeDeleted { get; set; }
    public Delegate? Delegate { get; set; }

    public Event() { }

    public Event(Delegate? del)
    {
        Delegate = del;
        ShouldBeDeleted = false;
    }
}

