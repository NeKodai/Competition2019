using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Bullet_Move : MonoBehaviour
{
    // Start is called before the first frame update
    private float x_vector = 0;
    private float y_vecotr = 0;
    private float damage = 0;
    private int penetration = 0;

    public void Init(float intput_x_vector, float input_y_vecotr, float input_damage, int input_penetration = 0)
    {
        x_vector = intput_x_vector;
        y_vecotr = input_y_vecotr;
        damage = input_damage;
        penetration = input_penetration;
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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(x_vector, y_vecotr, 0);
        if (this.transform.position.x > 10 || this.transform.position.x < -10 || this.transform.position.y < -5.7f || this.transform.position.y > 5.7f)
        {
            Destroy(this.gameObject);
        }
    }
}
