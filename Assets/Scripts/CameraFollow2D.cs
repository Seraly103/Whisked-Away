using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Camera Bounds")]
    public bool useBounds = true;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                return;
            }
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 finalPosition = new Vector3(desiredPosition.x, desiredPosition.y, offset.z);

        if (useBounds && cam != null)
        {
            float cameraHalfHeight = cam.orthographicSize;
            float cameraHalfWidth = cameraHalfHeight * cam.aspect;

            float clampedX = desiredPosition.x;
            float clampedY = desiredPosition.y;

            if (maxX > minX)
            {
                clampedX = Mathf.Clamp(desiredPosition.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);
            }

            if (maxY > minY)
            {
                clampedY = Mathf.Clamp(desiredPosition.y, minY + cameraHalfHeight, maxY - cameraHalfHeight);
            }

            finalPosition = new Vector3(clampedX, clampedY, offset.z);
        }

        transform.position = Vector3.Lerp(
            transform.position,
            finalPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}