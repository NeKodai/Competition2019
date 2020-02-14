using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Status : MonoBehaviour
{

    Camera Main_Camera; //メインカメラ

    float x_vector = 0; //弾丸のxベクトル

    float y_vector = 0; //弾丸のyベクトル

    float damage = 0; //弾丸のダメージ

    float max_width = 0; //画面のx最大
    float max_height = 0; //画面のy最大
    float start_wait = 0; //弾丸出現から移動するまでのウェイト

    int range = 0; //弾丸の射程
    int range_count = 0; //弾丸の射程のカウンタ変数

    bool penetrate = false; //弾丸が貫通するか


    //弾がオブジェクトに当たったら
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player_Status>().INVISIBLE == false)
            {
                other.gameObject.GetComponent<Player_Status>().HP -= damage;
                Destroy(this.gameObject);
            }
        }
    }

    //弾丸の初期設定(Attackスクリプトから呼び出し)
    public void Init(float i_x_vector, float i_y_vector, float i_damage, float i_start_wait = 0, int i_range = -1, bool i_penetrate = false)
    {
        x_vector = i_x_vector;
        y_vector = i_y_vector;
        damage = i_damage;
        start_wait = i_start_wait;
        range = i_range;
        penetrate = i_penetrate;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(start_wait);
        this.enabled = true;
    }


    void Start()
    {
        Main_Camera = Camera.main;
        max_height = Main_Camera.orthographicSize;
        max_width = max_height * (16.0f / 9.0f);
        if (start_wait != 0)
        {
            StartCoroutine(Wait());
            this.enabled = false;
        }

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
