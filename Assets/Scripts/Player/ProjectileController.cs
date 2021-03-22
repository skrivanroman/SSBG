using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Projectile" && other.gameObject.tag != "Player"){
            Destroy(gameObject);
        }
    }    

}
