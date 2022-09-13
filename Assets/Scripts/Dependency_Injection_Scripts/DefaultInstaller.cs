using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] private LevelData _levelData;

    public override void InstallBindings()
    {
        Container.Bind<ILevelData>().To<LevelData>().FromInstance(_levelData);
    }
}