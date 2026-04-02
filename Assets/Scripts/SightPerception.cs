using UnityEngine;

public class SightPerception : MonoBehaviour
{
    public bool isDetected = false;

    [SerializeField] private float      detectionRadius = 15f;
    [SerializeField] private GameObject detectionObject;

    private void Update()
    {
        if (detectionObject == null)
        {
            isDetected = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, detectionObject.transform.position);
        isDetected = distance <= detectionRadius;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isDetected ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
}
