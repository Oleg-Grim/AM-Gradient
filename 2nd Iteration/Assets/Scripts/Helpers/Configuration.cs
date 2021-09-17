using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gradient
{
    [System.Serializable]
    internal class Configuration
    {
        [Header("Rect transforms to resize")]
        public RectTransform CheckButton;
        public RectTransform WorkAreaCanvas;
        public RectTransform ScoreText;

        [Header("Prefabs reference")]
        public GameObject ImagePrefab;
        public GameObject CheckMarkPrefab;
        public GameObject CrossMarkPrefab;

        [Header("UI Reference")]
        public EventSystem EventSystem;
        public GraphicRaycaster WorkAreaRaycaster;
        public GraphicRaycaster CheckButtonRaycaster;

        [Header("Game Settings")]
        public int Rows;
        public int Columns;

        [Header("Gap between tiles")]
        public int GapSize;

        [Header("Blank tiles random range")]
        public int BlankMin;
        public int Blankax;

        [Header("Color gradients setings")]
        public Color[] Colors;
        
        [Header("Right side color to fade to")]
        public Color SideColor;

        [Header("Bottom side color to fade to")]
        public Color BottomCOlor;
        
        [HideInInspector] public float MarkTimer = 2;
        [HideInInspector] public bool Replay;
        [HideInInspector] public bool Rearrange;
        [HideInInspector] public bool Checking;
        [HideInInspector] public int Score;
    }
}