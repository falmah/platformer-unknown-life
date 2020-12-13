using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class PlayerTest
    {
        private PlayerScript game;
        private LifeManagement game_info;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Player"));
            game_info = gameGameObject.GetComponent<LifeManagement>();
            game = gameGameObject.GetComponent<PlayerScript>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
            Object.Destroy(game_info.gameObject);
        }

        [Test]
        public void RespawnPositionCheck()
        {
            game_info.lifeCount = 1;
            game_info.respawn();
            Assert.AreEqual(new Vector3(-8, 0), game_info.player.position);
        }

        [Test]
        public void GameOverCheck()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/game_over_txt"));
            game_info.lifeCount = 0;
            game_info.gameOverText = gameGameObject.GetComponent<Text>();
            game_info.respawn();
            Assert.AreEqual(true, game_info.gameOverText.enabled);
        }
/*
        [Test]
        public void PickUpStarCheck() 
        {
            game.starsCount = 0;
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Star"));
            GameObject starGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Star_count"));
            game.starsText = starGameObject.GetComponent<Text>();
            game.OnTriggerEnter2D(gameGameObject.GetComponent<Collider2D>());
            Assert.AreEqual(1, game.starsCount);
        }

        [UnityTest]
        public IEnumerable CheckJumpState()
        {
            game.Jump();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(true, game.isJumping);
        }
*/
    }
}
