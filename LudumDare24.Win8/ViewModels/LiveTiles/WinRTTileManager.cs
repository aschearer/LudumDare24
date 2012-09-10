using System;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace LudumDare24.Win8.ViewModels.LiveTiles
{
    public class WinRTTileManager
    {
        public void UpdateTile(int levelNumber, int numberOfTurns)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWidePeekImage01);
            var image = tileXml.GetElementsByTagName("image").First();
            image.Attributes.GetNamedItem("src").InnerText = "ms-appx:/Assets/WideLogo.png";

            var text = tileXml.GetElementsByTagName("text");
            text.First().InnerText = string.Format("Cleared Level {0}", levelNumber);

            string turnFormat = numberOfTurns > 1 ? "{0} Turns" : "{0} Turn";
            text.Skip(1).First().InnerText = string.Format(turnFormat, numberOfTurns);

            TileNotification tileNotification = new TileNotification(tileXml);
            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddDays(7);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        public void ResetTile()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }
    }
}