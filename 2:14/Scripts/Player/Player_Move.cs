using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Camera Main_Camera; //メインカメラ
    float max_height; //画面最大x
    float max_width; //画面最大y
    float speed = 0; //キャラの移動速度(必ずPlayer_Statusから変更する)

    //プロパティここから
    //必ずPlayer_Statusから変更する!!
    public float SPEED
    {
        set { speed = value; }
        get { return speed; }
    }

    //プロパティここまで
    void Key_Input()
    {
        if (Input.GetKey(KeyCode.A) && this.transform.position.x > -max_width + 1.2)
        {

            this.transform.Translate(-speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) && this.transform.position.x < max_width - 1.2)
        {

            this.transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.W) && this.transform.position.y < max_height - 0.8)
        {

            this.transform.Translate(0, speed, 0);
        }
        else if (Input.GetKey(KeyCode.S) && this.transform.position.y > -max_height + 0.8)
        {

            this.transform.Translate(0, -speed, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Main_Camera = Camera.main;
        max_height = Main_Camera.orthographicSize;
        max_width = max_height * (16.0f / 9.0f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Key_Input();
    }
}
