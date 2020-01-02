using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EC01_Attack : MonoBehaviour
{
    private GameObject E_Bullet = null;

    private GameObject Player;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Player_Status>().Hp -= 10;
        }
    }
    public void Init(float bullet_speed, float shot_delay, int number)
    {
        StartCoroutine(Circle(bullet_speed, shot_delay, number));
    }

    IEnumerator Circle(float bullet_speed, float shot_delay, int number)
    {
        E_Bullet = (GameObject)Resources.Load("E_Bullet");
        while (true)
        {
            for (int i = 0; i < 360; i += (360 / number))
            {
                double rad = Math.PI * i / 180;
                GameObject new_obj = Instantiate(E_Bullet, new Vector3(this.transform.position.x - 0.7f, this.transform.position.y, 0), Quaternion.identity);
                new_obj.GetComponent<E_Bullet_Status>().Init((float)Math.Cos(rad) * bullet_speed, (float)Math.Sin(rad) * bullet_speed);
            }
            yield return new WaitForSeconds(shot_delay);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
