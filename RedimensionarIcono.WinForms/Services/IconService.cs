using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Services
{
    internal static class IconService
    {
        private sealed class AppConfig
        {
            public string IconPath { get; set; } = string.Empty;
            public int IconIndex { get; set; }
            public string? SavedIconIco { get; set; }
            public string? SavedIconPng { get; set; }
            public int[]? LastSizesZip { get; set; }
            public int[]? LastSizesRes { get; set; }
        }

        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern bool PickIconDlg(IntPtr hwnd, StringBuilder pszIconPath, int cchIconPath, ref int piIconIndex);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern uint ExtractIconEx(string lpszFile, int nIconIndex, IntPtr[]? phiconLarge, IntPtr[]? phiconSmall, uint nIcons);

        public static void LoadAndApplySavedIcon(Form form, PictureBox pbMobile)
        {
            try
        	{
                var cfg = Load();
                if (cfg != null)
                {
                    if (!string.IsNullOrWhiteSpace(cfg.SavedIconIco) && File.Exists(cfg.SavedIconIco))
                    {
                        using var ico = new Icon(cfg.SavedIconIco);
                        form.Icon = (Icon)ico.Clone();
                    }
                    else if (File.Exists(cfg.IconPath))
                    {
                        using var ico = ExtractIcon(cfg.IconPath, cfg.IconIndex, large: true);
                        if (ico != null)
                        {
                            form.Icon = (Icon)ico.Clone();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(cfg.SavedIconPng) && File.Exists(cfg.SavedIconPng))
                    {
                        pbMobile.Image = Image.FromFile(cfg.SavedIconPng);
                    }
                }
            }
            catch { /* no bloquear carga */ }
        }

        public static void ChooseAndSaveIcon(Form form, PictureBox pbMobile)
        {
            string startPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
            int iconIndex = 0;
            var cfg = Load();
            if (cfg != null && !string.IsNullOrWhiteSpace(cfg.IconPath))
            {
                startPath = cfg.IconPath;
                iconIndex = cfg.IconIndex;
            }

            var sb = new StringBuilder(startPath, 260);
            if (!File.Exists(startPath)) sb = new StringBuilder(260);

            bool ok;
            try
            {
                ok = PickIconDlg(form.Handle, sb, sb.Capacity, ref iconIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(form, "No se pudo abrir el selector de iconos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ok) return;

            var chosenPath = sb.ToString();
            try
            {
                using var ico = ExtractIcon(chosenPath, iconIndex, large: true);
                string? savedIco = null;
                string? savedPng = null;
                if (ico != null)
                {
                    form.Icon = (Icon)ico.Clone();
                    var bmp = ico.ToBitmap();
                    pbMobile.Image = bmp;

                    var baseName = Path.GetFileNameWithoutExtension(chosenPath);
                    var icoOut = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{baseName}_{iconIndex}.ico");
                    var pngOut = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{baseName}_{iconIndex}.png");
                    using (var fs = new FileStream(icoOut, FileMode.Create, FileAccess.Write))
                    {
                        ico.Save(fs);
                    }
                    bmp.Save(pngOut, System.Drawing.Imaging.ImageFormat.Png);
                    savedIco = icoOut;
                    savedPng = pngOut;
                }

                Save(new AppConfig { IconPath = chosenPath, IconIndex = iconIndex, SavedIconIco = savedIco, SavedIconPng = savedPng });
                MessageBox.Show(form, "Icono guardado en config.json", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(form, "No se pudo guardar la configuración: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sugerencias de tamaños persistentes para exportaciones
        public static int[] GetSuggestedSizesForZip()
        {
            var cfg = Load();
            return cfg?.LastSizesZip != null && cfg.LastSizesZip.Length > 0
                ? cfg.LastSizesZip.Distinct().OrderBy(x => x).ToArray()
                : new[] { 16, 20, 24, 32, 48, 64, 96, 128, 180, 192, 256, 512 };
        }

        public static int[] GetSuggestedSizesForRes()
        {
            var cfg = Load();
            return cfg?.LastSizesRes != null && cfg.LastSizesRes.Length > 0
                ? cfg.LastSizesRes.Distinct().OrderBy(x => x).ToArray()
                : new[] { 16, 32, 48, 64, 128, 256 };
        }

        public static void SaveLastSizesForZip(int[] sizes)
        {
            var cfg = Load() ?? new AppConfig();
            cfg.LastSizesZip = sizes?.Distinct().OrderBy(x => x).ToArray();
            Save(cfg);
        }

        public static void SaveLastSizesForRes(int[] sizes)
        {
            var cfg = Load() ?? new AppConfig();
            cfg.LastSizesRes = sizes?.Distinct().OrderBy(x => x).ToArray();
            Save(cfg);
        }

        private static AppConfig? Load()
        {
            if (!File.Exists(ConfigPath)) return null;
            try
            {
                var json = File.ReadAllText(ConfigPath, Encoding.UTF8);
                return JsonSerializer.Deserialize<AppConfig>(json);
            }
            catch { return null; }
        }

        private static void Save(AppConfig cfg)
        {
            var json = JsonSerializer.Serialize(cfg, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json, Encoding.UTF8);
        }

        private static Icon? ExtractIcon(string path, int index, bool large)
        {
            try
            {
                IntPtr[] largeArr = large ? new IntPtr[1] : null;
                IntPtr[] smallArr = !large ? new IntPtr[1] : null;
                uint count = ExtractIconEx(path, index, largeArr, smallArr, 1);
                if (count == 0) return null;
                IntPtr hIcon = large ? (largeArr![0]) : (smallArr![0]);
                if (hIcon == IntPtr.Zero) return null;
                var ico = Icon.FromHandle(hIcon);
                return (Icon)ico.Clone();
            }
            catch { return null; }
        }
    }
}
