using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;
    public float score = 0;
    public bool isGameOver { get; private set; }
    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }
    public void AddScore(float newScore)
    {
        if(!isGameOver)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }
    public void EndGame()
    {
        isGameOver = true;
        UIManager.instance.GoToGameoverScene();
    }
}
