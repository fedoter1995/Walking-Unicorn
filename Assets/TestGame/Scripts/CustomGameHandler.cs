using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TestGame.Scripts.Characters.Enemy;
using TestGame.Scripts.Characters.Player;
using TestGame.Scripts.Locations;
using TestGame.Scripts.Tools;
using TestGame.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestGame.Scripts
{
    public class CustomGameHandler : MonoBehaviour
    {
        [Header("Active characters")]
        [SerializeField]
        private Actor _actor;
        [SerializeField]
        private Finish _finish;

        [Header("UI elements")]
        [SerializeField]
        private EndUI _endUI;
        [SerializeField]
        private StartUI _startUI;
        [SerializeField]
        private TextWidget _locationNameUI;


        [Header("Locations")]
        [SerializeField]
        private List<LocationTransition> _locations = new List<LocationTransition>();


        [Header("Audio")]
        [SerializeField]
        private GameAudioController _audioController;
        
        [Header("Global Volume")]
        [SerializeField]
        private GlobalVolumeHandler _globalVolumeHandler;
        [Header("Time")]
        [SerializeField]
        private TimeManager _timeManager;



        private List<HumanoidEnemy> enemies = new List<HumanoidEnemy>();


    
        private void Awake()
        {
            CheckComponents();
            Initialize();   
        }
        private void StartGame()
        {
            EnableLocations();
            _actor.StartMoving();
            _startUI.HideWidget();
        }
        private void Initialize()
        {

            _actor.Initialize();
            _actor.LooseEvent += OnLoose;
            _finish.OnFinishEvent += OnWin;
            _timeManager.OnTimeOfDayChangeEvent += _actor.FlashlightActivity;


            _timeManager.Initialize();
            _audioController.Initialize();
            _globalVolumeHandler.Initialize();

            InitializeEnemies();
            InitializeLocations();
            InitializeUI();
        }
        private void InitializeEnemies()
        {
            var findedEnemies = FindObjectsOfType<HumanoidEnemy>();

            foreach (var enemy in findedEnemies)
            {
                enemy.Initialize();
                enemy.OnDeathEvent += OnEnemyDeath;
                enemies.Add(enemy);
            }
        }
        private void InitializeLocations()
        {
            foreach (var location in _locations)
            {
                location.Disable();
                location.OnTransitionEvent += ChangeLocation;
            }
        }
        private void InitializeUI()
        {
            _startUI.StartEvent.AddListener(StartGame);
            _endUI.ExitEvent.AddListener(ExitGame);
            _endUI.RestartEvent.AddListener(RestartGame);

            _endUI.HideWidget();

            _startUI.ShowWidget();
        }
        private void CheckComponents()
        {
            if (_finish == null)
                throw new Exception("Add Finish component in inspector!");
            if (_actor == null)
                throw new Exception("Add Actor component in inspector!");
            if (_endUI == null)
                throw new Exception("Add EndUI component in inspector!");
            if (_startUI == null)
                throw new Exception("Add StartUI component in inspector!");
        }

        /// <summary>
        /// We change some stats when switching to another location.
        /// </summary>
        /// <param name="locationSettings"></param>
        private void ChangeLocation(LocationSettings locationSettings)
        {
            _globalVolumeHandler.SmoothlyChangeVolume(locationSettings.LocatonVolumeProfile);
            _audioController.ChangeClipSmoothly(locationSettings.AudioClip,2f,true);
            _locationNameUI.SetText(locationSettings.LocationName);
            _locationNameUI.ShowWidget();
        }

        /// <summary>
        /// Temporary solution
        /// </summary>
        private void EnableLocations()
        {
            foreach (var location in _locations)
            {
                location.Enable();
            }
        }
        private void OnEnemyDeath(HumanoidEnemy enemy)
        {
            if(enemies.Contains(enemy))
                enemies.Remove(enemy);
        }
        private void OnWin()
        {
            _actor.OnWin();
            _endUI.ShowWidget();
            _endUI.OnWin();
        }
        private void OnLoose()
        {

            _audioController.ChangeClipHard("loose_song");


            foreach (var enemy in enemies)
            {
                enemy.OnWin();
            }
            _endUI.ShowWidget();
            _endUI.OnLoose();
        }
        private void ExitGame()
        {
            Application.Quit();
        }
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}
