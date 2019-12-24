using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_Status : Player_Bullet_Move
{
    private float x_vector;
    private float y_vecotr;
    private float damage = 0;
    private int pattarn = 0;
    private int penetrate = 0;

    public void Init(float input_x_vector, float input_y_vector, int input_pattarn, float input_damage, int input_penetrate = 0)
    {
        x_vector = input_x_vector;
        y_vecotr = input_y_vector;
        damage = input_damage;
        pattarn = input_pattarn;
        penetrate = input_penetrate;
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
    }
}