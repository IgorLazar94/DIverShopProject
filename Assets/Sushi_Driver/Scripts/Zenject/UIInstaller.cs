using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIController uIController;
    public override void InstallBindings()
    {
        var uiInstance = Container.InstantiatePrefabForComponent<UIController>(uIController, transform);

        Container.Bind<UIController>().FromInstance(uiInstance).AsSingle().NonLazy();
    }
}
