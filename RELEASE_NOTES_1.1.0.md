# v1.1.0

Mejoras y nuevas funciones (WinForms .NET 8):

- Drag & Drop: ahora puedes arrastrar una imagen al preview o a la ventana para cargarla.
- Nombre base: campo "Nombre base" para nombrar archivos y el manifest de forma consistente.
- Vista móvil: preview 96×96 para validar el icono.
- Manifest: panel para "Generar manifest" (icons 192/512 con `purpose: any maskable`) y botón "Copiar JSON".
- ICO:
  - Guardado individual (formato ICO) del tamaño actual.
  - Guardar ICO multi-tamaño: genera un único `.ico` con entradas 16, 20, 24, 32, 48, 64, 128, 256 (entradas PNG con alfa).
- Transparencia: si "Fondo transparente (PNG)" está activado, el combo de formato limita a PNG/ICO (JPG se oculta).
- Actualización a .NET 8 y configuración DPI para mejor compatibilidad del diseñador.

Notas:
- JPG no soporta transparencia.
- WEBP se admite para abrir, no para guardar (limitación de GDI+).
