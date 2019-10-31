using RobotManipulation.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotManipulation.Concretes
{
    public class StreamReadWriteInstance : IInputOutputStream
    {
        private IInputOutputStream _readerWriter;
        private TextReader _reader;
        private TextWriter _writer;
        public TextReader TextReader { get { return _reader; }
            set { _reader = value; }
        }
        public TextWriter TextWriter
        {
            get { return _writer; }
            set { _writer = value; }
        }
        public StreamReadWriteInstance()
        {

        }
        public StreamReadWriteInstance(IInputOutputStream readerWriter, TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
            _readerWriter = readerWriter;
        }

        public string ReadLine()
        {
            return _readerWriter.ReadLine();
        }

        public void WriteLine(string text)
        {
            _readerWriter.WriteLine(text);
        }
    }
}
