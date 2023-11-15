using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public static class CommandManager
    {
        private static Stack<BaseCommand> _commandBuffer = new Stack<BaseCommand>();
        private static Stack<BaseCommand> _commandBuffer2 = new Stack<BaseCommand>();

        public static void AddCommand(BaseCommand command)
        {
            command.Execute();
            _commandBuffer.Push(command);
        }

        public static void Undo()
        {
            if (_commandBuffer.Count == 0)
                return;

            BaseCommand command = _commandBuffer.Pop();
            command.Undo();
            _commandBuffer2.Push(command);
        }

        public static void Redo()
        {
            if (_commandBuffer2.Count == 0)
                return;

            BaseCommand command = _commandBuffer2.Pop();
            command.Redo();
            _commandBuffer.Push(command);
        }

        public static void Dispose()
        {
            foreach (BaseCommand command in _commandBuffer)
            {
                command.Dispose();
            }

            foreach (BaseCommand command in _commandBuffer2)
            {
                command.Dispose();
            }

            _commandBuffer.Clear();
            _commandBuffer2.Clear();
        }
    }
}
