using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;

    public void Return()
    {
        if (scene1.activeSelf == true)
        {
            return;
        }

        if (scene2.activeSelf == true)
        {
            scene2.SetActive(false);
            scene1.SetActive(true);
            return;
        }

        if (scene3.activeSelf == true)
        {
            scene3.SetActive(false);
            scene2.SetActive(true);
            return;
        }

        if (scene4.activeSelf == true)
        {
            scene4.SetActive(false);
            scene3.SetActive(true);
            return;
        }
    }

    public void Skip()
    {
        if (scene1.activeSelf == true)
        {
            scene1.SetActive(false);
            scene2.SetActive(true);
            return;
        }

        if (scene2.activeSelf == true)
        {
            scene2.SetActive(false);
            scene3.SetActive(true);
            return;
        }

        if (scene3.activeSelf == true)
        {
            scene3.SetActive(false);
            scene4.SetActive(true);
            return;
        }

        if (scene4.activeSelf == true)
        {
            SceneManager.LoadScene("Nvl Tutorial");
        }
    }
}
