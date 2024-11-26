using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 updatedPosition = new Vector3(transform.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, updatedPosition, followSpeed * Time.deltaTime);
    }
}
