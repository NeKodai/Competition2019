using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
CACちゃんの弾丸の発射の仕方を管理しています
*/
public class Bullet_System : MonoBehaviour
{
    private GameObject Bullet;

    void Start()
    {
        Bullet = (GameObject)Resources.Load("Bullet");
    }
    //発射間隔を返します
    public int Change_Wait_Time(int bullet_id)
    {
        int wait_time = 0;
        switch (bullet_id)
        {
            case 0:
                wait_time = 5;
                break;
            case 1:
                wait_time = 11;
                break;
        }
        return wait_time;
    }
    public void Straight(GameObject Player, float dmgrate)
    {
        GameObject new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y, 0.0f), Quaternion.identity);
        new_obj.GetComponent<Bullet_Move>().Init(0.5f, 0, 5f * dmgrate);
    }

    public void Way_3(GameObject Player, float dmgrate)
    {
        GameObject new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y, 0.0f), Quaternion.identity);
        new_obj.GetComponent<Bullet_Move>().Init(0.5f, 0.1f, 4f * dmgrate);
        new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y, 0.0f), Quaternion.identity);
        new_obj.GetComponent<Bullet_Move>().Init(0.5f, 0, 4f * dmgrate);
        new_obj = Instantiate(Bullet, new Vector3(Player.transform.position.x, Player.transform.position.y, 0.0f), Quaternion.identity);
        new_obj.GetComponent<Bullet_Move>().Init(0.5f, -0.1f, 4f * dmgrate);
    }

}
