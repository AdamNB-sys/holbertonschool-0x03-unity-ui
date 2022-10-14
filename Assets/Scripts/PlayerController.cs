using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    private Vector3 moveInput;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text gameOverText;
    public Image gameOverBG;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setting variables for player movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");

        // Stops player from moving faster on diagonal
        moveInput.Normalize();

        rb.velocity = moveInput * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the object can be picked up
        if (other.tag == "Pickup")
        {
            Destroy(other.gameObject);
            score += 1;
            SetScoreText();
            // Debug.Log($"Score: {score}");
        }

        // If the object is a trap
        if (other.tag == "Trap")
        {
            health -= 1;
            SetHealthText();
            // Debug.Log($"Health: {health}");
        }

        // Win condition
        if (other.tag == "Goal")
        {
            // Debug.Log("You win!");
            gameOverText.text = "You Win!";
            gameOverText.color = Color.black;
            gameOverBG.color = Color.green;
            gameOverBG.gameObject.SetActive(true);
        }
    }

    // Update checks health and resets game
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");
            health = 5;
            score = 0;
            SceneManager.LoadScene("maze");
        }
    }

    // Sets score in UI
    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    // Sets health in UI
    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }
}
