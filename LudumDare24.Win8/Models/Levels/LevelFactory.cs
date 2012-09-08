using System;
using System.Collections.Generic;
using LudumDare24.Cereal.Serialization;
using LudumDare24.Models.Boards;
using LudumDare24.Models.Doodads;
using LudumDare24.Models.Sessions;
using Microsoft.Xna.Framework.Content;

namespace LudumDare24.Models.Levels
{
    public class LevelFactory
    {
        private readonly ContentManager content;
        private readonly IBoard board;
        private readonly DoodadFactory doodadFactory;
        private readonly Session session;

        public LevelFactory(ContentManager content, IBoard board, DoodadFactory doodadFactory, Session session)
        {
            this.content = content;
            this.board = board;
            this.doodadFactory = doodadFactory;
            this.session = session;
        }

        public int CurrentLevel
        {
            get { return this.session.CurrentLevel; }
        }

        public void AdvanceToNextLevel()
        {
            this.session.CurrentLevel++;
            if (this.session.CurrentLevel > 12)
            {
                this.session.CurrentLevel = 1;
            }

            this.LoadLevel();
        }

        public void LoadLevel()
        {
            this.board.Doodads.Clear();

            var doodadPlacements = this.content.Load<IEnumerable<DoodadPlacement>>("Levels/Level" + this.session.CurrentLevel);
            foreach (DoodadPlacement doodadPlacement in doodadPlacements)
            {
                Type doodadType = Type.GetType("LudumDare24.Models.Doodads." + doodadPlacement.DoodadType);
                var doodad = this.doodadFactory.CreateDoodad(doodadType, doodadPlacement.Column, doodadPlacement.Row);
                this.board.Doodads.Add(doodad);
            }
        }

    }
}