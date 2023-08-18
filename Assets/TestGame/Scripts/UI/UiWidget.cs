using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TestGame.Scripts.UI
{
    public class UiWidget : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup _group;

        [Header("Gradual Settings")]
        [SerializeField]
        public bool isGradual = false;
        [SerializeField]
        public bool isConstant = true;
        [SerializeField]
        public float openDuration = 1f;
        [SerializeField]
        public float showDuration = 1f;


        public void ShowWidget()
        {
            if (isGradual)
                ShowGradually();          
            else
                ShowWidgetHard();
        }
        public void HideWidget()
        {
            if (isGradual)
                HideGradually();
            else
                HideWidgetHard();
        }
        private void ShowGradually()
        {
            _group.interactable = true;
            _group.blocksRaycasts = true;
            _group.DOFade(1f, openDuration).onComplete = StartShowDuration;
        }
        private void StartShowDuration()
        {
            if(!isConstant)
                StartCoroutine(ShowDurationRoutine());
        }
        private IEnumerator ShowDurationRoutine()
        {
            yield return new WaitForSeconds(showDuration);
            HideWidget();
        }
        private void HideGradually()
        {
            _group.interactable = false;
            _group.blocksRaycasts = false;
            _group.DOFade(0f, openDuration);
        }
        private void ShowWidgetHard()
        {
            _group.alpha = 1;
            _group.interactable = true;
            _group.blocksRaycasts = true;
            StartShowDuration();
        }
        private void HideWidgetHard()
        {
            _group.alpha = 0;
            _group.interactable = false;
            _group.blocksRaycasts = false;
        }

    }
}
