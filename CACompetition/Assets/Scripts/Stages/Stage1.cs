using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    private GameObject Enemy;
    private int flame_count = 0;
    [SerializeField] GameObject Enemy_Field;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = (GameObject)Resources.Load("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        switch (flame_count)
        {
            case 0:
                break;
        }
        flame_count += 1;
    }
}
