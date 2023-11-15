using System;

namespace CommandPattern
{
    public interface ICommand : IDisposable
    {
        public void Execute();
        public void Undo();
        public void Redo();
    }
}
