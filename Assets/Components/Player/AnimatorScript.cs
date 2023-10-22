using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatorScript : MonoBehaviour
{
    public Animator playerAnimator;
    private void Update()
    {
        Debug.Log("Animator Working");

        playerAnimator.SetBool("IsWalking", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A));
        playerAnimator.SetBool("IsJumping", Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W));
        playerAnimator.SetBool("IsAttacking", Input.GetKey(KeyCode.Mouse0));
        playerAnimator.SetBool("IsParrying", Input.GetKey(KeyCode.Mouse1));
    }

}
