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
    
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _currentLevelText;
    private TextMeshProUGUI _nextLevelText;
    
    private Slider _slider;
    private const int Rate = 10;

    public bool isActive = true;
    public int Score { get; set; }
    public int Level { get; set; }

    private void Awake()
    {
        Level = 1;
        if (!Instance) Instance = this;
        else Destroy(Instance.gameObject);

        _scoreText = uiObj.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _slider = uiObj.gameObject.transform.GetChild(1).GetComponent<Slider>();
        _currentLevelText = uiObj.gameObject.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();
        _nextLevelText = uiObj.gameObject.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>();

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
        if (_slider.value + score > _slider.maxValue)
        {
            NextLevel();
            ResetSlider(Level);
            ShowLevelUpPanel();
        }
        else
        {
            _slider.value += score;
        }
    }

    public void NextLevel()
    {
        Level++;
        LoadLevelText();
    }

    private void LoadScoreText()
    {
        _scoreText.text = Score.ToString();
    }

    private void LoadLevelText()
    {
        _currentLevelText.text = Level.ToString();
        _nextLevelText.text = (Level + 1).ToString();
    }

    private void ResetSlider(int level)
    {
        _slider.value = 0;
        _slider.maxValue = level * Rate;
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

    public void ResetGame()
    {
        Level = 1;
        Score = 0;
        
        LoadScoreText();
        LoadLevelText();
        HideLevelUpPanel();
        ResetSlider(Level);
        BallController.Instance.ClearAllBall();
    }
}
