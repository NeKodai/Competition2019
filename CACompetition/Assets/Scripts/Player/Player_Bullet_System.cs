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
    [SerializeField] private GameObject Player;
    public void N_Way(float speed, float damage, int n, float degree)
    {
        double rad = degree * (Math.PI / 180);
        double half_degree = degree * (n - 1) / 2;
        double half_rad = half_degree * (Math.PI / 180);
        for (int i = 0; i < n; i++)
        {
            float x_vector = (float)Math.Cos(-(half_rad) + (rad * i));
            float y_vector = (float)Math.Sin(-(half_rad) + (rad * i));
            GameObject new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y, 0), Quaternion.identity);
            new_obj.GetComponent<Player_Bullet_Status>().Init(x_vector * speed, y_vector * speed, 0, damage);
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

    }
}