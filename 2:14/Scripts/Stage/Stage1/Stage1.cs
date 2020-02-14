using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] GameObject E_Liner = null;
    [SerializeField] GameObject Enemy_Field = null;

    int time_count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (time_count)
        {
            case -1:
                IEnumerator repeat()
                {
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject new_obj = Instantiate(E_Liner, new Vector3(10, 0, 0), Quaternion.identity, Enemy_Field.transform);
                        new_obj.GetComponent<Enemy_Status>().Init(100);
                        new_obj.AddComponent<Straight>();
                        new_obj.AddComponent<Circle>();
                        new_obj.GetComponent<Straight>().Init(-0.03f, 0);
                        new_obj.GetComponent<Circle>().Init(1.5f, 30, 0.1f);
                        yield return new WaitForSeconds(1);
                    }
                }
                StartCoroutine(repeat());
                break;
        }
        time_count += 1;
    }
}
