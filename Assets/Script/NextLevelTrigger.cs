using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelTrigger : MonoBehaviour
{
    Fade myFade;

    public string levelName;

    // Start is called before the first frame update
    void Start()
    {
        myFade = GetComponent<Fade>();
        Debug.Log(myFade);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // 페이드 인/아웃
            StartCoroutine(myFade.FadeOutStart());
            StartCoroutine(OpenLevel());
        }
    }
    IEnumerator OpenLevel()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(levelName);
    }
}
