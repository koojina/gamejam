using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] gateRange;
    public bool gateButton;
  public void ClickGate()
    {
   for(int i=0; i< gateRange.Length;i++)
        {
            gateRange[i].SetActive(true);
            gateButton = true;
        }

}

    
}
