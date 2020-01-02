using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField] private float hp = 30;
    private float max_hp = 0;

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public float Hp
    {
        set
        {
            hp = value;
            max_hp = hp;
        }
        get { return this.hp; }
    }
    public float Max_Hp
    {
        get { return this.max_hp; }
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
