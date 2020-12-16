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
        private StarsManagement game_star;
        private MovementController move_controller;
        private EnemyController enemy_controller;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Player"));
            GameObject enemyGameObject =
               MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/enemy1"));
            game_info = gameGameObject.GetComponent<LifeManagement>();
            game = gameGameObject.GetComponent<PlayerScript>();
            move_controller = gameGameObject.GetComponent<MovementController>();
            game_star = gameGameObject.GetComponent<StarsManagement>();
            enemy_controller = enemyGameObject.GetComponent<EnemyController>();
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

        [Test]
        public void PickUpStarCheck()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Star"));
            GameObject starGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Star_count"));
            Assert.AreNotEqual(game_star, null);
            game_star.starsText = starGameObject.GetComponent<Text>();
            game_star.OnTriggerEnter2D(gameGameObject.GetComponent<Collider2D>());
            Assert.AreEqual(1, game_star.starsCount);
        }


        [UnityTest]
        public IEnumerable CheckJumpState()
        {
            move_controller.UpdateJump();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(true, move_controller.isJumping);
        }

        [UnityTest]
        public IEnumerable CheckEnemyPunch()
        {
            GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefubs/Player"));
            enemy_controller.KillHero(gameGameObject.GetComponent<Collider2D>());
            yield return new WaitForSeconds(0.1f);
            var lifes = gameGameObject.GetComponent<LifeManagement>();
            Assert.AreEqual(0,lifes.lifeCount);
        }

    }
}
