using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class GenericFish : MonoBehaviour
{
    [SerializeField] protected FishSpriteController childSprite;
    [SerializeField] private float idleSpeed;
    [SerializeField] private float timeToCatch;
    private float runSpeed;
    private Rigidbody rb;
    private Vector3 randomDirection;
    private Sequence sequence;
    private bool isRunFromPlayer;
    private Transform playerPos;


    private void Start()
    {
        runSpeed = idleSpeed * 3f;
        isRunFromPlayer = false;
        rb = GetComponent<Rigidbody>();
        childSprite.gameObject.SetActive(false);
        sequence = DOTween.Sequence();
        //FistRotation();
        StartCoroutine(ChooseRandomDirection());
    }

    protected void Swim(Vector3 randomDirection)
    {
        Vector3 targetPoint = transform.position + Quaternion.Euler(0, -270f, 0) * randomDirection;
        rb.velocity = randomDirection * idleSpeed * Time.deltaTime;
        transform.LookAt(targetPoint);
    }

    protected void SwimFromPlayer()
    {
        Vector3 targetPoint = transform.position + Quaternion.Euler(0, -270f, 0) * randomDirection;
        Vector3 direction = (transform.position - playerPos.position).normalized;
        rb.velocity = (direction * runSpeed) * Time.deltaTime;
        transform.LookAt(targetPoint);
    }

    private Vector3 GetRandomDirection()
    {
        int xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        int zDirection = Random.Range(0, 2) == 0 ? -1 : 1;

        Vector3 randomDirection = new Vector3(xDirection, 0f, zDirection);
        return randomDirection;
    }

    private IEnumerator ChooseRandomDirection()
    {
        while (true && !isRunFromPlayer)
        {
            randomDirection = GetRandomDirection();
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void FixedUpdate()
    {
        if (!isRunFromPlayer)
        {
            Swim(randomDirection);
        }
        else
        {
            CheckPlayerDistance(playerPos);
            SwimFromPlayer();
        }
    }

    private void FistRotation()
    {
        sequence.Append(transform.DORotate(new Vector3(0f, transform.eulerAngles.y + 15f, 0f), 0.5f)).SetEase(Ease.InElastic).OnComplete(() => Debug.Log("left"));
        sequence.Append(transform.DORotate(new Vector3(0f, transform.eulerAngles.y - 15f, 0f), 0.5f)).SetEase(Ease.InElastic).OnComplete(() => Debug.Log("right"));
        sequence.SetLoops(-1);
        sequence.Play();
    }

    private void ReverseDirection()
    {
        randomDirection = randomDirection.Reverse();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagList.Wall))
        {
            randomDirection = randomDirection.Reverse();
        }
        if (collision.gameObject.TryGetComponent<GenericFish>(out GenericFish fish))
        {
            ReverseDirection();
            fish.ReverseDirection();
        }
    }

    public IEnumerator StartRunFromPlayer(Transform playerPos)
    {
        isRunFromPlayer = true;
        EnableFishing(true);
        while (true && isRunFromPlayer)
        {
            UpdatePlayerLos(playerPos);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void CheckPlayerDistance(Transform _playerPos)
    {
        if ((transform.position - _playerPos.position).magnitude > 10f)
        {
            StopRunFromPlayer();
        }
    }

    private void UpdatePlayerLos(Transform _playerPos)
    {
        playerPos = _playerPos;
    }

    public void StopRunFromPlayer()
    {
        isRunFromPlayer = false;
        EnableFishing(false);
        StopCoroutine(StartRunFromPlayer(playerPos));
    }

    private void EnableFishing(bool isActivate)
    {
        childSprite.gameObject.SetActive(isActivate);
        if (isActivate)
        {
            childSprite.FishingStartTimer(timeToCatch, this);
        } else
        {
            childSprite.FishingStopTimer();
        }
    }

    public void FishCaught()
    {
        transform.DOJump(playerPos.position, 1f, 1, 0.3f).OnComplete(() => PassTheFishToPlayer());
    }

    private void PassTheFishToPlayer()
    {
        PlayerInventoryPresenter.OnCurrentFishChanged(1);
        Destroy(gameObject);
    }
}


