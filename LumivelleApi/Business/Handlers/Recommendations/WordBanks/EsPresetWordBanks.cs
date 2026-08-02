using System.Collections.Generic;

namespace Business.Handlers.Recommendations.WordBanks;

internal static class EsPresetWordBanks
{
    public static readonly Dictionary<string, string> SeasonMood = new()
    {
        ["Spring"] = "Fresca y luminosa",
        ["Summer"] = "Suave y fresca",
        ["Autumn"] = "Cálida e intensa",
        ["Winter"] = "Nítida y clara"
    };

    public static readonly Dictionary<string, string> ContrastWord = new()
    {
        ["Low"] = "Suave",
        ["Medium"] = "Equilibrado",
        ["High"] = "Audaz"
    };

    public static readonly Dictionary<string, string> ContrastDescriptor = new()
    {
        ["Low"] = "suave y sencillo",
        ["Medium"] = "pulido y natural",
        ["High"] = "llamativo y definido"
    };

    public static readonly Dictionary<string, string> UndertoneAdjective = new()
    {
        ["Warm"] = "besado por el sol",
        ["Cool"] = "besado por la escarcha",
        ["Neutral"] = "equilibrado sin esfuerzo"
    };

    public static readonly Dictionary<string, string> UndertoneWord = new()
    {
        ["Warm"] = "cálido",
        ["Cool"] = "frío",
        ["Neutral"] = "neutro"
    };

    public static readonly Dictionary<string, string> ContrastLevel = new()
    {
        ["Low"] = "bajo",
        ["Medium"] = "medio",
        ["High"] = "alto"
    };

    public static readonly Dictionary<string, string> FamilyName = new()
    {
        ["Spring"] = "primavera",
        ["Summer"] = "verano",
        ["Autumn"] = "otoño",
        ["Winter"] = "invierno"
    };

