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
            var count = GameplayController.instance.PlugCount;
            mainText.text =
                string.Format("Congratulations on plugging the volcano {0} time{1}. Would you like to continue playing?",
                    count, count > 1 ? "s" : "");
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
