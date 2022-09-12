using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Action : MonoBehaviour
{
    private ILevelManager _levelManager;
    
    [Inject]
    private void Initialize(ILevelManager levelManager)
    {
        _levelManager = levelManager;
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
