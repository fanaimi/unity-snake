using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNAKE;
using System;

/// <summary>
/// class to store details of each snake fragment
/// </summary>
public class Fragment
{
    public Vector3 m_position;
    public Vector3 m_rotation;

    /// <summary>
    /// constructor to store given position and rotation
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public Fragment(Vector3 pos, Vector3 rot)
    {
        m_position = pos;
        m_rotation = rot;
    }
}





/// <summary>
/// @gObj   Snake
/// @desc   controller for snake 
/// </summary>
[RequireComponent(typeof(BoxCollider))] 
public class SnakeController : MonoBehaviour
{
    // ====== vars 
    private Direction m_snakeDir = Direction.Up;
    // [SerializeField] 
    private int m_startSize = 25;
    // [SerializeField] 
    private float  m_snakeSpeed = 15f;

    public Vector3 m_direction = Vector3.forward;
    private Vector3 m_input;
    private Vector3 m_snakeRot;

    private List<Transform> m_snakeFragments = new List<Transform>();

    // ====== refs
    [SerializeField] private Transform m_UP;
    [SerializeField] private Transform m_DOWN;
    [SerializeField] private Transform m_LEFT;
    [SerializeField] private Transform m_RIGHT;

    [SerializeField] private Transform m_snakeHead;
    [SerializeField] private Rigidbody m_snakeHeadRb;
    [SerializeField] private Transform m_snakeBodyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        SetUpSnake();
    }


    public void SetUpSnake()
    {
        m_snakeDir = Direction.Up;
        m_snakeHead.position = new Vector3(0, .5f,0);

        // detroy all fragments but head
        for (int i = 1; i < m_snakeFragments.Count; i++)
        {
            Destroy(m_snakeFragments[i].gameObject);
        }

        // keep only head in list of framgents
        m_snakeFragments.Clear();
        m_snakeFragments.Add(m_snakeHead);
        IncreaseLenght(m_startSize);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance._gameOver && GameManager.Instance._isPlaying)
        {
            RegisterInput();
        }
    }

    private void RegisterInput()
    {

        // == KEYBOARD CONTROL
#if UNITY_STANDALONE_WIN || UNITY_EDITOR


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // print("UP");
            if (m_snakeDir == Direction.Up || m_snakeDir == Direction.Down) return;
            m_snakeDir = Direction.Up;
            m_input = Vector3.forward;
            m_snakeRot = m_UP.eulerAngles;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            // print("DOWN");
            if (m_snakeDir == Direction.Down || m_snakeDir == Direction.Up) return;
            m_snakeDir = Direction.Down;
            m_input = Vector3.back;
            m_snakeRot = m_DOWN.eulerAngles;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // print("LEFT");
            if (m_snakeDir == Direction.Left || m_snakeDir == Direction.Right) return;
            m_snakeDir = Direction.Left;
            m_input = Vector3.left;
            m_snakeRot = m_LEFT.eulerAngles;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // print("RIGHT");
            if (m_snakeDir == Direction.Right || m_snakeDir == Direction.Left) return;
            m_snakeDir = Direction.Right;
            m_input = Vector3.right;
            m_snakeRot = m_RIGHT.eulerAngles;
        }


         m_snakeHead.eulerAngles = m_snakeRot;
#endif


        

    }

    private void FixedUpdate()
    {

        if (!GameManager.Instance._gameOver && GameManager.Instance._isPlaying)
        {
            // Set the new direction based on the input
            if (m_input != Vector3.zero)
            {
                m_direction = m_input;
            }

            // controlling body fragments
            // Set each segment's position to be the same as the one it follows. We
            // must do this in reverse order so the position is set to the previous
            // position, otherwise they will all be stacked on top of each other.
            for (int i = m_snakeFragments.Count - 1; i > 0; i--)
            {
                m_snakeFragments[i].position = m_snakeFragments[i - 1].position;
            }


            m_snakeHead.position += m_direction * m_snakeSpeed * Time.fixedDeltaTime;

        }
    }


    public void IncreaseLenght(int newPartsNo)
    {
        for (int i = 0; i < newPartsNo; i++)
        {
            Transform newFragment = Instantiate(m_snakeBodyPrefab);
            newFragment.SetParent(transform);
            newFragment.position = m_snakeFragments[m_snakeFragments.Count - 1].position; 
            m_snakeFragments.Add(newFragment);
        }
    }


}
