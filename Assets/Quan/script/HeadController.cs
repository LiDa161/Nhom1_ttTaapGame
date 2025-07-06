using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Character))]
public class SushiHeadController : MonoBehaviour
{
    [Header("Animation Settings")]
    public float idleBounceHeight = 0.2f;
    public float idleBounceSpeed = 0.4f;

    private Vector3 _originalPosition;
    private Tween _idleTween;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _originalPosition = transform.localPosition;
        StartIdleAnimation();
    }

    private void StartIdleAnimation()
    {
        _idleTween = transform.DOLocalMoveY(
            _originalPosition.y + idleBounceHeight,
            idleBounceSpeed
        ).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        _idleTween?.Kill();
    }
}