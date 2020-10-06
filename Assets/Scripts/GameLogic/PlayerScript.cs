﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private Transform currentAmmo;

    [SerializeField]
    public Transform ammoTemplate;

    [SerializeField]
    public Transform spwanPoint;

    // Use this for initialization
    void Start()
    {
        //only needed for IOS
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            Ray myray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f,0f));
            RaycastHit hit;

            if (Physics.Raycast(myray, out hit))
            {
                if (hit.collider.gameObject.name.Contains("Target"))
                {
                    Destroy(hit.collider.gameObject, 1f);
                }
            }

            currentAmmo = Instantiate(ammoTemplate);
            currentAmmo.position = spwanPoint.position;
            currentAmmo.rotation = spwanPoint.rotation;
            var audioData = GetComponent<AudioSource>();
            audioData.Play(0);

            currentAmmo.GetComponent<Rigidbody>().AddForce(transform.forward * 700f);
            currentAmmo.LookAt(hit.point);
            Destroy(currentAmmo.gameObject, 3);
        }
    }
}
