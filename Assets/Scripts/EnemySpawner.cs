using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Skeleton skeletonPrefab;

    public Transform[] spawnPoints;

    public float damageMax = 40f;
    public float damageMin = 20f;

    public float healthMax = 200f;
    public float healthMin = 100f;

    public float speedMax = 1f;
    public float speedMin = 0.4f;

    public float time = 0f;
    public float score = 0f;
    public Color strongSkeletonColor = Color.red;

    private List<Skeleton> skeletons = new List<Skeleton>();
    private int wave;

    private void Update()
    {
        if(GameManager.instance != null && GameManager.instance.isGameOver)
        {
            return;
        }
        if(skeletons.Count <= 0)
        {
            SpawnWave();
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        time += Time.deltaTime;
        score = wave * time;
        UIManager.instance.UpdateWaveText(wave, skeletons.Count);
        UIManager.instance.UpdateScoreText(score);
    }
    private void SpawnWave()
    {
        wave++;
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);
        for ( int i = 0; i < spawnCount; i++)
        {
            float skeletonIntensity = Random.Range(0f, 1f);
            CreatEnemy(skeletonIntensity);
        }
    }
    private void CreatEnemy(float intensity)
    {
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed = Mathf.Lerp(speedMin, speedMax, intensity);

        Color skinColor = Color.Lerp(Color.white, strongSkeletonColor, intensity);
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Skeleton skeleton = Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);
        skeleton.Setup(health, damage, speed, skinColor);
        skeletons.Add(skeleton);

        skeleton.onDeath += () => skeletons.Remove(skeleton);
        skeleton.onDeath += () => Destroy(skeleton.gameObject, 10f);
    }
}
