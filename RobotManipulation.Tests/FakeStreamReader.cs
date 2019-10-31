using System.IO;

namespace RobotManipulation.Tests
{
    internal class FakeStreamReader : TextReader
    {
        private string _lineToRead;
        public FakeStreamReader()
        {

        }
        public FakeStreamReader(string lineToRead)
        {
            _lineToRead = lineToRead;
        }
        public override string ReadLine()
        {
            return _lineToRead;
        }
    }
}