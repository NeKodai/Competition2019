using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
弾丸の動きを管理しています
*/
public class Bullet_Move : MonoBehaviour
{
    private float x_vector = 0;
    private float y_vector = 0;
    private int penetration = 0;
    private float damage = 0;

    /*弾丸の初期設定*/
    public void Init(float input_x_vecter, float input_y_vector, float input_damage, int input_penetration = 0)
    {
        x_vector = input_x_vecter;
        y_vector = input_y_vector;
        penetration = input_penetration;
        damage = input_damage;

    }
    /*当り判定*/
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (penetration == 0)
            {
                Destroy(this.gameObject);
            }
            other.gameObject.GetComponent<Enemy_Status>().Damage(damage);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(x_vector, y_vector, 0);
        if (this.transform.position.x > 10 || this.transform.position.x < -10 || this.transform.position.y < -5.7f || this.transform.position.y > 5.7f)
        {
            Destroy(this.gameObject);
        }
    }
}
