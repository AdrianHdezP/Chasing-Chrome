using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public Player player;

    public bool isPaused {  get; private set; } 
    public float initialLevelTime { get; private set; }
    public int creditsValue { get; private set; }
    public int pointsMultiValue { get; private set; }
    public int scrapValue { get; private set; }
    public int currentBullets { get; private set; }
    public bool isInvisible { get; private set; }

    [Header("Time")]
    [HideInInspector] public bool isTimeStop;
    [SerializeField] private TextMeshProUGUI timerTMP;
    [SerializeField] private Slider timeSlider;
    public float levelTimer;
    private int minutes;
    private int seconds;
    private int levelIntTimer;  

    [Header("Credits")]
    [SerializeField] private TextMeshProUGUI creditsTMP;

    [Header("Multiplier")]
    [SerializeField] private GameObject[] multi;

    [Header("Scrap")]
    [SerializeField] private TextMeshProUGUI scrapTMP;
    [SerializeField] private GameObject scrapObject;

    [Header("Inventory")]
    public Item powerup;
    private Image powerupImage;
    public RectTransform powerupSpriteRectTransform;

    [Header("Bullets")]
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private GameObject noBulletsLeft;
    [SerializeField] private GameObject youHaveMaxBullets;
    public int maxBullets;

    [Header("Power-ups")]
    [SerializeField] private float boostForce;
    [Space]
    public float tackleForce;
    [HideInInspector] public bool isDoingTackle;
    [Space]
    public int invisibilityDuration;
    private float invisibilityTimer;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        #region Setup Variables

        isPaused = false;
        isTimeStop = false;

        timeSlider.maxValue = levelTimer;
        initialLevelTime = levelTimer;
        creditsValue = 0;
        pointsMultiValue = 1;
        scrapValue = 0; // Tendremos que pasar el valor atraves de las playerprefs no seteralo a cero

        isInvisible = false;
        isDoingTackle = false;

        #endregion

        SetupBullets();
    }

    private void Update()
    {
        IsPaused();
        LevelTimer();
        UpdateCanvas();
        UpdatePowerupsValues();

        if (levelTimer <= 0)
            GameOver();
    }

    private void IsPaused()
    {
        if (Time.timeScale == 0)
            isPaused = true;
        else
            isPaused = false;
    }

    #region Canvas

    private IEnumerator WarningAdvice(GameObject _warning, float _seconds)
    {
        _warning.SetActive(true);
        yield return new WaitForSeconds(_seconds);
        _warning.SetActive(false);
    }

    private void UpdateCanvas()
    {
        creditsTMP.SetText(creditsValue.ToString());

        if (creditsValue < 0)
            creditsValue = 0;

        if (pointsMultiValue < 1)
            pointsMultiValue = 1;
    }

    #region Timer

    private void LevelTimer()
    {
        if (isTimeStop)
            return;

        levelTimer -= Time.deltaTime;

        minutes = (int)(levelTimer / 60f);
        seconds = (int)(levelTimer - minutes * 60f);

        levelIntTimer = (int)levelTimer;    

        if (levelIntTimer < 0)
            levelIntTimer = 0;

        timeSlider.value = levelTimer;

        timerTMP.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    public void AddTime(int _amountOfTime) => levelTimer += _amountOfTime;

    public void SubtractTime(int _amountOfTime) => levelTimer -= _amountOfTime;

    #endregion

    #region Credits

    public void AddCredits(int _amountOfPoints)
    {
        _amountOfPoints *= pointsMultiValue;

        creditsValue += _amountOfPoints;
    }

    public void SubtractCredits(int _amountOfPoints) => creditsValue -= _amountOfPoints;

    #endregion

    #region Multi

    public void AddMulti()
    {
        for (int i = 0; i < pointsMultiValue;)
        {
            multi[i].SetActive(false);
            i++;

            if (multi[i].activeSelf == false)
            {
                pointsMultiValue += 1;
                multi[i].SetActive(true);
                break;
            }
        }
    }

    public void RestartMulti()
    {
        for(int i = 0;i < multi.Length; i++)
            multi[i].SetActive(false);

        pointsMultiValue = 1;
        multi[0].SetActive(true);
    }

    #endregion

    #region Scrap

    public void AddScrap()
    {
        scrapValue++;
        StartCoroutine(WarningAdvice(scrapObject, 1.5f));
    }

    #endregion

    #region Inventory

    public void AddPowerupsToTheInventory(GameObject _gameObject)
    {     
        powerup = _gameObject.GetComponent<Item>();

        powerupImage = powerupSpriteRectTransform.GetComponent<Image>();
        powerupImage.enabled = true;
        powerupImage.sprite = _gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public bool IsPowerupPickedUp()
    {
        if (powerup == null)
            return false;

        return true;
    }

    public void UsePowerups()
    {
        if (IsPowerupPickedUp())
        {
            powerup.UsePowerup();
            powerup = null;
            powerupImage.enabled = false;
            powerupImage.sprite = null;
        }
        else
            Debug.LogWarning("No powerup picked up");
    }

    #endregion

    #endregion

    #region Bullets

    private void SetupBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            currentBullets++;
            maxBullets++;
        }
    }

    public void UpdateBullets()
    {
        if(currentBullets >= 1)
        {
            currentBullets--;
            bullets[currentBullets].SetActive(false);
        }

        if (currentBullets <= 0)
            StartCoroutine(WarningAdvice(noBulletsLeft, 2.5f));
    }

    public void PickUpBullets(GameObject _itemToDestroy)
    {
        if (currentBullets == maxBullets)
        {
            StartCoroutine(WarningAdvice(youHaveMaxBullets, 2.5f));
            return;
        }

        if(currentBullets < maxBullets)
        {
            currentBullets++;
            bullets[currentBullets - 1].SetActive(true);
            Destroy(_itemToDestroy);
        }
    }

    #endregion

    #region Power-ups

    private void UpdatePowerupsValues()
    {
        invisibilityTimer -= Time.deltaTime;

        if (invisibilityTimer < 0)
            isInvisible = false;
    }

    public void Boost() => player.rb.velocity = new Vector2(player.rb.velocity.x, boostForce);

    public void Tackle()
    {
        isDoingTackle = true;
        player.stateMachine.ChangeState(player.tackleState);
    }

    public void Inisibility()
    {
        isInvisible = true;
        invisibilityTimer = invisibilityDuration;

        StartCoroutine(player.fx.InvisibleFx(invisibilityDuration));
    }

    public void StunGranade() => player.stateMachine.ChangeState(player.useGranade);

    #endregion

    private void GameOver() => SceneManager.LoadScene("Game Over");

    public void GoToMainMenu() => SceneManager.LoadScene("Main Menu");

}
