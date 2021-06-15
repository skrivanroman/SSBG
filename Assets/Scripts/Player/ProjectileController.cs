using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float liveSpan = 2f;
    public float damage;
    private float startTime;

    public bool isEnemy = false;

    public void Start(){
        startTime = Time.time;
    }
    public void Update(){
        if (Time.time - startTime >= liveSpan){
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (isEnemy){
            if (other.gameObject.tag != "Projectile" && other.gameObject.tag != "Enemy"){
                Destroy(gameObject);
            }
        }
        else{
            if (other.gameObject.tag != "Projectile" && other.gameObject.tag != "Player"){
                Destroy(gameObject);
            }
        }
    }    

}
