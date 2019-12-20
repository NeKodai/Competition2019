using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Title : MonoBehaviour
{
    private string output_str = "";
    [SerializeField] private GameObject stage_title;

    private IEnumerator title_del(string str)
    {
        for (int i = str.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j <= i; j++)
            {
                output_str += str[j];
                stage_title.GetComponent<Text>().text = output_str;
            }
            output_str = "";
            yield return new WaitForSeconds(0.03f);
            output_str = "";
            stage_title.GetComponent<Text>().text = output_str;
        }
        yield break;
    }
    public IEnumerator title(string str)
    {
        foreach (char c in str)
        {
            for (int i = 0; i < 10; i++)
            {
                string temp_str = output_str + (char)Random.Range(33, 126);
                stage_title.GetComponent<Text>().text = temp_str;
                yield return new WaitForSeconds(0.01f);
            }
            output_str += c;
            stage_title.GetComponent<Text>().text = output_str;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        output_str = "";
        StartCoroutine(title_del(str));
        yield break;
    }


}
