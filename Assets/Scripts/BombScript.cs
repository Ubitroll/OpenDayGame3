using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public bool doesKnockack = false;
    public int explosiveDamage;
    public float explosivePower = 5.0f;
    public float explosiveRadius = 20.0f;
    public float explosiveUpforce = 1.0f;
    public float fuse = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BlowUp());
       
    }

    void Detonate()
    {
        // Set explosion position on the bomb
        Vector3 explosionPosition = this.transform.position;

        // Set up overlap sphere for explosion
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosiveRadius);

        // For each thing effected by the explosion
        foreach (Collider hit in colliders)
        {
            
            // Damage them for explosive damage


            // If the explosion does knockback
            if (doesKnockack == true)
            {
                // Find the hit 
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                // If the hit thing has a rigidbody
                if (rb != null)
                {
                    // Add explosive knockback
                    rb.AddExplosionForce(explosivePower, explosionPosition, explosiveRadius, explosiveUpforce, ForceMode.Impulse);
                }
            }
            
        }

        // Destroy bomb
        Destroy(this.gameObject);
    }

    IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(fuse);
        Detonate();
    }
}
