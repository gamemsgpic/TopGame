using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TopGameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;
    public GameObject youWinText;
    public GameObject gameOverText;

    public TextMeshProUGUI timeText;

    private PlayerHealth playerHealth;
    private MonsterController monsterController;

    private float surviveTime;

    private bool isGameOVer;

    private void Awake()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        monsterController = monster.GetComponent<MonsterController>();
        youWinText.SetActive(false);
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (playerHealth != null && playerHealth.isDeath)
        {
            OnEndGame();
        }

        if (monsterController != null && monsterController.isDeath)
        {
            OnEndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (isGameOVer)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("SampleScene");
            }
            return;
        }

        surviveTime += Time.deltaTime;
        timeText.text = $"Time: {Mathf.FloorToInt(surviveTime)}";
    }

    public void OnEndGame()
    {
        isGameOVer = true;
        if (playerHealth.isDeath)
        {
            gameOverText.SetActive(true);
        }
        else if (monsterController.isDeath)
        {
            youWinText.SetActive(true);
        }
    }
}