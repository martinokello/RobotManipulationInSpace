using RobotManipulation.Interfaces;
using RobotManipulation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RobotManipulation.Concretes
{
    public class EnvironmentSetup : ICommand
    {
        private RobotController _controller;
        private Plane _plane;
        private IList<Robot> _robots;
        private TextReader _reader;
        private TextWriter _writer;
        IInputOutputStream _streamInstance;



        TextReader TextReader
        {
            get { return _streamInstance.TextReader; }
            set { _streamInstance.TextReader = value; }
        }
        TextWriter TextWriter
        {
            get { return _streamInstance.TextWriter; }
            set { _streamInstance.TextWriter = value; }
        }

        public EnvironmentSetup(RobotController controller, Plane plane, IList<Robot> robots, IInputOutputStream streamInstance)
        {
            _controller = controller;
            _plane = plane;
            _robots = robots;
            _streamInstance = streamInstance;
        }

        public void Execute()
        {
            while (true)
            {
                _streamInstance.TextWriter.WriteLine("Enter the cordinates and Orientation Of all Robots in the format: X Y Orientation \n(note* either of the values for Orientation N or E or S or W is acceptable for the orientation.");
                var cordsAndOrientation = _streamInstance.TextReader.ReadLine();
                var RobotLocations = cordsAndOrientation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (RobotLocations.Length != 3) throw new ArgumentException("Robot Locations should be valid");
                var x = -1;
                var y = -1;
                Int32.TryParse(RobotLocations[0], out x);
                Int32.TryParse(RobotLocations[1], out y);

                var orientation = RobotLocations[2].Substring(0, 1).ToUpper();
                var robot1 = new Robot(_plane);
                OrientationPosition.Orientation actualOrientation = OrientationPosition.Orientation.N;
                Enum.TryParse(orientation, out actualOrientation);
                robot1.Orientation = actualOrientation;
                robot1.Location = new Location { X = x, Y = y };
                _robots.Add(robot1);
                _streamInstance.TextWriter.WriteLine("Enter the control sequence as a string to control this Robot e.g. LRMMLLLMR");
                var controlSequence = _streamInstance.TextReader.ReadLine();
                MoveRobotSequence(controlSequence, _controller, robot1);
                _streamInstance.TextWriter.WriteLine("To Add another Robot enter Y for Yes, or else N for No");
                var anotherRobot = _streamInstance.TextReader.ReadLine();

                if (!anotherRobot.ToLower().StartsWith("y", StringComparison.OrdinalIgnoreCase)) break;
            }
            _controller.Robots = _robots.ToArray();
            _streamInstance.TextWriter.WriteLine("Expected Output:");
            foreach (var Robot in _controller.Robots)
            {
                _streamInstance.TextWriter.WriteLine($"Position Of Robot: {Robot.Location.X} { Robot.Location.Y} {Robot.Orientation.ToString()}");
            }
        }
        private void MoveRobotSequence(string controlSequence, RobotController controller, Robot robot1)
        {
            controlSequence = controlSequence.ToUpper();
            foreach (var movement in controlSequence)
            {
                if (movement.Equals('L'))
                {
                    controller.TurnLeft(robot1);
                }
                else if (movement.Equals('R'))
                {
                    controller.TurnRight(robot1);
                }
                else if (movement.Equals('M'))
                {
                    controller.MoveForward(robot1);
                }
            }
        }
        
        public void WriteLine(string text)
        {
            _streamInstance.TextWriter.WriteLine(text);
        }

        public string ReadLine()
        {
            return _streamInstance.TextReader.ReadLine();
        }
    }
}
