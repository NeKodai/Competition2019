using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class E_Bullet_Move_Homing : MonoBehaviour
{
    private float speed = 0;
    private float damage = 0;
    private float change_degree = 0;
    private double radian = 0;
    private int penetration = 0;
    private GameObject Player;

    public void Init(GameObject input_Player, float input_speed, float input_damage, int input_penetration = 0, float input_degree = 1)
    {
        Player = input_Player;
        speed = input_speed;
        damage = input_damage;
        penetration = input_penetration;
        change_degree = input_degree;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (penetration == 0)
            {
                Destroy(this.gameObject);
            }
            other.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    double DegreeToRadian(float input_degree)
    {
        return 2 * Math.PI * (input_degree / 360);
    }
    float RadianToDegree(double rad)
    {
        return (float)(360 * (rad / 2 * Math.PI));
    }
    // Update is called once per frame
    void Update()
    {
        double ori_rad = Math.Atan2(this.gameObject.transform.position.y - Player.transform.position.y, this.gameObject.transform.position.x - Player.transform.position.x);
        if (ori_rad < radian)
        {
            radian -= DegreeToRadian(change_degree);
        }
        if (ori_rad > radian)
        {
            radian += DegreeToRadian(change_degree);
        }
        this.gameObject.transform.Translate((float)(Math.Cos(radian) * speed), (float)(Math.Sin(radian) * speed), 0);
        if (this.transform.position.x > 10 || this.transform.position.x < -10 || this.transform.position.y < -5.7f || this.transform.position.y > 5.7f)
        {
            Destroy(this.gameObject);
        }
    }
}
