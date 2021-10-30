using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : LifeEntity
{
    public LayerMask whatIsTarget;

    private LifeEntity targetEntity;
    private NavMeshAgent pathFinder;

    //public ParticleSystem hitEffect;
    public AudioClip deathSound;
    public AudioClip hitSound;

    private Animator skeletonAnimator;
    private AudioSource skeletonAudioPlayer;
    private Renderer[] skeletonRenderers;

    public float damage = 20f;
    public float timeBetAttack = 0.5f;
    private float lastAttackTime;

    private bool hasTarget
    {
        get
        {
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        }
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        skeletonAnimator = GetComponent<Animator>();
        skeletonAudioPlayer = GetComponent<AudioSource>();
        skeletonRenderers = GetComponentsInChildren<Renderer>();
    }
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor)
    {
        startingHealth = newHealth;
        health = newHealth;
        damage = newDamage;
        pathFinder.speed = newSpeed;
        for (int i = 0; i < skeletonRenderers.Length; i++)
        {
            skeletonRenderers[i].material.color = skinColor;
        }
    }
    private void Start()
    {
        StartCoroutine(UpdatePath());
    }
    private void Update()
    {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션을 재생
        skeletonAnimator.SetBool("HasTarget", hasTarget);
    }
    IEnumerator UpdatePath()
    {
        while(!dead)
        {
            if(hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                pathFinder.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);
                for(int i = 0; i < colliders.Length; i++)
                {
                    LifeEntity lifeEntity = colliders[i].GetComponent<LifeEntity>();
                    if (lifeEntity != null && !lifeEntity.dead)
                    { 
                        targetEntity = lifeEntity;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        //hitEffect.transform.position = hitPoint;
        //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        //hitEffect.Play();
        skeletonAudioPlayer.PlayOneShot(hitSound);
    }
    public override void Die()
    {
        base.Die();
        Collider[] skeletonColliders = GetComponents<Collider>();
        for(int i = 0; i < skeletonColliders.Length; i++)
        {
            skeletonColliders[i].enabled = false;
        }
        pathFinder.isStopped = true;
        pathFinder.enabled = false;
        skeletonAnimator.SetTrigger("Die");
        skeletonAudioPlayer.PlayOneShot(deathSound);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            LifeEntity attackTarget = other.GetComponent<LifeEntity>();
            if(attackTarget != null && attackTarget ==  targetEntity && attackTarget.mode)
            {
                lastAttackTime = Time.time;
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
