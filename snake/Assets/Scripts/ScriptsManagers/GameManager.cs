using System;
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
        public bool _isPlaying = false;
        public int _noOfLives = 5;

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
            SetUpNewGame();   
        }

        private void SetUpNewGame()
        {
            SpawnManager.Instance.ClearAllItems();
        }



        // Update is called once per frame
        void Update()
        {
            DetectStartGame();
            DetectPause();

        }

        private void DetectPause()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pause();
            }
        }

        public void Pause()
        {
            _isPlaying = !_isPlaying;
            Time.timeScale =  0;
        }

        private void DetectStartGame()
        {
            if (!Input.anyKeyDown) return;
            
            _isPlaying = true;
            Time.timeScale = 1;         
        }

        public void GameOver()
        {
            _isPlaying = false;
            _gameOver = true;
            Time.timeScale = 0;
            // will update UI...
        }

    }


} // SNAKE namespace
