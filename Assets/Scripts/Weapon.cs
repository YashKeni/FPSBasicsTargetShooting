using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Impact Settings")]
    [SerializeField] GameObject bulletHolePrefab;

    [Header("Audio/Video Settings")]
    [SerializeField] Camera FPCamera;
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] AudioSource audioSource;

    [Header("Muzzle Flash Settings")]
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] float muzzleFlashTime = 0.05f;

    [Header("Bullet Trace Settings")]
    [SerializeField] Transform gunBarrel;
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] float bulletLifetime = 0.2f;

    LineRenderer lineRenderer;
    GameObject bulletHole;

    private void Start()
    {
        muzzleFlash.SetActive(false);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }
        DestroyBulletHoleAfterSpawn();
    }

    private void Shoot()
    {
        RaycastHit bulletHit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out bulletHit))
        {
            audioSource.PlayOneShot(shootingSFX);
            StartCoroutine(ShowMuzzleFlash());
            bulletHole = Instantiate(bulletHolePrefab, bulletHit.point, Quaternion.LookRotation(bulletHit.normal));
            bulletHole.transform.position += bulletHole.transform.forward / 1000;
            ShowBulletTrace(bulletHole.transform.position);
        }
    }

    private void ShowBulletTrace(Vector3 bulletHolePosition)
    {
        if (bulletHole != null)
        {
            Ray ray = new Ray(gunBarrel.position, bulletHolePosition - gunBarrel.position);
            RaycastHit traceHit;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, gunBarrel.position);

            if (Physics.Raycast(ray, out traceHit, Mathf.Infinity))
            {
                lineRenderer.SetPosition(1, traceHit.point);
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + ray.direction * 1000f);
            }

            Invoke("DisableLineRenderer", bulletLifetime);
        }
    }

    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashTime);
        muzzleFlash.SetActive(false);
    }

    void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }

    private void DestroyBulletHoleAfterSpawn()
    {
        Destroy(bulletHole, 10f);
    }
}
