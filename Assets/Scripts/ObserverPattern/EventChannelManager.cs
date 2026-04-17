using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class will be responsible to hold all of the channels for the observer pattern
/// </summary>
public class EventChannelManager : PersistentSingleton<EventChannelManager>
{
    public VoidEventChannel voidEvent;
    public FloatEventChannel floatEvent;
    public GameDataEventChannel gameDataEvent;
}
