using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*EL01_Attack*/

public class EL01_Attack : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Player_Status>().Hp -= 10;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
