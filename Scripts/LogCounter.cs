using System;
using UnityEngine;
using UnityEngine.UI;

public class LogCounter : MonoBehaviour
{
    [SerializeField] private Image[] _logImages;
    [SerializeField] private Color _takenLogImage;
    [SerializeField] private int _numberOfLogsToCompleteLevel;

    public static event Action CollectedEnoughLogs;

    private int logsCollected=0;

    private void Awake()
    {
        Log.OnLogTaken += CollectLog;
    }

    private void OnDisable()
    {
        Log.OnLogTaken -= CollectLog;
    }

    private void CollectLog()
    {
        logsCollected++;
        MakeLogImageOpaque();
        if(logsCollected == _numberOfLogsToCompleteLevel)
        {
            CollectedEnoughLogs?.Invoke();
        }
    }

    private void MakeLogImageOpaque()
    {
        if (logsCollected <= _logImages.Length)
        {
            _logImages[logsCollected - 1].color = _takenLogImage;
        }
    }
}
