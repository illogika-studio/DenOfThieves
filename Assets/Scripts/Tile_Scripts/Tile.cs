using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public abstract int LightValue { get; }
    public abstract int SoundValue { get; }
    
    public Coordinates Coordinates { get; private set; }
    public Dictionary<InteractableElement, List<Action>> AvailableActionList { get; private set; }

    private void Awake()
    {
        var roomPosition = transform.parent.parent.position;
        //int z = (int)transform.position.z + (int)roomPosition.z;
        //int x = (int)transform.position.x + (int)roomPosition.x;
        
        Coordinates = new Coordinates((int)transform.position.z, (int)transform.position.x);
    }

    public abstract void UpdateLightValue(int amount);
    
    public abstract void UpdateSoundValue(int amount);
}
