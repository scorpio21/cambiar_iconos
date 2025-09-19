# Cambiar Iconos (WinForms .NET)

Aplicación de escritorio (Windows Forms, .NET 6) para redimensionar imágenes a tamaños comunes de iconos.

## Funcionalidad

- Cargar imagen (PNG/JPG/JPEG/WEBP/BMP/GIF).
- Selector de tamaños: 16, 20, 24, 32, 36, 48, 72, 96, 120, 144, 152, 167, 180, 192, 256, 384, 512.
- Fondo de color o fondo transparente (PNG).
- Redimensionar y previsualizar.
- Guardar individual (PNG o JPG; si es transparente se fuerza PNG).
- Guardar "básicos" (siempre PNG): 16×16, 20×20, 24×24, 32×32, 180×180, 192×192, 512×512.

## Estructura

- `RedimensionarIcono.WinForms.sln`: solución Visual Studio.
- `RedimensionarIcono.WinForms/`: proyecto principal.
  - `MainForm.cs`, `MainForm.Designer.cs`: interfaz y lógica.
  - `Program.cs`: punto de entrada.
  - `RedimensionarIcono.WinForms.csproj`: configuración de proyecto.

## Requisitos

- Windows 10/11
- .NET 6 SDK o superior
- Visual Studio 2022 (recomendado) con workload de .NET Desktop

## Ejecutar

1. Abre `RedimensionarIcono.WinForms.sln` en Visual Studio.
2. Compila en `Debug | Any CPU`.
3. Ejecuta (F5).

## Notas

- Transparencia no está disponible en JPG; el programa fuerza PNG cuando se marca "Fondo transparente".
- WEBP se admite para abrir, no para guardar (GDI+ no soporta guardado WEBP nativo).

## Licencia

MIT
