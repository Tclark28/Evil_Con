using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public float interactDistance = 3f;                // how close the player needs to be
    public Material defaultMaterial;
    public Material highlightMaterial;
    public Camera playerCamera;                        // assign your Main Camera
    public GameObject promptCanvas;                    // assign your "Press E" Canvas

    private Transform player;
    private Renderer doorRenderer;
    private bool isHighlighted = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        doorRenderer = GetComponent<Renderer>();
        promptCanvas.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if close enough
        if (distance <= interactDistance)
        {
            // Check if camera is facing the door
            Vector3 directionToDoor = (transform.position - playerCamera.transform.position).normalized;
            float dot = Vector3.Dot(playerCamera.transform.forward, directionToDoor);

            if (dot > 0.7f) // roughly means camera is looking toward the door
            {
                HighlightDoor(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Only allow interaction when highlighted
                    if (isHighlighted)
                    {
                        SceneManager.LoadScene("MainLobby");
                    }
                }
            }
            else
            {
                HighlightDoor(false);
            }
        }
        else
        {
            HighlightDoor(false);
        }
    }

    void HighlightDoor(bool state)
    {
        if (state && !isHighlighted)
        {
            doorRenderer.material = highlightMaterial;
            promptCanvas.SetActive(true);
            isHighlighted = true;
        }
        else if (!state && isHighlighted)
        {
            doorRenderer.material = defaultMaterial;
            promptCanvas.SetActive(false);
            isHighlighted = false;
        }
    }
}
