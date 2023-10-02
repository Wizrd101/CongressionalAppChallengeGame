using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int coinCount;

    void Start()
    {
        coinCount = PlayerPrefs.GetInt("coins", 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coinCount++;
            Destroy(other.gameObject);
        }
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", coinCount);
    }
}