    public static readonly Dictionary<string, string[]> SeasonNouns = new()
    {
        ["Spring"] =
        [
            "Flor Cítrica", "Flor de Coral", "Resplandor de Melocotón", "Luz Clara", "Flor Fresca",
            "Flor Dorada", "Pradera Luminosa", "Rubor de Pétalos", "Rocío Matutino", "Flor de Cerezo",
            "Luz de Miel", "Pradera de Primavera", "Resplandor de Botón de Oro", "Pétalo Sonrosado",
            "Flor Rociada", "Luz de Albaricoque", "Flor Silvestre", "Narciso Suave",
            "Resplandor de Agua de Rosas", "Flor del Amanecer", "Flor Pastel", "Luz de Pradera Verde",
            "Resplandor de Lirio", "Rubor de Tulipán"
        ],
        ["Summer"] =
        [
            "Rosa Empolvado", "Azul Pizarra", "Lavanda Suave", "Brisa Fresca", "Azul Brumoso", "Rosa Polvo",
            "Neblina Silenciosa", "Vidrio Marino", "Gris Nube", "Bruma Violácea", "Niebla Suave",
            "Neblina Plateada", "Azul Polvo", "Bruma de Glicina", "Anochecer Fresco", "Lila Brumoso",
            "Acero Suave", "Espuma de Mar", "Rosa Nublado", "Orquídea Pálida", "Azul Escarchado",
            "Lluvia Silenciosa", "Perla Gris", "Iris Apagado"
        ],
        ["Autumn"] =
        [
            "Hora Dorada", "Calidez Terracota", "Resplandor Ámbar", "Arcilla Especiada", "Óxido y Oliva",
            "Luz Terrosa", "Brasa de Otoño", "Especia de Canela", "Oro de Cosecha", "Hoja de Bronce",
            "Resplandor de Caoba", "Calidez de Castaña", "Brasa de Cobre", "Ámbar Tostado", "Siena Tostada",
            "Trigo Dorado", "Resplandor de Arce", "Luz Ocre", "Especia de Otoño", "Clavo Cálido",
            "Resplandor Rojizo", "Bronce Amielado", "Calidez de Nuez Moscada", "Brasa de Caléndula"
        ],
        ["Winter"] =
        [
            "Contraste Nítido", "Tono Joya", "Luz Escarchada", "Negro Puro", "Claridad Helada",
            "Joya Profunda", "Escarcha de Invierno", "Noche de Zafiro", "Escarcha Plateada",
            "Brillo de Obsidiana", "Claridad Ártica", "Joya de Medianoche", "Profundidad de Ónix",
            "Luz de Estrellas Congelada", "Noche Esmeralda", "Escarcha de Platino", "Pizarra de Invierno",
            "Joya de Rubí", "Luz Glacial", "Amatista Profunda", "Negro Tinta", "Escarcha de Cristal",
            "Joya de Acero", "Claridad Polar"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupTitles = new()
    {
        ["Spring"] =
        [
            "Rubor coral", "Labios melocotón", "Brillo dorado", "Resplandor de rocío", "Rubor de albaricoque",
            "Delineador soleado", "Labios en flor", "Ojos de pradera", "Brillo de miel", "Tinte cereza",
            "Brillo de pétalos", "Iluminador de rocío", "Párpado botón de oro", "Labios pétalo sonrosado",
            "Mejillas flor silvestre", "Brillo de narciso", "Brillo de agua de rosas", "Rubor de amanecer",
            "Delineador pastel", "Iluminador de lirio", "Labios tulipán", "Delineador pradera verde",
            "Brillo de albaricoque", "Mejillas rocío fresco"
        ],
        ["Summer"] =
        [
            "Labios nude rosado", "Rubor empolvado", "Brillo de lavanda", "Resplandor brumoso",
            "Delineador pizarra", "Rubor polvo", "Labios brisa", "Brillo silencioso",
            "Delineador gris azulado", "Toque violáceo", "Brillo vidrio marino", "Rubor nube",
            "Párpado glicina", "Delineador acero", "Iluminador espuma de mar", "Labios rosa nublado",
            "Brillo orquídea pálida", "Delineador azul escarchado", "Rubor lluvia silenciosa",
            "Iluminador perlado", "Párpado iris apagado", "Delineador chambray", "Brillo gris paloma",
            "Labios lila polvo"
        ],
        ["Autumn"] =
        [
            "Labios terracota", "Resplandor dorado", "Rubor durazno", "Brillo ámbar", "Labios arcilla especiada",
            "Bronce cálido", "Delineador óxido", "Toque oliva", "Labios canela", "Brillo hoja de bronce",
            "Delineador caoba", "Rubor castaña", "Párpado brasa de cobre", "Labios ámbar tostado",
            "Delineador siena tostada", "Rubor trigo dorado", "Brillo de arce", "Toque de párpado ocre",
            "Delineador clavo cálido", "Labios rojizo", "Mejillas bronce amielado", "Brillo nuez moscada",
            "Iluminador caléndula", "Brillo siena"
        ],
        ["Winter"] =
        [
            "Labios rojo puro", "Iluminador helado", "Ojos joya", "Rubor escarchado", "Delineador ónix",
            "Labios frutos rojos", "Brillo platino", "Ahumado carbón", "Delineador zafiro",
            "Brillo escarcha plateada", "Pestañas obsidiana", "Labios ciruela medianoche",
            "Delineador profundidad ónix", "Iluminador luz de estrellas", "Delineador esmeralda",
            "Párpado platino", "Ojos ahumados pizarra", "Labios rubí", "Iluminador glacial",
            "Delineador amatista", "Pestañas negro tinta", "Brillo de cristal", "Delineador joya de acero",
            "Labios polar"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupSubtitles = new()
    {
        ["Spring"] =
        [
            "Mejillas frescas de día", "Nude cítrico suave", "Ojos luminosos de mañana",
            "Acabado fresco luminoso", "Mejillas de tono cálido", "Definición bronce ligera",
            "Toque coral translúcido", "Brillo suave verde dorado", "Labios dorados translúcidos",
            "Rubor rosado suave", "Toque de ojos rosa claro", "Resplandor fresco piel de cristal",
            "Toque suave amarillo dorado", "Tinte rosado translúcido", "Rubor multitono",
            "Párpado dorado luminoso", "Labios rosados rociados", "Resplandor cálido durazno",
            "Definición lila suave", "Resplandor fresco luminoso", "Coral rosado luminoso",
            "Definición salvia suave", "Brillo cálido translúcido", "Rubor natural luminoso"
        ],
        ["Summer"] =
        [
            "Rosa apagado de día", "Mejillas de tono frío", "Ojos suaves y fríos",
            "Acabado rociado translúcido", "Definición ahumada suave", "Rubor rosado frío",
            "Tinte malva translúcido", "Iluminador frío apagado", "Definición ahumada suave",
            "Brillo frío de párpado", "Iluminador frío iridiscente", "Rubor frío casi imperceptible",
            "Toque suave gris violáceo", "Definición gris fría", "Resplandor frío iridiscente",
            "Tinte malva apagado", "Ojos lavanda-rosado suaves", "Definición fría sutil",
            "Mejillas de tono gris suave", "Resplandor frío luminoso", "Toque violeta frío",
            "Tono azul vaquero suave", "Iluminador frío discreto", "Tinte frío translúcido"
        ],
        ["Autumn"] =
        [
            "Nude cálido · de día", "Ojos champán", "Rubor cremoso", "Look de ojos bronce",
            "Acabado mate intenso", "Mejillas besadas por el sol", "Ojos ahumados cálidos",
            "Tono de ojos terroso", "Nude cálido especiado", "Ojos cálidos metalizados",
            "Definición cálida intensa", "Mejillas terracota cálidas", "Brillo metalizado cálido",
            "Nude caramelo cálido", "Definición cálida intensa", "Mejillas cálidas tocadas por el sol",
            "Ojos bronce cálidos", "Tono cálido terroso", "Definición marrón especiado",
            "Rojo-marrón cálido intenso", "Rubor dorado cálido", "Tono de ojos especiado cálido",
            "Resplandor dorado cálido", "Brillo terracota cálido"
        ],
        ["Winter"] =
        [
            "Acabado clásico audaz", "Brillo frío nítido", "Tono zafiro intenso", "Mejillas de tono frío",
            "Línea nítida y definida", "Frutos rojos fríos intensos", "Tono iluminador helado",
            "Ojos de invierno dramáticos", "Definición joya intensa", "Iluminador metalizado helado",
            "Acabado dramático nítido", "Ciruela-frutos rojos fríos intensos", "Definición nítida intensa",
            "Resplandor helado luminoso", "Definición verde joya intensa", "Brillo metalizado frío",
            "Acabado dramático frío", "Rojo joya intenso", "Resplandor frío helado",
            "Definición púrpura intensa", "Línea de pestañas nítida y definida", "Iluminador metalizado helado",
            "Definición metalizada fría", "Rosa escarchado frío"
        ]
    };

    public static readonly Dictionary<string, string[]> AccessoryPool = new()
    {
        ["Spring"] =
        [
            "Aretes de perlas", "Bolso tejido", "Pañuelo floral", "Brazalete dorado", "Sombrero de paja",
            "Diadema pastel", "Aretes de aro oro rosa", "Bolso de lona", "Broche de margarita",
            "Pulsera coral", "Pañuelo de lino para vestido de verano", "Bolso amarillo mantequilla",
            "Collar de margaritas", "Lazo de cinta rosada", "Clutch de paja tejida",
            "Anillo esmaltado pastel", "Broche de flor de cerezo", "Pañuelo amarillo mantequilla",
            "Aretes colgantes coral", "Sombrero de mimbre", "Colgante de cuarzo rosa",
            "Pañuelo de lino para cabeza", "Pulsera rosa pétalo", "Anillo dorado de enredadera"
        ],
        ["Summer"] =
        [
            "Aretes de aro plateados", "Chaqueta vaquera", "Pañuelo de lino", "Anillo esmaltado azul",
            "Bolso de algodón", "Pulsera de perlas", "Chal chambray", "Colgante de vidrio marino",
            "Collar de perlas de agua dulce", "Bufanda de cachemira gris", "Tobillera plateada",
            "Aretes de vidrio marino", "Sombrero de pescador vaquero", "Collar de conchas",
            "Pañuelo de algodón para cabeza", "Anillo de topacio azul", "Bolso de rafia tejida",
            "Pañuelo azul polvo", "Aretes de concha plateados", "Cinturón envolvente de lino",
            "Sombrero de fieltro gris", "Tobillera de perlas", "Diadema chambray", "Anillo de vidrio marino"
        ],
        ["Autumn"] =
        [
            "Aretes de aro dorados", "Pañuelo de seda", "Bolso de cuero", "Aretes de ámbar",
            "Cinturón de gamuza", "Broche de carey", "Brazalete de cobre", "Chal de lana",
            "Botas de cuero castaño", "Bolso de cesta tejida", "Aretes de aro de latón",
            "Bufanda de lana camel", "Botines de gamuza", "Collar con colgante de ámbar",
            "Bufanda de lana tejida", "Brazalete rígido de bronce", "Bolso cruzado de cuero",
            "Aretes colgantes de cobre", "Sombrero de fieltro camel", "Gafas de sol de carey",
            "Boina tejida óxido", "Set de anillos de latón", "Bufanda de lana a cuadros",
            "Cinturón de cuero castaño"
        ],
        ["Winter"] =
        [
            "Aretes plateados", "Pañuelo de terciopelo", "Guantes de cuero negro", "Anillo de ónix",
            "Boina de lana", "Aretes colgantes de cristal", "Estola de piel sintética",
            "Brazalete de platino", "Brazalete de plata esterlina", "Cinta de satén negro",
            "Aretes de diamante", "Bufanda de lana gris carbón", "Bufanda envolvente de cachemira",
            "Aretes colgantes de perlas", "Diadema de terciopelo negro", "Colgante de copo de nieve plateado",
            "Orejeras de piel sintética", "Gemelos de ónix", "Guantes de lana gris carbón",
            "Broche de cristal para el pelo", "Collar de cadena de platino", "Clutch de terciopelo",
            "Aretes de aro de plata esterlina", "Pañuelo ciruela intenso"
        ]
    };

    public static string BuildTitle(string contrast, string noun) => $"{ContrastWord[contrast]} {noun}";

    public static string BuildSubtitle(int variant, string family, string undertone, string contrast, string noun)
    {
        var moodLower = SeasonMood[family].ToLowerInvariant();
        var undertoneLower = UndertoneWord[undertone];
        var contrastLower = ContrastLevel[contrast];
        var familyLower = FamilyName[family];
        var nounLower = noun.ToLowerInvariant();
        var contrastDesc = ContrastDescriptor[contrast];

        return variant switch
        {
            0 => $"Tonos {moodLower}, ajustados a tu paleta de contraste {contrastLower} y tono {undertoneLower}",
            1 => $"{Capitalize(contrastDesc)}: tonos ideales para tu brillo {undertoneLower}",
            2 => $"Energía {nounLower}, exclusiva de la paleta {familyLower}",
            3 => $"{Capitalize(undertoneLower)}, contraste {contrastLower}, una selección inspirada en {nounLower}",
            4 => $"Inspirado en {familyLower}, el estilo de hoy con contraste {contrastLower}",
            _ => $"La elección de Lumi en tono {undertoneLower}, combinada con {nounLower}"
        };
    }

    public static string BuildDescription(string family, string undertone, string contrast, string noun)
    {
        var moodLower = SeasonMood[family].ToLowerInvariant();
        var nounLower = noun.ToLowerInvariant();
        var undertoneAdjective = UndertoneAdjective[undertone];
        var contrastDesc = ContrastDescriptor[contrast];

        return $"La luz de hoy es {moodLower} — Lumi apostó por {nounLower} para mantener tu brillo {undertoneAdjective} {contrastDesc}.";
    }

    private static string Capitalize(string value) =>
        string.IsNullOrEmpty(value) ? value : char.ToUpperInvariant(value[0]) + value[1..];
}
