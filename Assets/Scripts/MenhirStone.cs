using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenhirStone : MonoBehaviour
{
    private MeshRenderer[] stoneRenderers;
    private Color decalColor;
    private Color decakEmissionColor;

    enum State
    {
        Rest,
        Ready
    };
    private State state;

    private float lastBlessTime = 0f;
    private float timeBetBless;
    private void Awake()
    {
        stoneRenderers = GetComponentsInChildren<MeshRenderer>();
        decalColor = stoneRenderers[0].material.GetColor("_DecalsColor");
        decakEmissionColor = stoneRenderers[0].material.GetColor("_DecakEmissionColor");

        state = State.Rest;
        timeBetBless = Random.Range(10f, 20f);
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
        }
    }
}
