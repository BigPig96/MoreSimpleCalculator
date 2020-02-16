namespace RPNCalculator
{
    public static class Messages
    {
        public const string Welcome = "Welcome to Calculator! \\help для справки\n";
        public const string Calculate = "Введите арифметическую операцию и нажмите Enter";
        public const string VariablesExample = "Пример использования сохраненных переменных : 515 + {pi} * 5 - {x}";
        public const string EnterKey = "Введите ключ и нажмите Enter";
        public const string EnterValue = "Введите значение и нажмите Enter";
        public const string EmptyVariables = @"У вас нет сохраненных переменных! Чтобы добавить используйте команду \addVariable";
        public const string VariableSaved = "Переменная сохранена успешно";
        public const string VariableNotSaved = "Ошибка сохранения!";
        public const string UnknownCommand = @"Неизвестная команда \help для справки";
    }
}