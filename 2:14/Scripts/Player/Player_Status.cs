using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour
{
    float hp = 100; //キャラの体力
    float speed = 0.0f; //キャラのスピード
    float damage = 0;　//弾丸のダメージ
    int shot_interval = 0; //弾丸の発射間隔
    int shot_type = 2;//射撃タイプ
    int level = 0; //射撃レベル
    bool invisible = false; //無敵時間かどうか


    //ここからプロパティ
    public float HP
    {
        set
        {
            if (value - hp < 0)
            {
                invisible = true;
                StartCoroutine(Invisible_Fnc());
            }

            hp = value;
            if (hp <= 0) //体力がゼロ以下なら消滅する
            {
                Destroy(this.gameObject);
            }

        }
        get { return hp; }
    }
    public int LEVEL
    {
        set
        {
            level = value;
            this.GetComponent<Player_Shot>().LEVEL = value;
        }
        get { return level; }
    }
    public int SHOT_TYPE
    {
        set
        {
            shot_type = value;
            this.GetComponent<Player_Shot>().SHOT_TYPE = value;
        }
        get { return shot_type; }
    }

    public float SPEED
    {
        set
        {
            speed = value;
            this.GetComponent<Player_Move>().SPEED = value;
        }
        get { return speed; }
    }
    public int SHOT_INTERVAL
    {
        set
        {
            shot_interval = value;
            this.GetComponent<Player_Shot>().SHOT_INTERVAL = value;
        }
        get { return shot_interval; }
    }
    public float DAMAGE
    {
        set
        {
            damage = value;
            this.GetComponent<Player_Shot>().DAMAGE = value;

        }
        get
        {
            return damage;
        }
    }
    public bool INVISIBLE
    {
        set
        {
            invisible = value;
        }
        get
        {
            return invisible;
        }
    }
    //プロパティここまで

    IEnumerator Invisible_Fnc()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform Animation = this.transform.GetChild(0);
            Animation.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.05f);
            Animation.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        invisible = false;
    }

    void Start()
    {
        this.SPEED = 0.1f;
        this.SHOT_INTERVAL = 5;
        this.LEVEL = 1;
        this.SHOT_TYPE = 2;
        this.DAMAGE = 5f;
    }

}
