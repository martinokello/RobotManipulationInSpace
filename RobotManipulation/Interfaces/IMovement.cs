using RobotManipulation.Models;

namespace RobotManipulation.Interfaces
{
    public interface IMovement
    {
        void MoveForward(Robot Robot);

        bool TurnRight(Robot Robot);

        bool TurnLeft(Robot Robot);
    }
}
