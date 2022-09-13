using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private  BaseThief _thief;

    private void Awake()
    {
        var player = InstantiatePlayer();
        _playerController.SetPlayer(player);
    }

    private BaseThief InstantiatePlayer()
    {
        return Instantiate(_thief, _playerController.transform);
    }
}
