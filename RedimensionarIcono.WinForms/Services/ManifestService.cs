using System.IO;

namespace RedimensionarIcono.WinForms.Services
{
    internal static class ManifestService
    {
        // Genera el bloque de icons del manifest a partir del nombre base
        public static string GenerateIconsBlock(string baseName)
        {
            using var sw = new StringWriter();
            sw.WriteLine("\"icons\": [");
            var icons = new (int size, string purpose)[]
            {
                (192, "any maskable"),
                (512, "any maskable")
            };
            for (int i = 0; i < icons.Length; i++)
            {
                var ic = icons[i];
                sw.Write($"  {{ \"src\": \"img/{baseName}-{ic.size}x{ic.size}.png\", \"sizes\": \"{ic.size}x{ic.size}\", \"type\": \"image/png\", \"purpose\": \"{ic.purpose}\" }}");
                sw.WriteLine(i < icons.Length - 1 ? "," : string.Empty);
            }
            sw.Write("]");
            return sw.ToString();
        }
    }
}
