using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
プレイヤーに向かって直線的に弾丸を発射します
敵に当たってもダメージ発生
*/

public class Snipe : MonoBehaviour
{

    GameObject Player; //プレイヤーのオブジェクト
    GameObject Bullet; //弾丸のオブジェクト
    float damage = 10; //ダメージ
    float speed = 0; //弾速
    float shot_interval = 0; //発射間隔(秒)
    //衝突したら
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Status>().HP -= damage;
            Destroy(this.gameObject);
        }
    }

    //初期設定
    public void Init(float i_shot_interval, float i_speed = 0.05f, float i_damage = 10)
    {
        shot_interval = i_shot_interval;
        damage = i_damage;
        speed = i_speed;
    }

    //一定間隔で発射する
    IEnumerator Shot()
    {
        float x_vector = 0; //弾丸のxベクトル
        float y_vector = 0; //弾丸のyベクトル
        while (true)
        {
            float distance_x = Player.transform.position.x - this.gameObject.transform.position.x;
            float distance_y = Player.transform.position.y - this.gameObject.transform.position.y;
            double rad = Math.Atan2(distance_y, distance_x);
            float degree = (float)(180 * rad / Math.PI);
            rad = Math.PI * (degree / 180.0);
            x_vector = (float)Math.Cos(rad) * speed;
            y_vector = (float)Math.Sin(rad) * speed;
            GameObject new_bullet = Instantiate(Bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            new_bullet.GetComponent<Enemy_Bullet_Status>().Init(x_vector, y_vector, damage);
            Transform B_Image = new_bullet.transform.GetChild(0);
            B_Image.rotation = Quaternion.Euler(0, 0, degree);
            yield return new WaitForSeconds(shot_interval);
        }
    }
    void Start()
    {
        Player = GameObject.Find("Player");
        Bullet = (GameObject)Resources.Load("Bullet_2");
        StartCoroutine(Shot());
    }
}
