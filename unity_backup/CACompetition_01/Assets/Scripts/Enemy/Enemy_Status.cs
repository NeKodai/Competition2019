using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
敵のステータスを管理します
*/
public class Enemy_Status : MonoBehaviour
{
    private float hp = 0;
    private int type = 0;

    /*ダメージを受けるときに呼び出す関数*/
    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Init(int input_hp, int input_type)
    {
        hp = input_hp;
        type = input_type;
    }
    // Start is called before the first frame update
    void Start()
    {
    }
}
