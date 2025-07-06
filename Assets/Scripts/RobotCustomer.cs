using UnityEngine;
using System.Collections;

public class RobotCustomer : MonoBehaviour
{
    [Header("Face States")]
    public GameObject normalFace;

    public GameObject happyFace;
    public GameObject angryFace;

    [Header("Effects")]
    public float happyDuration = 1.5f;

    public GameObject smokeEffectPrefab;

    [HideInInspector]
    public SushiDotReceiver linkedReceiver;

    private bool hasReacted = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (!hasReacted && MoveCounterManager.Instance != null && MoveCounterManager.Instance.ParLimitReached())
        {
            ReactAngry();
        }
    }

    private void OnEnable()
    {
        hasReacted = false;
        SetFace(normalFace);

        if (linkedReceiver == null)
            linkedReceiver = FindNearestReceiver();

        if (linkedReceiver != null)
            linkedReceiver.OnSushiConsumed += HandleSushiConsumed;
    }

    private void OnDisable()
    {
        if (linkedReceiver != null)
            linkedReceiver.OnSushiConsumed -= HandleSushiConsumed;
    }

    private SushiDotReceiver FindNearestReceiver()
    {
        var all = Object.FindObjectsByType<SushiDotReceiver>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        SushiDotReceiver nearest = null;
        float minDist = float.MaxValue;

        foreach (var dot in all)
        {
            float dist = Vector2.Distance(transform.position, dot.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = dot;
            }
        }

        return nearest;
    }

    private void HandleSushiConsumed()
    {
        if (hasReacted) return;

        hasReacted = true;
        StartCoroutine(ReactHappyThenDisappear());
    }

    private IEnumerator ReactHappyThenDisappear()
    {
        SetFace(happyFace);
        yield return new WaitForSeconds(happyDuration);

        SetFace(normalFace);

        if (smokeEffectPrefab != null)
            Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);

        // Safe destroy
        if (gameObject != null)
            gameObject.SetActive(false);
    }

    private void ReactAngry()
    {
        hasReacted = true;
        SetFace(angryFace);
        StartCoroutine(RevertToNormalAfterDelay(2f));
    }

    private IEnumerator RevertToNormalAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetFace(normalFace);
    }

    private void SetFace(GameObject face)
    {
        normalFace?.SetActive(false);
        happyFace?.SetActive(false);
        angryFace?.SetActive(false);

        face?.SetActive(true);
    }

    public void ResetEmotionToNormal()
    {
        SetFace(normalFace);
        hasReacted = false;
    }

    private void OnDestroy()
    {
    }
}