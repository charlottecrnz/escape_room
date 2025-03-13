using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class MorseInteraction : MonoBehaviour
{
    [SerializeField] public GameObject morse;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = morse.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        rb.isKinematic = true ;
    }
    private void OnTriggerExit(Collider other) {
        rb.isKinematic = false ;

    }
}
