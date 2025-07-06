using UnityEngine;

public class Character : MonoBehaviour
{
    public SushiShape characterShape;
    public GameObject deathEffectPrefab; // Gán prefab effect vào đây
    public AudioClip deathSound;

    public System.Action OnCharacterDestroyed;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathEffects()
    {
        // Gọi effect
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // Phát âm thanh
        if (deathSound != null)
        {
            if (_audioSource != null)
                _audioSource.PlayOneShot(deathSound);
            else
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        }

        OnCharacterDestroyed?.Invoke();
    }

    private void OnDestroy()
    {
        PlayDeathEffects();
    }
}