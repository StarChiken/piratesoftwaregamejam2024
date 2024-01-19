using System.Collections;
using System.Collections.Generic;
using System.Text;
using Base.Core.Components;
using TMPro;

namespace Base.Core.Managers
{
    public class ShowMiracleDetails : MyMonoBehaviour
    {
        public TextMeshProUGUI text;

        public void ShowMiracles()
        {
            DisplayDictionaryContent(GameManager.Player.Devotion.DevotionActionsList);
        }

        public void ShowCommandments()
        {
            DisplayDictionaryContent(GameManager.Player.Devotion.CommandmentsList);
        }

        private void DisplayDictionaryContent<T, U>(Dictionary<T, U> dictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var kvp in dictionary)
            {
                stringBuilder.AppendLine($"{kvp.Key}: {kvp.Value}");
            }

            text.text = stringBuilder.ToString();
        }
    }
}
