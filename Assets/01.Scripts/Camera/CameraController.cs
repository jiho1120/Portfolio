using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LookAround();

    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = this.transform.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        x = Mathf.Clamp(x, 10f, 30f);

        this.transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

    }
}
