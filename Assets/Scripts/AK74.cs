using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK74 : MonoBehaviour, IItem
{
    public int ammo = 30;
    public void Use(GameObject target)
    {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();
        
        if(playerShooter != null)
        {
            playerShooter.gun[playerShooter.gunNumber].gameObject.SetActive(false);
            playerShooter.gunNumber = playerShooter.AK74;
            playerShooter.gun[playerShooter.gunNumber].gameObject.SetActive(true);

            playerShooter.gun[playerShooter.gunNumber].ammoRemain += ammo;
        }

        Destroy(gameObject);
    }
}
