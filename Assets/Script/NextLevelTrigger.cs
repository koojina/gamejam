using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelTrigger : MonoBehaviour
{
    Fade myFade;
    // Start is called before the first frame update
    void Start()
    {
        myFade = this.GetComponent<Fade>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // 페이드 인/아웃
            myFade.FadeInStart();
            StartCoroutine(OpenLevel());
        }
    }
    IEnumerator OpenLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("2Stage");
    }
}
