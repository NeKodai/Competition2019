using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Generator : MonoBehaviour
{
    private GameObject Enemy_01;
    private GameObject Enemy_02;
    private IEnumerator Generate()
    {
        while (true)
        {
            float y_point = Random.Range(-4f, 4f);
            GameObject new_obj = Instantiate(Enemy_01, new Vector3(12, y_point, 0.0f), Quaternion.identity);
            new_obj.GetComponent<Enemy_Status>().Init(10, 0);
            new_obj = Instantiate(Enemy_01, new Vector3(13, y_point, 0.0f), Quaternion.identity);
            new_obj.GetComponent<Enemy_Status>().Init(10, 0);
            new_obj = Instantiate(Enemy_01, new Vector3(14, y_point, 0.0f), Quaternion.identity);
            new_obj.GetComponent<Enemy_Status>().Init(10, 0);
            if (UnityEngine.Random.Range(0, 3) == 0)
            {
                new_obj = Instantiate(Enemy_02, new Vector3(12, y_point, 0.0f), Quaternion.identity);
                new_obj.GetComponent<Enemy_Status>().Init(100, 0);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Enemy_01 = (GameObject)Resources.Load("Enemy_01");
        Enemy_02 = (GameObject)Resources.Load("Enemy_02");
        StartCoroutine("Generate");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
