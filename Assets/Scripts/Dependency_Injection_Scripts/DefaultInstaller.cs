using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField]
    private LevelManager _levelManager;
    
    public override void InstallBindings()
    {
        //ILevelManager useless = Container.InstantiatePrefabForComponent<ILevelManager>(_levelManager);
        //Container.Bind<LevelManager>().AsSingle();
        Container.Bind<ILevelManager>().To<LevelManager>().AsSingle();
    }
}