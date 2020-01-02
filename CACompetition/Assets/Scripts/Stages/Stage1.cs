using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stage1 : MonoBehaviour
{
    private int flame_count = 0;
    [SerializeField] GameObject Enemy_Field = null;
    // Start is called before the first frame update

    private void Function_Liner(int number, float width, float position_y, float vector_x, float vector_y, float position_x = 11f)
    {
        GameObject new_obj = null;
        GameObject Enemy = (GameObject)Resources.Load("EL_01");
        for (int i = 0; i < number; i++)
        {
            new_obj = Instantiate(Enemy, new Vector3(position_x, position_y, 0), Quaternion.identity, Enemy_Field.transform);
            new_obj.GetComponent<EL01_Move>().Init(vector_x, vector_y);
            double rad = Math.Atan2(vector_y, vector_x);
            position_x -= (float)Math.Cos(rad) * width;
            position_y += (float)Math.Sin(rad) * width;
        }
    }

    private IEnumerator Coroutine_Liner(int type, int number, float wait, float position_y, float vector_x, float vector_y, float position_x = 11f)
    {
        GameObject new_obj;
        switch (type)
        {
            case 0:
                GameObject Enemy = (GameObject)Resources.Load("ES_01");
                for (int i = 0; i < number; i++)
                {
                    new_obj = Instantiate(Enemy, new Vector3(position_x, position_y, 0), Quaternion.identity, Enemy_Field.transform);
                    new_obj.GetComponent<ES01_Move>().Init(vector_x, vector_y);
                    new_obj.GetComponent<ES01_Attack>().Init(0.1f, 1f);
                    yield return new WaitForSeconds(wait);
                }
                yield break;

            case 1:
                Enemy = (GameObject)Resources.Load("EC_01");
                for (int i = 0; i < number; i++)
                {
                    new_obj = Instantiate(Enemy, new Vector3(position_x, position_y, 0), Quaternion.identity, Enemy_Field.transform);
                    new_obj.GetComponent<EC01_Move>().Init(vector_x, vector_y);
                    new_obj.GetComponent<EC01_Attack>().Init(0.08f, 2f, 12);
                    yield return new WaitForSeconds(wait);
                }
                yield break;
        }

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject new_obj = null;
        float position_y = 0;
        switch (flame_count)
        {
            case 0:

                break;
            case 10:
                Function_Liner(5, 2, 4, -0.05f, 0);
                break;
            case 100:
                Function_Liner(5, 2, -4, -0.05f, 0);
                break;
            case 250:
                position_y = UnityEngine.Random.Range(-5f, 5f);
                StartCoroutine(Coroutine_Liner(0, 5, 1f, position_y, -0.05f, 0));
                break;
            case 400:
                position_y = UnityEngine.Random.Range(-5f, 5f);
                StartCoroutine(Coroutine_Liner(0, 5, 1f, position_y, -0.05f, 0));
                break;
            case 650:
                Function_Liner(5, 2, -2, -0.05f, 0);
                Function_Liner(5, 2, 0, -0.05f, 0);
                Function_Liner(5, 2, 2, -0.05f, 0);
                position_y = UnityEngine.Random.Range(-5f, 5f);
                StartCoroutine(Coroutine_Liner(1, 3, 2.5f, position_y, -0.03f, 0));
                break;
            case 1500:
                new_obj = Instantiate((GameObject)Resources.Load("Dragon"), new Vector3(12, 0, 0), Quaternion.identity, Enemy_Field.transform);
                new_obj.GetComponent<Enemy_Status>().Hp = 6000;
                break;

        }
        flame_count += 1;
    }
}
