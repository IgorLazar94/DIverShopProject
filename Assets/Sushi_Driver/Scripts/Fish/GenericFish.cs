using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericFish : MonoBehaviour
{
    [field: SerializeField] protected float speed;
    private Rigidbody rb;
    private Vector3 randomDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SwimInRandomDirection());
    }

    protected void Swim(Vector3 randomDirection)
    {
        rb.velocity = randomDirection * speed * Time.deltaTime;
        Vector3 targetPoint = transform.position + Quaternion.Euler(0,-270f, 0)  * randomDirection;
        transform.LookAt(targetPoint);
    }

    private Vector3 GetRandomDirection()
    {
        int xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        int zDirection = Random.Range(0, 2) == 0 ? -1 : 1;

        Vector3 randomDirection = new Vector3(xDirection, 0f, zDirection);
        Debug.Log(randomDirection + "random direction for fish");
        return randomDirection;
    }

    private IEnumerator SwimInRandomDirection()
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






}

