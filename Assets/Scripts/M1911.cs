using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1911 : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        if (playerShooter != null)
        {
            playerShooter.gun[playerShooter.gunNumber].gameObject.SetActive(false);
            playerShooter.gunNumber = playerShooter.M1911;
            playerShooter.gun[playerShooter.gunNumber].gameObject.SetActive(true);
        }
    }
}
