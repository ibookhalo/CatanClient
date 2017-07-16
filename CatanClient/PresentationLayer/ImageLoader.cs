using Catan.Client.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    static class ImageLoader
    {
        private static List<KeyValuePair<Color, Image>> cachedSiedlungen = new List<KeyValuePair<Color, Image>>();
        public static Image GetImageByLandfeldTyp(Catan.Game.Hexagon.LandfeldTyp landfeldTyp)
        {
            switch (landfeldTyp)
            {
                case Catan.Game.Hexagon.LandfeldTyp.Weideland:
                    return Resources.BackgroundHexagonWeideland;
                case Catan.Game.Hexagon.LandfeldTyp.Ackerland:
                    return Resources.BackgroundHexagonAckerland;
                case Catan.Game.Hexagon.LandfeldTyp.BergwerkGold:
                    return Resources.BackgroundHexagonBerkwerkGold;
                case Catan.Game.Hexagon.LandfeldTyp.Eisenmine:
                    return Resources.BackgroundHexagonEisenmine;
                case Catan.Game.Hexagon.LandfeldTyp.MeersFeld:
                    return Resources.BackgroundHexagonMeeresfeld;
                case Catan.Game.Hexagon.LandfeldTyp.Wohnstaette:
                    return Resources.BackgroundHexagonWohnstaette;
                default:
                    throw new NotImplementedException();
            }
        }

        public static Image GetSiedlungImage(Color color,int height, int width)
        {
            Image siedlungImage;
            if ((siedlungImage=cachedSiedlungen.Find(siedlung=>siedlung.Key.Equals(color)).Value)!=null)
            { 
                return siedlungImage;
            }
            siedlungImage=ImageHelper.ResizeImageAndTransparent(ImageHelper.ReplaceColor(Resources.HouseGray,Color.FromArgb(195,195,195), color),Color.White, width, height);
            cachedSiedlungen.Add(new KeyValuePair<Color, Image>(color, siedlungImage));
            return siedlungImage;
        }

    }
}
