using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public GameObject zombie;

    public bool activeMode; 
    // Start is called before the first frame update
    void Start()
    {
        activeMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = zombie.transform.position;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player"&& activeMode == true)
        {
            zombie.GetComponent<Animator>().SetBool("z_attack", true);
        }
    }
}
