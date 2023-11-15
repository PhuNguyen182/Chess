using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public abstract class BaseCommand : ICommand
    {
        public abstract void Execute();

        public abstract void Undo();

        public abstract void Redo();

        public abstract void Dispose();
    }
}
