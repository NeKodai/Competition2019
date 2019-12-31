using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour
{
    [SerializeField] private GameObject Player = null;
    [SerializeField] private float hp = 100;
    [SerializeField] private float speed = 0.2f;
    public GameObject Player_Get
    {
        get { return this.Player; }
    }
    public float Speed
    {
        set
        {
            speed = value;
            Player.gameObject.GetComponent<Player_Move>().Speed = speed;
        }
        get
        {
            return this.speed;
        }
    }
    public float Hp
    {
        set
        {
            hp = value;
        }
        get
        {
            return this.hp;
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
