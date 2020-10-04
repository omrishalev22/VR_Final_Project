using Assets.Scripts.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera m_mainCam;

    private Ray m_ray;
    private RaycastHit m_hit;
    private string m_hitted;
    private bool m_hittedObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            m_ray = m_mainCam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            if (Physics.Raycast(m_ray, out m_hit))
            {
                if (m_hit.rigidbody != null)
                {
                    m_hitted = m_hit.rigidbody.transform.name;
                    m_hittedObj = true;
                }
            }
            if (m_hittedObj)
            {
                m_hittedObj = false;
                switch (m_hitted)
                {
                    case "PlayButton":
                        GameManager.isGameOver = false;
                        SceneManager.LoadScene(1);
                        break;
                    case "ExitButton":
                        Application.Quit();
                        break;
                    case "ScoresButton":
                        SceneManager.LoadScene(2);
                        break;
                    case "MenuButton":
                        SceneManager.LoadScene(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
