using UnityEngine;

public class gun : MonoBehaviour
{
    public LayerMask IgnoreRaycast;
    public int damage = 10;
    public float range = 100f;
    //public float fireRate = 15f;
    public float impactForce = 60f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //&& Time.time >= nextTimeToFire
        {
            //nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        } 
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~IgnoreRaycast))
        {
            Debug.Log(hit.transform.name);

            enemy target = hit.transform.GetComponent<enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody !=null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            

            GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
