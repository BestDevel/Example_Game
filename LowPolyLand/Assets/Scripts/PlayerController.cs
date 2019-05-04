using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour {

    private Animator mAnimator;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

     void Update()
    {
        bool Walking = Input.GetKey(KeyCode.W);
        mAnimator.SetBool("Walking", Walking);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            mAnimator.SetTrigger("Attack");
        }
    }
}
