using UnityEngine;
using Zenject;

public class FoodCollectionsInstaller : MonoInstaller
{
    [SerializeField] private FoodCollection foodCollection;
    public override void InstallBindings()
    {
        var foodInstance = Container.InstantiatePrefabForComponent<FoodCollection>(foodCollection, transform);

        Container.Bind<FoodCollection>().FromInstance(foodInstance).AsSingle().NonLazy();
    }
}