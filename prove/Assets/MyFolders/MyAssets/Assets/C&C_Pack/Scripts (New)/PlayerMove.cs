using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private CharacterController charController;
    private CharacterAnimations playerAnimations;

    public float movement_speed = 3f;
    public float gravity = 9.8f;
    public float rotation_speed = 0.15f;
    public float rotateDegreesPerSecond = 180f;


    // Use this for initialization
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimations = GetComponent<CharacterAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        AnimateWalk();
    }

    void Move()
    {
        if (Input.GetAxis(Axis.VERTICAL_AXIS) > 0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }
        else if (Input.GetAxis(Axis.VERTICAL_AXIS) < 0)
        {
            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }
        else
        {
            charController.Move(Vector3.zero);
        }
    }

    void Rotate()
    {
        Vector3 rotation_direction = Vector3.zero;

        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0)
        {
            rotation_direction = transform.TransformDirection(Vector3.left);
        }
        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0)
        {
            rotation_direction = transform.TransformDirection(Vector3.right);
        }
        if (rotation_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_direction),
                                 rotateDegreesPerSecond * Time.deltaTime);
        }
    }

    void AnimateWalk()
    {
        if (charController.velocity.sqrMagnitude != 0f)
        {
            playerAnimations.Walk(true);
        }
        else
        {
            playerAnimations.Walk(false);
        }
    }

}
