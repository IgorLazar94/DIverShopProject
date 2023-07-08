using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class GenericFish : MonoBehaviour
{
    [field: SerializeField] protected float speed;
    private Rigidbody rb;
    private Vector3 randomDirection;
    Sequence sequence;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sequence = DOTween.Sequence();
        //FistRotation();
        StartCoroutine(ChooseRandomDirection());
    }

    protected void Swim(Vector3 randomDirection)
    {
        rb.velocity = randomDirection * speed * Time.deltaTime;
        Vector3 targetPoint = transform.position + Quaternion.Euler(0, -270f, 0) * randomDirection;

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
        while (true)
        {
            randomDirection = GetRandomDirection();
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void FixedUpdate()
    {
        Swim(randomDirection);
    }

    private void FistRotation()
    {
        sequence.Append(transform.DORotate(new Vector3(0f, transform.eulerAngles.y + 15f, 0f), 0.5f)).SetEase(Ease.InElastic).OnComplete(() => Debug.Log("left"));
        sequence.Append(transform.DORotate(new Vector3(0f, transform.eulerAngles.y - 15f, 0f), 0.5f)).SetEase(Ease.InElastic).OnComplete(() => Debug.Log("right"));
        sequence.SetLoops(-1);
        sequence.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagList.Wall))
        {
           randomDirection = randomDirection.Reverse();
        }
    }
}

