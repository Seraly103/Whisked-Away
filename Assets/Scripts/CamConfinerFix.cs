using UnityEngine;
using Unity.Cinemachine;


public class CamConfinerFix : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("RefreshCameraConfiner Start() ran.");

        CinemachineConfiner2D confiner =
            GetComponent<CinemachineConfiner2D>();

        if (confiner == null)
        {
            Debug.LogError(
                "No Cinemachine Confiner 2D found on " + gameObject.name
            );
            return;
        }

        Debug.Log("Confiner found on " + gameObject.name);

        confiner.InvalidateBoundingShapeCache();
        confiner.InvalidateLensCache();

        Debug.Log("Confiner caches invalidated.");
    }
}
