using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenhirStone : MonoBehaviour
{
    private MeshRenderer[] stoneRenderers;
    private Color decalColor;
    private Color decakEmissionColor;

    public GameObject[] buffImage;

    enum State
    {
        Rest,
        Ready
    };
    private State state;

    private float lastBlessTime;
    private float timeBetBless;
    private int count;
    private void Awake()
    {
        stoneRenderers = GetComponentsInChildren<MeshRenderer>();
        decalColor = stoneRenderers[0].material.GetColor("_DecalsColor");
        decakEmissionColor = stoneRenderers[0].material.GetColor("_DecakEmissionColor");

        state = State.Rest;
        timeBetBless = Random.Range(10f, 20f);
        lastBlessTime = 0f;
    }
    private void Start()
    {
        for (int i = 0; i < stoneRenderers.Length; i++)
        {
            stoneRenderers[i].material.SetColor("_DecalsColor", Color.black);
            stoneRenderers[i].material.SetColor("_DecakEmissionColor", Color.black);
        }
    }
    private void Update()
    {
        SetStoneAbility();
    }
    private void SetStoneAbility()
    {
        if (Time.time >= lastBlessTime + timeBetBless)
        {
            if (state == State.Rest)
            {
                state = State.Ready;
                for (int i = 0; i < stoneRenderers.Length; i++)
                {
                    stoneRenderers[i].material.SetColor("_DecalsColor", decalColor);
                    stoneRenderers[i].material.SetColor("_DecakEmissionColor", decakEmissionColor);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Ready)
        {
            lastBlessTime = Time.time;
            timeBetBless = Random.Range(10f, 20f);
            state = State.Rest;
            for (int i = 0; i < stoneRenderers.Length; i++)
            {
                stoneRenderers[i].material.SetColor("_DecalsColor", Color.black);
                stoneRenderers[i].material.SetColor("_DecakEmissionColor", Color.black);
            }
            Debug.Log("이로운 효과!");
            StartCoroutine(Buff(collision));
        }
    }
    IEnumerator Buff(Collision cs)
    {
        int buffNumber = Random.Range(1, 2);

        buffImage[0].SetActive(false);
        buffImage[buffNumber].SetActive(true);
        switch (buffNumber)
        {
            case 1:
                yield return new WaitForSeconds(15f);
                break;
            case 2:
                StartCoroutine(GodMode(cs));
                yield return new WaitForSeconds(5f);
                break;
            case 3:
                StartCoroutine(Heal(cs));
                yield return new WaitForSeconds(10f);
                break;
        }

        buffImage[buffNumber].SetActive(false);
        buffImage[0].SetActive(true);
    }
    IEnumerator Heal(Collision c)
    {
        float health = 1f;
        float totalHealMount = 0;

        LifeEntity life = c.gameObject.GetComponent<LifeEntity>();
        if (life != null)
        {
            yield return new WaitForSeconds(1.0f);
            while (totalHealMount < 10)
            {
                life.RestoreHealth(health);
                totalHealMount += health;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
    IEnumerator GodMode(Collision c)
    {
        Material playerMaterial = c.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        LifeEntity life = c.gameObject.GetComponent<LifeEntity>();

        playerMaterial.SetFloat("_Metallic", 1.0f);
        life.mode = false;
        yield return new WaitForSeconds(5.0f);

        playerMaterial.SetFloat("_Metallic", 0.0f);
        life.mode = true;
    }
}
