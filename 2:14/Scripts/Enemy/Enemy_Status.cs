using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    float hp = 100; //体力

    bool invincible = false;

    //プロパティここから
    public float HP
    {
        set
        {
            if (invincible == false)
            {
                hp = value;
                if (hp <= 0) //体力がゼロ以下なら消滅
                {
                    Destroy(this.gameObject);
                }
            }
        }
        get
        {
            return hp;
        }
    }
    public bool INVINCIBLE
    {
        set
        {
            invincible = value;
        }
        get
        {
            return invincible;
        }
    }
    //プロパティここまで
    public void Init(float i_hp)
    {
        hp = i_hp;
    }

}
