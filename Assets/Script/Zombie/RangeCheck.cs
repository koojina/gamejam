using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    public ZombieAI.State state;

    public GameObject myZombie;

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
        //Debug.Log(zombie.transform.position);
        transform.position = zombie.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            zombie.SetState(state, other.gameObject);

            if (state == ZombieAI.State.Attack)
            {
                Debug.Log("공격 딜레이 실행");
                zombie.atkDelay++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 공격 범위에서 벗어난 경우 (추격 범위에는 적용X)
        if(other.gameObject.tag == "Player"&& state == ZombieAI.State.Attack)
        {
            zombie.atkDelay = 0;

            zombie.SetState(ZombieAI.State.Trace, other.gameObject);
        }
    }

}
