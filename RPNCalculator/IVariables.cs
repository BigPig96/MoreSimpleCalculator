using System.Collections.Generic;

namespace RPNCalculator
{
    public interface IVariables
    {
        bool Write(string key, string value);
        IEnumerable<string> ReadAll();
        string Read(string key);
        string Replace(string input);
    }
}