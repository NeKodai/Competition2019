using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class E_Bullet_System : MonoBehaviour
{
    private GameObject E_Bullet;
    private GameObject E_Bullet_Homing;
    [SerializeField] private GameObject Player;
    void Start()
    {
        E_Bullet = (GameObject)Resources.Load("E_Bullet");
        E_Bullet_Homing = (GameObject)Resources.Load("E_Bullet_Homing");

    }

    public void Straight(float x_vector, float y_vecotr, GameObject Enemy)
    {

        GameObject new_obj = Instantiate(E_Bullet, new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, 0.0f), Quaternion.identity);
        new_obj.GetComponent<E_Bullet_Move>().Init(x_vector, y_vecotr, 5f);

    }
    /*円を描くように弾幕を張ります*/
    public IEnumerator Circle_01(float speed, int degree, GameObject Enemy)
    {
        for (int i = UnityEngine.Random.Range(-3, 3); i < 360; i += degree)
        {

            GameObject new_obj = Instantiate(E_Bullet, new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, 0.0f), Quaternion.identity);
            float vector_x = (float)(Math.Cos(2 * Math.PI * i / 360)) * speed;
            float vector_y = (float)(Math.Sin(2 * Math.PI * i / 360)) * speed;

            new_obj.GetComponent<E_Bullet_Move>().Init(vector_x, vector_y, 5f);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    public void Homing_01(float speed, GameObject Enemy)
    {
        double rad = Math.Atan2(Enemy.transform.position.y - Player.transform.position.y, Enemy.transform.position.x - Player.transform.position.x);
        Debug.Log(rad);
        GameObject new_obj = Instantiate(E_Bullet, new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, 0.0f), Quaternion.identity);
        float vector_x = (float)(Math.Cos(rad)) * speed;
        float vector_y = (float)(Math.Sin(rad)) * speed;
        new_obj.GetComponent<E_Bullet_Move>().Init(vector_x, vector_y, 5f);
    }

    public void Homing_02(float speed, float change_degree, GameObject Enemy)
    {
        GameObject new_obj = Instantiate(E_Bullet_Homing, new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, 0.0f), Quaternion.identity);
        new_obj.GetComponent<E_Bullet_Move_Homing>().Init(Player, speed, 5, 0, change_degree);
    }

}
