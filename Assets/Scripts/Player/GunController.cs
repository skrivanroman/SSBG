using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=THnivyG0Mvo
//https://www.youtube.com/watch?v=T5y7L1siFSY
public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float precision = 1f;
    public float projectileSpeed = 30f;
    public bool isAutomatic = false;
    public GameObject projectile;
    
    private Transform firePoint;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = GameObject.Find("Player/Main Camera").GetComponent<Camera>();
        firePoint = GameObject.Find("SciFiHandGun/GunCylinder").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();

    }
    private void Shoot(){
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)){
            
            GameObject projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;

            projectileObj.GetComponent<Rigidbody>().velocity = (hit.point - firePoint.position).normalized * projectileSpeed;
        }
    }
}
