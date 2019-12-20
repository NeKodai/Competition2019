using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Change : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Change_HP(float hp)
    {
        GameObject HP_Obj = this.transform.Find("HP").gameObject;
        HP_Obj.GetComponent<Text>().text = "HP" + hp.ToString();
    }
}
