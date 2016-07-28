using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace GameLauncher
{
    public class ImageHelper
    {
        public static BitmapImage GenerateThumbnail(string imagePath, int thumbnailWidth)
        {
            BitmapImage thumbnail = null;

            if (string.IsNullOrEmpty(imagePath) == false)
            {
                thumbnail = new BitmapImage();

                /* This is the important part! only load a thumbnail of the picture
                 * to save memory and time.
                 * Size of thumbnail (only set heigth or width to maintain aspect ratio) */
                thumbnail.DecodePixelWidth = thumbnailWidth;

                thumbnail.BeginInit();
                thumbnail.UriSource = new Uri(imagePath, UriKind.Relative);
                thumbnail.CacheOption = BitmapCacheOption.OnLoad;
                thumbnail.EndInit();
            }
            else
            {
                /* Small hack to make sure a game always have a thumbnail.
                 * (this may be remove later) */                
                var emptyImageIcon = Application.Current.FindResource("NoImageIcon");
                if (emptyImageIcon != null)
                {
                    thumbnail = (BitmapImage)emptyImageIcon;
                }
            }

            return thumbnail;
        }
    }
}