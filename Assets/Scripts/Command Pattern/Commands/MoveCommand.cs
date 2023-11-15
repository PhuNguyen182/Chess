using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class MoveCommand : BaseCommand
    {
        private readonly Transform _moveableInstance;
        private readonly Vector3 _startPosition;
        private readonly Vector3 _endPosition;

        public MoveCommand(Transform moveableInstance, Vector3 startPosition, Vector3 endPosition)
        {
            _moveableInstance = moveableInstance;
            _startPosition = startPosition;
            _endPosition = endPosition;
        }

        public override void Dispose()
        {
            
        }

        public override void Execute()
        {
            _moveableInstance.position = _endPosition;
        }

        public override void Redo()
        {
            _moveableInstance.position = _endPosition;
        }

        public override void Undo()
        {
            _moveableInstance.position = _endPosition;
        }
    }
}
