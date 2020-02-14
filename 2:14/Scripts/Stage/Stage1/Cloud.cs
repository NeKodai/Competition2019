using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    Camera Main_Camera;
    [SerializeField] GameObject[] Clouds = { null, null };
    float max_height = 0;
    float max_width = 0;
    [SerializeField] float speed = -0.02f;

    float SPEED
    {
        set { speed = value; }
        get { return speed; }
    }
    // Update is called once per frame
    //14.35 -14.00
    void Start()
    {
        Main_Camera = Camera.main;
        max_height = Main_Camera.orthographicSize;
        max_width = max_height * (16.0f / 9.0f);
    }
    void FixedUpdate()
    {
        foreach (GameObject Cloud in Clouds)
        {
            if (Cloud.transform.position.x <= -(max_width + 7.6f))
            {
                Cloud.transform.position = new Vector3(max_width + 9f, 0, 5);
            }
            Cloud.transform.Translate(speed, 0, 0);
        }
    }
}
