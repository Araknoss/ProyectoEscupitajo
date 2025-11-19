using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents
{
    public class HealthEvent : UnityEvent<Component, int> { }
    public GenericEvent<HealthEvent> OnHealthChanged = new GenericEvent<HealthEvent>();
}
   

