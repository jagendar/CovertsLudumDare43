using Assets.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class YouWinController : MonoBehaviour
    {
        [SerializeField] private Text mainText;

        public void Update()
        {
            mainText.text =
                string.Format("Congratulations on plugging the volcano {0} times. Would you like to continue playing?",
                    GameplayController.instance.PlugCount);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void OnContinuePlayingClicked()
        {
            gameObject.SetActive(false);
        }
    }
}
