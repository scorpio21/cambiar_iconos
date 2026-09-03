# Changelog

Todos los cambios notables en RedimensionarIcono.WinForms.

## [v1.1.1]

- **Soporte SVG:** ahora puedes cargar archivos `.svg` que se renderizan automáticamente a 512×512 vía SkiaSharp.Svg.
- Añadido `SkiaSharp` 2.88.9 y `SkiaSharp.Svg` 1.60.0 como dependencias.
- Filtro de archivos y Drag & Drop actualizados para aceptar `.svg`.

## [v1.1.0]

- **Drag & Drop:** ahora puedes arrastrar una imagen al preview o a la ventana para cargarla.
- **Nombre base:** campo "Nombre base" para nombrar archivos y el manifest de forma consistente.
- **Vista móvil:** preview 96×96 para validar el icono.
- **Manifest:** panel para "Generar manifest" (icons 192/512 con `purpose: any maskable`) y botón "Copiar JSON".
- **ICO:**
  - Guardado individual (formato ICO) del tamaño actual.
  - Guardar ICO multi-tamaño: genera un único `.ico` con entradas 16, 20, 24, 32, 48, 64, 128, 256 (entradas PNG con alfa).
- **Transparencia:** si "Fondo transparente (PNG)" está activado, el combo de formato limita a PNG/ICO (JPG se oculta).
- Actualización a .NET 8 y configuración DPI para mejor compatibilidad del diseñador.

Notas:
- JPG no soporta transparencia.
- WEBP se admite para abrir, no para guardar (limitación de GDI+).
- SVG se admite para abrir (renderiza a PNG 512×512), no para guardar.
