using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class KinematicScene : MonoBehaviour
{
    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += LoadMainMenu;
    }

    private void LoadMainMenu(UnityEngine.Video.VideoPlayer vp) => SceneManager.LoadScene("Main Menu");

    public void LoadMainmenu() => SceneManager.LoadScene("Main Menu");

}
