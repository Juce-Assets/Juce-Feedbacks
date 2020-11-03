using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class PropertyLayoutHelper
    {
        private Rect currRect;

        private bool firstNext;

        public void Init(Rect currRect)
        {
            this.currRect = currRect;

            firstNext = true;
        }

        public Rect NextVerticalRect()
        {
            if (firstNext)
            {
                firstNext = false;

                currRect.height = EditorGUIUtility.singleLineHeight;
            }
            else
            {
                currRect.y += EditorGUIUtility.singleLineHeight + 2.0f;
            }

            return currRect;
        }

        public Rect CurrentHorizontalRect(int itemIndex, int toalItems)
        {
            float widthPerRect = currRect.width / toalItems;

            Rect ret = currRect;

            ret.x += (itemIndex * widthPerRect);
            ret.width = widthPerRect;

            return ret;
        }

        public float GetHeightOfElements(int elementsCount)
        {
            if (elementsCount <= 0)
            {
                return 0.0f;
            }

            return elementsCount * (EditorGUIUtility.singleLineHeight + 2.0f);
        }
    }
}