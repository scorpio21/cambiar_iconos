# Cambiar Iconos (WinForms .NET)

Aplicación de escritorio (Windows Forms, .NET 8) para redimensionar imágenes a tamaños comunes de iconos y generar recursos (PNG/JPG/ICO) y bloque de manifest.

## Funcionalidad

- Cargar imagen (PNG/JPG/JPEG/WEBP/BMP/GIF) y arrastrar y soltar (drag & drop).
- Selector de tamaños: 16, 20, 24, 32, 36, 48, 72, 96, 120, 144, 152, 167, 180, 192, 256, 384, 512.
- Fondo de color o fondo transparente (PNG).
- Redimensionar y previsualizar.
- Vista móvil (preview 96×96).
- Nombre base: usado para nombrar archivos en guardados y manifest.
- Guardar individual (PNG/JPG/ICO; si es transparente se fuerza PNG en PNG/JPG).
- Guardar "básicos" (siempre PNG): 16×16, 20×20, 24×24, 32×32, 180×180, 192×192, 512×512.
- Guardar ICO multi-tamaño (único .ico con entradas 16, 20, 24, 32, 48, 64, 128, 256 con alfa).
- Bloque Manifest: generar y copiar JSON con icons (192/512) y `purpose: "any maskable"`.

## Estructura

- `RedimensionarIcono.WinForms.sln`: solución Visual Studio.
- `RedimensionarIcono.WinForms/`: proyecto principal.
  - `MainForm.cs`, `MainForm.Designer.cs`: interfaz y lógica.
  - `Program.cs`: punto de entrada.
  - `RedimensionarIcono.WinForms.csproj`: configuración de proyecto.

## Requisitos

- Windows 10/11
- .NET 8 SDK
- Visual Studio 2022 (recomendado) con workload de .NET Desktop

## Ejecutar

1. Abre `RedimensionarIcono.WinForms.sln` en Visual Studio.
2. Compila en `Debug | Any CPU`.
3. Ejecuta (F5).

## Notas

- Transparencia: JPG no soporta alfa; si marcas "Fondo transparente (PNG)", se fuerza PNG para preservar transparencia.
- ICO: "Guardar" con formato ICO guarda un tamaño; "Guardar ICO multi-tamaño" crea un único .ico con varios tamaños (entradas PNG con alfa).
- WEBP: admitido para abrir, no para guardar (GDI+ no soporta guardado WEBP nativo).
- DPI y diseñador: el proyecto usa .NET 8 y `SystemAware` para High DPI. Si el diseñador muestra avisos en monitores con escalado >100%, puedes abrirlo en modo DPI-unaware.

## Licencia

MIT
