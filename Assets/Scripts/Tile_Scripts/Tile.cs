using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public abstract int LightValue { get; }
    public abstract int SoundValue { get; }
    
    public Coordinates Coordinates { get; private set; }
    public Dictionary<InteractableElement, List<Action>> AvailableActionList { get; private set; }
    
    public void SetCoordinates(Coordinates newCoordinates)
    {
        Coordinates = newCoordinates;
    }

    public abstract void UpdateLightValue(int amount);
    
    public abstract void UpdateSoundValue(int amount);
}
