namespace LudumDare24.Tools.Models.Analysis
{
    public class MovementNode
    {
        public readonly MovementNode Parent;
        public readonly bool IsWinningMove;
        public readonly bool IsLosingMove;
        public readonly string State;
        public readonly MovementType MovementType;
        public readonly float Rotation;
        public readonly int Depth;

        public MovementNode(
            MovementNode parent, 
            bool isWinningMove, 
            bool isLosingMove,
            string state, 
            MovementType movementType,
            float rotation, 
            int depth)
        {
            this.Parent = parent;
            this.IsWinningMove = isWinningMove;
            this.Rotation = rotation;
            this.IsLosingMove = isLosingMove;
            this.State = state;
            this.MovementType = movementType;
            this.Depth = depth;
        }

        public MovementNode ClockwiseMove { get; set; }
        public MovementNode CounterClockwiseMove { get; set; }
    }
}