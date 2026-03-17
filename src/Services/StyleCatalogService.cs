namespace Stilos.Services;

using Stilos.Models;

public sealed class StyleCatalogService
{
    private readonly List<StyleFamily> _families = BuildCatalog();

    public IReadOnlyList<StyleFamily> GetAllFamilies() => _families;

    public DesignStyle? GetStyleById(string id) =>
        _families
            .SelectMany(f => f.Styles)
            .FirstOrDefault(s => s.Id == id);

    public StyleFamily? GetFamilyByStyleId(string id) =>
        _families
            .FirstOrDefault(f => f.Styles.Any(s => s.Id == id));

    private static List<StyleFamily> BuildCatalog() =>
    [
        new(1, "I", "Minimalista", "◻️",
        [
            new("minimalism", "Minimalismo", "Claridad, serenidad",
                "SaaS, portfolios, blogs editoriales",
                "El minimalismo web reduce cada elemento a su esencia funcional. Amplios espacios en blanco, tipografía limpia y una paleta restringida crean una experiencia donde el contenido respira. Cada pixel tiene un propósito; nada es decorativo. La jerarquía visual se logra con tamaño tipográfico y peso, no con adornos. Ideal para marcas que transmiten sofisticación a través de la simplicidad.", "Minimalist"),
            new("flat-design", "Diseño Plano (Flat)", "Modernidad, dinamismo",
                "Apps móviles, dashboards, sistemas UI",
                "El diseño plano elimina sombras, degradados y texturas en favor de colores sólidos y vibrantes, formas geométricas simples e iconografía bidimensional. Nacido como respuesta al skeuomorfismo, prioriza la funcionalidad y la velocidad de carga. Los elementos interactivos se distinguen por color y posición, no por simulación de profundidad.", "Landing"),
            new("swiss", "Suizo / Internacional", "Rigor institucional",
                "Gobierno, transporte, académico",
                "Heredero directo del Estilo Tipográfico Internacional de los años 50. Grillas matemáticas rigurosas, tipografía sans-serif (Helvetica, Akzidenz), alineación asimétrica y jerarquía visual implacable. El contenido se organiza con precisión milimétrica. Los colores son funcionales, no decorativos. Perfecto para instituciones que necesitan comunicar autoridad y neutralidad.", "Swiss"),
            new("functional-minimalism", "Minimalismo Funcional", "Eficiencia, enfoque",
                "Herramientas de productividad, dev tools",
                "Lleva el minimalismo al extremo utilitario. Cada componente existe porque cumple una función medible. Las interfaces son densas en información pero visualmente limpias. Tipografía monoespaciada, contrastes altos y feedback inmediato. No hay decoración; la estética emerge de la función pura. El usuario profesional lo agradece.", "SaaS"),
            new("whitespace-first", "Espacio en Blanco", "Calma, respiración",
                "Lujo, bienestar, fotografía",
                "El espacio vacío no es ausencia, es el protagonista. Márgenes generosos, interlineado amplio y secciones que flotan en un océano de blanco. Las imágenes son grandes y aisladas. La tipografía es delgada y refinada. Crea una experiencia contemplativa donde cada elemento recibe atención total. Transmite exclusividad y cuidado artesanal.", "ECommerce")
        ]),
        new(2, "II", "Maximalista", "🎨",
        [
            new("maximalism", "Maximalismo", "Exuberancia, poder",
                "Moda de lujo, arte, festivales",
                "Más es más. Capas de textura, tipografía expresiva en múltiples tamaños, paletas vibrantes que colisionan intencionalmente, y una densidad visual que abruma los sentidos (en el buen sentido). Cada scroll revela nuevas sorpresas. Las imágenes se superponen, los textos se entrelazan con gráficos. Es un festín visual que celebra el exceso creativo.", "Portfolio"),
            new("cluttercore", "Cluttercore", "Calidez, autenticidad",
                "Tiendas vintage, zines, artesanía",
                "La estética del desorden organizado. Elementos dispersos que parecen colocados al azar pero siguen un ritmo interno. Texturas de papel, manchas de tinta, fotografías polaroid y tipografía manuscrita coexisten en un collage digital que evoca lo táctil y lo humano. Abraza la imperfección como filosofía de diseño.", "Cluttercore"),
            new("memphis", "Diseño Memphis", "Alegría, irreverencia",
                "Marcas Gen-Z, agencias creativas",
                "Nacido en Milán en 1981, el Memphis desafía toda regla del buen gusto. Patrones geométricos frenéticos (zigzags, puntos, ondas), colores primarios contra pasteles inesperados, y formas que parecen diseñadas por un niño con un doctorado en arquitectura. Es caótico, divertido e imposible de ignorar.", "Landing"),
            new("psychedelic", "Psicodélico / Groovy", "Alteración sensorial",
                "Música, cannabis, festivales",
                "Herencia directa del arte psicodélico de los 60. Degradados líquidos que se funden entre tonos neón, tipografía que se derrite y ondula, patrones fractales y efectos ópticos que marean. Los fondos vibran, las formas mutan. Es una experiencia visual inmersiva que busca alterar la percepción del usuario.", "Landing")
        ]),
        new(3, "III", "Editorial y Tipográfico", "📰",
        [
            new("editorial", "Diseño Editorial", "Autoridad intelectual",
                "Noticias, revistas, publicaciones culturales",
                "La web como publicación impresa de alta gama. Columnas cuidadosamente proporcionadas, capitulares decorativas, pull quotes destacados y una jerarquía tipográfica que guía la lectura. Cada página es una composición editorial pensada. El whitespace es rítmico, las imágenes están integradas con el texto. Transmite credibilidad y profundidad intelectual.", "Editorial"),
            new("type-first", "Tipográfico / Type-first", "Precisión, sofisticación",
                "Fundiciones tipográficas, estudios de diseño",
                "La tipografía no acompaña al contenido: ES el contenido. Letras a escala monumental, juegos de peso y contraste, kerning artesanal y composiciones donde cada carácter es tratado como una pieza escultórica. Los colores son secundarios; el drama viene del negro contra el blanco y las variaciones de escala. Es diseño tipográfico puro.", "Editorial"),
            new("brutalist-typography", "Tipografía Brutalista", "Confrontación, urgencia",
                "Moda avant-garde, contracultura",
                "Tipografía como arma visual. Tamaños extremos que rompen la grilla, superposición de textos que desafían la legibilidad, mezclas violentas de serif y sans-serif. Las letras gritan, se amontonan, invaden el espacio. No busca ser bonito; busca ser imposible de ignorar. Es el punk del diseño tipográfico.", "Portfolio"),
            new("magazine", "Layout de Revista", "Deseo, aspiración",
                "Lifestyle, moda, fotografía",
                "Composiciones dinámicas que emulan las mejores revistas de moda. Imágenes a sangre completa, textos que fluyen alrededor de formas orgánicas, columnas asimétricas y una cadencia visual que convierte el scroll en una experiencia narrativa. Cada sección es un spread editorial. La fotografía es protagonista absoluta.", "Editorial")
        ]),
        new(4, "IV", "Oscuro y Dramático", "🌑",
        [
            new("dark-mode", "Modo Oscuro / Dark UI", "Enfoque, sofisticación",
                "Dev tools, SaaS, streaming",
                "Fondos en negro profundo (#0a0a0a) o gris carbón que eliminan distracciones y reducen fatiga visual. Los acentos de color brillan como neón en la oscuridad: cian, violeta, verde esmeralda. La tipografía es nítida en alto contraste. Las sombras se convierten en resplandores sutiles. Es la interfaz preferida por desarrolladores, gamers y noctámbulos digitales.", "DarkUI"),
            new("gothic", "Gótico / Dark Aesthetic", "Fascinación oscura, misterio",
                "Moda dark, joyería, RPG",
                "Terciopelo negro, ornamentos victorianos y una atmósfera que evoca catedrales medievales iluminadas por velas. Tipografía blackletter combinada con serif elegante. Texturas de encaje, filigrana dorada sobre fondos abisales y una paleta de carmesí, púrpura profundo y plata envejecida. Cada elemento destila una belleza siniestra y refinada.", "Gothic"),
            new("noir", "Noir", "Tensión, elegancia peligrosa",
                "Cine, whisky, ficción detectivesca",
                "Inspirado en el cine negro de los 40. Alto contraste entre luces y sombras, composiciones diagonales que generan tensión, y una paleta monocromática con un único acento dramático (rojo sangre o dorado). Tipografía condensada que evoca carteles de cine clásico. Todo se ve a través de un filtro de peligrosa sofisticación.", "Noir"),
            new("horror-occult", "Horror / Oculto", "Pavor, fascinación macabra",
                "Juegos de horror, tiendas ocultistas",
                "Distorsión visual como recurso narrativo. Tipografía que se corrompe y parpadea, fondos con texturas de óxido y deterioro, paletas de rojo visceral contra negro absoluto. Elementos que se mueven de forma inesperada, glitch effects y una sensación constante de que algo acecha tras la interfaz. No es para todos, y eso es exactamente el punto.", "Horror")
        ]),
        new(5, "V", "Futurista y Tech", "🚀",
        [
            new("cyberpunk", "Cyberpunk", "Rebeldía, caos digital",
                "Gaming, crypto, vida nocturna",
                "Neón sobre hormigón digital. Gradientes que van del magenta al cian eléctrico, tipografía glitcheada, interfaces que parecen terminales hackeadas y fondos urbanos distópicos. Los elementos parpadean, se distorsionan y vibran con energía frenética. Líneas de escaneo CRT, ruido visual y una estética que dice 'el futuro ya llegó y es hermosamente caótico'.", "Cyberpunk"),
            new("scifi-space", "Sci-Fi / Space UI", "Asombro, exploración",
                "Aeroespacial, dashboards de datos",
                "Interfaces que parecen extraídas de la cabina de una nave espacial. Elementos HUD translúcidos, indicadores radiales, rejillas de datos y una paleta dominada por azules profundos, blancos fríos y acentos de alerta en ámbar. La tipografía es monoespaciada y técnica. Todo transmite precisión instrumental y la vastedad del cosmos.", "SciFi"),
            new("tech-saas", "Tech / SaaS Moderno", "Confianza, innovación",
                "B2B SaaS, fintech, productos IA",
                "La estética de las startups que conquistan el mundo. Gradientes suaves de azul a púrpura, ilustraciones 3D flotantes, tarjetas con glassmorphism sutil y una tipografía geométrica y limpia. Amplios espacios, CTAs claros y una sensación de producto pulido y confiable. Es el lenguaje visual de la innovación corporativa moderna.", "TechSaaS")
        ]),
        new(6, "VI", "Orgánico y Natural", "🌿",
        [
            new("biomorphic", "Orgánico / Biomórfico", "Fluidez, vida",
                "Bienestar, biotech, eco-marcas",
                "Formas que imitan la naturaleza: curvas amébicas, bordes ondulados, blobs que fluyen y mutan. La paleta va de verdes salvia a azules agua, pasando por terracota y arena. Las secciones no tienen esquinas; todo es suave y orgánico. Las animaciones son lentas y fluidas como el agua. Transmite conexión con lo vivo y lo cambiante.", "Biomorphic"),
            new("earthy", "Terroso / Natural", "Arraigo, calidez",
                "Comida orgánica, outdoor, moda sostenible",
                "Tonos que vienen directamente de la tierra: ocre, siena, verde musgo, marrón cacao y un blanco hueso cálido. Texturas de papel kraft, lino y madera. Tipografía con personalidad artesanal pero legible. Fotografía con luz natural cálida. Todo evoca una conexión honesta con la tierra, la tradición y lo hecho a mano.", "Earthy"),
            new("botanical", "Botánico / Floral", "Delicadeza, elegancia",
                "Perfumería, té, bodas, spa",
                "Ilustraciones botánicas detalladas, marcos de hojas y flores, y una paleta que va del verde esmeralda al rosa empolvado pasando por el dorado suave. Tipografía serif elegante con ornamentos florales. Los fondos pueden tener texturas de acuarela. Es refinado sin ser frío, natural sin ser rústico. Evoca jardines secretos y invernaderos victorianos.", "Botanical"),
            new("wabi-sabi", "Wabi-Sabi", "Belleza imperfecta, serenidad",
                "Cerámica, mindfulness, artesanía",
                "La filosofía japonesa de encontrar belleza en la imperfección. Texturas ásperas, paletas terrosas apagadas, asimetría intencional y espacios vacíos que invitan a la contemplación. Los bordes no son perfectos, las imágenes tienen grano. La tipografía es mínima y ubicada con cuidado asimétrico. Todo respira wabi (austeridad) y sabi (paso del tiempo).", "WabiSabi")
        ]),
        new(7, "VII", "Retro y Vintage", "📼",
        [
            new("pixel-art", "Pixel Art / 8-bit", "Nostalgia, juego",
                "Juegos indie, marcas retro",
                "Todo construido con bloques de píxeles visibles. Paletas de 16 colores, tipografía bitmap, bordes duros sin anti-aliasing y animaciones sprite sheet. Los fondos son tiles repetidos, los botones parecen controles de NES. La estética evoca la era dorada de los videojuegos y genera un sentimiento cálido de nostalgia en quienes crecieron con esos píxeles.", "PixelArt"),
            new("synthwave", "Neon Retro 80s / Synthwave", "Energía, nostalgia",
                "Música, vida nocturna, ropa",
                "Rejillas de perspectiva infinita bañadas en púrpura y magenta, soles geométricos en el horizonte, tipografía chrome con reflejos y una paleta de neones que brillan contra fondos oscuros. Todo parece sacado de un VHS de 1985. Las animaciones son líneas de escaneo, destellos y transiciones retro-futuristas. Es la nostalgia de un futuro que nunca llegó.", "Synthwave"),
            new("y2k", "Y2K", "Tecno-optimismo, diversión",
                "Moda Gen-Z, cultura pop",
                "El internet del año 2000 revisitado con ironía afectuosa. Degradados metálicos (plata, cromo), formas infladas y burbujeantes, estrellas y destellos, tipografía futurista-retro y colores que van del lila al plateado pasando por el celeste iridiscente. Es kitsch elevado a arte, la estética de los primeros teléfonos flip y las carátulas de CD.", "Y2k"),
            new("grunge", "Grunge / Underground 90s", "Autenticidad cruda, rebeldía",
                "Punk, skate, cultura DIY",
                "Texturas sucias: papel rasgado, manchas de tinta, cinta adhesiva y fotocopias de baja resolución. Tipografía typewriter desalineada, collages analógicos escaneados y una paleta de negros, grises y un acento sucio (amarillo mostaza, rojo oxidado). Todo parece hecho en un fanzine a las 3 AM. Es auténticamente imperfecto y orgullosamente amateur.", "Grunge")
        ]),
        new(8, "VIII", "Lujo y Premium", "💎",
        [
            new("luxury", "Lujo / High-end", "Exclusividad, atemporalidad",
                "Hoteles 5 estrellas, relojes suizos, yates",
                "Cada detalle comunica que el precio no es un problema. Paletas de negro, dorado y blanco marfil. Tipografía serif de trazo fino con tracking amplio. Animaciones lentas y elegantes. Imágenes de alta resolución con tratamiento cinematográfico. Los espacios son amplios, los elementos pocos y cada uno es exquisitamente renderizado. Menos elementos, más impacto.", "Luxury"),
            new("haute-couture", "Moda / Haute Couture", "Impacto visual, avant-garde",
                "Alta moda, cosméticos editoriales",
                "La web como pasarela de moda. Tipografía display monumental, fotografía en blanco y negro de alto contraste, layouts que desafían las convenciones y una presencial visual que domina la pantalla. Los textos son mínimos y crípticos. La navegación es experimental. Cada página es una declaración artística más que una interfaz funcional.", "HauteCouture"),
            new("jewelry", "Joyería / Tonos Joya", "Opulencia, romance",
                "Joyería fina, perfumería, licores premium",
                "Colores profundos de piedras preciosas: esmeralda, rubí, zafiro, amatista, sobre fondos de terciopelo negro. Degradados que simulan el brillo de gemas pulidas. Tipografía con detalles dorados. Bordes finos y ornamentados. Las imágenes tienen un tratamiento de iluminación dramática que hace que cada producto brille como bajo un foco de joyería.", "Jewelry"),
            new("corporate-prestige", "Prestigio Corporativo", "Autoridad, estabilidad",
                "Bancos de inversión, bufetes, consultoría",
                "El diseño que usan las firmas que mueven el mundo. Paleta sobria de azul navy, gris antracita y blanco. Tipografía serif clásica (Georgia, Garamond) que evoca contratos y documentos oficiales. Layouts estructurados con precisión militar. Fotografía corporativa profesional. Todo comunica que esta institución ha existido por siglos y existirá por siglos más.", "CorporatePrestige")
        ]),
        new(9, "IX", "Estilos de Interfaz", "🔮",
        [
            new("glassmorphism", "Glassmorphism", "Profundidad, modernidad",
                "Dashboards, landing pages",
                "Paneles translúcidos con blur de fondo que crean capas de profundidad. Bordes finos semi-transparentes, sombras sutiles y fondos con gradientes vibrantes que se ven a través del cristal esmerilado. Es el lenguaje visual de macOS y iOS moderno llevado a la web. Crea interfaces que parecen flotar sobre el contenido.", "Glassmorphism"),
            new("neumorphism", "Neumorfismo", "Suavidad, tactilidad",
                "Smart home, apps de bienestar",
                "Elementos que parecen extruidos del fondo mismo. Sombras dobles (una clara y una oscura) sobre fondos monocromáticos crean un efecto de relieve suave y táctil. Los botones parecen poder presionarse físicamente. La paleta es monocromática con variaciones mínimas de luminosidad. Es minimalista pero con una dimensión física que invita al tacto.", "Neumorphism"),
            new("claymorphism", "Claymorphism", "Amabilidad, tridimensionalidad",
                "Fintech, edu-tech",
                "Elementos que parecen hechos de arcilla o plastilina. Bordes redondeados suaves, sombras internas y externas que crean volumen, y una paleta de colores pastel vibrante. Los componentes parecen juguetes 3D amigables. Tipografía redondeada que combina con la estética moldeable. Es el diseño que te hace sonreír mientras haces una transferencia bancaria.", "Claymorphism"),
            new("skeuomorphism", "Skeuomorfismo", "Familiaridad, realismo",
                "Producción musical, utilidades",
                "Simulación digital de materiales reales: cuero con costuras, metal cepillado, madera con veta, perillas que giran. Cada elemento imita su contraparte física con texturas fotorrealistas. Las sombras son exactas, los reflejos siguen la fuente de luz. Es la estética del iPhone original y sigue siendo perfecta para interfaces que emulan herramientas físicas.", "Skeuomorphism"),
            new("material-design", "Material Design", "Consistencia, accesibilidad",
                "Android, apps empresariales",
                "El sistema de diseño de Google. Superficies de papel digital con elevaciones definidas, sombras que comunican jerarquía, y un sistema de componentes exhaustivamente documentado. Colores con propósito semántico, tipografía Roboto en una escala armónica y animaciones que siguen las leyes de la física. Es el framework más pragmático y accesible.", "MaterialDesign"),
            new("fluent-design", "Fluent Design", "Fluidez, inmersión",
                "Ecosistema Windows, productividad",
                "El sistema de diseño de Microsoft. Acrílico (blur translúcido), reveal highlights que siguen el cursor, profundidad con sombras y capas, y movimientos conectados entre vistas. La luz es un material de diseño: los elementos responden al hover con iluminación reveladora. Combina la funcionalidad de Office con la elegancia de un sistema operativo moderno.", "FluentDesign"),
            new("brutalism-web", "Brutalismo (Web)", "Honestidad cruda, anti-diseño",
                "Portfolios de artistas, experimental",
                "HTML crudo expuesto sin pretensiones. Tipografía por defecto del sistema (Times New Roman, Courier), fondos blancos agresivos, bordes negros de 3px, layouts que ignoran toda convención de UX. Los links son azules y subrayados. No hay hover effects elegantes. Es la antítesis del diseño polido, y en esa honestidad radical encuentra su propia belleza.", "BrutalismWeb"),
            new("anti-design", "Anti-Diseño", "Rebeldía, provocación",
                "Cultura underground, moda",
                "Rompe todas las reglas deliberadamente. Textos superpuestos e ilegibles, colores que chocan violentamente, cursores custom que confunden, y una navegación intencionalmente hostil. No es mal diseño por accidente; es destrucción artística de las convenciones. Funciona como statement cultural, no como interfaz funcional. Fascinante de ver, frustrante de usar.", "AntiDesign")
        ]),
        new(10, "X", "Artístico y Cultural", "🎭",
        [
            new("bauhaus", "Bauhaus", "Belleza funcional",
                "Arquitectura, educación de diseño",
                "La escuela que definió el diseño moderno en 1919. Formas geométricas primarias (círculo, triángulo, cuadrado), colores primarios puros contra negro y blanco, y una grilla que trata cada pixel como parte de una composición arquitectónica. Tipografía sans-serif geométrica, layouts asimétricos pero equilibrados. Función y forma son inseparables.", "Bauhaus"),
            new("constructivism", "Constructivismo", "Energía revolucionaria",
                "Político, activismo, museos",
                "El arte al servicio de la revolución. Composiciones diagonales dinámicas, paleta roja y negra sobre blanco, tipografía en ángulos dramáticos y fotomontajes propagandísticos. Los elementos parecen en movimiento constante, como carteles soviéticos de los años 20 reimaginados en formato digital. Es agresivo, es político, es imposible de ignorar.", "Constructivism"),
            new("surrealism", "Surrealismo", "Asombro, mundo onírico",
                "Galerías de arte, agencias creativas",
                "La lógica de los sueños aplicada al diseño web. Yuxtaposiciones imposibles, escalas distorsionadas, elementos que desafían la gravedad y composiciones que parecen salidas del subconsciente. Los fondos mutan suavemente, los objetos flotan sin razón. La tipografía puede ser perfectamente normal como contraste al caos visual. Es Dalí y Magritte en formato responsive.", "Surrealism"),
            new("pop-art", "Pop Art", "Diversión audaz, atractivo masivo",
                "Bienes de consumo, entretenimiento",
                "Puntos Ben-Day, colores sólidos en bloques imposiblemente saturados, contornos negros gruesos y composiciones que celebran la cultura de masas. Tipografía en bocadillos de cómic (BOOM!, POW!), iconos de cultura pop y una energía que convierte cualquier producto en un icono. Es Warhol y Lichtenstein diseñando tu landing page.", "PopArt"),
            new("abstract-expressionism", "Expresionismo Abstracto", "Emoción cruda, profundidad",
                "Arte fino, alta cultura",
                "Pinceladas expresivas como texturas de fondo, campos de color que evocan emociones sin representar nada concreto, y una escala monumental que abruma. La paleta es profunda y emocional. La tipografía es mínima y casi apologética ante la fuerza del color. Los layouts son amplios para dejar que la expresión visual domine.", "AbstractExpressionism"),
            new("japanese-minimalism", "Minimalismo Japonés", "Calma zen, vacío",
                "Té, cerámica, meditación",
                "Ma (間): el espacio vacío como elemento activo de diseño. Una asimetría deliberada que evoca el ikebana, paletas de grises cálidos con un único acento (rojo bermellón o azul índigo), y una reducción extrema de elementos. Todo lo innecesario ha sido removido dos veces. La tipografía es fina y ubicada con la precisión de una ceremonia del té.", "JapaneseMinimalism"),
            new("art-nouveau", "Art Nouveau", "Elegancia orgánica",
                "Patrimonio, cosméticos de lujo",
                "Líneas sinuosas inspiradas en la naturaleza: tallos ondulantes, flores estilizadas, formas de libélula y pavo real. Marcos ornamentales con curvas art nouveau envuelven el contenido. Paleta de verdes jade, dorados y violetas profundos. Tipografía con remates orgánicos que fluyen como enredaderas. Es la estética de Mucha y Gaudí llevada al navegador.", "ArtNouveau"),
            new("art-deco", "Art Deco", "Glamour geométrico",
                "Hoteles, coctelerías, moda",
                "Geometría lujosa de los años 20. Líneas de abanico, patrones sunburst, triángulos escalonados y simetría bilateral que evoca los rascacielos de Manhattan y los interiores de los transatlánticos. Dorado sobre negro, tipografía display con remates geométricos y ornamentos que combinan maquinaria con opulencia. Es el jazz visual del diseño.", "ArtDeco")
        ]),
        new(11, "XI", "Lúdico y Creativo", "🎪",
        [
            new("cartoon", "Lúdico / Cartoon", "Diversión, energía",
                "Marcas infantiles, gaming, comida",
                "Contornos gruesos de cómic, colores primarios y secundarios vibrantes, personajes ilustrados y animaciones exageradas con principios de animación clásica (squash & stretch). Los botones rebotan, los iconos tienen personalidad propia. La tipografía es redondeada y amigable. Es imposible visitar este sitio sin sonreír. Está diseñado para que cada interacción sea un momento de alegría.", "Cartoon"),
            new("kawaii", "Kawaii", "Ternura, afecto",
                "J-fashion, papelería, apps",
                "La estética de lo tierno llevada al extremo. Paleta de pasteles suaves (rosa bebé, lavanda, menta), esquinas ultra-redondeadas, iconos con ojos enormes y expresivos, y patrones de nubes, estrellas y corazones. La tipografía es redondeada y burbujosa. Cada elemento tiene una personalidad adorable. Es Hello Kitty diseñando una interfaz de usuario.", "Kawaii"),
            new("doodle", "Doodle / Dibujado a Mano", "Autenticidad, artesanía",
                "Educación, marcas indie",
                "Todo parece dibujado a mano con marcador. Bordes irregulares, ilustraciones estilo cuaderno de apuntes, flechas dibujadas, subrayados orgánicos y una tipografía que simula escritura manual. Las texturas de papel son omnipresentes. Los errores son intencionales y encantadores. Transmite humanidad, cercanía y una ausencia total de pretensión corporativa.", "Doodle"),
            new("sticker-collage", "Sticker / Collage", "Diversión ecléctica, DIY",
                "Redes sociales, marcas juveniles",
                "La estética de una laptop cubierta de stickers. Elementos superpuestos con bordes blancos recortados, rotaciones aleatorias, sombras que simulan pegatinas levantadas y una mezcla ecléctica de fotografía, ilustración y tipografía. Es el scrapbook digital de una generación que creció con Tumblr y Pinterest.", "Sticker"),
            new("paper-cut", "Paper Cut / Capas de Papel", "Profundidad, artesanía",
                "Invitaciones, eventos culturales",
                "Capas de papel virtuales que crean profundidad mediante sombras suaves y superposición. Cada sección parece cortada y elevada sobre la anterior. Los bordes pueden ser rectos o artesanalmente irregulares. La paleta suele ser de tonos complementarios que se oscurecen capa a capa. Es la técnica del kirigami aplicada al diseño web con un efecto 3D sorprendentemente elegante.", "PaperCut")
        ])
    ];
}
