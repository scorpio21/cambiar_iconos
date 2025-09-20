using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace RedimensionarIcono.WinForms.Services
{
    internal static class PackageService
    {
        // Crea un paquete ZIP con extensión .dll que contiene PNGs básicos y manifest.json
        public static void ExportZipAsDll(Form owner, Bitmap original, string baseName, Color? bg, bool includeManifest, string destinationPath, int[]? sizes = null)
        {
            var basics = sizes ?? new[] { 16, 20, 24, 32, 180, 192, 512 };
            var tempDir = Path.Combine(Path.GetTempPath(), "pkg_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
            try
            {
                var imgDir = Path.Combine(tempDir, "img");
                Directory.CreateDirectory(imgDir);
                // Generar PNGs
                foreach (var s in basics)
                {
                    using var bmp = ImageService.Redimensionar(original, s, s, bg);
                    var outPath = Path.Combine(imgDir, $"{baseName}-{s}x{s}.png");
                    bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);
                }
                // Manifest opcional
                if (includeManifest)
                {
                    var manifestBlock = ManifestService.GenerateIconsBlock(baseName);
                    File.WriteAllText(Path.Combine(tempDir, "manifest.json"), "{\n" + manifestBlock + "\n}");
                }
                // Crear ZIP con extensión .dll
                var tmpZip = Path.Combine(Path.GetTempPath(), "zip_" + Guid.NewGuid().ToString("N") + ".zip");
                if (File.Exists(tmpZip)) File.Delete(tmpZip);
                ZipFile.CreateFromDirectory(tempDir, tmpZip, CompressionLevel.Optimal, includeBaseDirectory: false);
                // Copiar como .dll (contenedor)
                File.Copy(tmpZip, destinationPath, overwrite: true);
            }
            finally
            {
                try { Directory.Delete(tempDir, recursive: true); } catch { /* ignore */ }
            }
        }

        // Intenta crear un DLL de recursos (resource-only) con ICONs generados
        public static void ExportResourceDll(Form owner, Bitmap original, string baseName, Color? bg, string destinationPath, int[]? sizes = null)
        {
            // Verificar herramientas
            var rcPath = FindOnPath("rc.exe");
            var linkPath = FindOnPath("link.exe");
            if (rcPath == null || linkPath == null)
            {
                MessageBox.Show(owner,
                    "No se encontraron rc.exe y/o link.exe en el PATH. Instala el Windows SDK o abre desde un Developer Command Prompt.",
                    "Herramientas no disponibles", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tempDir = Path.Combine(Path.GetTempPath(), "res_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
            try
            {
                var sizesArr = sizes ?? new[] { 16, 32, 48, 64, 128, 256 };
                // Generar .ico por tamaño
                for (int i = 0; i < sizesArr.Length; i++)
                {
                    int s = sizesArr[i];
                    using var bmp = ImageService.Redimensionar(original, s, s, bg);
                    var icoPath = Path.Combine(tempDir, $"icon_{s}.ico");
                    IcoService.SaveSingleIco(bmp, icoPath);
                }
                // .rc
                var rcPathFile = Path.Combine(tempDir, "icons.rc");
                using (var sw = new StreamWriter(rcPathFile, false))
                {
                    foreach (var s in sizesArr)
                    {
                        sw.WriteLine($"IDI_ICON_{s} ICON \"icon_{s}.ico\"");
                    }
                }
                // rc.exe -> .res
                var resPath = Path.Combine(tempDir, "icons.res");
                var rc = RunProcess(rcPath, $"/nologo \"{rcPathFile}\"");
                if (rc.ExitCode != 0)
                {
                    MessageBox.Show(owner, "Fallo rc.exe. Revisa que el SDK esté correctamente instalado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // rc genera icons.res en la misma carpeta del .rc
                if (!File.Exists(resPath))
                {
                    // Algunos rc crean .RES en mayúsculas
                    var alt = Path.Combine(tempDir, "ICONS.RES");
                    if (File.Exists(alt)) File.Move(alt, resPath);
                }
                if (!File.Exists(resPath))
                {
                    MessageBox.Show(owner, "No se generó el .res.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // link.exe /NOENTRY /DLL -> destino
                var args = $"/NOLOGO /DLL /NOENTRY /OUT:\"{destinationPath}\" \"{resPath}\"";
                var link = RunProcess(linkPath, args);
                if (link.ExitCode != 0)
                {
                    MessageBox.Show(owner, "Fallo link.exe al crear el DLL de recursos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            finally
            {
                try { Directory.Delete(tempDir, true); } catch { }
            }
        }

        private static string? FindOnPath(string exe)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c where {exe}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                using var p = Process.Start(psi)!;
                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit(3000);
                var first = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                return string.IsNullOrWhiteSpace(first) ? null : first.Trim();
            }
            catch { return null; }
        }

        private static ProcessResult RunProcess(string fileName, string args)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            using var p = Process.Start(psi)!;
            var stdout = p.StandardOutput.ReadToEnd();
            var stderr = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return new ProcessResult(p.ExitCode, stdout, stderr);
        }

        private readonly record struct ProcessResult(int ExitCode, string StdOut, string StdErr);
    }
}
