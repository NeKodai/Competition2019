using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
何もしません
当たった時にダメージが発生します
*/
public class Strike : MonoBehaviour
{
    float damage = 10; //ダメージ

    //衝突したら
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Status>().HP -= damage;
            Destroy(this.gameObject);
        }
    }
    //初期設定
    public void Init(float i_damage = 10)
    {
        damage = i_damage;
    }
}
