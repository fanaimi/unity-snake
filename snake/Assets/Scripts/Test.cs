using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity =
             // m_snakeHead.forward * m_snakeSpeed * Time.fixedDeltaTime;
             new Vector3(1,0,0) * 500 * Time.fixedDeltaTime;
    }
}
