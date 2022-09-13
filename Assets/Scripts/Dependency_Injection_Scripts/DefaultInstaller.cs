using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private TileManager _tileManager;
    
    public override void InstallBindings()
    {
        Container.Bind<ILevelData>().To<LevelData>().FromInstance(_levelData) ;
        Container.Bind<ITileManager>().To<TileManager>().FromInstance(_tileManager) ;
    }

    public BasePlayer InstantiatePlayer(BasePlayer player, Transform playerController)
    {
       return Container.InstantiatePrefabForComponent<BasePlayer>(player, playerController);
    }
}