using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject happySprite;
    [SerializeField] private GameObject angrySprite;
    [SerializeField] private GameObject dollarPrefab;

    private Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
        VisitCustomer();
    }



    private void VisitCustomer()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(initialPosition.x - 4f, 1f));
        sequence.AppendInterval(0.5f);
        sequence.Append(transform.DOMoveX(initialPosition.x, 1f));
        sequence.AppendInterval(0.5f);
        sequence.SetLoops(-1);
        sequence.SetEase(Ease.InQuad);
        sequence.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Shop shop))
        {
            shop.SetProductToCustomer(this);
        }
    }

    public void NoneFood()
    {
        ShowEmotion(angrySprite);
    }

    public void SetFood(Food food)
    {
        ShowEmotion(happySprite);

    }

    private void ShowEmotion(GameObject spriteObject)
    {
        spriteObject.transform.DOScale((Vector3.one * 0.2f), 0.5f).OnComplete(() => spriteObject.transform.DOScale(Vector3.zero, 0.5f));
    }

    private void FixedUpdate()
    {
        happySprite.transform.LookAt(Camera.main.transform);
        angrySprite.transform.LookAt(Camera.main.transform);
    }

    public void GiveMoney(Food food)
    {
        if (food.typeOfFood == TypeOfFood.FriedFish)
        {
            Debug.Log("fish price");
        }
        else if (food.typeOfFood == TypeOfFood.Sandwich)
        {
            Debug.Log("Sandwich");
        }
        else if (food.typeOfFood == TypeOfFood.Fishburger)
        {
            Debug.Log("Fishburger");
        }
    }

}

