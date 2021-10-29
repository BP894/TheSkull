using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float health = 30;

    public void Use(GameObject target)
    {
        LifeEntity life = target.GetComponent<LifeEntity>();

        if(life != null)
        {
            life.RestoreHealth(health);
        }

        Destroy(gameObject);
    }
}
