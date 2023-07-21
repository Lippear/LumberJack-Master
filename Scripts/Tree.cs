using System.Collections;
using UnityEngine;
using System;

public class Tree : MonoBehaviour
{
    private Animator animator;
    public static event Action<bool,GameObject> OnCharacterCanChop;
    public static event Action<Vector3> DestroyTree;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCharacterCanChop?.Invoke(true,gameObject);
            Debug.Log("11");
            animator.SetBool("IsCharacterNearTree", true);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCharacterCanChop?.Invoke(false,gameObject);
            Debug.Log("22");
            animator.SetBool("IsCharacterNearTree", false);
        }
    }

    public void CutTheTree()
    {
        DestroyTree?.Invoke(gameObject.transform.position);
        Destroy(gameObject);
    }
}
