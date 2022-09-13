using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Action : MonoBehaviour
{
    private ILevelData _levelData;
    
    [Inject]
    private void Initialize(ILevelData levelData)
    {
        _levelData = levelData;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
