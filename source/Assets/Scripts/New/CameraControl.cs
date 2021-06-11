//SETUP:
//Attach to empty pivot point object
//Make camera child of this
//Make player parent of this

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour {

    [SerializeField] private float curZoom = 5, minZoom = -1, maxZoom = 20, zoomSpeed = 5;
    [SerializeField] private float characterRotSpeed = 12;
    private float mouseX, mouseY;
    private float moveFB, moveLR;
    private float clipOffset = 0.5f;

    private Transform player; //Gets set to parent object in initialization
    private Transform cam; //Gets set to object tagged as main camera in initialization

    //The gameobject containing the players meshes
    [SerializeField] private Transform meshTrans;
    private Quaternion meshRot;

    // Start is called before the first frame update
    void Start() {
        curZoom = -curZoom;
        minZoom = -minZoom;
        maxZoom = -maxZoom;
        player = transform.parent;
        cam = Camera.main.transform;
        meshRot = meshTrans.rotation;
        //TODO: delete
        cam.localPosition = new Vector3(0, 0, curZoom);
    }

    // Update is called once per frame
    void Update() {
        //Change zoom with the mouse wheel.
        curZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (curZoom > minZoom)
            curZoom = minZoom;
        if (curZoom < maxZoom)
            curZoom = maxZoom;
        //Make sure camera is not obscured
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
                (cam.transform.position) - transform.position,
                out hit, -curZoom)) {
            cam.localPosition = new Vector3(0, 0, -Mathf.Abs(hit.distance) + clipOffset);
        }
        else {
            cam.localPosition = new Vector3(0, 0, curZoom);
        }

        //Get input from mouse
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);
        //Move camera
        player.localRotation = Quaternion.Euler(0, mouseX, 0);
        transform.localRotation = Quaternion.Euler(mouseY, 0, 0);
        //Move mesh smoothly
        float rotDist = Mathf.Abs(Mathf.Abs(meshRot.y) - Mathf.Abs(transform.rotation.y));
        meshRot = Quaternion.Slerp(meshRot, transform.rotation, Time.deltaTime * characterRotSpeed);
        if (rotDist > 0.5) {
            meshRot = Quaternion.Slerp(meshRot, transform.rotation, Time.deltaTime * characterRotSpeed * 2);
        }
        meshRot = Quaternion.Euler(0f, meshRot.eulerAngles.y, 0f);
        meshTrans.rotation = meshRot;

    }
}
