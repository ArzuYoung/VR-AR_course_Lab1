using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 5.0f;
    public float force = 10.0f;
    public GameObject prefabBoomPoint;
    public GameObject prefabBoomSphere;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Sphere")
        {
            Destroy(other.gameObject);
            Vector3 boomPosition = other.gameObject.transform.position;
            Instantiate(prefabBoomPoint, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Instantiate(prefabBoomSphere, other.gameObject.transform.position, other.gameObject.transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(boomPosition, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, boomPosition, radius, 3.0f);
                }
            }
        }
    }
}
