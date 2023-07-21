using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TreeAndLogCreator : MonoBehaviour
{
    [SerializeField] private float _secondsToCreateTreeAfterDestroy;
    [SerializeField] private GameObject _firstTreePrefab;
    [SerializeField] private GameObject _secondTreePrefab;
    [SerializeField] private GameObject _thirdTreePrefab;
    [SerializeField] private GameObject _logPrefab;

    private void Awake()
    {
        Tree.DestroyTree += CreateNewTree;
    }

    private void OnDestroy()
    {
        Tree.DestroyTree -= CreateNewTree;
    }

    private void CreateNewTree(Vector3 position)
    {
        CreateLog(position);
        StartCoroutine(WaitPauseToCreatNewTree(position));
    }

    private void CreateLog(Vector3 position)
    {
        position.y += 1.5f;
        GameObject log = Instantiate(_logPrefab, position, Quaternion.identity);
        log.transform.Rotate(Vector3.right, -90);
    }

    private IEnumerator WaitPauseToCreatNewTree(Vector3 posotion)
    {
        yield return new WaitForSeconds(_secondsToCreateTreeAfterDestroy);
        GameObject newTree = Instantiate(DesignateCreatingTree(),posotion,Quaternion.identity);
    }

    private GameObject DesignateCreatingTree()
    {
        int number = Random.Range(1, 4);
        GameObject newTree;
        if (number == 1)
        {
            newTree = _firstTreePrefab;
        }
        else if(number == 2)
        {
            newTree =_secondTreePrefab;
        }
        else
        {
            newTree = _thirdTreePrefab;
        }
        return newTree;
    }
}
