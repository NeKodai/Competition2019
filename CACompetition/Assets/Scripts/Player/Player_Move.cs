using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Playerの入力を取るクラスです
*/
public class Player_Move : MonoBehaviour
{
    private float speed;

    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
    }
    void Key_Input()
    {
        if (Input.GetKey(KeyCode.W) && Player.transform.position.y < 4.5)
        {
            Player.transform.Translate(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.A) && Player.transform.position.x > -8.5)
        {
            Player.transform.Translate(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && Player.transform.position.y > -4.5)
        {
            Player.transform.Translate(0, -speed, 0);
        }
        if (Input.GetKey(KeyCode.D) && Player.transform.position.x < 8.5)
        {
            Player.transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Player.GetComponent<Player_Bullet_System>().N_Way(0.5f, 5f, 5, 20);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Key_Input();
    }
}