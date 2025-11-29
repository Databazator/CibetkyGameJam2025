using System;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(15 * Time.deltaTime, 10* Time.deltaTime, 15* Time.deltaTime, Space.Self);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f * Mathf.Sin(Time.time), transform.position.z);
    }
}
