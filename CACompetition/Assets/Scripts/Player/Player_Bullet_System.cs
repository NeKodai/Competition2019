using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Playerの弾丸発射方式を管理します
*/
public class Player_Bullet_System : MonoBehaviour
{
    private GameObject Bullet;
    private int shot_wait = 0;
    private int wait_count = 0;
    [SerializeField] private GameObject Player;
    public void Change_Wait(int wait)
    {
        shot_wait = wait;
    }
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
                GameObject new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y, 0), Quaternion.identity);
                new_obj.GetComponent<Player_Bullet_Status>().Init(x_vector * speed, y_vector * speed, 0, damage, range, penetrate);
            }
            wait_count = 0;
        }

    }
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