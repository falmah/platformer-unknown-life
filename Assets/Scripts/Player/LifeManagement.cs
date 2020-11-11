using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManagement : MonoBehaviour
{
    public const float MAP_DEATH_LINE = -50.0f;

    public Transform player;
    public int lifeCount = 3;
    public Text lifeText;
    public Text gameOverText;
    public GameObject myObject;
    //public Button newGameBtn;

    void Start()
    {
        //GameObject lifeGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Lifes_count"));
        lifeText = GameObject.Find("Lifes_count").GetComponent<Text>();
        lifeText.text = lifeCount.ToString();
        //GameObject gameGameObject = Instantiate(Resources.Load<GameObject>("Prefubs/game_over_txt"));
        gameOverText = GameObject.Find("game_over_txt").GetComponent<Text>();
        gameOverText.enabled = false;   
        //newGameBtn.enabled = false;
    }

    void Update()
    {
        deathCheck();
    }

    public void respawn()
    {
        if (lifeCount > 0)
        {
            player.position = new Vector3(-8, 0);
        }
        else
        {
            gameOverText.enabled = true;
            //newGameBtn.enabled = true;
            Time.timeScale = 0;
        }
        
    }

    void deathCheck()
    {
        if (player.position.y < MAP_DEATH_LINE && lifeCount>0)
        {
            lifeCount--;
            lifeText.text = lifeCount.ToString();
            Debug.Log("Death! Lifes left: "+lifeCount);
            respawn();
        }
    }
}
