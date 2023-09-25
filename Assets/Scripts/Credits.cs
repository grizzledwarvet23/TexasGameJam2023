using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("Canvas");
        if (!go)
            return;

        image = go.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openCredits()
    {
        if (image)
            image.enabled = !image.enabled;
    }
}