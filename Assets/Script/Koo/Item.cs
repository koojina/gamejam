using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Item : MonoBehaviour
{
    Outline outline;
  
    GameObject itemObject;

    public bool activeCheck;
    void Awake()
    {
       
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Tile"))
        {
            outline = col.gameObject.GetComponent<Outline>();
            outline.eraseRenderer = false;
            outline.color = 1;
            activeCheck = true;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.W))
        {
            if (col.gameObject.CompareTag("Tile"))
            {
                outline = col.gameObject.GetComponent<Outline>();
                outline.eraseRenderer = true;
                outline.color = 1;
                activeCheck = false;
                this.gameObject.SetActive(false);

            }
        }
    }
}
