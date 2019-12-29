using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵を生成するクラス　仮
public class Enemy_Generate : MonoBehaviour
{
    private GameObject Enemy;
    [SerializeField] GameObject Enemy_Field = null;
    public IEnumerator Generate()
    {
        while (true)
        {
            Instantiate(Enemy, new Vector3(11f, Random.Range(-5f, 5f), 0), Quaternion.identity, Enemy_Field.transform);
            yield return new WaitForSeconds(1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Enemy = (GameObject)Resources.Load("Enemy");
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
