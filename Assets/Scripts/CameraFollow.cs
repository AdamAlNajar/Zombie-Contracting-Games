using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // maybe more than one camera in the scene.. later we will see // TODO
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

    }
}
