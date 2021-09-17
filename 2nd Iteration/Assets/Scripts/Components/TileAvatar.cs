using System.Collections.Generic;
using UnityEngine;

namespace Gradient
{
    internal struct TileAvatar
    {
        public GameObject Avatar;
        
        public Color CorrectColor;
        public Color WrongOne;
        public Color WrongTwo;

        public List<Color> AvailableColors;

        public int CurrentIndex;
    }
}