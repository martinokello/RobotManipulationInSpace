using RobotManipulation.Interfaces;

namespace RobotManipulation.Models
{
    public class Robot:IMoveAbility
    {
        //vital for both Robot and Controller to know about the Plane extents, going forward.
        public Robot(Plane plane)
        {
            this._plane = plane;
        }
        private Plane _plane;

        public Location Location { get; set; }

        public OrientationPosition.Orientation Orientation { get; set; }

        public bool CanMoveForward()
        {
            switch (this.Orientation)
            {
                case OrientationPosition.Orientation.N:
                    return Location.Y < _plane.GetYStretch();
                case OrientationPosition.Orientation.E:
                    return Location.X < _plane.GetXStretch();
                case OrientationPosition.Orientation.S:
                    return Location.Y > _plane.Origin.Y;
                case OrientationPosition.Orientation.W:
                    return Location.X > _plane.Origin.X;
            }
            return false;
        }
    }
}
