using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform shopPoint;
    [SerializeField] private Transform exitPoint;
    private List<CustomerBehaviour> customers = new List<CustomerBehaviour>();
    private float timeToSpawn;

    private void Start()
    {
        timeToSpawn = GameSettings.Instance.GetTimeToVisitCustomers();
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
            var customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
            var customerBehaviour = customer.GetComponent<CustomerBehaviour>();
            customers.Add(customerBehaviour);
            customerBehaviour.SetSpawnTime(timeToSpawn);
            customerBehaviour.TranslatePointsToCustomer(shopPoint, exitPoint, this);
        }
    }

    public void RemoveCustomer(CustomerBehaviour customer)
    {
        customers.Remove(customer);
        Destroy(customer.gameObject);
    }
}
