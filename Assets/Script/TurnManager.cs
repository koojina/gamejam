using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameObject[] zombiePrefab;

    public bool isTurn;

    // Start is called before the first frame update
    void Start()
    {
        isTurn = false;
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
            for(int i = 0; i<zombiePrefab.Length; ++i)
            {
                zombiePrefab[i].GetComponent<ZombieAI>().TurnPlaying();
            }
        }
    }
    public bool GetZombieTurn()
    {
        return isTurn;
    }
}
