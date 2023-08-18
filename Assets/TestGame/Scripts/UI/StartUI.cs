using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace TestGame.Scripts.UI
{
    public class StartUI : UiWidget
    {
        [SerializeField]
        private Button _startButton;

        public ButtonClickedEvent StartEvent => _startButton.onClick;

    }
}

