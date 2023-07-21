using System;
using UnityEngine;

public class NpcBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _readyIcon;
    [SerializeField] private Animator _animator;

    public static event Action OnGetLogs;

    private bool readyToTakeLogs = false;

    private void Awake()
    {
        LogCounter.CollectedEnoughLogs += MakeNpsReadyToTakeLogs;
    }

    private void OnDisable()
    {
        LogCounter.CollectedEnoughLogs -= MakeNpsReadyToTakeLogs;
    }

    private void MakeNpsReadyToTakeLogs()
    {
        _readyIcon.SetActive(true);
        readyToTakeLogs = true;
        Debug.Log("ewfd");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (readyToTakeLogs)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetBool("GetLogs", true);
                OnGetLogs?.Invoke();
            }
        }
    }
}
