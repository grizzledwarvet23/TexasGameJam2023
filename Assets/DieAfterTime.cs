using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
