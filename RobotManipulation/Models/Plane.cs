
namespace RobotManipulation.Models
{
    public class Plane
    {
        private int _xStretch;
        private int _yStretch;
        public Location Origin;
        public Plane(int X, int Y, Location origin)
        {
            _xStretch = X;
            _yStretch = Y;
            Origin = origin;
        }

        public int GetXStretch()
        {
            return _xStretch;
        }
        public int GetYStretch()
        {
            return _yStretch;
        }
    }
}
