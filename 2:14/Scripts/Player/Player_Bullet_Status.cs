using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Bullet_Status : MonoBehaviour
{
    Camera Main_Camera; //メインカメラ

    float x_vector = 0; //弾丸のxベクトル

    float y_vector = 0; //弾丸のyベクトル

    float damage = 0; //弾丸のダメージ

    float max_width = 0; //画面のx最大
    float max_height = 0; //画面のy最大

    int range = 0; //弾丸の射程
    int range_count = 0; //弾丸の射程のカウンタ変数

    bool penetrate = false; //弾丸が貫通するか


    //弾がオブジェクトに当たったら
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_Status>().HP -= damage;
            Destroy(this.gameObject);
        }
    }

    //弾丸の初期設定(Player_Bulletから呼び出し)
    public void Init(float i_x_vector, float i_y_vector, float i_damage, int i_range, bool i_penetrate)
    {
        x_vector = i_x_vector;
        y_vector = i_y_vector;
        damage = i_damage;
        range = i_range;
        penetrate = i_penetrate;
    }

    //ホーミング開始(Player_Bulletから呼び出し)
    public void Start_Homing(GameObject Target, float homing_degree, float speed)
    {
        float now_degree = 0; //現在の玉の角度

        //毎フレーム誘導
        IEnumerator Homing()
        {
            while (true)
            {
                if (Target && this.gameObject)
                {
                    if (Target.tag == "Enemy")
                    {
                        //敵との角度割り出し
                        float distance_x = Target.transform.position.x - this.gameObject.transform.position.x;
                        float distance_y = Target.transform.position.y - this.gameObject.transform.position.y;
                        double rad = Math.Atan2(distance_y, distance_x);
                        float degree = (float)(180 * rad / Math.PI);
                        //角度を0から360の範囲にする
                        if (degree >= 360)
                        {
                            degree = degree - 360;
                        }
                        else if (degree < 0)
                        {
                            degree = 360 + degree;
                        }

                        //時計回りの誘導にする
                        if ((360 + now_degree) - degree < 180)
                        {
                            now_degree += 360;
                        }

                        //誘導する
                        if (now_degree < degree - homing_degree)
                        {
                            now_degree += homing_degree;
                        }
                        else if (now_degree > degree + homing_degree)
                        {
                            now_degree -= homing_degree;
                        }
                        else
                        {
                            now_degree = degree;
                        }
                        rad = Math.PI * (now_degree / 180.0);
                        Transform B_Image = this.transform.GetChild(0);
                        B_Image.rotation = Quaternion.Euler(0, 0, now_degree);
                        x_vector = (float)Math.Cos(rad) * speed;
                        y_vector = (float)Math.Sin(rad) * speed;
                    }
                }
                else
                {
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
        StartCoroutine(Homing());
    }

    void Start()
    {
        Main_Camera = Camera.main;
        max_height = Main_Camera.orthographicSize;
        max_width = max_height * (16.0f / 9.0f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(x_vector, y_vector, 0);

        //画面外に出たら消滅
        if (this.gameObject.transform.position.x > max_width || this.gameObject.transform.position.x < -max_width || this.gameObject.transform.position.y < -max_height || this.gameObject.transform.position.y > max_height)
        {
            Destroy(this.gameObject);
        }
        if (range != -1)
        {
            range_count += 1;
            if (range_count >= range)
            {

                Destroy(this.gameObject);
            }
        }
    }
}
