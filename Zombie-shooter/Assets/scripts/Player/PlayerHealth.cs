using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // health at the beginning of game
    public int currentHealth; // health at the moment
    public Slider healthSlider; // reference for the HealthSlider
    public Image damageImage; // reference for the damageImage
    public AudioClip deathClip; // reference for the death clip
    public float flashSpeed = 5; // speed of damaging ???
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);// reference for the color by being damaged (red)

    Animator _anim;// reference for animation
    AudioSource _playerAudio;// reference for audio player
    PlayerMovement _playerMovement;// reference for the movement of the player

    bool isDead; // bool reference if player is dead
    bool damaged; // bool reference if player is damaged

    void Awake()
    {
        _anim = GetComponent<Animator>(); // assigning of animation reference
        _playerAudio = GetComponent<AudioSource>(); // assigning of audio reference
        _playerMovement = GetComponent<PlayerMovement>(); // assigning of playerMovement reference
        currentHealth = startingHealth; // making equal current and strating health 
    }

   
    private void Update()
    {
        if (damaged) // condition of being damaged or not
        {
            damageImage.color = flashColor; // making damageImage red by being damaged
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // smoothing of the colors 
            //changing beetwen red and clear???
        }
        damaged = false; // turning off bool damaged
    }
    public void TakeDamage(int amount) // method describing taken damage with parametr of the quantity of damage
    {
        damaged = true; // turning on the bool damaged
        currentHealth -= amount; // reassigning of currentHealth by subtraction the amount from the startingHealth
        healthSlider.value = currentHealth; // showing current Health in the healthSlider
        _playerAudio.Play(); // playing "PLayer hurt"
        if(currentHealth <= 0 && isDead) // condition of death of player - if current health <= 0
        {
            Death(); // awaking a method Death
        }
    }
    void Death() // method death of player
    {
        isDead = true; // bool shows that player is dead
        _anim.SetTrigger("Death"); // turning on the anim of death of player
        _playerAudio.clip = deathClip; // assigning clip "PLayers death" to audioplayer
        _playerAudio.Play(); // playing "PLayers death"
        _playerMovement.enabled = false; // Player can't move more
    }
}
