using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private BasePlayer _player;
    private ITileManager _tileManager;
    
    [Inject]
    public void Init(ITileManager tileManager)
    {
        _tileManager = tileManager;
    }
    
    private void Start()
    {
       _player.transform.position = _tileManager.StartingTilesList[0].transform.position;
    }

    public void SetPlayer(BasePlayer player)
    {
        _player = player;
    }
}
