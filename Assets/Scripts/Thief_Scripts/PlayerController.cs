using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BasePlayer _player;

    private void Start()
    {
        Debug.Log(_player.name);
    }

    public void SetPlayer(BasePlayer player)
    {
        _player = player;
    }
}
