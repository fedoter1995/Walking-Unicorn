using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TestGame.Scripts.Characters.Player;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TestGame.Scripts
{
     public class TimeManager : MonoBehaviour
     {

        [Header("Time settings components")]
        [SerializeField]
        private SpriteRenderer _daySkySprite;
        [SerializeField]
        private SpriteRenderer _nightSkySprite;
        [SerializeField]
        private Light2D _light;
        [SerializeField]
        private float _fadeDuration = 5f;

        [Header("Time settings")]
        [SerializeField]
        private TimeOfDay _timeOfDay = TimeOfDay.Day;
        [SerializeField]
        private List<Interactor> _interactors = new List<Interactor>();

        public event Action<TimeOfDay> OnTimeOfDayChangeEvent;


        public void Initialize()
        {

            // We use waypoints to change the day and night
            foreach (var interactor in _interactors)
            {
                interactor.OnInteractEvent += ChangeTime;
            }
            SetTimeOfDay(_timeOfDay);
        }
        /// <summary>
        /// A temporary solution for changing day and night
        /// </summary>
        private void ChangeTime()
        {
            switch (_timeOfDay)
            {
                case TimeOfDay.Night:
                    {
                        SetTimeOfDay(TimeOfDay.Day);
                        break;
                    }
                case TimeOfDay.Day:
                    {
                        SetTimeOfDay(TimeOfDay.Night);
                        break;
                    }
            }
        }

        private void SetTimeOfDay(TimeOfDay daytime)
        {
            _timeOfDay = daytime;
            switch (daytime)
            {
                case TimeOfDay.Night:
                    {
                        _daySkySprite.DOFade(0f, _fadeDuration);
                        _nightSkySprite.DOFade(1f, _fadeDuration);
                        DOVirtual.Float(_light.intensity, 0.6f, _fadeDuration, value => SetLightIntensity(value));
                        break;
                    }
                case TimeOfDay.Day:
                    {
                        _daySkySprite.DOFade(1f, _fadeDuration);
                        _nightSkySprite.DOFade(0f, _fadeDuration);
                        DOVirtual.Float(_light.intensity, 1f, _fadeDuration, value => SetLightIntensity(value));
                        break;
                    }
            }
            OnTimeOfDayChangeEvent?.Invoke(_timeOfDay);
        }
        private void SetLightIntensity(float value)
        {
            _light.intensity = value;
        }

    }
}

public enum TimeOfDay
{
    Day,
    Night,
}
