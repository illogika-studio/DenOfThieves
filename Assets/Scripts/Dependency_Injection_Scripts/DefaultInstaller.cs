using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private LevelData _levelData;
    
    public override void InstallBindings()
    {
        Container.Bind<ILevelData>().To<LevelData>().FromInstance(_levelData) ;
    }

    public BasePlayer InstantiatePlayer(BasePlayer player, Transform playerController)
    {
       return Container.InstantiatePrefabForComponent<BasePlayer>(player, playerController);
    }
}