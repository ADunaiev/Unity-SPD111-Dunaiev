using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D rigidBody; // Посилання на інший компонент того ж ГО, що й скріпт

    void Start()
    {
        Debug.Log("BirdScript Start");
        // пошук компоненту та одержання посилання на нього
        rigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector2(0, 500));
        }
    }
}
