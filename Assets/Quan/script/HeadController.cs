using UnityEngine;
using DG.Tweening;

public class SushiHeadController : MonoBehaviour
{
    public bool isAngry = false;

    private Tween moveTween;
    private Tween shakeTween;

    private Vector3 basePos;
    private Quaternion baseRot;

    private bool previousAngry = false;

    void Start()
    {
        basePos = transform.localPosition;
        baseRot = transform.localRotation;
    }

    public void StartIdleMotion()
    {
        StopAllMotion();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(-0.2f, 0.2f).SetEase(Ease.InOutSine));
        seq.Append(transform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.InOutSine));
        seq.AppendInterval(1f);
        seq.SetLoops(-1);

        moveTween = seq;
    }

    void StartAngryMotion()
    {
        StopAllMotion();

        transform.localPosition = new Vector3(basePos.x, -0.3f, basePos.z);
        transform.localRotation = Quaternion.identity;

        shakeTween = transform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.05f)
            .SetRelative(true)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void StopAngryMotion()
    {
        StopAllMotion();

        transform.localPosition = basePos;
        transform.localRotation = baseRot;
    }

    public void StopAllMotion()
    {
        moveTween?.Kill();
        shakeTween?.Kill();
        moveTween = null;
        shakeTween = null;
    }

    void Update()
    {
        if (previousAngry != isAngry)
        {
            previousAngry = isAngry;

            if (isAngry)
                StartAngryMotion();
            else
            {
                StopAngryMotion();
                StartIdleMotion();
            }
        }
    }
}