using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static Action OnNextTutorialStep;
    public static Action<Fish> OnAddNewFishA;
    public static int tutorialPhase { get; private set; }
    [SerializeField] private Transform playerPos;
    [SerializeField] private SpriteRenderer pointerSprite;
    [SerializeField] private Transform kitchenBuyingZone;
    [SerializeField] private Transform shopBuyingZone;
    [SerializeField] private UIController uIController;
    private Vector3 kitchenPosition;
    private Vector3 shopPosition;
    private List<BuyingZone> BuyingZones = new List<BuyingZone>();
    private Quaternion defaultSpriteRotation;
    private Fish targetFish;
    private float pointerOffset = 1.2f;
    private const string tutorialKey = "TutorialCompleted";

    private void OnEnable()
    {
        OnNextTutorialStep += UpdateTutorialLevel;
        OnAddNewFishA += AddFish;
    }

    private void OnDisable()
    {
        OnNextTutorialStep -= UpdateTutorialLevel;
        OnAddNewFishA -= AddFish;
    }

    private void Start()
    {
        CheckTutorialComplete();
    }

    private void CheckTutorialComplete()
    {
        bool tutorialCompleted = PlayerPrefs.GetInt(tutorialKey, 0) == 1;
        if (tutorialCompleted)
        {
            Destroy(gameObject);
        }
        else
        {
            tutorialPhase = 1;
            CheckTutorialPhase();
            kitchenPosition = kitchenBuyingZone.position;
            shopPosition = shopBuyingZone.position;
            defaultSpriteRotation = pointerSprite.transform.rotation;
        }
    }

    private void DiactivateBuyingZones()
    {
        BuyingZones = FindObjectsOfType<BuyingZone>().ToList();
        BuyingZones.ForEach(zone =>
            zone.GetComponent<Collider>().enabled = false);
    }

    private void FixedUpdate()
    {
        pointerSprite.transform.position = playerPos.position;
        if (tutorialPhase == 1 && targetFish != null)
        {
            ChooseTargetForPointer(targetFish.transform.position);
        }
        else if (tutorialPhase == 2)
        {
            ChooseTargetForPointer(kitchenPosition);
        }
        else if (tutorialPhase == 3)
        {
            ChooseTargetForPointer(shopPosition);
        }
    }


    private void ChooseTargetForPointer(Vector3 targetPos)
    {
        Vector3 directionToTarget = targetPos - pointerSprite.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        targetRotation *= defaultSpriteRotation;
        pointerSprite.transform.rotation = Quaternion.Slerp(pointerSprite.transform.rotation, targetRotation, 10.0f * Time.deltaTime);
        Vector3 spriteOffset = directionToTarget.normalized * pointerOffset;
        pointerSprite.transform.position = playerPos.position + spriteOffset;
    }

    private void AddFish(Fish fish)
    {
        targetFish = fish;
    }

    private void CheckTutorialPhase()
    {
        switch (tutorialPhase)
        {
            case 1:
                FirstStep();
                break;
            case 2:
                SecondStep();
                break;
            case 3:
                ThirdStep();
                break;
            case 4:
                FourthStep();
                break;
            default:
                break;
        }
    }

    private void FirstStep()
    {
        string message = "Need to catch fish";
        uIController.ShowTutorialMessage(message);
        DiactivateBuyingZones();
    }

    private void SecondStep()
    {
        string message = "Buy a kitchen and cook fish";
        uIController.ShowTutorialMessage(message);
        if (kitchenBuyingZone != null)
        {
            kitchenBuyingZone.GetComponent<BoxCollider>().enabled = true;
        }
        BuyingZones.Remove(kitchenBuyingZone.GetComponent<BuyingZone>());
    }

    private void ThirdStep()
    {
        string message = "Take the fish, buy a store and sell fish";
        uIController.ShowTutorialMessage(message);
        if (shopBuyingZone != null)
        {
            shopBuyingZone.GetComponent<BoxCollider>().enabled = true;
        }
        BuyingZones.Remove(shopBuyingZone.GetComponent<BuyingZone>());
    }

    private void FourthStep()
    {
        string message = "Now buyers will buy food, and you can develop your business";
        uIController.ShowTutorialMessage(message);
        EnableOtherBuyingZones();
        Invoke(nameof(MarkTutorialCompleted), 2.5f);
    }

    public void MarkTutorialCompleted()
    {
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }

    private void UpdateTutorialLevel()
    {
        Debug.Log(tutorialPhase + " tutorialPhase Update!");
        tutorialPhase += 1;
        CheckTutorialPhase();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnNextTutorialStep.Invoke();
        }
    }

    private void EnableOtherBuyingZones()
    {
        foreach (var zone in BuyingZones)
        {
            zone.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
