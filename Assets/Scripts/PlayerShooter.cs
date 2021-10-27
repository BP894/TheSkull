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

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gun[3].gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        gun[3].gameObject.SetActive(false);
    }
    private void Update()
    {
        if(playerInput.fire)
        {
            gun[3].Fire();
        }
        else if (playerInput.reload)
        {
            if(gun[3].Reload())
            {
                playerAnimator.SetTrigger("Reload");
            }
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        //gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        //IK를 이용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤.
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f); // 가중치 100%
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f); // 가중치 100%
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount[3].position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount[3].rotation);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount[3].position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount[3].rotation);
    }
}
