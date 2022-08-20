using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhoneItem : MonoBehaviour
{
    // Start is called before the first frame update

    int remainTurn;

    [SerializeField]
    TurnManager myTurnManager;

    

    void Start()
    {
        remainTurn = 5;
        
        myTurnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();

        myTurnManager.UseCellPhoneItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountingTurn()
    {
        remainTurn--;
        Debug.Log(remainTurn);  
        if(remainTurn <= 0)
        {
            //myTurnManager.DestroyCellPhoneItem();
            Destroy(gameObject);
        }
    }
}
