using System;
using System.IO;
using System.Text;

namespace RobotManipulation.Tests
{
    public class FakeStreamWriter : TextWriter
    {
        public FakeStreamWriter() : base()
        {
        }

        public override Encoding Encoding
        {
            get { return System.Text.ASCIIEncoding.ASCII; }
        }

        public override void WriteLine(string line)
        {
            base.WriteLine(line);
        }
    }
}