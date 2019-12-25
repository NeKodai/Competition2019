using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
弾丸のステータス
*/
public class Player_Bullet_Status : MonoBehaviour
{
    private float x_vector; //xベクトル
    private float y_vecotr; //yベクトル
    private float damage = 0; //ダメージ
    private int range = 0; //射程
    private int pattarn = 0; //発射パターン
    private int penetrate = 0; //貫通するか 0 or 1

    private int range_count = 0; //射程のカウント

    public void Init(float input_x_vector, float input_y_vector, int input_pattarn, float input_damage, int input_range = -1, int input_penetrate = 0)
    {
        x_vector = input_x_vector;
        y_vecotr = input_y_vector;
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
    public void Change_vector(float input_x_vector, float input_y_vector)
    {
        x_vector = input_x_vector;
        y_vecotr = input_y_vector;
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
        this.gameObject.transform.Translate(x_vector, y_vecotr, 0);

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