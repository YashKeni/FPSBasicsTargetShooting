using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GunPickup : MonoBehaviour
{
    [SerializeField] float pickupRange = 3f;
    [SerializeField] AudioClip gunEquipSFX;
    [SerializeField] TextMeshProUGUI gunPickupText;

    AudioSource audioSource;

    public bool hasPickedUp = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunPickupText.enabled = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                gunPickupText.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickupGun();
                }
            }
            else
            {
                gunPickupText.enabled = false;
            }
        }
    }

    private void PickupGun()
    {
        hasPickedUp = true;
        AudioSource.PlayClipAtPoint(gunEquipSFX, FindObjectOfType<FirstPersonController>().transform.position);
        gunPickupText.enabled = false;
        Destroy(gameObject);
    }
}
