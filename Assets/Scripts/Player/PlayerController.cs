using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public HealthBar healthBar;
    public GameObject deadMessage;
    public GameObject winMessage;
    public bool gameOver = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMax(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0 || transform.position.y < -5f){
            gameOver = true;
            deadMessage.SetActive(true);
            Invoke("EndLevel", 1);
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            winMessage.SetActive(true);
            Invoke("NextLevel", 1);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Projectile"){
            currentHealth -= other.gameObject.GetComponent<ProjectileController>().damage;
            healthBar.SetHealth(currentHealth);
        }
    }
    public void EndLevel(){
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    public void NextLevel(){
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 3);
    }
}
