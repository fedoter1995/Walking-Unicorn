using System.Collections;
using System.Collections.Generic;
using TestGame.Scripts.UI;
using TMPro;
using UnityEngine;

namespace TestGame.Scripts.UI
{
    public class TextWidget : UiWidget
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshPro;
        public void SetText(string text)
        {
            _textMeshPro.text = text;
        }
    }
}



