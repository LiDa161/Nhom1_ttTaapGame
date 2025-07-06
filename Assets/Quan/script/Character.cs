using UnityEngine;

public class Character : MonoBehaviour
{
    public SushiShape characterShape;
    public GameObject deathEffectPrefab; // Prefab effect của bạn
    public AudioClip deathSound;

    public System.Action OnCharacterDestroyed;
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void TriggerDeath()
    {
        // Hiệu ứng visual
        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        // Âm thanh
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);

        OnCharacterDestroyed?.Invoke();
        Destroy(gameObject);
    }
}