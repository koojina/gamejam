using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKoo : MonoBehaviour
{
   public GameObject wall;
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)

    {
        Debug.Log("enter");
        if (other.gameObject.CompareTag("Player"))

        {

            wall.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)

    {
        Debug.Log("enter");
        if (other.gameObject.CompareTag("Player"))

        {

            wall.SetActive(true);
        }
    }
}
