using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // Reference to the ScoreManager script
    public ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an ingredient
        if (other.CompareTag("Ingredient"))
        {
            // Get the type of ingredient from its tag
            string ingredientType = other.tag;

            // Determine which player collided with the ingredient
            string playerTag = gameObject.tag; // Assuming player objects have unique tags like "Player1", "Player2", etc.

            // Determine if the collision is correct based on the predefined orders
            bool correctCollision = CheckCorrectCollision(playerTag, ingredientType);

            // Update score based on collision result
            if (correctCollision)
            {
                scoreManager.HandleCollision(playerTag, 5); // Add 5 points for correct ingredient
            }
            else
            {
                scoreManager.HandleCollision(playerTag, -1); // Subtract 1 point for incorrect ingredient
            }

            // Destroy the ingredient object after collision
            Destroy(other.gameObject);
        }
    }

    // Method to check if the collision is correct based on predefined orders
    private bool CheckCorrectCollision(string playerTag, string ingredientType)
    {
        // Example of predefined orders (you can expand this logic based on your game's requirements)
        if (playerTag == "Player1")
        {
            if (ingredientType == "Sausage" || ingredientType == "Mushroom")
            {
                return true;
            }
        }
        else if (playerTag == "Player2")
        {
            if (ingredientType == "Pineapple" || ingredientType == "Pepper")
            {
                return true;
            }
        }

        return false;
    }
}