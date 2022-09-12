using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private LevelManager _levelManager;

    public override void InstallBindings()
    {
        Container.Bind<ILevelManager>().To<LevelManager>().FromInstance(_levelManager);
    }
}