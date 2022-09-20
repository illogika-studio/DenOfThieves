using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pattern", menuName = "ScriptableObjects/Pattern", order = 1)]
public class Pattern : ScriptableObject
{
    [SerializeField] private List<Coordinates> _coordinatesAffected;
    public List<Coordinates> CoodinatesAffected => _coordinatesAffected;
    
    [SerializeField] private List<Pattern> _extraPatterns;
    public List<Pattern> ExtraPatterns => _extraPatterns;

    private int _rotation = 0;

    public void OnEnable()
    {
        foreach(Pattern pattern in _extraPatterns)
        {
            foreach(Coordinates coordinates in pattern.CoodinatesAffected)
            {
                _coordinatesAffected.Add(coordinates);
            }
        }
    }

    public void SetRotation(int rotation)
    {
        foreach(Coordinates coordinates in _coordinatesAffected)
        {
            Coordinates offsetCoordinates;

            switch (rotation)
            {
                case 90:
                case -270:
                    offsetCoordinates = new Coordinates(coordinates.X, coordinates.Z);
                    _coordinatesAffected.Remove(coordinates);
                    _coordinatesAffected.Add(offsetCoordinates);
                    break;
                case 180:
                case -180:
                    offsetCoordinates = new Coordinates(coordinates.Z, -coordinates.X);
                    _coordinatesAffected.Remove(coordinates);
                    _coordinatesAffected.Add(offsetCoordinates);
                    break;
                case 270:
                case -90:
                    offsetCoordinates = new Coordinates(-coordinates.X, coordinates.Z);
                    _coordinatesAffected.Remove(coordinates);
                    _coordinatesAffected.Add(offsetCoordinates);
                    break;
            }
        }

        _rotation = rotation;
    }
}
