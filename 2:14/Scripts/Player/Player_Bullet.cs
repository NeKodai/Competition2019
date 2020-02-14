using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Bullet : MonoBehaviour
{
    [SerializeField] GameObject Enemy_Field = null; //敵のフィールド
    GameObject Bullet; //弾丸のゲームオブジェクト

    //通常弾　レベル上昇で発射速度上昇
    public void Normal(float speed, float damage, int level, int range = -1, bool penetrate = false)
    {

        GameObject new_bullet = Instantiate(Bullet, new Vector3(this.transform.position.x + 0.6f, this.transform.position.y, 0), Quaternion.identity);
        new_bullet.GetComponent<Player_Bullet_Status>().Init(speed, 0, damage, range, penetrate);
    }

    //拡散弾　レベル上昇で玉の数が増加
    public void N_Way(float speed, float damage, int level, float degree, int range = -1, bool penetrate = false)
    {

        double rad = degree * (Math.PI / 180);
        double half_degree = degree * (level - 1) / 2;
        double half_rad = half_degree * (Math.PI / 180);

        for (int i = 0; i < level; i++)
        {
            degree = (float)(180 * ((-(half_rad) + (rad * i)) / Math.PI));
            float x_vector = (float)Math.Cos(-(half_rad) + (rad * i));
            float y_vector = (float)Math.Sin(-(half_rad) + (rad * i));
            GameObject new_bullet = Instantiate(Bullet, new Vector3(this.transform.position.x + 0.6f, this.transform.position.y, 0), Quaternion.identity);
            Transform B_Image = new_bullet.transform.GetChild(0);
            B_Image.transform.rotation = Quaternion.Euler(0, 0, degree);
            new_bullet.GetComponent<Player_Bullet_Status>().Init(x_vector * speed, y_vector * speed, damage, range, penetrate);
        }
    }

    //誘導弾ここから
    //最も近い敵を検索する
    private IEnumerator Search_Target(GameObject Bullet, int delay, float homing_degree, float speed)
    {
        GameObject Target = null;
        float min_distance = 99999999;
        int wait_count = 0;

        foreach (Transform Child_Transform in Enemy_Field.transform)
        {
            float distance_x = Child_Transform.position.x - this.transform.position.x;
            if (distance_x < 0)
            {
                distance_x *= -1;
            }
            float distance_y = Child_Transform.position.y - this.transform.position.y;
            if (distance_y < 0)
            {
                distance_y *= -1;
            }
            float distance = (distance_x * distance_x) + (distance_y * distance_y);
            if (min_distance > distance)
            {
                min_distance = distance;
                Target = Child_Transform.gameObject;
            }
        }

        while (true)
        {
            wait_count += 1;
            if (wait_count >= delay)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (Bullet && Target)
        {
            Bullet.GetComponent<Player_Bullet_Status>().Start_Homing(Target, homing_degree, speed);
        }
        yield break;
    }

    //誘導弾 レベル上昇で誘導角度上昇
    //一応拡散可能
    public void N_Way_Homing(float speed, float damage, int level, float degree, int delay = 0, float homing_degree = 0, int range = -1, bool penetrate = false)
    {

        double rad = degree * (Math.PI / 180);
        double half_degree = degree * (level - 1) / 2;
        double half_rad = half_degree * (Math.PI / 180);
        for (int i = 0; i < level; i++)
        {
            float x_vector = (float)Math.Cos(-(half_rad) + (rad * i));
            float y_vector = (float)Math.Sin(-(half_rad) + (rad * i));
            degree = (float)(180 * ((-(half_rad) + (rad * i)) / Math.PI));
            GameObject new_bullet = Instantiate(Bullet, new Vector3(this.transform.position.x + 0.6f, this.transform.position.y, 0), Quaternion.identity);
            Transform B_Image = new_bullet.transform.GetChild(0);
            B_Image.transform.rotation = Quaternion.Euler(0, 0, degree);
            new_bullet.GetComponent<Player_Bullet_Status>().Init(x_vector * speed, y_vector * speed, damage, range, penetrate);
            StartCoroutine(Search_Target(new_bullet, delay, homing_degree, speed));

        }
    }
    //誘導弾ここまで

    void Start()
    {
        Bullet = (GameObject)Resources.Load("Bullet");
    }
}
