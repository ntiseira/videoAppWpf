using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStudioApp.Helper
{
   public static class WaterMark
    {
         /// <summary>
 /// This method will create a bitmap based 
 /// </summary>
 /// <param name="overlayText"></param>
 /// <param name="rootPath"></param>
 /// <param name="width"></param>
  /// <param name="height"></param>
  /// <returns></returns>
 public static string createOverlayImage(string overlayText, string rootPath, int width, int height)
 {
     // full path for a temporary bitmap
     string overlayFileName = rootPath + "\\" + Guid.NewGuid().ToString() + ".bmp";
   
     // create a bitmap
     Bitmap bitmap = new Bitmap(width, height);
     Graphics g = Graphics.FromImage(bitmap);
   
     // define the font
     Font font = new Font("Arial", (float)14.0);
   
     // define the area to draw on
     Rectangle area = new Rectangle(new Point(0, 0), new Size(width, height));
   
// draw the new image
g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
g.DrawString(overlayText, font, Brushes.Red, area);
     // save the picture with the text overlay
     bitmap.Save(overlayFileName);
// return the path to the overlay image
return overlayFileName;
 }


    }
}
