using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstTimeline;
    // [SerializeField] private GameObject secondTimeline;

    [SerializeField] private GameObject unstableInstructions;
    [SerializeField] private GameObject batteriesCollectedInstructions;
    [SerializeField] private GameObject playerDiedInstructions;
    [SerializeField] private GameObject playerGameObject;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject am = new GameObject("GameManager");
                am.AddComponent<GameManager>();
            }

            return _instance;
        }
    }


    [SerializeField] public Slider playerHealthSlider;
    [SerializeField] public Slider energyLevelSlider;
    [SerializeField] public Slider stabilityLevelSlider;

    [SerializeField] public Image playerHealthSliderImageFill;
    [SerializeField] public Image energyLevelSliderImageFill;
    [SerializeField] public Image stabilityLevelSliderImageFill;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip okyanus;

    public int killedEnemyNumbers = 0;
    public int collectedBatteryNumber = 0;

    private void Awake()
    {
        _instance = this;
    }

    // public Image eButton;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        //energyLevelSlider.value = 0;
        energyLevelSliderImageFill.fillAmount = 0f;
        // stabilityLevelSlider.value = 0;
        stabilityLevelSliderImageFill.fillAmount = 0f;
    }

    private void Update()
    {
        // if (stabilityLevelSlider.value <= 0)
        // {
        //     stabilityLevelSlider.value = 0;
        // }

        if (stabilityLevelSliderImageFill.fillAmount <= 0)
        {
            stabilityLevelSliderImageFill.fillAmount = 0;
        }
        
        // if (energyLevelSlider.value >= 5)
        // {
        //     
        //     print("WIN WIN WIN!!!");
        //     StartCoroutine(AllBatteriesCollected());
        // }
        if ( energyLevelSliderImageFill.fillAmount>=1)
        {
            StartCoroutine(AllBatteriesCollected());
        }

        

        CheckPlayersHealth();
        CheckStabilityLevels();
        // FillButtons();
    }

    public void CheckStabilityLevels()
    {
        // if (stabilityLevelSlider.value >= 99)
        // {
        //     StartCoroutine(UnstableGameOver());
        // }

        if (stabilityLevelSliderImageFill.fillAmount  >= 1)
        {
            StartCoroutine(UnstableGameOver());
        }
    }

    IEnumerator AllBatteriesCollected()
    {
        batteriesCollectedInstructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameFinish");
    }
    IEnumerator UnstableGameOver()
    {
        unstableInstructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("FirstLevel");
    }

    public void CheckPlayersHealth()
    {
        // if (playerHealthSlider.value <= 1)
        // {
        //     StartCoroutine(PlayerDied());
        // }

        if (playerHealthSliderImageFill.fillAmount <=0.01f)
        {
            StartCoroutine(PlayerDied());
        }
    }

    IEnumerator PlayerDied()
    {
        playerGameObject.SetActive(false);
        playerDiedInstructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("FirstLevel");
    }

    public void IncreaseCollectedBattery()
    {
        //energyLevelSlider.value++;
        energyLevelSliderImageFill.fillAmount += 0.2f;
        collectedBatteryNumber++;
        
    }

    public void IncreaseStability(float amount)
    {
        // stabilityLevelSlider.value += amount;
        stabilityLevelSliderImageFill.fillAmount += amount;
    }

    public void DecreaseStability(float amount)
    {
        // stabilityLevelSlider.value -= amount;
        stabilityLevelSliderImageFill.fillAmount -= amount;

    }

    // public void FillButtons()
    // {
    //     if (Input.GetKey(KeyCode.E))
    //     {
    //         eButton.fillAmount = eButton.fillAmount + 0.01f ;
    //         if (eButton.fillAmount >= 0.95f)
    //         {
    //             print("Completed");
    //         }
    //     }
    //     else
    //     {
    //         eButton.fillAmount = eButton.fillAmount - 0.01f ;
    //
    //     }
    // }

    
    public void PlayBackgroundMusic()
    {
        _audioSource.Play();
    }
}