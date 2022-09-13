using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private GameObject thiefPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<ILevelData>().To<LevelData>().FromInstance(_levelData) ;
        Container.Bind<Thief>().FromComponentInNewPrefab(thiefPrefab).AsSingle();
    }

    public void InstantiatePlayer(BaseThief thief, Transform playerController)
    {
        Container.InstantiatePrefab(thief, playerController);
    }
}