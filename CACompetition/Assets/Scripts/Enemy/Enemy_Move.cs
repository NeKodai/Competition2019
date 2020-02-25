using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敵の動きを管理するクラス
public class Enemy_Move : MonoBehaviour
{

    //線型移動ここから
    private IEnumerator Liner_System(float x_vector, float y_vector, int wait = -1)
    {
        while (true)
        {
            this.transform.Translate(x_vector, y_vector, 0);
            if (this.gameObject.transform.position.x < -9.0f || this.gameObject.transform.position.y < -5.0f || this.gameObject.transform.position.y > 5.0f)
            {
                Destroy(this.gameObject);
            }
            yield return 1;
        }
    }
    public void Liner(float x_vector, float y_vector, int wait = -1)
    {
        StartCoroutine(Liner_System(x_vector, y_vector, wait));
    }
    //線型移動ここまで

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
