using UnityEngine;

public class ReturnCell : MonoBehaviour
{
    private MiniGameManager _miniGameManager;

    private void Awake()
    {
        _miniGameManager = FindFirstObjectByType<MiniGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _miniGameManager?.ReturnToMainScene();
        }
    }
}
