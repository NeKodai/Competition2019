using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : MonoBehaviour
{
    Camera Main_Camera; //メインカメラ
    float x_vector = -0.03f; //xベクトル
    float y_vector = 0; //yベクトル
    float max_height; //画面最大x
    float max_width; //画面最大y

    //初期設定
    public void Init(float i_x_vector, float i_y_vector)
    {
        x_vector = i_x_vector;
        y_vector = i_y_vector;
    }
    void Start()
    {
        Main_Camera = Camera.main;
        max_height = Main_Camera.orthographicSize;
        max_width = max_height * (16.0f / 9.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //画面外に出たら消滅
        if (this.gameObject.transform.position.x < -max_width || this.gameObject.transform.position.y < -max_height || this.gameObject.transform.position.y > max_height)
        {
            Destroy(this.gameObject);
        }
        this.transform.Translate(x_vector, y_vector, 0);
    }
}
