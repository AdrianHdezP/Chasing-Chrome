using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalNoteManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Canvas")]
    [SerializeField] private GameObject finalNotePanel;
    [Space]
    [SerializeField] private TextMeshProUGUI initialLevelTimeTMP;
    private float initialLevelTime;
    private int initialMinutes;
    private int initialSeconds;
    [SerializeField] private TextMeshProUGUI remainingTimeTMP;
    private float remainingTime;
    private int minutes;
    private int seconds;
    [Space]
    [SerializeField] private TextMeshProUGUI obtaniedCreditsTMP;
    private int obtaniedCredits;
    [Space]
    [SerializeField] private TextMeshProUGUI finalCreditsTMP;
    private int finalCredits;

    [Header("Notes Values")]
    [SerializeField] private int e;
    [SerializeField] private int c;
    [SerializeField] private int b;
    [SerializeField] private int a;
    [SerializeField] private int s;
    [SerializeField] private GameObject eNote;
    [SerializeField] private GameObject dNote;
    [SerializeField] private GameObject cNote;
    [SerializeField] private GameObject bNote;
    [SerializeField] private GameObject aNote;
    [SerializeField] private GameObject sNote;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        finalCredits = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.player.canMove = false;

            gameManager.isTimeStop = true;

            finalNotePanel.SetActive(true);

            InitialLevelTime();
            RemainingTime();
            Credits();

            StartCoroutine(Secuence());
        }
    }

    private void InitialLevelTime()
    {
        initialLevelTime = gameManager.initialLevelTime;
        initialMinutes = (int)(initialLevelTime / 60);
        initialSeconds = (int)(initialLevelTime - initialMinutes * 60f);
        initialLevelTimeTMP.text = string.Format("LEVEL TIME - {0}:{1:00}", initialMinutes, initialSeconds);
    }

    private void RemainingTime()
    {
        remainingTime = gameManager.levelTimer;
        remainingTimeTMP.text = "0:00";
    }

    private int TimeTargetPoints(float remainingPoints)
    {
        if (remainingPoints <= 15)
            return 150;
        if (remainingPoints <= 30 && remainingPoints >= 16)
            return 300;
        if (remainingPoints <= 60 && remainingPoints >= 31)
            return 450;
        if (remainingPoints <= 90 && remainingPoints >= 61)
            return 600;
        else
            return 750;

    }

    private void Credits()
    {
        obtaniedCredits = gameManager.creditsValue;
        obtaniedCreditsTMP.text = "0";
    }

    private void FinalTime() => PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "Time", gameManager.levelTimer.ToString());

    private void FinalNote(int finalCredits)
    {
        string mySceneName = SceneManager.GetActiveScene().name;
        bool alreadyHaveNote = false;

        if (PlayerPrefs.HasKey(mySceneName + "Note"))
            alreadyHaveNote = true;

        if (finalCredits < e)
        {
            eNote.SetActive(true);

            if (alreadyHaveNote)
            {
                return;
            }
            else
            {
                PlayerPrefs.SetString(mySceneName + "Note", "E");
            }    
        }

        if (finalCredits < c && finalCredits >= e + 1)
        {
            dNote.SetActive(true);

            if (alreadyHaveNote)
            {
                string myNote = PlayerPrefs.GetString(mySceneName + "Note");

                if (myNote == "E")
                    PlayerPrefs.SetString(mySceneName + "Note", "D");
                else
                    return;
            }
            else
            {
                PlayerPrefs.SetString(mySceneName + "Note", "D");
            }
        }

        if (finalCredits < b && finalCredits >= c + 1)
        {
            cNote.SetActive(true);

            if (alreadyHaveNote)
            {
                string myNote = PlayerPrefs.GetString(mySceneName + "Note");

                if (myNote == "E" || myNote == "D")
                    PlayerPrefs.SetString(mySceneName + "Note", "C");
                else
                    return;
            }
            else
            {
                PlayerPrefs.SetString(mySceneName + "Note", "C");
            }
        }

        if (finalCredits < a && finalCredits >= b + 1)
        {
            bNote.SetActive(true);

            if (alreadyHaveNote)
            {
                string myNote = PlayerPrefs.GetString(mySceneName + "Note");

                if (myNote == "E" || myNote == "D" || myNote == "C")
                    PlayerPrefs.SetString(mySceneName + "Note", "B");
                else
                    return;
            }
            else
            {
                PlayerPrefs.SetString(mySceneName + "Note", "B");
            }
        }

        if (finalCredits < s && finalCredits >= a + 1)
        {
            aNote.SetActive(true);

            if (alreadyHaveNote)
            {
                string myNote = PlayerPrefs.GetString(mySceneName + "Note");

                if (myNote == "E" || myNote == "D" || myNote == "C" || myNote == "B")
                    PlayerPrefs.SetString(mySceneName + "Note", "A");
                else
                    return;
            }
            else
            {
                PlayerPrefs.SetString(mySceneName + "Note", "A");
            }
        }

        if (finalCredits >= s)
        {
            sNote.SetActive(true);

            if (alreadyHaveNote)
            {
                string myNote = PlayerPrefs.GetString(mySceneName + "Note");

                if (myNote != "S")
                    PlayerPrefs.SetString(mySceneName + "Note", "S");
                else
                    return;
            }
            else
            {
                PlayerPrefs.SetString(mySceneName + "Note", "S");
            }
        }
    }

    private IEnumerator LerpTimeValue(float start, float target, float lerpDuration)
    {
        float timeElapsed = 0;
        float current;

        while (timeElapsed < lerpDuration)
        {
            current = (int)Mathf.Lerp(start, target, timeElapsed / lerpDuration);
            minutes = (int)(current / 60);
            seconds = (int)(current - minutes * 60f);
            remainingTimeTMP.text = string.Format("{0}:{1:00}", minutes, seconds);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        current = target;
    }

    private IEnumerator LerpPointsValue(float start, float target, float lerpDuration, TextMeshProUGUI textToChange)
    {
        float timeElapsed = 0;
        float current;

        while (timeElapsed < lerpDuration)
        {
            current = (int)Mathf.Lerp(start, target, timeElapsed / lerpDuration);
            textToChange.text = current.ToString();
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        current = target;
        textToChange.text = current.ToString();
    }

    private IEnumerator Secuence()
    {
        float lerpDuration = 2;

        yield return new WaitForSeconds(1);

        StartCoroutine(LerpTimeValue(0, remainingTime, lerpDuration));

        yield return new WaitForSeconds(lerpDuration);

        finalCredits += TimeTargetPoints(remainingTime);
        StartCoroutine(LerpPointsValue(0, finalCredits, 2, finalCreditsTMP));

        yield return new WaitForSeconds(lerpDuration);

        StartCoroutine(LerpPointsValue(0, obtaniedCredits, lerpDuration, obtaniedCreditsTMP));

        yield return new WaitForSeconds(lerpDuration);

        int credits = finalCredits;
        finalCredits += obtaniedCredits;
        StartCoroutine(LerpPointsValue(credits, finalCredits, 2, finalCreditsTMP));

        yield return new WaitForSeconds(lerpDuration);

        FinalTime();
        FinalNote(finalCredits);

        yield return new WaitForSeconds(5);

        gameManager.GoToMainMenu();
    }
}
