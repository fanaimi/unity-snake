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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadly") || other.CompareTag("Player") )
        {
            // play sound
            m_snake.SetUpSnake();
            GameManager.Instance.Pause();
        }

        if (other.CompareTag("Edible")) 
        {
            print("adding");
            Destroy(other.gameObject);
            m_growthRate += 1;
            // play sound
            m_snake.IncreaseLenght(m_growthRate);
        }

    }

}