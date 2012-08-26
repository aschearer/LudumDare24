using System;
using System.Collections.Generic;
using LudumDare24.Cereal.Serialization;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using Microsoft.Xna.Framework.Content;

namespace LudumDare24.Models.Levels
{
    public class LevelFactory
    {
        private readonly ContentManager content;
        private readonly IBoard board;
        private readonly DoodadFactory doodadFactory;
        private int currentLevel;

        public LevelFactory(ContentManager content, IBoard board, DoodadFactory doodadFactory)
        {
            this.content = content;
            this.board = board;
            this.doodadFactory = doodadFactory;
            this.currentLevel = 1;
        }

        public int CurrentLevel
        {
            get { return this.currentLevel; }
        }

        public void AdvanceToNextLevel()
        {
            this.currentLevel++;
            if (this.currentLevel > 12)
            {
                this.currentLevel = 1;
            }

            this.LoadLevel();
        }

        public void LoadLevel()
        {
            this.board.Doodads.Clear();

            var doodadPlacements = this.content.Load<IEnumerable<DoodadPlacement>>("Levels/Level" + this.currentLevel);
            foreach (DoodadPlacement doodadPlacement in doodadPlacements)
            {
                Type doodadType = Type.GetType("LudumDare24.Models.Doodads." + doodadPlacement.DoodadType);
                var doodad = this.doodadFactory.CreateDoodad(doodadType, doodadPlacement.Column, doodadPlacement.Row);
                this.board.Doodads.Add(doodad);
            }
        }

    }
}