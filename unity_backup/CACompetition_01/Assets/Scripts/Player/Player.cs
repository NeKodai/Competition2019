using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
CACちゃんの動作を管理しています
CACちゃんのオブジェクトにアタッチしてください
*/
public class Player : MonoBehaviour
{
    private int shot_wait_count = 0;
    private int shot_wait_time = 0;
    private int bullet_type = 0;    // 0　or 1
    private int[] bullets_list = new int[2];
    private float speed = 0;
    private float damagerate = 1f;
    private GameObject Bullet;
    private GameObject ScriptObj;
    private GameObject Canvas;
    private float hp = 100;


    /*CACちゃんが弾丸を発射します*/
    void Shoot()
    {
        switch (bullets_list[bullet_type])
        {
            case 0:
                ScriptObj.GetComponent<Bullet_System>().Straight(this.gameObject, damagerate);
                break;
            case 1:
                ScriptObj.GetComponent<Bullet_System>().Way_3(this.gameObject, damagerate);
                break;
        }
    }


    /*CACちゃんを動かします*/
    void Move(int direction)
    {
        float move_speed_x = 0;
        float move_speed_y = 0;
        switch (direction)
        {
            case 0:
                if (this.transform.position.y < 4.0f)
                {
                    move_speed_y = speed;
                }
                break;
            case 1:
                if (this.transform.position.x > -8.0f)
                {
                    move_speed_x = -speed;
                }
                break;
            case 2:
                if (this.transform.position.y > -4.0f)
                {
                    move_speed_y = -speed;
                }
                break;
            case 3:
                if (this.transform.position.x < 8.0f)
                {
                    move_speed_x = speed;
                }
                break;
        }
        this.transform.Translate(move_speed_x, move_speed_y, 0);
    }
    /*キー入力を管理します*/
    void Key_Input()
    {
        //射撃
        if (shot_wait_count >= shot_wait_time)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
                shot_wait_count = 0;
            }
        }
        else
        {
            shot_wait_count += 1;
        }

        //移動処理
        if (Input.GetKey(KeyCode.W))
        {
            Move(0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(3);
        }

        //バレット切替
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (bullet_type == 0)
            {
                bullet_type = 1;
                shot_wait_time = ScriptObj.GetComponent<Bullet_System>().Change_Wait_Time(bullets_list[1]);
            }
            else
            {
                bullet_type = 0;
                shot_wait_time = ScriptObj.GetComponent<Bullet_System>().Change_Wait_Time(bullets_list[0]);
            }
        }
    }

    /*ダメージ処理*/
    public void Damage(float input_damage)
    {
        hp -= input_damage;
        Canvas.GetComponent<UI_Change>().Change_HP(hp);
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Bullet = (GameObject)Resources.Load("Bullet");
        ScriptObj = GameObject.Find("ScriptObject");
        Canvas = GameObject.Find("Canvas");
        shot_wait_time = 5;
        speed = 0.1f;
        bullets_list[0] = 0;//仮
        bullets_list[1] = 1;//仮
    }

    void Update()
    {
        Key_Input();
    }
}


