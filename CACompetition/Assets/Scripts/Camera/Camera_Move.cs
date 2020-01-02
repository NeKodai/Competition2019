using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    // Start is called before the first frame update
    public IEnumerator Shake(int number, float scale)
    {
        for (int i = 0; i < number; i++)
        {
            Camera.transform.position = new Vector3(Random.Range(-scale, scale), Random.Range(-scale, scale), -10);
            yield return new WaitForSeconds(0);
        }
        Camera.transform.position = new Vector3(0, 0, -10);
        yield return 0;
    }
}
