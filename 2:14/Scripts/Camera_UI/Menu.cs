using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject Menu_obj = null;
    [SerializeField] GameObject CACTyan = null;
    bool move_stop = false; //メニュー表示の途中かどうか
    //キー入力
    void Key_Input()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                if (move_stop == false)
                {
                    move_stop = true;
                    Menu_obj.SetActive(true);
                    IEnumerator Move()
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            CACTyan.transform.Translate(1, 0, 0);
                            yield return new WaitForSeconds(0);
                        }
                        move_stop = false;
                    }
                    StartCoroutine(Move());
                    Time.timeScale = 0;
                }

            }
            else
            {
                if (move_stop == false)
                {
                    move_stop = true;
                    IEnumerator Move()
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            CACTyan.transform.Translate(-1, 0, 0);
                            yield return new WaitForSeconds(0);
                        }
                        move_stop = false;
                        Menu_obj.SetActive(false);
                    }
                    StartCoroutine(Move());
                    Time.timeScale = 1;
                }

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Key_Input();
    }
}
