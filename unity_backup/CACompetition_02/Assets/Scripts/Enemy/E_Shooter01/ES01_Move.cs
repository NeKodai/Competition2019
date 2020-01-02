using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES01_Move : MonoBehaviour
{
    private float x_vector = 0;
    private float y_vector = 0;

    public void Init(float input_x_vec, float input_y_vec)
    {
        x_vector = input_x_vec;
        y_vector = input_y_vec;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(x_vector, y_vector, 0);
        if (this.transform.position.x < -9f)
        {
            Destroy(this.gameObject);
        }
    }
}
