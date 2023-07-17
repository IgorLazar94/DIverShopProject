using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FishSpawner : MonoBehaviour
{
    [Inject] private FishCollection fishCollection;
    private List<Fish> fishList = new List<Fish>();
    [SerializeField] Transform fishContainer;
    [SerializeField] private ushort maxCountOfFish;
    private GameObject currentFishGameObject;
    private TypeOfFish spawnTypeOfFish;
    //private ushort maxAFish;
    //private ushort maxBFish;
    //private ushort maxCFish;
    private bool isReadyToSpawn = true;
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = new Vector3(-20, 0, 0);
        spawnTypeOfFish = TypeOfFish.FishA;
        //CalculateSizeOfFishStack();
        StartSpawnNewFishes();
    }

    private IEnumerator SpawnNewFishes()
    {
        while (true)
        {
            CheckTypeOfFish();
            AddNewFish();
            CalculateNextTypeOfFish();
            CheckFishesCount();
            if (!isReadyToSpawn)
            {
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    private void AddNewFish()
    {
        var fish = Instantiate(currentFishGameObject, fishContainer);
        fishList.Add(fish.GetComponent<Fish>());
        SpawnToPosition(fish.transform);
    }

    private void CheckTypeOfFish()
    {
        switch (spawnTypeOfFish)
        {
            case TypeOfFish.FishA:
                currentFishGameObject = fishCollection.FishA;
                break;
            case TypeOfFish.FishB:
                currentFishGameObject = fishCollection.FishB;
                break;
            case TypeOfFish.FishC:
                currentFishGameObject = fishCollection.FishC;
                break;
            default:
                break;
        }
    }

    private void CalculateNextTypeOfFish()
    {
        int nextType = ((int)spawnTypeOfFish + 1) % System.Enum.GetNames(typeof(TypeOfFish)).Length;
        spawnTypeOfFish = (TypeOfFish)nextType;
    }

    //private void CalculateSizeOfFishStack()
    //{
    //    maxAFish = (ushort)(maxCountOfFish * 0.5f);
    //    maxBFish = (ushort)(maxCountOfFish * 0.25f);
    //    maxCFish = (ushort)(maxCountOfFish * 0.25f);
    //}

    private void SpawnToPosition(Transform fish)
    {
        Vector2 randomPoint = Random.insideUnitCircle * 5;
        fish.position = spawnPoint + new Vector3(randomPoint.x, -1f, randomPoint.y);
    }

    private void StartSpawnNewFishes()
    {
        isReadyToSpawn = true;
        StartCoroutine(SpawnNewFishes());
    }
    private void StopSpawnNewFishes()
    {
        isReadyToSpawn = false;
        StopCoroutine(SpawnNewFishes());
    }

    private void CheckFishesCount()
    {
        Debug.Log(fishList.Count + " fish list");
        if (fishList.Count >= maxCountOfFish)
        {

            StopSpawnNewFishes();
        }
    }
}
