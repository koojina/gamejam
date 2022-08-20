using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
  void Start()
    {
        Invoke("LoadMain", 5f);
    }

    void LoadMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
