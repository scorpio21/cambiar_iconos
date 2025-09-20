using System.Drawing;
using System.Drawing.Imaging;

namespace RedimensionarIcono.WinForms.Services
{
    internal static class ImageService
    {
        public static Bitmap Redimensionar(Bitmap original, int w, int h, Color? bg)
        {
            var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            if (bg.HasValue)
            {
                using var brush = new SolidBrush(Color.FromArgb(204, bg.Value));
                g.FillRectangle(brush, 0, 0, w, h);
            }
            float ar = (float)original.Width / original.Height;
            float drawW, drawH, offX = 0, offY = 0;
            if (ar > 1)
            {
                drawW = w; drawH = w / ar; offY = (h - drawH) / 2f;
            }
            else
            {
                drawH = h; drawW = h * ar; offX = (w - drawW) / 2f;
            }
            var dest = new RectangleF(offX, offY, drawW, drawH);
            g.DrawImage(original, dest);
            return bmp;
        }
    }
}
