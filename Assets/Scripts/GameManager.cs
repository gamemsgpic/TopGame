using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestRecordText;

    private float surviveTime;

    private bool isGameOVer;

    private void Awake()
    {
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (isGameOVer)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
            return;
        }

        surviveTime += Time.deltaTime;
        timeText.text = $"Time: {Mathf.FloorToInt(surviveTime)}";
    }

    public void OnEndGame()
    {
        isGameOVer = true;
        gameOverText.SetActive(true);

        float bestTIme = PlayerPrefs.GetFloat("BestTime", 0f); // 주로 게임 옵션을 저장할 때 사용

        if (bestTIme < surviveTime)
        {
            bestTIme = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTIme);
        }
        bestRecordText.text = $"Best Record: {Mathf.FloorToInt(bestTIme)}";
    }
}