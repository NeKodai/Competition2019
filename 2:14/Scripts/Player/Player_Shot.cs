using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shot : MonoBehaviour
{
    int shot_interval = 0; //発射間隔(秒)
    int shot_count = 0; //発射間隔用のカウント
    int shot_type = 0; //射撃タイプ
    int level = 0; //射撃レベル
    float damage = 0;//ダメージ

    //ここからプロパティ
    //必ずPlayer_Statusから変更する!!
    public int SHOT_TYPE
    {
        set { shot_type = value; }
    }
    public int SHOT_INTERVAL
    {
        set { shot_interval = value; }
    }
    public int LEVEL
    {
        set { level = value; }
    }
    public float DAMAGE
    {
        set { damage = value; }
        get { return damage; }
    }
    //プロパティここまで

    //キー入力
    void Key_Input()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            switch (shot_type)
            {
                case 0:
                    this.GetComponent<Player_Bullet>().Normal(0.3f, damage, level);
                    break;
                case 1:
                    this.GetComponent<Player_Bullet>().N_Way(0.3f, damage, level, 15);
                    break;
                case 2:
                    this.GetComponent<Player_Bullet>().N_Way_Homing(0.3f, damage, 3, 15, 3, level * 10f);
                    break;

            }
            shot_count = 0;
            this.GetComponent<Player_Status>().SPEED = 0.05f;
        }
        else
        {
            this.GetComponent<Player_Status>().SPEED = 0.1f;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //発射ウェイト
        if (shot_count >= shot_interval)
        {
            this.Key_Input();
        }
        else
        {
            shot_count += 1;
        }
    }
}
