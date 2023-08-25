using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FishCollectionInstaller : MonoInstaller
{
    [SerializeField] private FishCollection fishCollection;
    public override void InstallBindings()
    {
        var fishInstance = Container.InstantiatePrefabForComponent<FishCollection>(fishCollection, transform);

        Container.Bind<FishCollection>().FromInstance(fishCollection).AsSingle().NonLazy();
    }
}
