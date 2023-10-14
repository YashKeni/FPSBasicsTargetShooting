using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipGun : MonoBehaviour
{
    [SerializeField] GameObject gunModel;

    GunPickup gunPickup;

    void Start()
    {
        gunPickup = FindObjectOfType<GunPickup>();
        gunModel.SetActive(false);
    }

    void Update()
    {
        if (gunPickup.hasPickedUp)
        {
            gunModel.SetActive(true);
        }
    }
}
