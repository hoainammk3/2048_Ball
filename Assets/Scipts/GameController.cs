using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    public GameObject uiObj;
    public GameObject levelUpPanel;
    public GameObject gameOverPanel;
    
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI currentLevelText;
    private TextMeshProUGUI nextLevelText;
    
    private Slider slider;
    private const int Rate = 10;

    public bool isActive = true;
    public int Score { get; set; }
    public int Level { get; set; }

    private void Awake()
    {
        Level = 1;
        if (!Instance) Instance = this;
        else Destroy(Instance.gameObject);

        scoreText = uiObj.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        slider = uiObj.gameObject.transform.GetChild(1).GetComponent<Slider>();
        currentLevelText = uiObj.gameObject.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        nextLevelText = uiObj.gameObject.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>();

        LoadScoreText();
        LoadLevelText();
        HideLevelUpPanel();
        ResetSlider(Level);
    }

    private void Start()
    {
        LoadScoreText();
        LoadLevelText();
    }

    public void IncreaseScore(int score)
    {
        Score += score;
        LoadScoreText();
        if (slider.value + score > slider.maxValue)
        {
            NextLevel();
            ResetSlider(Level);
            ShowLevelUpPanel();
        }
        else
        {
            slider.value += score;
        }
    }

    public void NextLevel()
    {
        Level++;
        LoadLevelText();
    }

    private void LoadScoreText()
    {
        scoreText.text = Score.ToString();
    }

    private void LoadLevelText()
    {
        currentLevelText.text = Level.ToString();
        nextLevelText.text = (Level + 1).ToString();
    }

    private void ResetSlider(int level)
    {
        slider.value = 0;
        slider.maxValue = level * Rate;
    }

    private void ShowLevelUpPanel()
    {
        levelUpPanel.SetActive(true);
        isActive = false;
    }

    public void HideLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
        isActive = true;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);        
    }
    
    public void HideGameOverPanel()
     {
         gameOverPanel.SetActive(false);
     }
}
