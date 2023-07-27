using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class CustomerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject happySprite;
    [SerializeField] private GameObject angrySprite;
    private NavMeshAgent navMeshAgent;
    private Transform shopPoint;
    private Transform exitPoint;
    private Shop connectShop;
    private Animator customerAnimator;
    private CustomerController customerController;
    private float spawnTime;
    private bool isFindShop = false;
    private void Start()
    {
        customerAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination(shopPoint.position);
        StartCoroutine(CheckShop());
    }

    private IEnumerator CheckShop()
    {
        yield return new WaitForSeconds(6.5f);
        if (!isFindShop)
        {
            MoveToExit();
        }
    }

    public void SetSpawnTime(float _spawnTime)
    {
        spawnTime = _spawnTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Shop shop))
        {
            shop.SetProductToCustomer(this);
            connectShop = shop;
            isFindShop = true;
        }
    }

    public void NoneFood()
    {
        ShowEmotion(angrySprite, false);
    }

    public void SetFood(Food food)
    {
        ShowEmotion(happySprite, true);

    }

    private void ShowEmotion(GameObject spriteObject, bool isHappy)
    {
        spriteObject.transform.DOScale((Vector3.one * 0.2f), 0.5f).OnComplete(() => spriteObject.transform.DOScale(Vector3.zero, 0.5f));
        int random = Random.Range(0, 2);
        customerAnimator.SetInteger(AnimParameters.RandomReaction, random);
        if (isHappy)
        {
            customerAnimator.SetBool(AnimParameters.isHappy, true);
        }
        else
        {
            customerAnimator.SetBool(AnimParameters.isHappy, false);
        }
        customerAnimator.SetTrigger(AnimParameters.Transaktion);
        Invoke(nameof(MoveToExit), 3f);
    }

    private void MoveToExit()
    {
        SetDestination(exitPoint.position);
        customerAnimator.SetTrigger(AnimParameters.GoAway);
        Invoke(nameof(CallControllerToRemove), spawnTime);
    }

    private void CallControllerToRemove()
    {
        customerController.RemoveCustomer(this);
    }

    private void FixedUpdate()
    {
        happySprite.transform.LookAt(Camera.main.transform);
        angrySprite.transform.LookAt(Camera.main.transform);
    }

    public void GiveMoney(ushort price)
    {
        for (int i = 0; i < price; i++)
        {
            connectShop.InstantDollar();
        }
    }

    private void SetDestination(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }

    public void TranslatePointsToCustomer(Transform _shopPoint, Transform _exitPoint, CustomerController _customerController)
    {
        shopPoint = _shopPoint;
        exitPoint = _exitPoint;
        customerController = _customerController;
    }
}

