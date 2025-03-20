using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class DisplayKeypad : MonoBehaviour
{
    [SerializeField] public GameObject HiddenKP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HiddenKP.SetActive(false);
        Debug.Log("DisplayKeyPad activé");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision détectée avec : {other.gameObject.name} - Tag : {other.tag}");
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Manette"))
        {
            HiddenKP.SetActive(true);
        }
    }
}
