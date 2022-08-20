using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 offset2;

    public float xMin, xMax, zMin, zMax;
    // Start is called before the first frame update
    void Start()
    {
        
        offset = transform.position - player.transform.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        offset2 = player.transform.position + offset;
        float x = Mathf.Clamp(offset2.x, xMin, xMax);
        float z = Mathf.Clamp(offset2.z, zMin, zMax);
        transform.position = new Vector3(offset2.x, transform.position.y , offset2.z);
    }
}
