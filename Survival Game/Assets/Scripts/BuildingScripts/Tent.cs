using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tent : MonoBehaviour
{
    [SerializeField] GameObject image;
    public bool Used { get; set; }
    private void Start()
    {
        if(Used)
        {
            StartCoroutine(ShowBlackScreen());
        }
        
    }

    IEnumerator ShowBlackScreen()
    {
        image.GetComponent<Image>().gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        image.GetComponent<Image>().gameObject.SetActive(false);

        Used = false;

    }
}
