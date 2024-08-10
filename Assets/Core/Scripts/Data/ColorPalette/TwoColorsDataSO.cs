using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName =("Data/Color Palette/Two Colors SO"))]
    public class TwoColorsDataSO : ScriptableObject
    {
        [SerializeField] private Color _mainBarColor;
        [SerializeField] private Color _subBarColor;

        public Color MainColor => _mainBarColor;
        public Color SubColor => _subBarColor;
    }
}