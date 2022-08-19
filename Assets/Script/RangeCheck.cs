using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    public ZombieAI.State state;

    GameObject myZombie;

    ZombieAI zombie;
    // Start is called before the first frame update
    void Start()
    {
        //myZombie = transform.Find("DummyZombie").gameObject;

        zombie = myZombie.GetComponent<ZombieAI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(state);
            zombie.SetState(state);
        }
    }
}
