using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using cakeslice;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //Outline outline;
    
    public GameObject[] gateRange;
    public bool gateButton;

    public Camera getCamera;
    private RaycastHit hit;
    public GameObject gate;
    public Quaternion rot;

    public Button gateBtn;
     void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);
           int layerMask = (1 << 10);  // Player 레이어만 충돌 체크함
           
            if (Physics.Raycast(ray,out hit, 100f,layerMask))
            {
                string objectName = hit.collider.gameObject.name;
                Debug.Log(objectName);

                if (objectName == "GateCol")
                {
                    Vector3 pos = hit.collider.gameObject.transform.position;
                    Debug.Log(hit.collider.gameObject.transform.position);
                    Instantiate(gate, pos, rot);
                    gateBtn.interactable = false;
                    Invoke("BtnEnable", 13f);
                }
               else if (objectName == "GateCol_A")
                {
                    Vector3 pos = hit.collider.gameObject.transform.position;
                    Debug.Log(hit.collider.gameObject.transform.position);
                    rot = Quaternion.Euler(0, -90, 0);
                    Instantiate(gate, pos, rot);
                    gateBtn.interactable = false;
                    Invoke("BtnEnable", 13f);
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

    public void BtnEnable()
    {
        gateBtn.interactable = true;
    }

    
}
