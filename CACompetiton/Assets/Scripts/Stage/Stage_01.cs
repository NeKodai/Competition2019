using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_01 : MonoBehaviour
{
    private GameObject Enemy_01;
    private GameObject Enemy_02;
    private long time_count = 0;
    [SerializeField] private GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        Enemy_01 = (GameObject)Resources.Load("Enemy_01");
        Enemy_02 = (GameObject)Resources.Load("Enemy_02");
        StartCoroutine(Canvas.GetComponent<Stage_Title>().title("Stage01"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (time_count)
        {
            case 0:
                GameObject new_obj = Instantiate(Enemy_01, new Vector3(12, 4, 0.0f), Quaternion.identity);
                new_obj.GetComponent<Enemy_Status>().Init(10, 0);

                new_obj = Instantiate(Enemy_01, new Vector3(13, 4, 0.0f), Quaternion.identity);
                new_obj.GetComponent<Enemy_Status>().Init(10, 0);
                new_obj = Instantiate(Enemy_01, new Vector3(14, 4, 0.0f), Quaternion.identity);
                new_obj.GetComponent<Enemy_Status>().Init(10, 0);
                break;
            case 100:
                break;
        }
        time_count += 1;
    }
}
