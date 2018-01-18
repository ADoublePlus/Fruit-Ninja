using UnityEngine;

namespace FruitNinja
{
    public class Blade : MonoBehaviour
    {
        GameObject currentBladeTrail;
        Camera cam;
        Rigidbody2D rigid2D;
        Vector2 previousPosition;
        CircleCollider2D cC2D;

        public GameObject bladeTrailPrefab;

        public float minCuttingVelocity = .001f;

        bool isCutting = false;

        // Use this for initialization
        void Start ()
        {
            cam = Camera.main;

            rigid2D = GetComponent<Rigidbody2D>();

            cC2D = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        void Update ()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCutting();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCutting();
            }

            if (isCutting)
            {
                UpdateCut();
            }

        }

        void UpdateCut ()
        {
            Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            rigid2D.position = newPosition;

            float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;

            if (velocity > minCuttingVelocity)
            {
                cC2D.enabled = true;
            }
            else
            {
                cC2D.enabled = false;
            }

            previousPosition = newPosition;
        }

        void StartCutting ()
        {
            isCutting = true;

            currentBladeTrail = Instantiate(bladeTrailPrefab, transform);

            previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            cC2D.enabled = false;
        }

        void StopCutting ()
        {
            isCutting = false;

            currentBladeTrail.transform.SetParent(null);

            Destroy(currentBladeTrail, 2f);

            cC2D.enabled = false;
        }

    }
}