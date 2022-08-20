using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameObject[] zombiePrefab;

    public bool isTurn;

    public int turnCount = 0;

    public GameObject cellPhone;
    // Start is called before the first frame update
    void Start()
    {
        isTurn = false;

        cellPhone = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingZombieArray()
    {
        zombiePrefab = GameObject.FindGameObjectsWithTag("Zombie");
    }

    public void SetZombieTurn(bool setTurn)
    {
        if(setTurn)
        {

            if(cellPhone)
            {
                cellPhone.GetComponent<CellPhoneItem>().CountingTurn();
            }
            for(int i = 0; i<zombiePrefab.Length; ++i)
            {
                zombiePrefab[i].GetComponent<ZombieAI>().IsTurn = true;
                zombiePrefab[i].GetComponent<ZombieAI>().TurnPlaying();
            }
        }
    }

    public void UseCellPhoneItem()
    {
        cellPhone = GameObject.FindWithTag("CellPhoneItem");

        for(int i = 0; i< zombiePrefab.Length; i++)
        {
            zombiePrefab[i].GetComponent<ZombieAI>().SetState(ZombieAI.State.Trace, cellPhone);
        }
        SetZombieTurn(true);
    }
    public void DestroyCellPhoneItem()
    {
        Debug.Log(zombiePrefab.Length);
        for (int i = 0; i < zombiePrefab.Length; i++)
        {
            zombiePrefab[i].GetComponent<ZombieAI>().SetState(ZombieAI.State.Pace, null);
        }
        SetZombieTurn(true);
    }
    //public bool GetZombieTurn()
    //{
    //    return isTurn;
    //}
}
