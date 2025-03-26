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
    [SerializeField] private GameObject Display;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private AudioSource audioSource ;
    [SerializeField] private AudioClip OpenDoor ;
    [SerializeField] private AudioClip PushBouton ;

    private string codeEntré = "";
    private bool codeValide = false; // Empêche l'entrée après validation

    private void Start() {
        Debug.Log("KeyPad Interaction activé");
        Display.GetComponent<Renderer>().material = blue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (codeValide) return; // Si le code est validé, ignorer toute entrée

        if (other.CompareTag("Bouton"))
        {
            string chiffre = other.gameObject.name; 
            AjouterChiffre(chiffre);
        }
    }

    private void AjouterChiffre(string chiffre)
    {
        if (codeValide) return; // Vérifie à nouveau avant d'ajouter un chiffre
        
        if (codeEntré.Length < 4) // Correction de la condition (doit être 4 et pas 5)
        {
            codeEntré += chiffre;
            ecran.text = codeEntré;
            Display.GetComponent<Renderer>().material = blue;
            audioSource.PlayOneShot(PushBouton);
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
            codeValide = true; // Empêche toute autre entrée
            StartCoroutine(OuvrirPorte());
            Display.GetComponent<Renderer>().material = green;
            ecran.text = codeCorrect;
        }
        else
        {
            Debug.Log("Code incorrect.");
            codeEntré = "";
            ecran.text = "";
            Display.GetComponent<Renderer>().material = red;
        }
    }

    private IEnumerator OuvrirPorte()
    {
        float temps = 0f;
        Quaternion rotationInitiale = porte.transform.rotation;
        Quaternion rotationFinale = Quaternion.Euler(0f, -90f, 0f);
        audioSource.PlayOneShot(OpenDoor); 

        while (temps < 1f)
        {
            porte.transform.rotation = Quaternion.Lerp(rotationInitiale, rotationFinale, temps);
            temps += Time.deltaTime * vitesseOuverture;
            yield return null;
        }

        porte.transform.rotation = rotationFinale;
    }
}
