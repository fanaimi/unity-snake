using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNAKE;

/// <summary>
/// @gObj   SnakeHead
/// @desc   this controls snake head collision with snake body, walls food etc
/// </summary>
public class SnakeCollisions : MonoBehaviour
{
    [SerializeField] private SnakeController m_snake;
    [SerializeField] private int m_growthRate = 0;


    public event Action OnLoseLifeEvent;
    public event Action OnDeathEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadly") || other.CompareTag("Player") )
        {
            // play sound
            if (GameManager.Instance._noOfLives > 0)
            {
                m_snake.SetUpSnake();

                if (OnLoseLifeEvent != null) OnLoseLifeEvent();     
            }
            else 
            {
                if (OnDeathEvent != null) OnDeathEvent();
                                
            }
        }

        if (other.CompareTag("Edible")) 
        {
            // print("adding");
            Destroy(other.gameObject);
            SpawnManager.Instance.AddItem();
            m_growthRate += 1;
            // play sound

            AudioManager.Instance.Play("bite");
            m_snake.IncreaseLenght(m_growthRate);

        }

    }

}