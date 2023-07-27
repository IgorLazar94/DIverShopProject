using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Transform PlayerPos;
    [SerializeField] private SpriteRenderer pointerSprite;
    private Quaternion defaultSpriteRotation;
    [SerializeField] private Transform testCube;
    public static Action OnNextTutorialStep;
    public static Action<Fish> OnAddNewFishA;
    private Fish targetFish;
    private int tutorialPhase;
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
        defaultSpriteRotation = pointerSprite.transform.rotation;
        bool tutorialCompleted = PlayerPrefs.GetInt(tutorialKey, 0) == 1;
        if (tutorialCompleted)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Init tutorial");
            tutorialPhase = 1;
            CheckTutorialPhase();
        }
    }

    private void FixedUpdate()
    {
        pointerSprite.transform.position = PlayerPos.position;
        if (tutorialPhase == 1 && targetFish != null)
        {
            Debug.Log(targetFish + "look at targetFish");
            //Vector3 direction = (testCube.position - transform.position);
            pointerSprite.transform.LookAt(testCube);
        }
    }

    private void AddFish(Fish fish)
    {
        //Debug.Log("1");
        //if (targetFish != null)
        //{
        //    Debug.Log("2");
            targetFish = fish;
        //} else
        //{
        //    return;
        //}
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
            default:
                break;
        }
    }

    private void FirstStep()
    {

    }

    private void SecondStep()
    {

    }

    private void ThirdStep()
    {
        MarkTutorialCompleted();
    }

    public void MarkTutorialCompleted()
    {
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }

    private void UpdateTutorialLevel()
    {
        Debug.Log(tutorialPhase + " tutorialPhase");
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
}
