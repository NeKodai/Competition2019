using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    private GameObject Enemy;
    private int flame_count = 0;
    [SerializeField] GameObject Enemy_Field = null;
    // Start is called before the first frame update

    private void Formation_Liner(int number, float width, float position_y, float speed, float position_x = 11f)
    {
        GameObject new_obj = null;
        for (int i = 0; i < number; i++)
        {
            new_obj = Instantiate(Enemy, new Vector3(position_x, position_y, 0), Quaternion.identity, Enemy_Field.transform);
            new_obj.GetComponent<Enemy_Move>().Liner(speed, 0);
            position_x += width;
        }
    }
    void Start()
    {
        Enemy = (GameObject)Resources.Load("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject new_obj = null;
        switch (flame_count)
        {
            case 0:
                new_obj = Instantiate(Enemy, new Vector3(11f, Random.Range(-5f, 5f), 0), Quaternion.identity, Enemy_Field.transform);
                new_obj.GetComponent<Enemy_Move>().Liner(-0.05f, 0);
                break;
            case 10:

                Formation_Liner(5, 2, Random.Range(2f, 5f), -0.05f);
                Formation_Liner(5, 2, Random.Range(-2f, -5f), -0.05f);

                break;
        }
        flame_count += 1;
    }
}
