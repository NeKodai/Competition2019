using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Playerの入力を取るクラス
*/
public class Player_Move : MonoBehaviour
{
    private float speed;
    private int b_number = 1;
    private GameObject Player;


    public float Speed
    {
        set { speed = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        var Status = this.gameObject.GetComponent<Player_Status>();
        Player = Status.Player_Get;
        speed = Status.Speed;
        Player.GetComponent<Player_Bullet_System>().Shot_Wait = 5;
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
            Player.GetComponent<Player_Bullet_System>().N_Way_Homing(0.3f, 5f, b_number, 40, 2, 0.05f);
            //Player.GetComponent<Player_Bullet_System>().N_Way(0.3f, 5f, b_number, 10);
            Player.GetComponent<Player_Status>().Speed = 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Player.GetComponent<Player_Status>().Speed = 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            b_number += 1;
            if (b_number == 10) b_number = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Key_Input();
    }
}