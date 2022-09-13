using UnityEngine;
using Zenject;

public class ThiefPlayer : BasePlayer
{
    [Inject]
    private ThiefData _thiefData;

    [Inject]
    public void Initialize(ThiefData thiefData)
    {
        if (thiefData == null)
        {
            _thiefData = thiefData;
        }
    }

    private void Start()
    {
        Debug.Log(_thiefData.MaxActionPoints);
    }
}
