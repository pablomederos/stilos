# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- **Image Proxy (`/api/img`)**: Nuevo controlador MVC que actúa como proxy para imágenes de Picsum Photos. Soporta parámetros `seed`, `w`, `h` y `grayscale`. Las imágenes se cachean en el directorio temporal del SO para evitar descargas duplicadas y prevenir errores de CORS o cookies de terceros en el navegador.
  - 45 referencias directas a `picsum.photos` en `.cshtml` y `style-demos.css` migradas al endpoint local `/api/img`.
- **Documentation**: Actualizado `THIRD_PARTY_NOTICES.md`, creado `NOTICE.md` y añadido `LICENSE.md` (MIT) con atribuciones completas para el autor y todas las librerías de terceros.


### Fixed
- **Docker Build Failure**: Consolidado el controlador `ImageProxyController` en un solo archivo y estandarizado el `Dockerfile`. Esto elimina errores de vinculación de clases `partial` (`CS0117`/`CS0103`) que ocurrían específicamente en el entorno de compilación de Docker al no resolver correctamente los métodos generados por fuente.
- **Lighthouse Performance Optimization**: Implementación de una estrategia de carga segmentada y "Critical-First" para alcanzar una puntuación de rendimiento >95%.
  - **Critical CSS Inlining**: Extracción de estilos esenciales (Header, Hero, Variables) e inyección directa en el `<head>` para un renderizado instantáneo del LCP.
  - **Lazy Loading de Recursos**: Diferimiento de la carga de `style-demos.css`, GSAP y fuentes decorativas hasta la primera interacción del usuario con los acordeones de estilos.
  - **Loading UX**: Adición de una barra de progreso visual para indicar la carga de recursos bajo demanda.
  - **Font Optimization**: Reducción del stack tipográfico inicial a fuentes core (Inter, Space Grotesk, JetBrains Mono), mejorando el First Contentful Paint.
- **Cache Optimization**: Implementación de políticas de caché eficientes para mejorar la puntuación de Lighthouse.
  - El endpoint `/api/img` ahora devuelve un encabezado `Cache-Control: public,max-age=31536000,immutable` (1 año).
  - Se añadió un middleware global al inicio del pipeline para asegurar un tiempo de vida mínimo de caché de 1 día (`max-age=86400`) para todas las respuestas satisfactorias, respetando encabezados más específicos preexistentes.
- **Forced Reflow Optimization**: Mejora del rendimiento en las demos al mitigar "forced reflows" causados por scripts externos de autofill.
  - Se añadieron atributos `autocomplete="off"`, `spellcheck="false"` y `data-lpignore="true"` a todos los inputs de las demos para evitar la interferencia de gestores de contraseñas y funciones de autorelleno del navegador (ej. `bootstrap-autofill-overlay.js`).
- **Dotnet Watch Warnings**: Se resolvió el aviso de `BrowserRefreshMiddleware` relacionado con la inyección de scripts en respuestas comprimidas. Se añadió `launchSettings.json` para definir explícitamente el entorno de desarrollo y se ajustó la lógica de middleware para omitir `ResponseCompression` durante sesiones de `dotnet watch`.
- **Estilos de Interfaz**: Añadidos breakpoints CSS para la familia completa (*Glassmorphism*, *Neumorphism*, *Claymorphism*, *Skeuomorphism*, *Material Design*, *Fluent Design*, *Brutalismo (Web)* y *Anti-Diseño*). Se solucionaron desbordamientos, formas extrañas y superposiciones, manteniendo la esencia visual (profundidad, sombras, texturas, "honestidad" brutalista) 100% responsiva.
- **Editorial y Tipográfico**: Añadidos breakpoints CSS para asegurar que los estilos `editorial`, `type-first`, `brutalist-typography` y `magazine` sean 100% responsivos en dispositivos móviles, manteniendo su integridad artística y tipográfica agresiva.
- **Constructivismo**: Corregido problema de carga y visualización de imágenes.
    - Sustitución de placeholders externos por imágenes locales generadas de alta calidad.
    - Ajuste del layout CSS para evitar el colapso de la grilla.
- **Surrealismo**: Rediseño inmersivo y corrección de legibilidad.
    - Implementación de un contenedor con glassmorphism (`backdrop-filter`) para proteger la lectura del texto.
    - Reubicación de elementos flotantes (reloj, ojo) para evitar solapamientos con el contenido principal.
    - Adición de texturas de ruido (noise) y efectos de iluminación dinámica.
    - Sustitución de imágenes remotas por recursos locales generados ad-hoc.
- **Pop Art**: Refinado con stickers al estilo Lichtenstein, fondo de puntos Ben-Day (halftone), grilla Warhol con mezclas cromáticas y onomatopeyas dinámicas.
- **Expresionismo Abstracto**: Rediseño visual masivo con filtros SVG para bordes rugosos y expansión de tinta. Implementación de capas de "Action Painting" con salpicaduras, goteos y tipografía monumental sobre campos de color estilo Rothko. Añadida textura de lienzo para una sensación física.
