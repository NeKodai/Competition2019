using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
カメラを動かすスクリプト
必ずカメラにアタッチする
*/
public class Camera_Move : MonoBehaviour
{
    public IEnumerator Camera_Shake(int times)
    {
        for (int i = 0; i < times; i++)
        {
            this.transform.position = new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), -10);
            yield return new WaitForSeconds(0);
        }
        this.transform.position = new Vector3(0, 0, -10);
    }
}
