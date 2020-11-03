using Juce.Utils.Singletons;
using UnityEditor;

namespace Juce.Feedbacks
{
    internal class UndoHelper : Singleton<UndoHelper>
    {
        private int currGroupId;

        public void BeginUndo(string name)
        {
            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName(name);
            currGroupId = Undo.GetCurrentGroup();
        }

        public void EndUndo()
        {
            Undo.CollapseUndoOperations(currGroupId);
        }
    }
}