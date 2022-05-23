using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNAKE;

public class UiManager : MonoBehaviour
{
    // singleton
    private static UiManager s_instance;
    public static UiManager Instance { get { return s_instance; } }



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
        
    }
}
