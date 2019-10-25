using RobotManipulation.Interfaces;
using RobotManipulation.Models;

namespace RobotManipulation.Concretes
{
    public class RobotController : IMovement
    {
        //vital for both Robot and Controller to know about the Plane extents, going forward.
        public RobotController(Plane plane)
        {
            this._plane = plane;
        }
        private Plane _plane;
        public Robot[] Robots { get; set; }

        public void MoveForward(Robot Robot)
        {
            if (Robot.CanMoveForward())
            {
                ConfigureMoveForward(Robot);
            }
        }



        public bool TurnLeft(Robot Robot)
        {
            ConfigureTurnLefOrientation(Robot);
            return true;
        }
        public bool TurnRight(Robot Robot)
        {
            ConfigureTurnRightOrientation(Robot);
            return true;
        }

        private void ConfigureTurnLefOrientation(Robot Robot)
        {
            switch (Robot.Orientation)
            {
                case OrientationPosition.Orientation.N:
                    Robot.Orientation = OrientationPosition.Orientation.W;
                    break;
                case OrientationPosition.Orientation.E:
                    Robot.Orientation = OrientationPosition.Orientation.N;
                    break;
                case OrientationPosition.Orientation.S:
                    Robot.Orientation = OrientationPosition.Orientation.E;
                    break;
                case OrientationPosition.Orientation.W:
                    Robot.Orientation = OrientationPosition.Orientation.S;
                    break;
            }
        }
        private void ConfigureTurnRightOrientation(Robot Robot)
        {
            switch (Robot.Orientation)
            {
                case OrientationPosition.Orientation.N:
                    Robot.Orientation = OrientationPosition.Orientation.E;
                    break;
                case OrientationPosition.Orientation.E:
                    Robot.Orientation = OrientationPosition.Orientation.S;
                    break;
                case OrientationPosition.Orientation.S:
                    Robot.Orientation = OrientationPosition.Orientation.W;
                    break;
                case OrientationPosition.Orientation.W:
                    Robot.Orientation = OrientationPosition.Orientation.N;
                    break;
            }
        }
        private void ConfigureMoveForward(Robot Robot)
        {
            switch (Robot.Orientation)
            {
                case OrientationPosition.Orientation.N:
                    Robot.Location.Y += 1;
                    break;
                case OrientationPosition.Orientation.E:
                    Robot.Location.X += 1;
                    break;
                case OrientationPosition.Orientation.S:
                    Robot.Location.Y -= 1;
                    break;
                case OrientationPosition.Orientation.W:
                    Robot.Location.X -= 1;
                    break;
            }
        }
    }
}
