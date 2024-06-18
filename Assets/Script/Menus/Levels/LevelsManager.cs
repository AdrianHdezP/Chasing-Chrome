using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour, IPointerEnterHandler
{
    private static LevelsManager instance;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI note;
    [Space]
    [SerializeField] private GameObject[] notesImages;

    private string selectedData;

    private int minutes;
    private int seconds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Levels Manager in the scene");
        }

        instance = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterLevelTutorial(eventData);
        OnPointerLevel1(eventData);
        OnPointerLevel2(eventData);
        OnPointerLevel3(eventData);
        OnPointerLevel4(eventData);
        OnPointerLevel5(eventData);
        OnPointerLevel6(eventData);
        OnPointerLevel7(eventData);
        OnPointerLevel8(eventData);
        OnPointerLevel9(eventData);
        OnPointerLevel10(eventData);
    }

    private void OnSelected()
    {
        OnSelectedLevelTutorial(selectedData);
        OnSelectedLevel1(selectedData);
        OnSelectedLevel2(selectedData);
        OnSelectedLevel3(selectedData);
        OnSelectedLevel4(selectedData);
        OnSelectedLevel5(selectedData);
        OnSelectedLevel6(selectedData);
        OnSelectedLevel7(selectedData);
        OnSelectedLevel8(selectedData);
        OnSelectedLevel9(selectedData);
        OnSelectedLevel10(selectedData);
    }

    private void CheckForSelectedData()
    {
        if (ControlDevice.instance.IsGamepadActive())
            selectedData = EventSystem.current.currentSelectedGameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>().name;
        else
            selectedData = null;
    }

    private void Update()
    {
        CheckForSelectedData();
        OnSelected();
    }

    #region On Pointer Enter

    private static void OnPointerEnterLevelTutorial(PointerEventData onPointerEventData)
    {
        if (onPointerEventData.pointerEnter.gameObject.name == "Level Tutorial")
        {
            instance.CalculateTime(onPointerEventData.pointerEnter.gameObject.name);
            instance.CalculateNote(onPointerEventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel1(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 1")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name); 
        }
    }

    private static void OnPointerLevel2(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 2")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel3(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 3")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel4(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 4")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel5(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 5")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel6(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 6")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel7(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 7")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel8(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 8")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel9(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 9")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    private static void OnPointerLevel10(PointerEventData eventData)
    {
        if (eventData.pointerEnter.gameObject.name == "Level 10")
        {
            instance.CalculateTime(eventData.pointerEnter.gameObject.name);
            instance.CalculateNote(eventData.pointerEnter.gameObject.name);
        }
    }

    #endregion

    #region On Selected Enter

    private static void OnSelectedLevelTutorial(string data)
    {
        if (data == "Level Tutorial")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel1(string data)
    {
        if (data == "Level 1")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel2(string data)
    {
        if (data == "Level 2")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel3(string data)
    {
        if (data == "Level 3")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel4(string data)
    {
        if (data == "Level 4")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel5(string data)
    {
        if (data == "Level 5")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel6(string data)
    {
        if (data == "Level 6")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel7(string data)
    {
        if (data == "Level 7")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel8(string data)
    {
        if (data == "Level 8")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel9(string data)
    {
        if (data == "Level 9")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    private static void OnSelectedLevel10(string data)
    {
        if (data == "Level 10")
        {
            instance.CalculateTime(data);
            instance.CalculateNote(data);
        }
    }

    #endregion

    #region Calculate Time & Notes

    public void CalculateTime(string level)
    {
        if (PlayerPrefs.HasKey(level + "Time"))
        {
            float timer = float.Parse(PlayerPrefs.GetString(level + "Time"));
            minutes = (int)(timer / 60);
            seconds = (int)(timer - minutes * 60f);
            time.text = string.Format("TIME: {0}:{1:00}", minutes, seconds);
        }
        else
        {
            time.text = "TIME: LEVEL INCOMPLETE";
        }
    }

    public void CalculateNote(string level)
    {
        for (int i = 0; i < notesImages.Length; i++)
        {
            notesImages[i].SetActive(false);
        }

        if (PlayerPrefs.HasKey(level + "Note"))
        {
            note.text = "NOTE:";

            CalculateNoteImages(level);
        }
        else
            note.text = "NOTE: LEVEL INCOMPLETE";
    }

    private void CalculateNoteImages(string level)
    {
        if (PlayerPrefs.GetString(level + "Note") == "E")
            notesImages[0].SetActive(true);

        if (PlayerPrefs.GetString(level + "Note") == "D")
            notesImages[1].SetActive(true);

        if (PlayerPrefs.GetString(level + "Note") == "C")
            notesImages[2].SetActive(true);

        if (PlayerPrefs.GetString(level + "Note") == "B")
            notesImages[3].SetActive(true);

        if (PlayerPrefs.GetString(level + "Note") == "A")
            notesImages[4].SetActive(true);

        if (PlayerPrefs.GetString(level + "Note") == "S")
            notesImages[5].SetActive(true);
    }

    #endregion

    #region Go To Levels

    public void GoToLevel1() => SceneManager.LoadScene("Level 1");

    #endregion

}