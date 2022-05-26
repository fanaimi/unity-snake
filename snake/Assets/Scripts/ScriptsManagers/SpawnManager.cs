using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNAKE;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_items = new List<GameObject>();
    [SerializeField] private List<GameObject> m_addedItems = new List<GameObject>();
    [SerializeField] private float m_interval;
    [SerializeField] private float m_minX = -24;
    [SerializeField] private float m_maxX = 24;
    [SerializeField] private float m_minZ = -24;
    [SerializeField] private float m_maxZ = 24;
    [SerializeField] private int index;
    [SerializeField] private Transform m_environment;

    // singleton
    private static SpawnManager s_instance;
    public static SpawnManager Instance { get { return s_instance; } }


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
        SetUpToAdd();
     }

    private void SetUpToAdd()
    {
        m_interval = Random.Range(1, 5);
        Invoke("AddItem", m_interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem()
    {
        index = Random.Range(0, m_items.Count - 1);
        Vector3 pos = new Vector3(
            Random.Range(m_minX, m_maxX), 
            0,
            Random.Range(m_minZ, m_maxZ)           
        );

        var newItem = Instantiate(m_items[index], pos, Quaternion.identity, m_environment);
        m_addedItems.Add(newItem);

        if (index == 2) SetUpToAdd(); // add another item if last added was a wallCube
    }


    public void ClearAllItems()
    {
        m_addedItems.Clear();
    }


}
