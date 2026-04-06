using UnityEngine;

public class SightPerception : MonoBehaviour
{
    public bool isDetected = false;

    [SerializeField] private float      detectionRadius = 8f;
    [SerializeField] private GameObject detectionObject;

    /// <summary>Hauteur du point d'émission du rayon (niveau des yeux de Garry).</summary>
    private const float EyeHeight = 1.5f;

    private void Update()
    {
        if (detectionObject == null)
        {
            isDetected = false;
            return;
        }

        Vector3 origin    = transform.position + Vector3.up * EyeHeight;
        Vector3 targetPos = detectionObject.transform.position + Vector3.up * EyeHeight;
        Vector3 direction = targetPos - origin;
        float   distance  = direction.magnitude;

        if (distance > detectionRadius)
        {
            isDetected = false;
            return;
        }

        // Si le raycast touche quelque chose avant Ava, elle est cachée
        if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, distance))
            isDetected = hit.collider.gameObject == detectionObject;
        else
            isDetected = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isDetected ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (detectionObject != null)
        {
            Vector3 origin    = transform.position + Vector3.up * EyeHeight;
            Vector3 targetPos = detectionObject.transform.position + Vector3.up * EyeHeight;
            Gizmos.color = isDetected ? Color.red : Color.green;
            Gizmos.DrawLine(origin, targetPos);
        }
    }
#endif
}
