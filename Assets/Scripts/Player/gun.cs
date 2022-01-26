using UnityEngine;

public class gun : MonoBehaviour
{
    public LayerMask IgnoreRaycast;
    public int damage = 10; //daño que recibe el enemigo
    public float range = 100f; //rango máximo al que alcanza la bala
    //public float fireRate = 15f;
    public float impactForce = 60f; //retroceso del objeto impactado

    public Camera fpsCam; //camara desde donde se dispara el raycast
    public ParticleSystem muzzleFlash; //efecto de particulas de disparo
    public GameObject ImpactEffect; //efecto de particulas del objeto al que impacta

    //private float nextTimeToFire = 0f;
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
        muzzleFlash.Play(); //efecto disparar particulas
        RaycastHit hit;
        //dispara un raycast desde la camara del personaje hacía delante
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~IgnoreRaycast))
        {
            Debug.Log(hit.transform.name);

            //interaccion con el enemigo, le dice cuanto daño recibe y cuanto retroceso le causa
            enemy target = hit.transform.GetComponent<enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody !=null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            
            //coge la normal del poligono en el que ha impactado el raycast y duplica el efecto de particulas
            //justo en esa posicion y orientacion, despues de un tiempo lo borra
            GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
