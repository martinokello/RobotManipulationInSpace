using System.IO;

namespace RobotManipulation.Tests
{
    public class FakeStreamReader : TextReader
    {
        private string _lineToRead;
        public FakeStreamReader():base()
        {

        }
        public FakeStreamReader(string lineToRead):base()
        {
            _lineToRead = lineToRead;
        }
        public override string ReadLine()
        {
            return base.ReadLine();
        }
    }
}