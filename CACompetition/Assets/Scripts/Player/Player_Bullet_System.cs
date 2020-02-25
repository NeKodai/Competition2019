using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Playerの弾丸発射方式を管理するクラス
*/
public class Player_Bullet_System : MonoBehaviour
{
    private GameObject Bullet;
    private int shot_wait = 0;
    private int wait_count = 0;
    private int temp = 0;
    [SerializeField] private GameObject Player = null;
    [SerializeField] private GameObject Enemy_Field = null;
    public void Change_Wait(int wait)
    {
        shot_wait = wait;
    }
    //通常弾
    public void N_Way(float speed, float damage, int n, float degree, int range = -1, int penetrate = 0)
    {
        if (wait_count >= shot_wait)
        {
            double rad = degree * (Math.PI / 180);
            double half_degree = degree * (n - 1) / 2;
            double half_rad = half_degree * (Math.PI / 180);
            for (int i = 0; i < n; i++)
            {
                float x_vector = (float)Math.Cos(-(half_rad) + (rad * i));
                float y_vector = (float)Math.Sin(-(half_rad) + (rad * i));
                GameObject new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x + 0.6f, Player.transform.position.y, 0), Quaternion.identity);
                new_obj.GetComponent<Player_Bullet_Status>().Init(x_vector * speed, y_vector * speed, 0, damage, range, penetrate);
            }
            wait_count = 0;
        }

    }

    //ここから誘導弾
    private IEnumerator Search_Target(GameObject Bullet, int delay, float homing_delay, float speed)
    {
        GameObject Target = null;
        float min_distance = 99999999;
        int wait_count = 0;

        foreach (Transform Child_Transform in Enemy_Field.transform)
        {
            float distance_x = Child_Transform.position.x - Player.transform.position.x;
            if (distance_x < 0)
            {
                distance_x *= -1;
            }
            float distance_y = Child_Transform.position.y - Player.transform.position.y;
            if (distance_y < 0)
            {
                distance_y *= -1;
            }
            float distance = (distance_x * distance_x) + (distance_y * distance_y);
            if (min_distance > distance)
            {
                min_distance = distance;
                Target = Child_Transform.gameObject;
            }
        }

        while (true)
        {
            wait_count += 1;
            if (wait_count >= delay)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (Bullet && Target)
        {
            Bullet.GetComponent<Player_Bullet_Status>().Start_Homing(Target, homing_delay, speed);
        }
        yield break;
    }


    public void N_Way_Homing(float speed, float damage, int n, float degree, int delay = 0, float homing_delay = 0, int range = -1, int penetrate = 0)
    {
        if (wait_count >= shot_wait)
        {
            double rad = degree * (Math.PI / 180);
            double half_degree = degree * (n - 1) / 2;
            double half_rad = half_degree * (Math.PI / 180);
            for (int i = 0; i < n; i++)
            {
                float x_vector = (float)Math.Cos(-(half_rad) + (rad * i));
                float y_vector = (float)Math.Sin(-(half_rad) + (rad * i));
                GameObject new_bullet = Instantiate(Bullet, new Vector3(Player.transform.position.x + 0.6f, Player.transform.position.y, 0), Quaternion.identity);
                new_bullet.GetComponent<Player_Bullet_Status>().Init(x_vector * speed, y_vector * speed, 0, damage, range, penetrate);
                StartCoroutine(Search_Target(new_bullet, delay, homing_delay, speed));

            }
            wait_count = 0;
        }

    }
    //誘導弾ここまで
    
    // Start is called before the first frame update
    void Start()
    {
        Bullet = (GameObject)Resources.Load("Player_Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (wait_count < shot_wait)
        {
            wait_count += 1;
        }
    }
}