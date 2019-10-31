using System.IO;

namespace RobotManipulation.Interfaces
{
    public interface IInputOutputStream
    {
        string ReadLine();
        void WriteLine(string text);
        TextReader TextReader { get; set; }
        TextWriter TextWriter { get; set; }
    }
}