using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class FishSpawner : MonoBehaviour
{
    [Inject] private FishCollection fishCollection;
    private List<Fish> fishList = new List<Fish>();
    [SerializeField] Transform fishContainer;
    [SerializeField] private ushort maxCountOfFish;
    private GameObject currentFishGameObject;
    private TypeOfFish spawnTypeOfFish;
    private ushort maxAFish;
    private ushort maxBFish;
    private ushort maxCFish;
    private ushort currentAFish;
    private ushort currentBFish;
    private ushort currentCFish;
    private bool isReadyToSpawn = true;
    private bool isSpawning = true;
    private Vector3 spawnPoint;
    private float timeToSpawnNewFishes = 1f;

    private void Start()
    {
        spawnPoint = new Vector3(-20, 0, 0);
        spawnTypeOfFish = TypeOfFish.FishA;
        CalculateSizeOfFishStack();
        StartSpawnNewFishes();
    }

    private IEnumerator SpawnNewFishes()
    {
        while (true)
        {
            if (!isReadyToSpawn)
            {
                yield break;
            }
            yield return new WaitForSeconds(timeToSpawnNewFishes);
            CheckTypeOfFish();
            AddNewFish();
            CalculateNextTypeOfFish();
            CheckFishesCount();
        }
    }

    private void AddNewFish()
    {
        var fish = Instantiate(currentFishGameObject, fishContainer);
        fish.transform.localScale = Vector3.zero;
        fish.transform.DOScale(Vector3.one, 0.75f);
        var fishScript = fish.GetComponent<Fish>();
        fishScript.SetFishSpawner(this);
        CheckTutorialPhase(fishScript);
        fishList.Add(fishScript);
        SpawnToPosition(fish.transform);
    }

    private void CheckTutorialPhase(Fish _fish)
    {
        TutorialController.OnAddNewFishA?.Invoke(_fish);
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
        if (currentAFish < maxAFish)
        {
            spawnTypeOfFish = TypeOfFish.FishA;
            currentAFish++;
        }
        else if (currentBFish < maxBFish)
        {
            spawnTypeOfFish = TypeOfFish.FishB;
            currentBFish++;
        }
        else if (currentCFish < maxCFish)
        {
            spawnTypeOfFish = TypeOfFish.FishC;
            currentCFish++;
        }
    }

    private void CalculateSizeOfFishStack()
    {
        maxAFish = (ushort)(maxCountOfFish * 0.5f);
        maxBFish = (ushort)(maxCountOfFish * 0.25f);
        maxCFish = (ushort)(maxCountOfFish * 0.25f);
    }

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
        isSpawning = false;
        StopCoroutine(SpawnNewFishes());
    }

    private void CheckFishesCount()
    {
        if (fishList.Count >= maxCountOfFish)
        {
            StopSpawnNewFishes();
        }
        else if (fishList.Count < maxCountOfFish && !isSpawning)
        {
            isReadyToSpawn = true;
            isSpawning = true;
            StartSpawnNewFishes();
        }
    }

    public void RemoveFish(Fish fish)
    {
        fishList.Remove(fish);
        if (fish.typeOfFish == TypeOfFish.FishA)
        {
            currentAFish--;
        }
        else if (fish.typeOfFish == TypeOfFish.FishB)
        {
            currentBFish--;
        }
        else if (fish.typeOfFish == TypeOfFish.FishC)
        {
            currentCFish--;
        }
        CheckFishesCount();
    }
}
