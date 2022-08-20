using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Item : MonoBehaviour
{
    Outline outline;
   
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Tile"))
        {
            outline = col.gameObject.GetComponent<Outline>();
            outline.color = 1;
            Debug.Log(col);
        }
    }
}
