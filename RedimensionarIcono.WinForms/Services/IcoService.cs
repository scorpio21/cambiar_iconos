using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Services
{
    internal static class IcoService
    {
        // Guarda un ICO de un solo tamaño a partir de un Bitmap 32bpp con alfa
        public static void SaveSingleIco(Bitmap bmp, string path)
        {
            using var icon = Icon.FromHandle(bmp.GetHicon());
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            icon.Save(fs);
        }

        // Guarda un .ico multi-resolución componiendo entradas PNG con alfa
        public static void SaveMultiIcon(string path, int[] sizes, System.Collections.Generic.List<Bitmap> bitmaps)
        {
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            // ICONDIR
            bw.Write((ushort)0);      // reserved
            bw.Write((ushort)1);      // type = 1 (icon)
            bw.Write((ushort)sizes.Length); // count

            // Codificar PNGs en memoria
            var pngBytes = new System.Collections.Generic.List<byte[]>();
            foreach (var bmp in bitmaps)
            {
                using var ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Png);
                pngBytes.Add(ms.ToArray());
            }

            int dirSize = 6 + 16 * sizes.Length;
            int offset = dirSize;

            // ICONDIRENTRYs
            for (int i = 0; i < sizes.Length; i++)
            {
                int w = sizes[i];
                int h = sizes[i];
                var data = pngBytes[i];
                bw.Write((byte)(w == 256 ? 0 : w)); // width (0 => 256)
                bw.Write((byte)(h == 256 ? 0 : h)); // height
                bw.Write((byte)0);                  // color count
                bw.Write((byte)0);                  // reserved
                bw.Write((ushort)1);                // planes
                bw.Write((ushort)32);               // bit count
                bw.Write((uint)data.Length);        // bytes in res
                bw.Write((uint)offset);             // image offset
                offset += data.Length;
            }

            // Escribir los PNG concatenados
            foreach (var data in pngBytes)
            {
                bw.Write(data);
            }
        }
    }
}
