using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class KeypadInteraction : MonoBehaviour
{
    [SerializeField] private TMP_Text ecran; 
    [SerializeField] private string codeCorrect = "2506"; 
    [SerializeField] private GameObject porte; 
    [SerializeField] private float vitesseOuverture = 1.5f; 

    private string codeEntré = "";

    private void Start() {
        //Debug.Log("script activé");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision détectée avec : {other.gameObject.name} - Tag : {other.tag}");
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Bouton"))
        {
            Debug.Log(other.gameObject.name);
            string chiffre = other.gameObject.name; 
            AjouterChiffre(chiffre);
        }
    }

    private void AjouterChiffre(string chiffre)
    {
        if (codeEntré.Length < 5)
        {
            codeEntré += chiffre;
            ecran.text = codeEntré;
        }

        if (codeEntré.Length == 4)
        {
            VerifierCode();
        }
    }

    private void VerifierCode()
    {
        if (codeEntré == codeCorrect)
        {
            Debug.Log("Code correct ! Ouverture de la porte.");
            StartCoroutine(OuvrirPorte());
        }
        else
        {
            Debug.Log("Code incorrect.");
            codeEntré = "";
            ecran.text = "";
        }
    }

    private IEnumerator OuvrirPorte()
    {
        float temps = 0f;
        Quaternion rotationInitiale = porte.transform.rotation;
        Quaternion rotationFinale = Quaternion.Euler(0f, -90f, 0f); 

        while (temps < 1f)
        {
            porte.transform.rotation = Quaternion.Lerp(rotationInitiale, rotationFinale, temps);
            temps += Time.deltaTime * vitesseOuverture;
            yield return null;
        }

        porte.transform.rotation = rotationFinale;
    }
} 
