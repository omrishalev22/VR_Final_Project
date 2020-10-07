using UnityEngine;
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
            Ray myray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            currentAmmo = Instantiate(ammoTemplate);
            currentAmmo.position = spwanPoint.position;
            currentAmmo.rotation = transform.rotation;

            var audioData = GetComponent<AudioSource>();
            audioData.Play(0);

            var rb = currentAmmo.GetComponent<Rigidbody>();
            rb.AddForce(myray.direction.normalized * 700f, ForceMode.Acceleration);
            rb.AddTorque(transform.right * 500f , ForceMode.VelocityChange);

            if (Physics.Raycast(myray, out hit))
            {
                if (hit.collider.gameObject.name.Contains("Target"))
                {
                    Destroy(hit.collider.gameObject, 1f);
                }
            }

            Destroy(currentAmmo.gameObject, 3);
        }
    }
}
