using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class includes the camera logic of the FPS game
/// </summary>
public class FPSCamera : MonoBehaviour
{
    public Transform player;
    public float lookSensitivity = 100f;
    float verticalRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //variables de las direcciones de la vista
        float XLook = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float YLook = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        //Bloqueo de la rotación en Y para que no podamos ver más allá de nosotros
        verticalRotation -= YLook;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        //El jugador rota con la vista
        player.Rotate(Vector3.up * XLook);

    }
}
