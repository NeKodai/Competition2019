using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spiral : MonoBehaviour
{
    GameObject Bullet; //弾丸のオブジェクト
    float damage = 0; //ダメージ
    float shot_interval = 0; //発射間隔
    float spiral_speed = 0; //螺旋を描く速度
    float change_degree = 0; //発射角度の増加量
    float speed = 0; //弾丸の速度
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
    public void Init(float i_shot_interval, float i_change_degree, float i_spiral_speed, float i_speed = 0.05f, float i_damage = 10)
    {
        shot_interval = i_shot_interval;
        change_degree = i_change_degree;
        spiral_speed = i_spiral_speed;
        damage = i_damage;
        speed = i_speed;

    }

    //一定間隔で螺旋状に発射する
    IEnumerator Shot()
    {
        float x_vector = 0; //弾丸のxベクトル
        float y_vector = 0; //弾丸のyベクトル
        while (true)
        {
            float degree = 0;
            while (degree < 360)
            {
                double rad = Math.PI * (degree / 180.0);
                x_vector = (float)Math.Cos(rad) * speed;
                y_vector = (float)Math.Sin(rad) * speed;
                GameObject new_bullet = Instantiate(Bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                new_bullet.GetComponent<Enemy_Bullet_Status>().Init(x_vector, y_vector, damage);
                Transform B_Image = new_bullet.transform.GetChild(0);
                B_Image.rotation = Quaternion.Euler(0, 0, degree);
                degree += change_degree;
                yield return new WaitForSeconds(spiral_speed);
            }
            yield return new WaitForSeconds(shot_interval);
        }
    }

    void Start()
    {
        Bullet = (GameObject)Resources.Load("Bullet_2");
        StartCoroutine(Shot());
    }
}
