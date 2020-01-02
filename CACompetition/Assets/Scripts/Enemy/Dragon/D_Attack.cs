using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class D_Attack : MonoBehaviour
{
    private GameObject Player;
    private GameObject E_Bullet;
    private Camera Camera;
    private Coroutine spread;
    private Coroutine circle;


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Status>().Hp -= 10;
        }
    }
    private double DegreeToRadian(double degree)
    {
        return Math.PI * degree / 180;
    }
    private double RadianToDegree(double rad)
    {
        return 180 * rad / Math.PI;
    }
    public IEnumerator Spread(int loop_number, int spred_number, float degree, float speed, float shot_delay, float wait_delay)
    {
        while (true)
        {
            double distance_x = Player.transform.position.x - this.gameObject.transform.position.x;
            double distance_y = Player.transform.position.y - this.gameObject.transform.position.y;
            double atan2 = Math.Atan2(distance_y, distance_x);
            double rad = DegreeToRadian(degree);
            double half_degree = degree * (spred_number - 1) / 2;
            double half_rad = DegreeToRadian(half_degree);
            for (int i = 0; i < loop_number; i++)
            {

                for (int j = 0; j < spred_number; j++)
                {
                    float x_vector = (float)Math.Cos(atan2 - (half_rad) + (rad * j));
                    float y_vector = (float)Math.Sin(atan2 - (half_rad) + (rad * j));
                    GameObject new_obj = Instantiate(E_Bullet, new Vector3(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.y, 0), Quaternion.identity);
                    new_obj.GetComponent<E_Bullet_Status>().Init(x_vector * speed, y_vector * speed);

                }
                yield return new WaitForSeconds(shot_delay);

            }
            yield return new WaitForSeconds(wait_delay);
        }

    }

    public IEnumerator Circle(float bullet_speed, float shot_delay, int number)
    {
        E_Bullet = (GameObject)Resources.Load("E_Bullet");
        while (true)
        {
            for (int i = 0; i < 360; i += (360 / number))
            {
                double rad = Math.PI * i / 180;
                GameObject new_obj = Instantiate(E_Bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                new_obj.GetComponent<E_Bullet_Status>().Init((float)Math.Cos(rad) * bullet_speed, (float)Math.Sin(rad) * bullet_speed);
                yield return new WaitForSeconds(0);
            }
            yield return new WaitForSeconds(shot_delay);
        }

    }
    public IEnumerator Strike(float start_delay, float delay, int strike_number, float strike_delay = 0f)
    {
        while (true)
        {
            this.gameObject.tag = "Untagged";
            if (circle != null)
            {
                StopCoroutine(circle);
                StopCoroutine(spread);
                circle = null;
                spread = null;
            }
            while (this.gameObject.transform.position.x <= 12)
            {
                this.gameObject.transform.Translate(0.3f, 0.1f, 0);
                yield return new WaitForSeconds(0);
            }
            this.gameObject.transform.position = new Vector3(12, 0, 0);

            float[] start_list = new float[strike_number];
            GameObject[] red_zone_list = new GameObject[strike_number];
            float[] degree_list = new float[strike_number];
            for (int i = 0; i < strike_number; i++)
            {
                start_list[i] = UnityEngine.Random.Range(-8f, 8f);
            }
            for (int i = 0; i < strike_number; i++)
            {
                double distance_x = 0;
                double distance_y = 0;
                if (i == 0)
                {
                    distance_x = Player.transform.position.x - 12;
                    distance_y = Player.transform.position.y - start_list[i];
                }
                else
                {
                    distance_x = UnityEngine.Random.Range(-5f, 5f) - 12;
                    distance_y = UnityEngine.Random.Range(-5f, 5f) - start_list[i];
                }
                double atan2 = Math.Atan2(distance_y, distance_x);
                degree_list[i] = (float)RadianToDegree(atan2);
                float center_x = (float)(12 - (distance_x / 2));
                float center_y = (float)(start_list[i] - (distance_y / 2));
                red_zone_list[i] = Instantiate((GameObject)Resources.Load("Red_Zone"), new Vector3(center_x, center_y, 0), Quaternion.Euler(0, 0, degree_list[i]));
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(start_delay);
            for (int i = 0; i < strike_number; i++)
            {
                this.gameObject.transform.position = new Vector3(12, start_list[i], 0);

                this.transform.rotation = Quaternion.Euler(0, 0, degree_list[i] - 180);
                //ドラゴン通過処理
                for (int j = 0; j < 30; j++)
                {
                    //this.transform.Translate((float)Math.Cos(atan2) * 0.95f, (float)Math.Sin(atan2) * 0.95f, 0);
                    this.transform.Translate(-0.98f, 0, 0);
                    if (j == 10)
                    {
                        StartCoroutine(Camera.GetComponent<Camera_Move>().Shake(10, 0.1f));
                    }
                    GameObject new_obj = Instantiate(E_Bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                    int degree = UnityEngine.Random.Range(0, 360);
                    double rad = DegreeToRadian(degree);
                    new_obj.GetComponent<E_Bullet_Status>().Init((float)Math.Cos(rad) * 0.03f, (float)Math.Sin(rad) * 0.03f);
                    yield return new WaitForSeconds(0);
                }

                Destroy(red_zone_list[i]);
                yield return new WaitForSeconds(strike_delay);
            }
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.gameObject.transform.position = new Vector3(12, 0, 0);
            this.gameObject.tag = "Enemy";
            while (this.gameObject.transform.position.x >= 5.5)
            {
                this.transform.Translate(-0.1f, 0, 0);
                yield return new WaitForSeconds(0);
            }
            spread = StartCoroutine(Spread(10, 11, 2, 0.1f, 0.1f, 1));
            circle = StartCoroutine(Circle(0.08f, 0.3f, 40));
            yield return new WaitForSeconds(delay);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        E_Bullet = (GameObject)Resources.Load("E_Bullet");
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
