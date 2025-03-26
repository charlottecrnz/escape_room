using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;

public class OpenDrawer : MonoBehaviour
{
    [SerializeField] public Transform drawer;  
    [SerializeField] public Vector3 openPosition; 
    [SerializeField] public Vector3 closedPosition;  
    [SerializeField] public float openSpeed = 0.5f;  
    [SerializeField] private bool isOpen = false;
    [SerializeField] private InputDevice targetDevice;
    [SerializeField] private bool isCoroutineRunning = false;
    [SerializeField] private bool isControllerNear = false; // Pour détecter si la manette est proche
    [SerializeField] private AudioSource audioSource1; // Référence à l'AudioSource
    [SerializeField] private AudioClip DrawerOpen; // Le son à jouer
    [SerializeField] private AudioClip DrawerClose;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];  // Prend la première manette trouvée
        }

        drawer.localPosition = closedPosition;
    }

    void Update()
    {
        if (targetDevice.isValid && isControllerNear) // Vérifie que la manette touche le tiroir
        {
            bool triggerPressed;
            if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
            {
                if (!isCoroutineRunning)
                {
                    if (isOpen)
                        StartCoroutine(CloseDrawer());
                    else
                        StartCoroutine(OpenDrawer2());
                }
            }
        }
        else if (!targetDevice.isValid)
        {
            Start(); // Re-détecte la manette
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Manette")) // Vérifie que c'est bien la manette qui touche
        {
            isControllerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Manette"))
        {
            isControllerNear = false;
        }
    }

    private IEnumerator OpenDrawer2()
    {
        isOpen = true;
        isCoroutineRunning = true;
        audioSource1.PlayOneShot(DrawerOpen);
        
        while (Vector3.Distance(drawer.localPosition, openPosition) > 0.01f)
        {
            drawer.localPosition = Vector3.MoveTowards(drawer.localPosition, openPosition, openSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        isCoroutineRunning = false;
    }

    private IEnumerator CloseDrawer()
    {
        isOpen = false;
        isCoroutineRunning = true;
        audioSource1.PlayOneShot(DrawerClose);

        while (Vector3.Distance(drawer.localPosition, closedPosition) > 0.01f)
        {
            drawer.localPosition = Vector3.MoveTowards(drawer.localPosition, closedPosition, openSpeed * Time.deltaTime);
            
            yield return null;
        }

        isCoroutineRunning = false;
    }


}
