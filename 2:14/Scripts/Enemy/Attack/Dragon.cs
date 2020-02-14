using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//ドラゴンの挙動
public class Dragon : MonoBehaviour
{
    Camera Main_Camera; //メインカメラ
    GameObject Bullet_fire; //炎の弾のゲームオブジェクト
    GameObject Bullet_Circle;//円の弾
    GameObject Bullet_Direction;//矢印みたいな弾丸
    GameObject Red_Box; //予測線
    GameObject Player;//プレイヤー
    Coroutine Fire_Coroutine; //火を吐く攻撃を入れるコルーチン
    Coroutine Circle_Coroutine; //円状の攻撃を入れるコルーチン
    float max_hp = 0;//最大体力
    float max_width = 0; //画面のx最大
    float max_height = 0; //画面のy最大
    int phase = 0; //行動切替の変数

    //衝突したら
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Status>().HP -= 10;
        }
    }

    //突進攻撃　引数は突進回数
    IEnumerator Strike(int strike_times)
    {
        float[] start_list = new float[strike_times];
        float[] degree_list = new float[strike_times];
        GameObject[] redline_list = new GameObject[strike_times];

        while (true)
        {
            this.GetComponent<Enemy_Status>().INVINCIBLE = true; //突進中は無敵にする
            //画面外まで移動させる
            while (this.transform.position.x <= 13)
            {
                this.transform.Translate(0.1f, 0.1f, 0);
                yield return new WaitForSeconds(0f);
            }
            this.transform.position = new Vector3(13, 0, 0);

            //突進するラインを決める
            for (int i = 0; i < strike_times; i++)
            {
                //高さを適当に決める
                float start_y = UnityEngine.Random.Range(-7.0f, 7.0f);
                start_list[i] = start_y;


                float distance_x = 0;//プレイヤーとの距離X
                float distance_y = 0;//プレイヤーとの距離Y

                //最初の突進は必ずプレイヤーを狙う
                if (i == 0)
                {
                    //プレイヤーが存在しているなら攻撃する
                    try
                    {
                        distance_x = (this.gameObject.transform.position.x) - Player.transform.position.x;
                        distance_y = (start_y) - Player.transform.position.y;
                    }
                    catch
                    {
                        yield break;
                    }
                }
                else
                {
                    //プレイヤーが存在しているなら攻撃する
                    try
                    {
                        distance_x = (this.gameObject.transform.position.x) - UnityEngine.Random.Range(-max_width + 2, max_width - 2);
                        distance_y = (start_y) - UnityEngine.Random.Range(-max_height + 1, max_height - 1);
                    }
                    catch
                    {
                        yield break;
                    }
                }

                double rad = Math.Atan2(distance_y, distance_x); //プレイヤーとの角度を割り出し
                float degree = (float)(180 * (rad / Math.PI));
                degree_list[i] = degree;
                redline_list[i] = Instantiate(Red_Box, new Vector3(this.transform.position.x - (distance_x / 2), start_y - (distance_y / 2), 3), Quaternion.Euler(0, 0, degree));
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(1f);



            //突進処理
            for (int i = 0; i < strike_times; i++)
            {
                this.transform.position = new Vector3(13, start_list[i], 0);
                this.transform.rotation = Quaternion.Euler(0, 0, degree_list[i]);



                //ここが画面外に出たらループ終了でないのは衝突間隔を一定にするため
                for (int j = 0; j < 30; j++)
                {
                    this.transform.Translate(-1f, 0, 0);
                    if (j == 10)
                    {
                        StartCoroutine(Main_Camera.GetComponent<Camera_Move>().Camera_Shake(10));//カメラを揺らす
                    }
                    //突進したところに弾丸を残す処理
                    GameObject new_bullet = Instantiate(Bullet_Direction, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                    float bullet_rad = UnityEngine.Random.Range(0f, 2 * (float)Math.PI);
                    new_bullet.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, (float)(180 * (bullet_rad / Math.PI)));
                    float x_vector = (float)Math.Cos(bullet_rad);
                    float y_vector = (float)Math.Sin(bullet_rad);
                    float speed = 0.02f;
                    new_bullet.GetComponent<Enemy_Bullet_Status>().Init(x_vector * speed, y_vector * speed, 10, 1);
                    yield return new WaitForSeconds(0f);
                }
                Destroy(redline_list[i]);
                yield return new WaitForSeconds(0.05f);
            }
            //後ろに戻す処理
            this.transform.position = new Vector3(13, 0f, 0);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);


            //元の位置に戻る処理
            while (this.transform.position.x > 5)
            {
                this.transform.Translate(-0.2f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
            this.GetComponent<Enemy_Status>().INVINCIBLE = false;
            yield return new WaitForSeconds(2);
        }
    }

    //火を吐く攻撃
    IEnumerator Fire()
    {
        while (true)
        {
            float distance_x = 0;
            float distance_y = 0;
            float offset_x = -2.2f;
            float offset_y = -1.5f;
            //プレイヤーが存在しているなら攻撃する
            try
            {
                distance_x = Player.transform.position.x - (this.gameObject.transform.position.x + offset_x);
                distance_y = Player.transform.position.y - (this.gameObject.transform.position.y + offset_y);
            }
            catch
            {
                yield break;
            }
            float speed = 0.07f; //弾丸の速度

            double rad = Math.Atan2(distance_y, distance_x);//プレイヤーとの角度を割り出し
            float degree = (float)(180 * (rad / Math.PI));
            for (int i = 0; i < 8; i++) //炎の横の長さ
            {
                for (int j = -10; j <= 10; j += 5) //炎の縦の長さ
                {
                    rad = Math.PI * ((degree + j) / 180.0);
                    float x_vector = (float)Math.Cos(rad) * speed;
                    float y_vector = (float)Math.Sin(rad) * speed;
                    GameObject new_bullet = Instantiate(Bullet_fire, new Vector3(this.transform.position.x + offset_x, this.transform.position.y + offset_y, 0), Quaternion.identity);
                    new_bullet.GetComponent<Enemy_Bullet_Status>().Init(x_vector, y_vector, 10);
                    Transform B_Image = new_bullet.transform.GetChild(0);
                    B_Image.rotation = Quaternion.Euler(0, 0, degree);
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5f); //間隔
        }
    }

    //円状に弾幕を出す
    IEnumerator Circle()
    {


        float x_vector = 0; //弾丸のxベクトル
        float y_vector = 0; //弾丸のyベクトル
        float speed = 0.06f;
        while (true)
        {
            float distance_x = 0; //プレイヤーとの距離の差X
            float distance_y = 0;  //プレイヤーとの距離の差Y

            float offset_x = 0; //発生源のオフセット調整
            float offset_y = -1.5f;

            //プレイヤーが存在しているなら攻撃する
            try
            {
                distance_x = Player.transform.position.x - (this.gameObject.transform.position.x + offset_x);
                distance_y = Player.transform.position.y - (this.gameObject.transform.position.y + offset_y);
            }
            catch
            {
                yield break;
            }
            double rad = Math.Atan2(distance_y, distance_x);
            float degree = (float)(180 * (rad / Math.PI));

            for (int i = 0; i < 360; i += 30) //弾の発射数
            {
                rad = Math.PI * ((degree + i) / 180.0);
                x_vector = (float)Math.Cos(rad) * speed;
                y_vector = (float)Math.Sin(rad) * speed;
                GameObject new_bullet = Instantiate(Bullet_Circle, new Vector3(this.transform.position.x + offset_x, this.transform.position.y + offset_y, 0), Quaternion.identity);
                new_bullet.GetComponent<Enemy_Bullet_Status>().Init(x_vector, y_vector, 10);
                Transform B_Image = new_bullet.transform.GetChild(0);
                B_Image.rotation = Quaternion.Euler(0, 0, degree);
            }
            yield return new WaitForSeconds(1); //間隔
        }
    }
    void Start()
    {
        try
        {
            max_hp = this.GetComponent<Enemy_Status>().HP;
            Bullet_fire = (GameObject)Resources.Load("Bullet_3");
            Bullet_Circle = (GameObject)Resources.Load("Bullet_4");
            Bullet_Direction = (GameObject)Resources.Load("Bullet_2");
            Red_Box = (GameObject)Resources.Load("Red_Box");
            Player = GameObject.Find("Player");
            Main_Camera = Camera.main;
            max_height = Main_Camera.orthographicSize;
            max_width = max_height * (16.0f / 9.0f);
        }
        catch
        {
            Debug.Log("エラーが発生しました");
        }
        //Fire_Coroutine = StartCoroutine(Fire());
        //Circle_Coroutine = StartCoroutine(Circle());
        StartCoroutine(Strike(3));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hp = this.GetComponent<Enemy_Status>().HP;
        if (hp < max_hp / 2 && phase == 0)
        {
            StopAllCoroutines();
            phase += 1;
        }
    }
}
