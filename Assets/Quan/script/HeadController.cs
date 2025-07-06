using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Character))]
public class SushiHeadController : MonoBehaviour
{
    [Header("Settings")]
    public float bounceHeight = 0.2f;
    public float bounceSpeed = 0.4f;
    public float angryShakeAngle = 5f;

    private Vector3 _originalPos;
    private Tween _bounceTween;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _originalPos = transform.localPosition;
        _character.OnCharacterDestroyed += OnDeath;
        StartBounce();
    }

    private void StartBounce()
    {
        _bounceTween = transform.DOLocalMoveY(
            _originalPos.y + bounceHeight,
            bounceSpeed
        ).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetAngry(bool isAngry)
    {
        _bounceTween?.Kill();

        if (isAngry)
            transform.DOShakeRotation(0.5f, new Vector3(0, 0, angryShakeAngle), 10, 90);
        else
            StartBounce();
    }

    private void OnDeath()
    {
        _bounceTween?.Kill();
        transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }

    private void OnDestroy() => _character.OnCharacterDestroyed -= OnDeath;
}