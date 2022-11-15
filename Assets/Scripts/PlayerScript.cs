using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject RestartScreen;
    List<Vector3> path = new List<Vector3>();
    public GameObject GFX;
    Vector3 vect = new Vector3();
    NavMeshAgent agent;
    public LayerMask movementMask;
    Camera cam;
    int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        vect = transform.position;
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    public void ResetCoins()
    {
        coins = 0;
        coinText.text = "0";
    }

    public void ResetPosition()
    {
        transform.position = vect;
        agent.isStopped = false;
        agent.SetDestination(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            CollectCoins();
            other.gameObject.SetActive(false);
        }

        if (other.tag == "spike")
        {
            StartCoroutine(ResetScreen());
        }
    }
    void CollectCoins()
    {
        coins++;
        coinText.text = coins.ToString();
    }

    IEnumerator ResetScreen()
    {
        GFX.SetActive(false);
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        RestartScreen.SetActive(true);
        Debug.Log(RestartScreen.activeSelf);
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GFX.activeSelf)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                MoveToPoint(hit.point);
            }
        }
    }
}
