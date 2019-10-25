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
        private TextReader _inputStream;
        private TextWriter _ouputStream;

        public TextReader TextReader{
            get {return _inputStream;}
            set{ _inputStream = value;}
        }
        public TextWriter TextWriter
        {
            get { return _ouputStream; }
            set { _ouputStream = value; }
        }
        public EnvironmentSetup(RobotController controller, Plane plane, IList<Robot> robots, TextReader inputStream, TextWriter ouputStream)
        {
            _controller = controller;
            _plane = plane;
            _robots = robots;
            _inputStream = inputStream;
            _ouputStream = ouputStream;
        }

        public void Execute()
        {
            while (true)
            {
                _ouputStream.WriteLine("Enter the cordinates and Orientation Of all Robots in the format: X Y Orientation \n(note* either of the values for Orientation N or E or S or W is acceptable for the orientation.");
                var cordsAndOrientation = _inputStream.ReadLine();
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
                _ouputStream.WriteLine("Enter the control sequence as a string to control this Robot e.g. LRMMLLLMR");
                var controlSequence = Console.ReadLine();
                MoveRobotSequence(controlSequence, _controller, robot1);
                _ouputStream.WriteLine("To Add another Robot enter Y for Yes, or else N for No");
                var anotherRobot = Console.In.ReadLine();

                if (!anotherRobot.ToLower().StartsWith("y", StringComparison.OrdinalIgnoreCase)) break;
            }
            _controller.Robots = _robots.ToArray();
            _ouputStream.WriteLine("Expected Output:");
            foreach (var Robot in _controller.Robots)
            {
                _ouputStream.WriteLine("Position Of Robot: {0} {1} {2}", Robot.Location.X, Robot.Location.Y, Robot.Orientation.ToString());
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
    }
}
