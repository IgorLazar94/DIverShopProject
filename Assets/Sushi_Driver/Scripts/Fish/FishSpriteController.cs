using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishSpriteController : MonoBehaviour
{
    [SerializeField] private Transform fullCircleSprite;
    private Fish parentFish;
    private Tween timerTween;

    public void FishingStartTimer(float time, Fish _parent)
    {
        parentFish = _parent;
        fullCircleSprite.localScale = Vector3.zero;
        timerTween = fullCircleSprite.DOScale(Vector3.one, time).OnComplete(() => parentFish.FishCaught());
    }

    public void FishingStopTimer ()
    {
        fullCircleSprite.localScale = Vector3.zero;
        timerTween?.Kill();
    }

    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
