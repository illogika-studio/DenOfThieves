using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private BasePlayer _basePlayer;
    [SerializeField] private DefaultInstaller _defaultInstaller;
    
    private void Awake()
    {
        BasePlayer basePlayer = _defaultInstaller.InstantiatePlayer(_basePlayer, _playerController.transform);
        _playerController.SetPlayer(basePlayer);
    }
}
