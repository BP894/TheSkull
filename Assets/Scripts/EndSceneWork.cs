using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneWork : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        FinalScore();
    }
    private void FinalScore()
    {
        if (GameManager.instance != null)
        {
            scoreText.text = "Score : " + string.Format("{0:N1}", GameManager.instance.score);
        }
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
        Destroy(GameObject.Find("GameManager"));
    } 
}
