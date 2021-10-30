using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenhirStone : MonoBehaviour
{
    private MeshRenderer[] stoneRenderers;
    private Color decalColor;
    private Color decakEmissionColor;

    GameObject[] buffImage;
    public GameObject buffText;
    public GameObject border;
    GameObject name;
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
        buffImage = new GameObject[border.transform.childCount];
    }
    private void Start()
    {
        for (int i = 0; i < stoneRenderers.Length; i++)
        {
            stoneRenderers[i].material.SetColor("_DecalsColor", Color.black);
            stoneRenderers[i].material.SetColor("_DecakEmissionColor", Color.black);
        }
        for (int i = 0; i < border.transform.childCount; i++)
        {
            buffImage[i] = border.transform.GetChild(i).gameObject;
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
        if (state == State.Ready && buffImage[0].activeSelf)
        {
            lastBlessTime = Time.time;
            timeBetBless = Random.Range(10f, 20f);
            state = State.Rest;
            for (int i = 0; i < stoneRenderers.Length; i++)
            {
                stoneRenderers[i].material.SetColor("_DecalsColor", Color.black);
                stoneRenderers[i].material.SetColor("_DecakEmissionColor", Color.black);
            }
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
                StartCoroutine(SpeedUpText());
                StartCoroutine(SpeedUp(cs));
                yield return new WaitForSeconds(15f);

                break;
            case 2:
                StartCoroutine(GodModeText());
                StartCoroutine(GodMode(cs));
                yield return new WaitForSeconds(5f);

                break;
            case 3:
                StartCoroutine(HealText());
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
    IEnumerator HealText()
    {
        buffText.GetComponent<Text>().text = "당신에게 회복의 축복이 내립니다...";
        buffText.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        buffText.GetComponent<Text>().text = "";
        buffText.SetActive(false);
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
    IEnumerator GodModeText()
    {
        buffText.GetComponent<Text>().text = "당신에게 무적의 축복이 내립니다...";
        buffText.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        buffText.GetComponent<Text>().text = "";
        buffText.SetActive(false);
    }
    IEnumerator SpeedUp(Collision c)
    {
        PlayerMovement playerMovement = c.gameObject.GetComponent<PlayerMovement>();
        //Gun playerGun = c.gameObject.GetComponentInChildren<Gun>();
        Transform gunPivot = GameObject.Find("Gun Pivot").transform;

        for (int i = 0; i < gunPivot.childCount; i++)
        {
            gunPivot.GetChild(i).gameObject.GetComponent<Gun>().timeBetFire /= 1.5f;
        }
        playerMovement.moveSpeed *= 1.5f;
        //playerGun.timeBetFire /= 1.5f;

        yield return new WaitForSeconds(15.0f);

        for (int i = 0; i < gunPivot.childCount; i++)
        {
            gunPivot.GetChild(i).gameObject.GetComponent<Gun>().timeBetFire *= 1.5f;
        }
        playerMovement.moveSpeed /= 1.5f;
        //playerGun.timeBetFire *= 1.5f;
    }
    IEnumerator SpeedUpText()
    {
        buffText.GetComponent<Text>().text = "당신에게 속도의 축복이 내립니다...";
        buffText.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        buffText.GetComponent<Text>().text = "";
        buffText.SetActive(false);
    }
}
