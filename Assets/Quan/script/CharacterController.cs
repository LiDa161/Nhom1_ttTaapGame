using UnityEngine;

public class SushiCharacterController : MonoBehaviour
{
    public SushiHeadController headController;
    public DraggableSnap dragSnap;

    private bool wasSnapped = false;

    void Start()
    {
        if (headController == null)
            headController = GetComponentInChildren<SushiHeadController>();

        if (dragSnap == null)
            dragSnap = GetComponent<DraggableSnap>();
    }

    void Update()
    {
        if (dragSnap != null)
        {
            if (!wasSnapped && dragSnap.IsSnapped)
            {
                headController.StartIdleMotion();
                wasSnapped = true;
            }

            if (!dragSnap.IsSnapped)
            {
                wasSnapped = false;
                headController.StopAllMotion();
            }
        }
    }
}
