using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum TypeOfFish
{
    FishA,
    FishB,
    FishC
}
public class Fish : MonoBehaviour
{
    public bool isRunFromPlayer { get; private set; }
    [field: SerializeField] public TypeOfFish typeOfFish { get; set; }
    //[SerializeField] private FishSpriteController childSprite;
    [SerializeField] private float idleSpeed;
    [SerializeField] private float timeToCatch;
    [SerializeField] private float availabilityLevel;
    [SerializeField] private Image emptyImage;
    [SerializeField] private GameObject fishCanvas;
    [SerializeField] private SpriteRenderer blockSprite;
    [SerializeField] private ParticleSystem rippleFX;
    [SerializeField] private Transform fishModel;
    private float runSpeed;
    private Rigidbody rb;
    private Vector3 randomDirection;
    private Sequence sequence;
    private FishSpawner fishSpawner;
    private Transform playerPos;
    private Outline outline;
    private Transform mainCameraTransform;
    private Tween fillTween;
    private float timeToCheckFishing = 0.25f;
    private Sequence rotationSequence;
    private void Start()
    {
        //rippleFX = GetComponentInChildren<ParticleSystem>();
        mainCameraTransform = Camera.main.transform;
        runSpeed = idleSpeed * 3f;
        isRunFromPlayer = false;
        outline = GetComponent<Outline>();
        rb = GetComponent<Rigidbody>();
        ActivateOutline(false);
        EnableFishBar(false);
        //childSprite.gameObject.SetActive(false);
        sequence = DOTween.Sequence();
        //FistRotation();
        StartRotation();
        StartCoroutine(ChooseRandomDirection());
    }

    public void ShowBlockSprite()
    {
        ActivateBlockSprite(true);
        Invoke(nameof(HideBlockSprite), 1f);
    }

    private void HideBlockSprite()
    {
        ActivateBlockSprite(false);
    }

    private void ActivateOutline(bool value)
    {
        outline.enabled = value;
    }

    private void ActivateBlockSprite(bool value)
    {
        blockSprite.gameObject.SetActive(value);
    }

    public void SetFishSpawner(FishSpawner _fishSpawner)
    {
        fishSpawner = _fishSpawner;
    }

    protected void Swim(Vector3 randomDirection)
    {
        Vector3 targetPoint = transform.position + Quaternion.Euler(0, -270f, 0) * randomDirection;
        rb.velocity = randomDirection * idleSpeed * Time.deltaTime;
        transform.LookAt(targetPoint);
    }

    protected void SwimFromPlayer()
    {
        Vector3 targetPoint = transform.position + Quaternion.Euler(0, 0, 0) * randomDirection;
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
            RotateFishBarToCamera();
            CheckPlayerDistance(playerPos);
            SwimFromPlayer();
        }
        blockSprite?.transform.LookAt(Camera.main.transform);
    }

    private void RotateFishBarToCamera()
    {
        Vector3 toCamera = mainCameraTransform.position - fishCanvas.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(-toCamera, mainCameraTransform.up);
        fishCanvas.transform.rotation = lookRotation;
    }

    private void EnableFishBar(bool value)
    {
        emptyImage.transform.parent.gameObject.SetActive(value);
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
        if (collision.gameObject.TryGetComponent(out Fish fish))
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
            yield return new WaitForSeconds(timeToCheckFishing);
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
        //childSprite.gameObject.SetActive(isActivate);
        ActivateOutline(isActivate);
        EnableFishBar(isActivate);
        if (isActivate)
        {
            fillTween = DOTween.To(() => emptyImage.fillAmount, 
                                   v => emptyImage.fillAmount = v, 
                                   1f, 
                                   timeToCatch)
                                   .OnComplete(() => FishCaught());
            //childSprite.FishingStartTimer(timeToCatch, this);
        }
        else
        {
            emptyImage.fillAmount = 0;
            fillTween?.Kill();
            //childSprite.FishingStopTimer();
        }
    }

    public void FishCaught()
    {
        var particle = Instantiate(rippleFX, transform.position, Quaternion.identity);
        particle.Play();
        EnableFishBar(false);
        transform.DOJump(playerPos.position, 1f, 1, 0.3f).OnComplete(() => PassTheFishToPlayer());
    }

    private void PassTheFishToPlayer()
    {
        PlayerInventoryPresenter.OnCurrentFishChanged?.Invoke(1, typeOfFish);
        fishSpawner.RemoveFish(this);
        Destroy(gameObject);
    }

    private void Update()
    {
        // Debugging
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerInventoryPresenter.OnCurrentFishChanged?.Invoke(1, TypeOfFish.FishA);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerInventoryPresenter.OnCurrentFishChanged?.Invoke(1, TypeOfFish.FishB);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerInventoryPresenter.OnCurrentFishChanged?.Invoke(1, TypeOfFish.FishC);
        }
    }

    private void StartRotation()
    {
        rotationSequence = DOTween.Sequence();
        rotationSequence.Append(fishModel.DOLocalRotate(new Vector3(0f, 30f, 0f), 0.5f).SetEase(Ease.InOutQuad));
        rotationSequence.Append(fishModel.DOLocalRotate(new Vector3(0f, -30f, 0f), 0.5f).SetEase(Ease.InOutQuad));
        rotationSequence.SetLoops(-1);
        rotationSequence.Play();
    }
}


