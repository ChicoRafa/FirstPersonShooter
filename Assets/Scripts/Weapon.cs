using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the weapon logic
/// this includes shooting and reloading 
/// </summary>
public class Weapon : MonoBehaviour
{
    public Camera camera;
    [Header("Shoot")]
    public float range = 100f;
    public GameObject bulletDecal;
    public float shootRate = 15f;
    private float nextTimeToShoot = 0f;
    public bool autoshoot = false;
    [Header("Ammo and Reload")]
    public int currentAmmo;
    public int ammoCapacity = 30;
    public bool isReloading = false;
    public float reloadDuration = 1f;
    private float reloadTimer = 0f;
    [Header("Animations")]
    public Animator animator;
    public WeaponChanger changer;
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip shootClip, reloadClip;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        ShootOrReload();
    }
    /// <summary>
    /// This method decides if we are able to shoot 
    /// based in if we are changing weapons or reloading
    /// </summary>
    private void ShootOrReload()
    {
        if (isReloading)
        {
            if (reloadTimer < reloadDuration)
            {
                reloadTimer = reloadTimer + Time.deltaTime;
            }
            else
            {
                currentAmmo = ammoCapacity;
                isReloading = false;
                reloadTimer = 0f;
            }
        }
        else
        {
            if (currentAmmo > 0 && !isReloading && !changer.isChanging)
            {
                ListenShootInput();
            }

            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < ammoCapacity)
            {
                animator.SetTrigger("Reload");
                audioSource.PlayOneShot(reloadClip);
                isReloading = true;
            }
        }
    }

    private void ListenShootInput()
    {
        if (autoshoot)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
            {
                //calc time between shoot
                nextTimeToShoot = Time.time + 1f / shootRate;
                //Shoot
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Shoot
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        audioSource.PlayOneShot(shootClip);
        RaycastHit shootHit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out shootHit, range))
        {
            //instantiate bulletDecal as prefab in the scene
            GameObject decal = Instantiate(bulletDecal, shootHit.point + (shootHit.normal * 0.025f), Quaternion.identity) as GameObject;
            //rotate decal taking in account the shootHit normal/surface
            decal.transform.rotation = Quaternion.FromToRotation(Vector3.up, shootHit.normal);
            //set decal as son of the object we are colliding with
            decal.transform.parent = shootHit.transform;

            //destruction of the target when shot
            if (shootHit.transform.gameObject.tag == "Target")
            {
                Destroy(shootHit.transform.gameObject);
                FindAnyObjectByType<GameManager>().remainingTargets--;
            }
        }
        currentAmmo--;
    }
}
