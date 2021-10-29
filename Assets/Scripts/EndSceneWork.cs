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
        scoreText.text = "Score : " + string.Format("{0:N1}", GameManager.instance.score);
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
