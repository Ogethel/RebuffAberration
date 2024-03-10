using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is where we set the actual concrete implmentations of the event structure. 
/// These are created and destroied dynamically both in and coming out of runtime
/// </summary>
public interface IEvent { }

public struct SwitchMenuFocus : IEvent { public int View; }

public struct PlayerHeathChange : IEvent { public int Health; }
