using System.Collections.Generic;
using System.Linq;
using LudumDare24.Cereal.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using TiledLib;

namespace LudumDare24.ContentPipeline
{
    [ContentProcessor(DisplayName = "TMX Processor")]
    public class LevelProcessor : ContentProcessor<MapContent, IEnumerable<DoodadPlacement>>
    {
        public override IEnumerable<DoodadPlacement> Process(MapContent input, ContentProcessorContext context)
        {
            // build the textures
            TiledHelpers.BuildTileSetTextures(input, context);

            // generate source rectangles
            TiledHelpers.GenerateTileSourceRectangles(input);

            List<DoodadPlacement> doodadPlacements = new List<DoodadPlacement>();
            foreach (LayerContent layer in input.Layers)
            {
                if (layer is TileLayerContent && layer.Name == "Main")
                {
                    this.ProcessMainLayer((TileLayerContent)layer, input.TileSets, doodadPlacements);
                }
            }

            return doodadPlacements;
        }

        private void ProcessMainLayer(
            TileLayerContent layer,
            IEnumerable<TileSetContent> tileSets,
            List<DoodadPlacement> doodadPlacements)
        {
            for (int column = 0; column < layer.Width; column++)
            {
                for (int row = 0; row < layer.Height; row++)
                {
                    uint tileID = layer.Data[column + row * layer.Width];
                    int tileIndex;
                    SpriteEffects spriteEffects;
                    TiledHelpers.DecodeTileID(tileID, out tileIndex, out spriteEffects);

                    Tile tileDef = this.GetTile(tileIndex, tileSets);

                    if (tileDef == null)
                    {
                        continue;
                    }

                    DoodadPlacement placement = new DoodadPlacement();
                    placement.DoodadType = tileDef.Properties["Type"];
                    placement.Column = column;
                    placement.Row = row;
                    doodadPlacements.Add(placement);
                }
            }
        }

        private Tile GetTile(int gid, IEnumerable<TileSetContent> tileSets)
        {
            return (from tileSet in tileSets
                    let adjustedTileIndex = gid - tileSet.FirstId
                    where adjustedTileIndex >= 0 && adjustedTileIndex < tileSet.Tiles.Count
                    select tileSet.Tiles[adjustedTileIndex]).FirstOrDefault();
        }
    }
}