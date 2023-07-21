using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Log : MonoBehaviour
{
    [SerializeField] private float upwardForce;
    [SerializeField] private float rightwardForce;

    public static event Action OnLogTaken;

    private void Awake()
    {
        ApplyInitialForces();
    }

    private void ApplyInitialForces()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        rb.AddForce(Vector3.right * rightwardForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnLogTaken?.Invoke();
            Destroy(gameObject);
        }
    }
}
