using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTrigger : MonoBehaviour
{
    public GameObject[] myAlphaObject;

    public List<Material> myMaterial;

    public Material tempMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for(int i=0; i<myAlphaObject.Length; ++i)
            {
                myMaterial.Add(myAlphaObject[i].GetComponent<MeshRenderer>().material);
                myAlphaObject[i].GetComponent<MeshRenderer>().material = tempMaterial;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < myAlphaObject.Length; ++i)
            {
                myAlphaObject[i].GetComponent<MeshRenderer>().material = myMaterial[i];
            }
        }
    }
}
