using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject FadeImage;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator FadeInStart()
    {
        FadeImage.SetActive(true);
        for(float f = 1f; f>0; f-=0.02f)
        {
            Color c = FadeImage.GetComponent<Image>().color;
            c.a = f;
            FadeImage.GetComponent<Image>().color = c;  
            yield return null;  
        }
        yield return new WaitForSeconds(1);
        FadeImage.SetActive(false);
    }

    public IEnumerator FadeOutStart()
    {
        FadeImage.SetActive(true);

        for(float f = 0f; f<1; f+= 0.02f)
        {
            Color c = FadeImage.GetComponent<Image>().color;
            c.a = f;
            FadeImage.GetComponent <Image>().color = c;
            yield return null;
        }
    }
}
