using System;

namespace RPNCalculator
{
    public class Command
    {
        public readonly string Name;
        public readonly string Description;
        private readonly Action _work;

        public Command(string name, string description, Action work)
        {
            Name = name;
            Description = description;
            _work = work;
        }

        public void Invoke()
        {
            _work?.Invoke();
        }
    }
}