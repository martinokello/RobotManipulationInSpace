using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RobotManipulation.Concretes;
using RobotManipulation.Interfaces;
using RobotManipulation.Models;
using System.Collections.Generic;
using System.IO;
using Plane = RobotManipulation.Models.Plane;

namespace RobotManipulation.Tests
{
    [TestClass]
    public class RobotControllerTest
    {
        private RobotController _controller;
        private TextReader _inputStream = new FakeStreamReader(string.Empty);
        private TextWriter _ouputStream = new FakeStreamWriter();
        private EnvironmentSetup _environment;
        private StreamReadWriteInstance _streamInstance;

        [TestInitialize]
        public void SetUp()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });

            var robot1 = new Robot(plane);
            robot1.Location = new Location { X = 1, Y = 2 };
            robot1.Orientation = OrientationPosition.Orientation.N;

            var robot2 = new Robot(plane);
            robot2.Location = new Location { X = 3, Y = 3 };
            robot2.Orientation = OrientationPosition.Orientation.E;

            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 1, Y = 2 };
            robot3.Orientation = OrientationPosition.Orientation.W;

            var robot4 = new Robot(plane);
            robot4.Location = new Location { X = 5, Y = 3 };
            robot4.Orientation = OrientationPosition.Orientation.S;
            _controller = new RobotController(plane);

            var Robots = new List<Robot>();
            Robots.Add(robot1);
            Robots.Add(robot2);
            Robots.Add(robot3);
            Robots.Add(robot4);

            _controller.Robots = Robots.ToArray();
            _streamInstance = new StreamReadWriteInstance();
            _streamInstance.TextReader = new FakeStreamReader();
            _streamInstance.TextWriter = new FakeStreamWriter();
            _environment = new EnvironmentSetup(_controller, plane, Robots,_streamInstance);
        }

        [TestMethod]
        public void Test_CanMoveForward()
        {
            foreach(var Robot in _controller.Robots)
            {
                Assert.IsTrue(Robot.CanMoveForward());
            }
        }

        [TestMethod]
        public void Test_MoveForward_And_New_Positions_Are_Correct()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot1 = new Robot(plane);
            robot1.Location = new Location { X = 1, Y = 2 };
            robot1.Orientation = OrientationPosition.Orientation.N;
            _controller.MoveForward(robot1);
            Assert.AreEqual(robot1.Orientation, OrientationPosition.Orientation.N);
            Assert.AreEqual(robot1.Location.X, 1);
            Assert.AreEqual(robot1.Location.Y, 3);

            var robot2 = new Robot(plane);
            robot2.Location = new Location { X = 3, Y = 3 };
            robot2.Orientation = OrientationPosition.Orientation.E;
            _controller.MoveForward(robot2);
            Assert.AreEqual(robot2.Orientation, OrientationPosition.Orientation.E);
            Assert.AreEqual(robot2.Location.X, 4);
            Assert.AreEqual(robot2.Location.Y, 3);

            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 1, Y = 2 };
            robot3.Orientation = OrientationPosition.Orientation.W;
            _controller.MoveForward(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.W);
            Assert.AreEqual(robot3.Location.X, 0);
            Assert.AreEqual(robot3.Location.Y, 2);

            var robot4 = new Robot(plane);
            robot4.Location = new Location { X = 5, Y = 3 };
            robot4.Orientation = OrientationPosition.Orientation.S;
            _controller.MoveForward(robot4);
            Assert.AreEqual(robot4.Orientation, OrientationPosition.Orientation.S);
            Assert.AreEqual(robot4.Location.X, 5);
            Assert.AreEqual(robot4.Location.Y, 2);
        }
        [TestMethod]
        public void Test_Not_CanMoveForward_When_At_Max_East_Position_And_Orientation_Is_East()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 10, Y = 3 };
            robot3.Orientation = OrientationPosition.Orientation.E;
            _controller.MoveForward(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.E);
            Assert.AreEqual(robot3.Location.X, 10);
            Assert.AreEqual(robot3.Location.Y, 3);
        }
        [TestMethod]
        public void Test_Not_CanMoveForward_When_At_Min_West_Position_And_Orientation_Is_West()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 0, Y = 3 };
            robot3.Orientation = OrientationPosition.Orientation.W;
            _controller.MoveForward(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.W);
            Assert.AreEqual(robot3.Location.X, 0);
            Assert.AreEqual(robot3.Location.Y, 3);
        }

        [TestMethod]
        public void Test_Not_CanMoveForward_When_At_Max_North_Position_And_Orientation_Is_North()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 4, Y = 10 };
            robot3.Orientation = OrientationPosition.Orientation.N;
            _controller.MoveForward(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.N);
            Assert.AreEqual(robot3.Location.X, 4);
            Assert.AreEqual(robot3.Location.Y, 10);
        }
        [TestMethod]
        public void Test_Not_CanMoveForward_When_At_Min_South_Position_And_Orientation_Is_South()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 4, Y = 0 };
            robot3.Orientation = OrientationPosition.Orientation.S;
            _controller.MoveForward(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.S);
            Assert.AreEqual(robot3.Location.X, 4);
            Assert.AreEqual(robot3.Location.Y, 0);
        }

        [TestMethod]
        public void Test_Turn_Right_When_At_Min_South_Position_And_Orientation_Is_South()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 4, Y = 0 };
            robot3.Orientation = OrientationPosition.Orientation.S;
            _controller.TurnRight(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.W);
            Assert.AreEqual(robot3.Location.X, 4);
            Assert.AreEqual(robot3.Location.Y, 0);
        }
        [TestMethod]
        public void Test_Turn_Left_When_At_Min_South_Position_And_Orientation_Is_South()
        {
            var plane = new Plane(10, 10, new Location { X = 0, Y = 0 });
            var robot3 = new Robot(plane);
            robot3.Location = new Location { X = 4, Y = 0 };
            robot3.Orientation = OrientationPosition.Orientation.S;
            _controller.TurnLeft(robot3);
            Assert.AreEqual(robot3.Orientation, OrientationPosition.Orientation.E);
            Assert.AreEqual(robot3.Location.X, 4);
            Assert.AreEqual(robot3.Location.Y, 0);
        }

        [TestMethod]
        public void Test_Execute_Method_ON_Environment_With_Input_X2Y4OrientationEastAndControlInputSequenceMMRMMRMRRM()
        {
            //This Test Can't be Executed due to the fact that Streams are multi-read, and can't be faked easily unless
            //the dynamic execution, is counting number of reads which could be possible, but not reliable, and therefore too complex to test.
            /*Entry For Robot 1:
            10 10
            1 2 N
            MMRMMRMRRM
            */
            var plane = new Plane(7, 6, new Location { X = 0, Y = 0 });
            var robot1 = new Robot(plane);
            robot1.Location = new Location { X = 2, Y = 4 };
            robot1.Orientation = OrientationPosition.Orientation.E;
            var controller = new RobotController(plane);

            var robots = new List<Robot> { robot1 };
            var inputOutputInstance = new StreamReadWriteInstance();
            inputOutputInstance.TextReader = new FakeStreamReader();
            inputOutputInstance.TextWriter = new FakeStreamWriter();

            var environmentSetup = new Mock<EnvironmentSetup>();
            /*environmentSetup.Setup(mq => mq.Robots).Returns(robots);
            environmentSetup.Setup(mq => mq.Controller).Returns(controller);
            environmentSetup.Setup(mq => mq.Plane).Returns(plane);*/
            environmentSetup.SetupProperty(mq => mq.Robots, robots);
            environmentSetup.SetupProperty(mq => mq.Controller, controller);
            environmentSetup.SetupProperty(mq => mq.Plane, plane);
            environmentSetup.SetupProperty(mq => mq.IInputOutputStream, inputOutputInstance);
            environmentSetup.Setup(mq => mq.IInputOutputStream).Returns(inputOutputInstance);
            environmentSetup.Setup(mq => mq.ReadPlaneCordinates()).Returns("7 6");
            environmentSetup.Setup(mq => mq.ReadControlSequence()).Returns("MMRMMRMRRM");
            environmentSetup.Setup(mq => mq.Write1stLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.Write2ndLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.Write3rdLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.Write4thLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.MoveRobotSequence(It.IsAny<string>(), It.IsAny<RobotController>(), It.IsAny<Robot>())).CallBase();

            environmentSetup.Object.MoveRobotSequence("MMRMMRMRRM", controller, robot1);

            Assert.AreEqual(robot1.Location.X, 4);
            Assert.AreEqual(robot1.Location.Y, 2);
            Assert.AreEqual(robot1.Orientation, OrientationPosition.Orientation.E);
        }
        [TestMethod]
        public void Test_Execute_Method_ON_Environment_With_Input_X3Y4OrientationNorthAndControlInputSequenceLMLMLMLMM()
        {
            //This Test Can't be Executed due to the fact that Streams are multi-read, and can't be faked easily unless
            //the dynamic execution, is counting number of reads which could be possible, but not reliable, and therefore too complex to test.
            /*Entry For Robot 1:
            10 10
            1 2 N
            MMRMMRMRRM
            */
            var plane = new Plane(10,10, new Location { X = 0, Y = 0 });
            var robot1 = new Robot(plane);
            robot1.Location = new Location { X = 3, Y = 4 };
            robot1.Orientation = OrientationPosition.Orientation.N;
            var controller = new RobotController(plane);

            var robots = new List<Robot> { robot1 };
            var inputOutputInstance = new StreamReadWriteInstance();
            inputOutputInstance.TextReader = new FakeStreamReader();
            inputOutputInstance.TextWriter = new FakeStreamWriter();

            var environmentSetup = new Mock<EnvironmentSetup>();
            /*environmentSetup.Setup(mq => mq.Robots).Returns(robots);
            environmentSetup.Setup(mq => mq.Controller).Returns(controller);
            environmentSetup.Setup(mq => mq.Plane).Returns(plane);*/
            environmentSetup.SetupProperty(mq => mq.Robots, robots);
            environmentSetup.SetupProperty(mq => mq.Controller, controller);
            environmentSetup.SetupProperty(mq => mq.Plane, plane);
            environmentSetup.SetupProperty(mq => mq.IInputOutputStream, inputOutputInstance);
            environmentSetup.Setup(mq => mq.IInputOutputStream).Returns(inputOutputInstance);
            environmentSetup.Setup(mq => mq.ReadPlaneCordinates()).Returns("10 10");
            environmentSetup.Setup(mq => mq.ReadControlSequence()).Returns("LMLMLMLMM");
            environmentSetup.Setup(mq => mq.Write1stLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.Write2ndLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.Write3rdLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.Write4thLineOfOutput()).CallBase();
            environmentSetup.Setup(mq => mq.MoveRobotSequence(It.IsAny<string>(), It.IsAny<RobotController>(), It.IsAny<Robot>())).CallBase();

            environmentSetup.Object.MoveRobotSequence("LMLMLMLMM", controller, robot1);

            Assert.AreEqual(robot1.Location.X, 3);
            Assert.AreEqual(robot1.Location.Y, 5);
            Assert.AreEqual(robot1.Orientation, OrientationPosition.Orientation.N);
        }
    }
}
