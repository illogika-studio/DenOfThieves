using UnityEngine;
using Zenject;

public class Thief : BaseThief
{
    private ThiefData _thiefData;

    [Inject]
    public void Initialize(ThiefData thiefData)
    {
        _thiefData = thiefData;
    }
    
    private void Start()
    {
        Debug.Log(_thiefData.MaxActionPoints);
    }
}
