using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敵の動きを管理するクラス
public class Enemy_Move : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < -11)
        {
            Destroy(this.gameObject);
        }
        this.transform.Translate(-0.05f, 0, 0);
    }
}
