using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public int lives = 20;
    public int coin = 100;

    public TMP_Text coinText;
    public TMP_Text livesText;
    public void LoseLife(int l = 1)
    {
        lives -= l;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void Update()
    {
        coinText.text = coin.ToString();
        livesText.text = lives.ToString();
    }
}
