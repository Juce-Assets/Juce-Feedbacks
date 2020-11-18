using UnityEditor;
using UnityEngine;

namespace Juce.Feedbacks
{
    internal class DragHelper
    {
        private int draggedStartId = -1;
        private int draggedEndId = -1;

        public void CheckDraggingItem(Event e, Rect rect, Color rectColor, int index)
        {
            if (e.type == EventType.MouseDown)
            {
                if (rect.Contains(e.mousePosition))
                {
                    draggedStartId = index;
                    e.Use();
                }
            }

            // Draw rect if feedback is being dragged
            if (draggedStartId == index && rect != Rect.zero)
            {
                EditorGUI.DrawRect(rect, rectColor);
            }

            // If hovering at the top of the feedback while dragging one, check where
            // the feedback should be dropped: top or bottom
            bool rectContainsMousePosition = rect.Contains(e.mousePosition);

            if (!rectContainsMousePosition || draggedStartId < 0)
            {
                return;
            }

            draggedEndId = index;

            Rect headerSplit = rect;
            headerSplit.height *= 0.5f;
            headerSplit.y += headerSplit.height;

            if (headerSplit.Contains(e.mousePosition))
            {
                draggedEndId = index + 1;
            }
        }

        public bool ResolveDragging(Event e, out int startIndex, out int endIndex)
        {
            bool ret = false;

            startIndex = -1;
            endIndex = -1;

            if (draggedStartId >= 0 && draggedEndId >= 0)
            {
                if (draggedEndId != draggedStartId)
                {
                    if (draggedEndId > draggedStartId)
                    {
                        draggedEndId--;
                    }

                    startIndex = draggedStartId;
                    endIndex = draggedEndId;

                    draggedStartId = draggedEndId;

                    ret = true;
                }
            }

            if (draggedStartId >= 0 || draggedEndId >= 0)
            {
                if (e.type == EventType.MouseUp)
                {
                    draggedStartId = -1;
                    draggedEndId = -1;
                    e.Use();
                }
            }

            return ret;
        }
    }
}