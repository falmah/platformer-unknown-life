using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsManagement : MonoBehaviour
{
    public int starsCount = 0;
    public Text starsText;

    void Start()
    {
        starsText = GameObject.Find("Star_count").GetComponent<Text>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            starsCount++;
            starsText.text = starsCount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}