using System;

namespace Common.UI
{
    public interface IButton
    {
        void Subscribe();
        void Unsubscribe();
        void Init(Action handler);
    }
}