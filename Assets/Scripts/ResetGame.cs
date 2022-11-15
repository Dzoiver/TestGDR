using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject[] coins;
    [SerializeField] PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartGame()
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
        player.GFX.SetActive(true);
        player.ResetCoins();
        player.ResetPosition();
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
