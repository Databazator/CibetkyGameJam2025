using System;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 10* Time.deltaTime, 0f, Space.Self);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f * Mathf.Sin(Time.time), transform.position.z);
    }
}
