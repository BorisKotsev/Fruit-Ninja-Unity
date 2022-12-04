using UnityEngine;

public class Blade : MonoBehaviour
{
    public Vector3 direction { get; private set; }
    public float minSliceVelocity = 0.01f; 
    public float sliceForce = 5f;

    private Collider bladeColl;
    private Camera mainCamera; 
    private TrailRenderer bladeTrail;
    private bool slicing;

    private void Awake()
    {
        mainCamera = Camera.main;

        bladeColl = GetComponent<Collider>();  
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void StartSlicing()
    {
        Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        transform.position = newPos;

        slicing = true;
        bladeColl.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeColl.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        direction = newPos - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;

        bladeColl.enabled = velocity > minSliceVelocity;

        transform.position = newPos;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if(slicing)
        {
            ContinueSlicing();
        }
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }
}
