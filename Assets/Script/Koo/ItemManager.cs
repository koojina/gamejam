using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{

    Item item;
    public GameObject[] gateRange;
    public bool gateButton;

    public Camera getCamera;
    private RaycastHit hit;

     void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);
           int layerMask = (1 << 10);  // Player 레이어만 충돌 체크함
            //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, layerMask);
            if (Physics.Raycast(ray,out hit, 100f,layerMask))
            {
                string objectName = hit.collider.gameObject.name;
                Debug.Log(objectName);

                if (objectName == "GateCol")
                {
                    Debug.Log(hit.collider.gameObject.transform.position);

                }
            }
        }
    }
    public void ClickGate()
    {
   for(int i=0; i< gateRange.Length;i++)
        {          
            gateRange[i].SetActive(true);
            gateButton = true;
        }

}

    public void SetGate()
    {
        for (int i = 0; i < gateRange.Length; i++)
        {
            gateRange[i].SetActive(false);
            gateButton = false;
        }
    }

    
}
