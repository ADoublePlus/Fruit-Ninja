using UnityEngine;

namespace FruitNinja
{
    public class Fruit : MonoBehaviour
    {
        Rigidbody2D rigid;

        public GameObject fruitSlicedPrefab;

        public float startForce = 15f;

        // Use this for initialization
        void Start ()
        {
            rigid = GetComponent<Rigidbody2D>();

            rigid.AddForce(transform.up * startForce, ForceMode2D.Impulse);
        }

        void OnTriggerEnter2D (Collider2D col)
        {
            if (col.tag == "Blade")
            {
                Vector3 direction = (col.transform.position - transform.position).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);

                GameObject slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);

                Destroy(slicedFruit, 3f);

                Destroy(gameObject);
            }
        }
    }
}