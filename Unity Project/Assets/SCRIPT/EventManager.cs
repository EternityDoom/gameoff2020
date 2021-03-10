using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<EventChecker> events;

    /// <summary>
    /// Updates the events
    /// </summary>
    public void UpdateEvents(){
        foreach (EventChecker e in events)
        {
            e.Check();
        }
    }
}
