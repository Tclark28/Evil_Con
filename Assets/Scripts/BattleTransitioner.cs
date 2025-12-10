using UnityEngine;
using UnityEngine.SceneManagement;

public static class BattleTransitioner
{
    private const string BATTLE_SCENE_NAME = "BattleScene";

    // Static property to store the name of the enemy that initiated combat.
    public static string EncounteredEnemyName { get; private set; }

    /// <summary>
    /// Initiates the combat sequence by pausing the overworld and loading the battle scene.
    /// </summary>
    /// <param name="enemyGameObject">The enemy GameObject that triggered the encounter.</param>
    public static void InitiateForcedCombat(GameObject enemyGameObject)
    {
        // 1. Safety check
        if (SceneManager.GetActiveScene().name == BATTLE_SCENE_NAME)
        {
            Debug.LogWarning("Attempted to start combat while already in the Battle Scene.");
            return;
        }

        // 2. Disable player movement (This is a redundant check as it's done in the encounter scripts, but useful for safety)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            BasicPlayerController playerController = player.GetComponent<BasicPlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
        }

        // 3. Store the enemy's name for the Battle Scene State Machines
        EncounteredEnemyName = enemyGameObject.name;

        Debug.Log($"Combat initiated by {EncounteredEnemyName}. Loading {BATTLE_SCENE_NAME}.");

        // 4. Load the Battle Scene
        SceneManager.LoadScene(BATTLE_SCENE_NAME);
    }
}