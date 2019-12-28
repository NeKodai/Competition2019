using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
弾丸のステータスを管理するクラス
*/
public class Player_Bullet_Status : MonoBehaviour
{
    private float x_vector; //xベクトル
    private float y_vector; //yベクトル
    private float damage = 0; //ダメージ
    private int range = 0; //射程
    private int pattarn = 0; //発射パターン
    private int penetrate = 0; //貫通するか 0 or 1

    private int range_count = 0; //射程のカウント

    public void Init(float input_x_vector, float input_y_vector, int input_pattarn, float input_damage, int input_range = -1, int input_penetrate = 0)
    {
        x_vector = input_x_vector;
        y_vector = input_y_vector;
        damage = input_damage;
        pattarn = input_pattarn;
        range = input_range;
        penetrate = input_penetrate;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_Status>().Damage(damage);
            if (penetrate == 0)
            {

                Destroy(this.gameObject);
            }
        }
    }
    public void Start_Homing(GameObject Target, float homing_delay, float speed)
    {
        StartCoroutine(Homing(Target, homing_delay, speed));
    }
    private IEnumerator Homing(GameObject Target, float homing_delay, float speed)
    {
        while (true)
        {
            if (Target && this.gameObject)
            {
                float distance_x = Target.transform.position.x - this.gameObject.transform.position.x;
                float distance_y = Target.transform.position.y - this.gameObject.transform.position.y;
                double rad = Math.Atan2(distance_y, distance_x);
                x_vector = (float)Math.Cos(rad) * speed;
                y_vector = (float)Math.Sin(rad) * speed;
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(homing_delay);
        }
    }
    public void Change_vector(float input_x_vector, float input_y_vector)
    {
        x_vector = input_x_vector;
        y_vector = input_y_vector;
    }
    void Start()
    {
        switch (pattarn)
        {
            case 0:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(x_vector, y_vector, 0);

        //画面外に出たら消滅
        if (this.gameObject.transform.position.x > 9.0f || this.gameObject.transform.position.x < -9.0f || this.gameObject.transform.position.y < -5.0f || this.gameObject.transform.position.y > 5.0f)
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