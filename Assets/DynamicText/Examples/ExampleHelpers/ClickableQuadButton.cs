using UnityEngine;
using System.Collections;

public class ClickableQuadButton : MonoBehaviour
{
    public Camera cam;
    public string messageName;
    public Transform target;
    public Material hiliteMaterial;

    Material orgMaterial;

    void Start()
    {
        orgMaterial = GetComponent<Renderer>().material;
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        GetComponent<Renderer>().material = orgMaterial;

        if (cam == null)
        {
            Debug.LogError("No camera", this);
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        bool pressed = Input.GetMouseButtonDown(0);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ray = cam.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                pressed = true;
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                ClickableQuadButton curl = hit.collider.GetComponent<ClickableQuadButton>();
                if (curl && curl == this)
                {
                    GetComponent<Renderer>().material = hiliteMaterial;
                    if (pressed)
                        target.SendMessage(messageName);
                }
            }
        }
    }
}
