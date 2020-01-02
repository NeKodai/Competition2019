using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Move : MonoBehaviour
{
    private int move_count = 0;

    public int Move_Count
    {
        set { move_count = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var Status = this.gameObject.GetComponent<Enemy_Status>();
        if (this.gameObject.transform.position.x >= 5.5f && move_count == 0)
        {
            this.transform.Translate(-0.1f, 0, 0);
        }
        else if (move_count == 0)
        {
            StartCoroutine(this.gameObject.GetComponent<D_Attack>().Spread(10, 11, 2, 0.1f, 0.1f, 1));
            StartCoroutine(this.gameObject.GetComponent<D_Attack>().Circle(0.08f, 0.3f, 40));
            move_count += 1;
        }
        else if (Status.Hp <= Status.Max_Hp / 1.5 && move_count == 1)
        {
            StopAllCoroutines();
            StartCoroutine(this.gameObject.GetComponent<D_Attack>().Strike(1, 6, 1, 0f));
            move_count += 1;
        }
        else if (Status.Hp <= Status.Max_Hp / 2.5 && move_count == 2)
        {
            StopAllCoroutines();
            StartCoroutine(this.gameObject.GetComponent<D_Attack>().Strike(1, 6, 3, 0f));
            move_count += 1;
        }
    }
}
