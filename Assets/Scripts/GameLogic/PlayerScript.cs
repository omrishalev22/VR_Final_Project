using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    //declare GameObjects and create isShooting boolean.
    private bool isShooting;

    private Transform currentKnife;

    [SerializeField]
    public Transform KnifeTemplate;

    // Use this for initialization
    void Start()
    {

        //only needed for IOS
        Application.targetFrameRate = 60;

        //set isShooting bool to default of false
        isShooting = false;
    }

    //Shoot function is IEnumerator so we can delay for seconds
    IEnumerator Shoot(RaycastHit hit)
    {

        //set is shooting to true so we can't shoot continuosly
        isShooting = true;

        

        //play the gun shot sound and gun animation
        GetComponent<AudioSource>().Play();
        // transform.GetComponent<Animation>().Play();

        //wait for 1 second and set isShooting to false so we can shoot again
        yield return new WaitForSeconds(1f);
        isShooting = false;

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

            currentKnife = Instantiate(KnifeTemplate, Camera.main.transform.position, Quaternion.identity);

            currentKnife.GetComponent<Rigidbody>().AddForce(myray.direction * 200f);
            currentKnife.LookAt(hit.point);
            Destroy(currentKnife.gameObject, 3);
        }
    }
}
