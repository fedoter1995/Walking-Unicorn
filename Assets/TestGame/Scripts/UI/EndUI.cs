using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace TestGame.Scripts.UI
{
    public class EndUI : UiWidget
    {
        [SerializeField]
        private TextMeshProUGUI _textmesh;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _exitButton;


        public ButtonClickedEvent RestartEvent => _restartButton.onClick;
        public ButtonClickedEvent ExitEvent => _exitButton.onClick;


        public void OnLoose()
        {
            _textmesh.text = "You Loose";
        }

        public void OnWin()
        {
            _textmesh.text = "You Win!";
        }
    }
}
