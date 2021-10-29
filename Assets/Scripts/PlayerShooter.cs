using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun[] gun;
    public Transform gunPivot;
    public Transform[] leftHandMount;
    public Transform[] rightHandMount;

    private PlayerInput playerInput;
    private Animator playerAnimator;

    public int AK74 = 1;
    public int M4 = 2;
    public int M1911 = 3;

    public int gunNumber;
    private void Awake()
    {
        gunNumber = 3;
    }
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gun[gunNumber].gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        gun[gunNumber].gameObject.SetActive(false);
    }
    private void Update()
    {
        if(playerInput.fire)
        {
            gun[gunNumber].Fire();
        }
        else if (playerInput.reload)
        {
            if(gun[gunNumber].Reload())
            {
                playerAnimator.SetTrigger("Reload");
            }
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        if(gun != null && UIManager.instance != null)
        {
            switch(gunNumber)
            {
                case 1:
                    UIManager.instance.UpdateAmmoText(gun[gunNumber].magAmmo, gun[gunNumber].ammoRemain, Color.red, gunNumber);
                    break;
                case 2:
                    UIManager.instance.UpdateAmmoText(gun[gunNumber].magAmmo, gun[gunNumber].ammoRemain, Color.cyan, gunNumber);
                    break;
                case 3:
                    UIManager.instance.UpdateAmmoText(gun[gunNumber].magAmmo, gun[gunNumber].ammoRemain, Color.yellow, gunNumber);
                    break;
            }
            
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        //gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        //IK를 이용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤.
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f); // 가중치 100%
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f); // 가중치 100%
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount[gunNumber].position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount[gunNumber].rotation);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount[gunNumber].position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount[gunNumber].rotation);
    }
}
