using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private Transform currentAmmo;

    [SerializeField]
    public Transform ammoTemplate;

    // Use this for initialization
    void Start()
    {

        //only needed for IOS
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
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

            var ammoPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 0.5f, Camera.main.transform.position.z);
            currentAmmo = Instantiate(ammoTemplate, ammoPosition, Quaternion.identity);
            GetComponent<AudioSource>().Play();

            var direction = new Vector3(myray.direction.x, myray.direction.y + 0.1f, myray.direction.z);
            currentAmmo.GetComponent<Rigidbody>().AddForce(direction * 900f);
            currentAmmo.LookAt(hit.point);
            Destroy(currentAmmo.gameObject, 3);
        }
    }
}
