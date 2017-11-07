using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private void Update()
    {
        if (!target || target == null)
            return;

        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPos, 5 * Time.deltaTime);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
