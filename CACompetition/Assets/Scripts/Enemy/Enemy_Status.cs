using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
敵のステータスを管理する
*/
public class Enemy_Status : MonoBehaviour
{
    private float hp = 50;
    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }
    void Init(float input_hp, int input_pattarn)
    {
        hp = input_hp;

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
