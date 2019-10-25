using RobotManipulation.Concretes;
using RobotManipulation.Models;
using System;
using System.Collections.Generic;

namespace RobotManipulation
{
    class Program
    {
        //Assumptions: Robots are entered dynamically during run time.
        //Otherwise I would need to read all input from a File.
        //This is a decision I made as there is no mention of how the input would stream into the program, so I can
        //assume it is coming from the keyboard dynamically. That is at Runtime.
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Enter the X and Y cordinates of the Plane");
            var xyCord = Console.In.ReadLine();
            var xyStrings = xyCord.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (xyStrings.Length != 2) throw new ArgumentException("You need to enter 2 integers");

            var x = 0;
            var y = 0;

            Int32.TryParse(xyStrings[0], out x);
            Int32.TryParse(xyStrings[1], out y);

            if (x < 1 || y < 1) throw new ArgumentException("Plane should have positive greater than 0 values");


            var plane = new Plane(x, y, new Location { X = 0, Y = 0 });

            var controller = new RobotController(plane);

            var robots = new List<Robot>();
            var environmentSetup = new EnvironmentSetup(controller, plane, robots, Console.In, Console.Out);
            environmentSetup.Execute();
        }


    }
}
