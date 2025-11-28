using UnityEngine;

public class TmpMovement : MonoBehaviour
{
    public float radius = 30f;
    public float angularSpeed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object in a circular path
        float angle = angularSpeed * Time.time;
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
