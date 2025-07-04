using UnityEngine;
using DG.Tweening;

public class SushiCharacterController : MonoBehaviour
{
  
    public SushiHeadController headController;

    private Vector3 basePos;

    void Start()
    {
        basePos = transform.localPosition;

        if (headController == null)
            headController = GetComponentInChildren<SushiHeadController>();
    }

}