using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_02 : MonoBehaviour
{
    private GameObject Script_Obj;
    private IEnumerator Move()
    {
        while (true)
        {
            if (this.transform.position.x >= 5)
            {
                this.transform.Translate(-0.2f, 0, 0);
            }
            else
            {
                StartCoroutine(Shot());
                yield break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            //Script_Obj.GetComponent<E_Bullet_System>().Straight(-0.1f, 0, this.gameObject);
            StartCoroutine(Script_Obj.GetComponent<E_Bullet_System>().Circle_01(0.1f, 9, this.gameObject));
            yield return new WaitForSeconds(1f);
        }
    }
    void Start()
    {
        StartCoroutine(Move());
        Script_Obj = GameObject.Find("ScriptObject");
    }
    void Update()
    {

    }
}
