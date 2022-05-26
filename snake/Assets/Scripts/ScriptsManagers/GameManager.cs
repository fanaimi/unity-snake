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
        // ==== singleton
        private static GameManager s_instance;
        public static GameManager Instance { get { return s_instance; } }

        // ==== Global utilities
        public bool _gameOver = false;
        public bool _isPlaying = false;
        public int _noOfLives = 5;

        // ==== refs
        [SerializeField] private SnakeCollisions m_snakeCollisions;

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
            AudioManager.Instance.Play("waitinBgm");
            SetUpNewGame();
            m_snakeCollisions.OnLoseLifeEvent += LoseLife;
            m_snakeCollisions.OnDeathEvent += GameOver;
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
            if (_isPlaying) return;

            AudioManager.Instance.Stop("waitinBgm");
            AudioManager.Instance.Play("bgm");
            _isPlaying = true;
            Time.timeScale = 1;         
        }

        public void GameOver()
        {

            AudioManager.Instance.Play("death");
            AudioManager.Instance.Stop("bgm");
            AudioManager.Instance.Play("waitinBgm");
            _isPlaying = false;
            _gameOver = true;
            Time.timeScale = 0;
            // will update UI... display game Over panel
        }

        private void OnDisable()
        {
            // unsubscribing
            m_snakeCollisions.OnLoseLifeEvent -= LoseLife;
            m_snakeCollisions.OnDeathEvent -= GameOver;
        }

        private void LoseLife()
        {
            AudioManager.Instance.Play("slam");
            _noOfLives -= 1;
            GameManager.Instance.Pause();
        }
    }


} // SNAKE namespace
