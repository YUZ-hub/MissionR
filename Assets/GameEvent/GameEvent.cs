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
        for( int i = listeners.Count-1; i>=0; i-- )
        {
            listeners[i].OnRaised();
        }
    }
}
