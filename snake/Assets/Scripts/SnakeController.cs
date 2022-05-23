using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNAKE;
using System;

/// <summary>
/// @gObj   Snake
/// @desc   controller for snake 
/// </summary>
public class SnakeController : MonoBehaviour
{
    // ====== vars 
    [SerializeField] private float m_snakeSpeed = 5f;
    private Direction m_snakeDir = Direction.Up;
    private Vector3 m_snakeRot;
    [SerializeField] private int m_gap = 220;
    [SerializeField] private int m_startSize = 5;

    private List<GameObject> m_snakeFragments = new List<GameObject>();
    private List<Vector3> m_allPositions = new List<Vector3>();

    // ====== refs
    [SerializeField] private Transform m_UP;
    [SerializeField] private Transform m_DOWN;
    [SerializeField] private Transform m_LEFT;
    [SerializeField] private Transform m_RIGHT;

    [SerializeField] private Transform m_snakeHead;
    [SerializeField] private GameObject m_snakeBodyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        IncreaseLenght(m_startSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance._gameOver && GameManager.Instance._gameStarted)
        {
            MoveSnake();
        }
    }

    private void MoveSnake()
    {
        // moving forward
        m_snakeHead.position += m_snakeHead.forward * m_snakeSpeed * Time.deltaTime;

        // storing position
        m_allPositions.Insert(0, m_snakeHead.position);

        // controlling body fragments
        int index = 0;
        foreach (var fragment in m_snakeFragments)
        {
            Vector3 point = m_allPositions[Mathf.Min(index * m_gap, m_allPositions.Count -1)];
            fragment.transform.position = point;
            index++;
        }


        // == KEYBOARD CONTROL
#if UNITY_STANDALONE_WIN || UNITY_EDITOR


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // print("UP");
            if (m_snakeDir == Direction.Up || m_snakeDir == Direction.Down) return;
            m_snakeDir = Direction.Up;
            m_snakeRot = m_UP.eulerAngles;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            // print("DOWN");
            if (m_snakeDir == Direction.Down || m_snakeDir == Direction.Up) return;
            m_snakeDir = Direction.Down;
            m_snakeRot = m_DOWN.eulerAngles;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // print("LEFT");
            if (m_snakeDir == Direction.Left || m_snakeDir == Direction.Right) return;
            m_snakeDir = Direction.Left;
            m_snakeRot = m_LEFT.eulerAngles;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // print("RIGHT");
            if (m_snakeDir == Direction.Right || m_snakeDir == Direction.Left) return;
            m_snakeDir = Direction.Right;
            m_snakeRot = m_RIGHT.eulerAngles;
        }


        m_snakeHead.eulerAngles = m_snakeRot;
        #endif
    }


    private void IncreaseLenght(int newPartsNo)
    {
        for (int i = 0; i < newPartsNo; i++)
        {
            GameObject bodyFragment = Instantiate(m_snakeBodyPrefab);
            bodyFragment.transform.SetParent(transform);
            m_snakeFragments.Add(bodyFragment);
        }
    }


}
