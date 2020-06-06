using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="EventManagerTemplate", menuName ="Template/Events/EventManager")]
public class EventManager : ScriptableSingleton<EventManager>
{
    [SerializeField] private List<EventTemplate> m_EventList;

    public void Register(Shell.Event eventId, UnityAction<EventParam> callback)
    {
        int index = (int)eventId;
        m_EventList[index].Register(callback);
    }
    
    public void Unregister(Shell.Event eventId, UnityAction<EventParam> callback)
    {
        int index = (int)eventId;
        m_EventList[index].Unresigter(callback);
    }

    public void RaiseEvent(Shell.Event eventId, EventParam param)
    {
        int index = (int)eventId;
        m_EventList[index].Raise(param);
    }
}
