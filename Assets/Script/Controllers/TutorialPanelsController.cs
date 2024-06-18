using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialPanel.SetActive(false);
        }
    }
}
