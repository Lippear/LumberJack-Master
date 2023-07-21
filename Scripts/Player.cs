using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Color _letToChopcolor;
    [SerializeField] private Color _banToChopColor;
    [SerializeField] private Button _axeButton;
    [SerializeField] private float _secondsToCutTree;

    private void Awake()
    {
        Tree.OnCharacterCanChop += LetToCutTree;
        _axeButton.onClick.AddListener(CutTheTree);
        NpcBuilder.OnGetLogs += StopCharacter;
    }

    private void OnDestroy()
    {
        Tree.OnCharacterCanChop -= LetToCutTree;
        NpcBuilder.OnGetLogs -= StopCharacter;
    }

    private readonly float m_interpolation = 10;
    private float m_currentV;
    private float m_currentH;
    private Vector3 m_currentDirection;
    private GameObject targetTree;
    private bool letToCut;
    private bool letToMove=true;

    private void FixedUpdate()
    {
        if (letToMove)
        {
            float v = _joystick.Vertical;
            float h = _joystick.Horizontal;
            Transform camera = Camera.main.transform;
            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);
            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;
            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;
            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * _moveSpeed * Time.deltaTime;

                _animator.SetFloat("Speed", direction.magnitude);
            }
        }
    }

    private void StopCharacter()
    {
        letToMove = false;
        _animator.SetFloat("Speed", 0);
    }

    private void LetToCutTree(bool letToCut,GameObject tree)
    {
        if (letToCut)
        {
            targetTree= tree;
            this.letToCut= true;
            _axeButton.image.color = _letToChopcolor;
        }
        else
        {
            this.letToCut= false;
            _axeButton.image.color= _banToChopColor;
        }
    }

    private void CutTheTree()
    {
        if (letToMove)
        {
            if (letToCut)
            {
                _animator.SetBool("Chop", true);
                transform.LookAt(targetTree.transform);
                letToCut = false;
                _axeButton.image.color = _banToChopColor;
                letToMove = false;
                StartCoroutine(CutingTree());
            }
        }
    }



    private IEnumerator CutingTree()
    {
        yield return new WaitForSeconds(_secondsToCutTree);
        Tree tree = targetTree.GetComponent<Tree>();
        tree.CutTheTree();
        letToMove = true;
        _animator.SetBool("Chop", false);
    }
}
