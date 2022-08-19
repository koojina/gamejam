using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class Toggle : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //아웃라인 키는 부분
            if(Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<Outline>().enabled = !GetComponent<Outline>().enabled;
            }
        }
    }
}