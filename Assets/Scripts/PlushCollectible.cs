using UnityEngine;

/// <summary>Placé sur chaque peluche. Détecte le joueur via trigger et notifie le manager.</summary>
public class PlushCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;

    private const string PlayerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) return;

        MiniGame2Manager.Instance?.OnPlushCollected();

        if (_collectSound != null)
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);

        gameObject.SetActive(false);
    }
}
