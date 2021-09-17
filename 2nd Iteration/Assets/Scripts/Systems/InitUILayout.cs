using UnityEngine;
using Leopotam.Ecs;
using UnityEngine.UI;

namespace Gradient
{
    internal class InitUILayout : IEcsInitSystem
    {
        private Configuration _config;

        public void Init()
        {
            Vector2 ScreenSize = new Vector2(Screen.width, Screen.height);
            Vector2 ScreenSizeFraction = new Vector2(ScreenSize.x /100, ScreenSize.y /100);
            
            float gap = _config.GapSize * ScreenSizeFraction.x;

            var button = _config.CheckButton;
            var text = _config.ScoreText;
            
            button.sizeDelta = new Vector2(ScreenSize.x - gap * 2, ScreenSizeFraction.y * 15);
            button.anchoredPosition = new Vector2(button.sizeDelta.x / 2 + gap, button.sizeDelta.y / 2 + gap);

            text.sizeDelta = new Vector2(ScreenSize.x - gap * 2, ScreenSizeFraction.y * 10);
            text.anchoredPosition = new Vector2(text.sizeDelta.x /2 + gap, ScreenSize.y - (text.sizeDelta.y / 2 + gap));
        }
    }
}