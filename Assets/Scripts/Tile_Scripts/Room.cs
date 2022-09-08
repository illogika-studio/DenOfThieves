using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] 
    private int _id;
    public int Id => _id;

    [SerializeField] 
    private List<Tile> _tilesList;
    public List<Tile> TilesList => _tilesList;
}
