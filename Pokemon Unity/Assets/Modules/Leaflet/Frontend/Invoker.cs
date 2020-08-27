/// Source: Leaflet
/// Purpose: Invoke UnityEvents from other scripts/events for Leaflet
/// Author: TeamPopplio
/// Contributors: TeamPopplio
using UnityEngine;
using UnityEngine.Events;
namespace Leaflet.Frontend {
public class Invoker : MonoBehaviour
{
    public InvokeType invokeMethod = InvokeType.awake;
    public UnityEvent Event;
    void Start()
    {
        if(invokeMethod == InvokeType.start)
            Event.Invoke();
    }
    void Awake()
    {
        if(invokeMethod == InvokeType.awake)
            Event.Invoke();
    }
    void Update()
    {
        if(invokeMethod == InvokeType.update)
            Event.Invoke();
    }
    void LateUpdate()
    {
        if(invokeMethod == InvokeType.lateUpdate)
            Event.Invoke();
    }
    void FixedUpdate()
    {
        if(invokeMethod == InvokeType.lateUpdate)
            Event.Invoke();
    }
    public void Invoke()
    {
        if(invokeMethod == InvokeType.scripted)
            Event.Invoke();
    }
    public enum InvokeType {
        awake,
        start,
        update,
        lateUpdate,
        fixedUpdate,
        scripted,
    }
}
}
