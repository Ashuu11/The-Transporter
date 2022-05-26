using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerDamage : MonoBehaviour , IDamageable
{
    // Start is called before the first frame update
    [SerializeField] private int currentHealth;
    [SerializeField] Text healthScore;
    [SerializeField] private PlayerStats playerStats; //reference to script and creating its variable    
    [SerializeField] private Color maxHealthColor;
    [SerializeField] private Color zeroHealthColor;
       

    private void Start()
    {
        
        currentHealth = playerStats.maxHealth; //getting current max health info       
        SetPlayerStatsUi();
    }

    void Update()
    {
        //SetPlayerStatsUi();

    }

    public void DealDamage(int damage)
    {
        //Code here manmeet
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }

    private void SetPlayerStatsUi()
    {
        healthScore.text = "Health: " + currentHealth;

        float healthPercent = CalculateHealthPercentage();
        healthScore.color = Color.Lerp(zeroHealthColor, maxHealthColor, healthPercent / 100);
        //BloodOverlayImage.color.a = Mathf.Lerp(0, 1, 1 / (health / 100));
        // healthbarFillImage.color = Color.Lerp(zeroHealthColor, maxHealthColor, healthPercentage / 100);
    }

    void takeDamage(int DamagePoints)
    { 
        
        currentHealth -= DamagePoints;
        SetPlayerStatsUi();
        
        CheckIfDead();
    }

    private float CalculateHealthPercentage()
    {
        return ((float)currentHealth / (float)playerStats.maxHealth) * 100;
    }

   
}
