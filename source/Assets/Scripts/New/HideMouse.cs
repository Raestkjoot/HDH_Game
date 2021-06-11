using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hides and locks the cursor
public class HideMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}