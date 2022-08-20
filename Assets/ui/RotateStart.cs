using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStart : MonoBehaviour
{
    GameObject rObject;
    public float rotSpeed = 100f;
     void Start()
    {
    }
    void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.Self);
    }
}
