using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private IPlayerController _playerController;
    [SerializeField] private BasePlayer _basePlayer;
    [SerializeField] private DefaultInstaller _defaultInstaller;

    [Inject]
    public void Init(IPlayerController playerController)
    {
        _playerController = playerController;
    }

    private void Awake()
    {
        BasePlayer basePlayer = _defaultInstaller.InstantiatePlayer(_basePlayer, _playerController.GetTransform());
        _playerController.SetPlayer(basePlayer);
    }
}
