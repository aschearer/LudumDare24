using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LudumDare24.Cereal.Serialization;
using LudumDare24.Models;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using LudumDare24.Tools.Models.Analysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LudumDare24.Tools
{
    public class EvaluateBoard
    {
        private readonly ContentManager content;
        private readonly DoodadFactory doodadFactory;
        private readonly BoardPacker boardPacker;

        public EvaluateBoard()
        {
            this.content = new ContentManager(new AppServiceProvider(), Environment.CurrentDirectory);
            this.doodadFactory = new DoodadFactory();
            this.boardPacker = new BoardPacker(Constants.NumberOfColumns, Constants.NumberOfRows);
        }

        public void ProcessRecord(string boardName)
        {
            var doodadPlacements = this.content.Load<IEnumerable<DoodadPlacement>>(boardName);
            var sortedDoodadPlacements = doodadPlacements.OrderBy(placement => placement.Row).ThenBy(placement => placement.Column).ToList();
            List<MovementNode> solutions = new List<MovementNode>();
            MovementNode root = new MovementNode(null, false, false, StreamUtilities.Compress(sortedDoodadPlacements), MovementType.Start, 0, 0);
            Queue<MovementNode> nodeStack = new Queue<MovementNode>();
            Dictionary<string, bool> visitedNodes = new Dictionary<string, bool>();
            nodeStack.Enqueue(root);
            visitedNodes[root.State] = true;

            MovementNode bestSolution = null;

            while (nodeStack.Count > 0)
            {
                MovementNode currentNode = nodeStack.Dequeue();

                if (currentNode.IsWinningMove || currentNode.IsLosingMove)
                {
                    if (currentNode.IsWinningMove)
                    {
                        solutions.Add(currentNode);
                        if (bestSolution == null || bestSolution.Depth < currentNode.Depth)
                        {
                            bestSolution = currentNode;
                        }
                    }

                    continue;
                }

                if (bestSolution != null && currentNode.Depth >= bestSolution.Depth)
                {
                    continue;
                }

                this.RotateBoard(nodeStack, visitedNodes, currentNode, MovementType.Clockwise, MathHelper.PiOver2);
                this.RotateBoard(nodeStack, visitedNodes, currentNode, MovementType.CounterClockwise, -MathHelper.PiOver2);
            }

            solutions.Sort(new MovementNodeComparer());

            Console.WriteLine("{0} Solutions:", solutions.Count);
            foreach (MovementNode solution in solutions)
            {
                List<MovementType> movementTypes = new List<MovementType>();
                MovementNode ancestor = solution;
                while (ancestor != null)
                {
                    movementTypes.Add(ancestor.MovementType);
                    ancestor = ancestor.Parent;
                }

                movementTypes.Reverse();
                StringBuilder builder = new StringBuilder(string.Format("\t{0} moves: ", solution.Depth));
                builder.Append(string.Format("\t{0}", this.ToAbbreviation(movementTypes.Skip(1).FirstOrDefault())));
                var remainingMoves = movementTypes.Skip(2);
                foreach (MovementType movementType in remainingMoves)
                {
                    builder.Append(string.Format(", {0}", this.ToAbbreviation(movementType)));
                }

                Console.WriteLine(builder);
            }
        }

        private void RotateBoard(
            Queue<MovementNode> nodeStack,
            Dictionary<string, bool> visitedNodes, 
            MovementNode currentNode, 
            MovementType movementType, 
            float rotation)
        {
            var doodads = this.RestoreLevel(currentNode.State);
            this.boardPacker.Pack(doodads, currentNode.Rotation + rotation);
            foreach (IDoodad doodad in doodads)
            {
                doodad.Update(doodads);
            }

            var childNode = new MovementNode(
                currentNode,
                this.IsWinningBoard(doodads),
                this.IsLosingBoard(doodads),
                this.ToLevelDefinition(doodads),
                movementType,
                currentNode.Rotation + rotation,
                currentNode.Depth + 1);

            if (!visitedNodes.ContainsKey(childNode.State))
            {
                nodeStack.Enqueue(childNode);
                visitedNodes[childNode.State] = true;
            }
        }

        private bool IsWinningBoard(IEnumerable<IDoodad> doodads)
        {
            return ((Mouse)doodads.First(doodad => doodad is Mouse)).GotTheCheese;
        }

        private bool IsLosingBoard(IEnumerable<IDoodad> doodads)
        {
            return ((Mouse)doodads.First(doodad => doodad is Mouse)).CaughtByCat;
        }

        private IEnumerable<IDoodad> RestoreLevel(string compressedState)
        {
            var doodadPlacements = StreamUtilities.Decompress<IEnumerable<DoodadPlacement>>(compressedState);
            return (from doodadPlacement in doodadPlacements
                   select this.doodadFactory.CreateDoodad(
                       Type.GetType("LudumDare24.Models.Doodads." + doodadPlacement.DoodadType),
                       doodadPlacement.Column,
                       doodadPlacement.Row)).ToList();
        }

        private string ToLevelDefinition(IEnumerable<IDoodad> doodads)
        {
            var doodadPlacements = (from doodad in doodads
                                    select new DoodadPlacement
                                               {
                                                   Column = doodad.Column,
                                                   Row = doodad.Row,
                                                   DoodadType = doodad.GetType().Name
                                               }).OrderBy(placement => placement.Row).ThenBy(placement => placement.Column).ToList();

            return StreamUtilities.Compress(doodadPlacements);
        }

        private string ToAbbreviation(MovementType movementType)
        {
            switch (movementType)
            {
                case MovementType.Start:
                    return "S";
                case MovementType.Clockwise:
                    return "C";
                case MovementType.CounterClockwise:
                    return "CC";
                default:
                    throw new ArgumentOutOfRangeException("movementType");
            }
        }
    }
}
