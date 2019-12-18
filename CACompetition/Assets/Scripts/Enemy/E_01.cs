using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_01 : MonoBehaviour
{
    private GameObject Script_Obj;
    private void Move()
    {
        this.transform.Translate(-0.04f, 0, 0);
        if (this.transform.position.x < -10 || this.transform.position.y < -5.7f || this.transform.position.y > 5.7f)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            //Script_Obj.GetComponent<E_Bullet_System>().Straight(-0.1f, 0, this.gameObject);
            //StartCoroutine(Script_Obj.GetComponent<E_Bullet_System>().Circle_01(0.1f, 9, this.gameObject));
            Script_Obj.GetComponent<E_Bullet_System>().Homing_01(-0.1f, this.gameObject);
            yield return new WaitForSeconds(2f);
        }
    }
    void Start()
    {
        Script_Obj = GameObject.Find("ScriptObject");
        StartCoroutine(Shot());
    }
    void Update()
    {
        Move();
    }
}
