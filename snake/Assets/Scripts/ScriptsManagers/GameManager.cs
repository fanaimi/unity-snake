using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SNAKE
{
    /// <summary>
    /// Snake directions
    /// </summary>
    enum Direction { Up, Down, Left, Right };

    /// <summary>
    /// @gObj === GameManager ===
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // singleton
        private static GameManager s_instance;
        public static GameManager Instance { get { return s_instance; } }

        /// <summary>
        /// Global utilities
        /// </summary>
        public bool _gameOver = false;
        public bool _gameStarted = false;

        private void Awake()
        {
            // singleton
            if (s_instance != null && s_instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                s_instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gameStarted = true;
            }
        }
    }


} // SNAKE namespace
