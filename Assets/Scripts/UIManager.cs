using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private static UIManager m_instance;

    public Text ammoText;
    public Text scoreText;
    public Text waveText;
    public GameObject[] gunImage;
    public GameObject gameManager;

    private int gunNumber = 3;

    public void UpdateAmmoText(int magAmmo, int remainAmmo, Color textColor, int number)
    {
        ammoText.text = magAmmo + " / " + remainAmmo;
        ammoText.color = textColor;
        gunImage[gunNumber].SetActive(false);
        gunNumber = number;
        gunImage[gunNumber].SetActive(true);
    }
    public void UpdateScoreText(float newScore)
    {
        scoreText.text = "Score : " + string.Format("{0:N1}", newScore);
    }
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }
    public void GoToGameoverScene()
    {
        //gameoverUI.SetActive(active);
        SceneManager.LoadScene(2);
        DontDestroyOnLoad(gameManager);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
