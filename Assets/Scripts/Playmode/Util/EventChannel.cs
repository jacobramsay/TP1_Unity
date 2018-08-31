using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventChannelEventHandler();

public abstract class EventChannel : MonoBehaviour
{
    //Pour écouter le canal.
    public event EventChannelEventHandler OnEventPublished;

    //Pour publier un événement sur le canal.
    public void Publish()
    {
        if (OnEventPublished != null) OnEventPublished();
    }
}
