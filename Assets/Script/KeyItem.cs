using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public GameObject nextLevelTrigger;

    public GameObject[] changeTile;

    public Material tempMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,30*Time.deltaTime,0));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            nextLevelTrigger.SetActive(true);
            for (int i = 0; i < changeTile.Length; ++i)
            {
                changeTile[i].layer = 6;
                changeTile[i].GetComponent<MeshRenderer>().material = tempMaterial;
            }
            Destroy(gameObject);
        }
    }
}
