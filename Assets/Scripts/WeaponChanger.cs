using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages the weapon changing mechanic
/// </summary>
public class WeaponChanger : MonoBehaviour
{
    //TODO: cambiar lógica para que coja automáticamente las armas del GameObject Weapon
    public GameObject weapon1, weapon2;
    public Text ammoText;
    public bool isChanging = false;
    public float timeToChangeWeapon = 0.2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isChanging)
        {
            if (timer < timeToChangeWeapon)
            {
                timer += Time.deltaTime;
            }
            else
            {
                isChanging = false;
                timer = 0f;
            }
        }
        ChangeWeapon();
        SetAmmoCounter();
    }

    public void ChangeWeapon()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0
            && weapon1.GetComponent<Weapon>().isReloading == false
            && weapon2.GetComponent<Weapon>().isReloading == false
            && !isChanging)
        {
            if (weapon1.activeInHierarchy)
            {
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            }
            else
            {
                weapon1.SetActive(true);
                weapon2.SetActive(false);
            }
            isChanging = true;
        }
    }

    public void SetAmmoCounter()
    {
        if (weapon1.activeInHierarchy)
        {
            ammoText.text = weapon1.GetComponent<Weapon>().currentAmmo.ToString();
        }
        else
        {
            ammoText.text = weapon2.GetComponent<Weapon>().currentAmmo.ToString();
        }
    }
}
