using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ThiefDataInstaller", menuName = "Installers/ThiefDataInstaller")]
public class ThiefDataInstaller : ScriptableObjectInstaller<ThiefDataInstaller>
{
    [SerializeField] private ThiefData _thiefData;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_thiefData).AsSingle();
    }
}