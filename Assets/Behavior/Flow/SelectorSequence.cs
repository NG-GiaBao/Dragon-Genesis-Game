using System;
using Unity.Properties;

namespace Unity.Behavior
{
    /// <summary>
    /// Executes branches in order until one succeeds or all fail.
    /// </summary>
    [Serializable, GeneratePropertyBag]
    [NodeDescription(
        name: "Selector",
        description: "Executes branches in order until one succeeds or all fail.",
        icon: "Icons/Selector",
        category: "Flow",
        id: "e7f1a2c9b3d4445f9a7e2c6d8b4f1234")]
    internal partial class SelectorComposite : Composite
    {
        [CreateProperty] private int m_CurrentChild;

        protected override Status OnStart()
        {
            m_CurrentChild = 0;
            // Start the first child
            return StartChild(m_CurrentChild);
        }

        protected override Status OnUpdate()
        {
            var currentNode = Children[m_CurrentChild];
            Status childStatus = currentNode.CurrentStatus;

            if (childStatus == Status.Failure)
            {
                // Try next child
                return StartChild(++m_CurrentChild);
            }

            // If child is running, we wait
            if (childStatus == Status.Running)
            {
                return Status.Waiting;
            }

            // If child succeeded, selector succeeds
            if (childStatus == Status.Success)
            {
                return Status.Success;
            }

            // Propagate other statuses
            return childStatus;
        }

        private Status StartChild(int childIndex)
        {
            // V�ng l?p qua t?ng child b?t ??u t? childIndex
            while (childIndex < Children.Count)
            {
                var status = StartNode(Children[childIndex]);
                switch (status)
                {
                    case Status.Running:
                        return Status.Waiting;   // child ?ang ch?y
                    case Status.Success:
                        return Status.Success;   // child th�nh c�ng ? Selector th�nh c�ng
                    case Status.Failure:
                        // child fail t?c th� ? th? child ti?p theo
                        childIndex++;
                        continue;
                    default:
                        return status;          // propagate c�c tr?ng th�i kh�c
                }
            }
            // N?u h?t child m� kh�ng th�nh c�ng
            return Status.Failure;
        }
    }
}
