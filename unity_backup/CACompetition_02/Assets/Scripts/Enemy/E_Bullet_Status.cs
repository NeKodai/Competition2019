using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Bullet_Status : MonoBehaviour
{
    private float damage = 10;
    private float x_vector = 0;
    private float y_vector = 0;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Status>().Hp -= damage;
            Destroy(this.gameObject);
        }
    }
    public void Init(float input_vec_x, float input_vec_y, float input_damage = 10)
    {
        x_vector = input_vec_x;
        y_vector = input_vec_y;
        damage = input_damage;
    }
    // Start is called before the first frame update
    void Start()
    {

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
    }
}
