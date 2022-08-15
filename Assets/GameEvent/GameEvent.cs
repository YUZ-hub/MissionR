using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New GameEvent",menuName ="GameEvent")]
public class GameEvent : ScriptableObject
{
    readonly public List<GameEventListener> listeners =
        new List<GameEventListener>();

    public void RegisterListener(GameEventListener listener)
    {
        if( listeners.Contains(listener) == false)
        {
            listeners.Add(listener);
        }
    }
    public void UnregisterListener(GameEventListener listener)
    {
        if( listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
    public void Raise()
    {
        foreach( GameEventListener listener in listeners)
        {
            listener.OnRaised();
        }
    }
}
