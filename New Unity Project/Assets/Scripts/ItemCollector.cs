using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pineappleCount = 0;
    [SerializeField] private Text scoreText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pineapple"))
        {
            Destroy(collision.gameObject);
            pineappleCount++;
            scoreText.text = "Score: " + pineappleCount;
        }
    }

}
