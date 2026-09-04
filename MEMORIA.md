# MEMORIA — cambiar_iconos (RedimensionarIcono.WinForms)

> Actualizado: 2026-09-04 · Directorio: `E:\xampp\htdocs\cambiar_iconos`

---

## 🎯 Objetivo

Herramienta WinForms en C# para **redimensionar, convertir y empaquetar iconos** (.ico, .png, .dll de recursos).  
Permite exportar iconos en múltiples tamaños, generar manifiestos y exportar DLLs de recursos.

---

## 🔨 Comandos de Build / Publicación

```powershell
# Compilar (Debug)
cd E:\xampp\htdocs\cambiar_iconos
dotnet build RedimensionarIcono.WinForms.sln

# Publicar (Release, self-contained)
dotnet publish RedimensionarIcono.WinForms\RedimensionarIcono.WinForms.csproj -c Release

# Salida en:
# dist\   → distribución
# publish\ → publicación
# releases\ → releases versionadas
```

---

## 📐 Arquitectura (indexada en MCP: `E-xampp-htdocs-cambiar_iconos`)

| Capa | Clases clave |
|------|-------------|
| **UI / Forms** | `MainForm`, `SettingsDialog`, `SeleccionarTamanosDialog` |
| **Servicios** | `IconService` (Load/Save), `ImageService` (Redimensionar), `IcoService` (SaveSingleIco/SaveMultiIcon), `ManifestService` (GenerateIconsBlock) |
| **Export** | `ExportZipAsDll`, `ExportResourceDll`, `RunProcess` |
| **Helpers** | `SanitizeBase`, `UpdateFormatOptions`, `ToggleActions`, `AppConfig` |

### Hotspots (métodos más usados)
- `IconService.Load` (fan-in: 11) — punto de entrada principal
- `IconService.Save` (fan-in: 9)
- `MainForm.SanitizeBase` (fan-in: 6)
- `MainForm.UpdateFormatOptions` (fan-in: 4)
- `MainForm.Redimensionar` (fan-in: 3)

---

## ✅ Estado actual

| Área | Estado |
|------|--------|
| Proyecto indexado en MCP knowledge graph | ✅ HECHO |
| Build/compilación funcional | ✅ (según CHANGELOG) |
| v1.1.0 — añadida validación nombre de salida antes de generar .h | ✅ (2026-09-01) |
| Rediseño UI | ⬜ PENDIENTE |
| MCP server propio para la herramienta | ⬜ PENDIENTE (idea futura) |
| Tests automatizados con Playwright | ⬜ PENDIENTE |

---

## 📝 Historial de sesiones relevantes

| Fecha | Conversación | Resumen |
|-------|-------------|---------|
| 2026-09-01 | `955e77ed` | Validación nombre de salida antes de generar .h; rechaza vacíos |
| 2026-09-04 | sesión actual | Indexado en MCP, creado MEMORIA.md, setup de skills/workflows |

---

## 🔮 Próximos pasos sugeridos

1. **Explorar el código con MCP** → `search_graph`, `trace_path` para entender flujo completo
2. **Decidir próxima feature** (rediseño UI, nuevo formato de exportación, etc.)
3. Opcional: crear `MCP server` que exponga `resize_icon`, `convert_to_ico`, `batch_resize`

---

## ⚠️ Gotchas conocidos

- Los directorios `dist/`, `publish/`, `releases/` están en `.gitignore` → no se indexan en MCP
- Imágenes en `img/` (.png, .svg) también ignoradas por MCP (por extensión)
- El proyecto es **WinForms** puro (.NET) — no hay frontend web que testear con Playwright

---

## 🛠️ Skills / MCP disponibles (en `c:\Users\sonsc\.agents\`)

| Tool | Cuándo usarlo |
|------|--------------|
| `codebase-memory-mcp` | Buscar código, trazar llamadas, auditar arquitectura |
| `memory` skill | Actualizar este archivo al final de cada sesión |
| `frontend-design` skill | Si se rediseña la UI o se crea versión web |
| `webapp-testing` skill | Si se añade frontend web |
| `mcp-builder` skill | Para crear MCP server de la herramienta |
| `graphify` workflow | Re-indexar el proyecto tras cambios grandes |
