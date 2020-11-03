using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class DragHelper
    {
        private int draggedStartID = -1;
        private int draggedEndID = -1;

        public void CheckDraggingItem(Event e, Rect rect, Color rectColor, int index)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            draggedStartID = index;
                            e.Use();
                        }
                    }
                    break;

                default:
                    break;
            }

            // Draw rect if feedback is being dragged
            if (draggedStartID == index && rect != Rect.zero)
            {
                EditorGUI.DrawRect(rect, rectColor);
            }

            // If hovering at the top of the feedback while dragging one, check where
            // the feedback should be dropped: top or bottom
            bool rectContainsMousePosition = rect.Contains(e.mousePosition);

            if (!rectContainsMousePosition || draggedStartID < 0)
            {
                return;
            }

            draggedEndID = index;

            Rect headerSplit = rect;
            headerSplit.height *= 0.5f;
            headerSplit.y += headerSplit.height;

            if (headerSplit.Contains(e.mousePosition))
            {
                draggedEndID = index + 1;
            }
        }

        public bool ResolveDragging(Event e, out int startIndex, out int endIndex)
        {
            bool ret = false;

            startIndex = -1;
            endIndex = -1;

            if (draggedStartID >= 0 && draggedEndID >= 0)
            {
                if (draggedEndID != draggedStartID)
                {
                    if (draggedEndID > draggedStartID)
                    {
                        draggedEndID--;
                    }

                    startIndex = draggedStartID;
                    endIndex = draggedEndID;

                    draggedStartID = draggedEndID;

                    ret = true;
                }
            }

            if (draggedStartID >= 0 || draggedEndID >= 0)
            {
                switch (e.type)
                {
                    case EventType.MouseUp:
                        {
                            draggedStartID = -1;
                            draggedEndID = -1;
                            e.Use();
                        }
                        break;

                    default:
                        break;
                }
            }

            return ret;
        }
    }
}