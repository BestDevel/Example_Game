using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingForce : MonoBehaviour
{

    public float hoverForce = 5;
    private Rigidbody rb;
    public Collider other;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        other = GetComponent<Collider>();
    }

    void OnTriggerStay(Collider other)
    {
        rb.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            OnTriggerStay(other);
        }
    }
}