using System.Collections.Generic;

namespace Business.Handlers.Recommendations.WordBanks;

internal static class FrPresetWordBanks
{
    // Masculine plural form — the only grammatically correct agreement for "{mood} tons" (tons is
    // masculine plural) in BuildSubtitle variant 0.
    public static readonly Dictionary<string, string> SeasonMood = new()
    {
        ["Spring"] = "Frais et lumineux",
        ["Summer"] = "Doux et frais",
        ["Autumn"] = "Chauds et riches",
        ["Winter"] = "Nets et éclatants"
    };

    // Feminine singular form — agrees with "la lumière" (feminine) in BuildDescription.
    public static readonly Dictionary<string, string> SeasonMoodFeminine = new()
    {
        ["Spring"] = "fraîche et lumineuse",
        ["Summer"] = "douce et fraîche",
        ["Autumn"] = "chaude et riche",
        ["Winter"] = "nette et éclatante"
    };

    public static readonly Dictionary<string, string> ContrastWord = new()
    {
        ["Low"] = "Doux",
        ["Medium"] = "Équilibré",
        ["High"] = "Audacieux"
    };

    public static readonly Dictionary<string, string> ContrastDescriptor = new()
    {
        ["Low"] = "doux et facile",
        ["Medium"] = "soigné et naturel",
        ["High"] = "saisissant et affirmé"
    };

    // Feminine — agrees with "ta luminosité" (feminine) in BuildDescription.
    public static readonly Dictionary<string, string> UndertoneAdjective = new()
    {
        ["Warm"] = "dorée de soleil",
        ["Cool"] = "frôlée de givre",
        ["Neutral"] = "naturellement équilibrée"
    };

    // Feminine — every BuildSubtitle use modifies a feminine noun (palette, luminosité, sélection).
    public static readonly Dictionary<string, string> UndertoneWord = new()
    {
        ["Warm"] = "chaude",
        ["Cool"] = "fraîche",
        ["Neutral"] = "neutre"
    };

    public static readonly Dictionary<string, string> ContrastLevel = new()
    {
        ["Low"] = "faible",
        ["Medium"] = "moyen",
        ["High"] = "élevé"
    };

    public static readonly Dictionary<string, string> FamilyName = new()
    {
        ["Spring"] = "printemps",
        ["Summer"] = "été",
        ["Autumn"] = "automne",
        ["Winter"] = "hiver"
    };

    public static readonly Dictionary<string, string[]> SeasonNouns = new()
    {
        ["Spring"] =
        [
            "Éclosion d'Agrumes", "Éclosion Corail", "Éclat de Pêche", "Lumière Claire", "Éclosion Fraîche",
            "Éclosion Dorée", "Prairie Lumineuse", "Rougeur de Pétale", "Rosée du Matin", "Fleur de Cerisier",
            "Lumière Mielleuse", "Prairie Printanière", "Éclat de Renoncule", "Pétale Rosé", "Éclosion Rosée",
            "Lumière Abricot", "Éclosion Sauvage", "Doux Narcisse", "Éclat d'Eau de Rose", "Éclosion de l'Aube",
            "Éclosion Pastel", "Lumière de Prairie Verte", "Éclat de Lys", "Rougeur de Tulipe"
        ],
        ["Summer"] =
        [
            "Rose Poudré", "Bleu Ardoise", "Douce Lavande", "Brise Fraîche", "Bleu Brumeux", "Rose Poudre",
            "Brume Silencieuse", "Verre de Mer", "Gris Nuage", "Brume Pervenche", "Brouillard Doux",
            "Brume Argentée", "Bleu Poudré", "Brume de Glycine", "Crépuscule Frais", "Lilas Brumeux",
            "Acier Doux", "Écume de Mer", "Rose Nuageux", "Orchidée Pâle", "Bleu Givré", "Pluie Silencieuse",
            "Perle Grise", "Iris Sourdine"
        ],
        ["Autumn"] =
        [
            "Heure Dorée", "Chaleur Terracotta", "Éclat Ambré", "Argile Épicée", "Rouille et Olive",
            "Lumière Ombrée", "Braise d'Automne", "Épice de Cannelle", "Or des Moissons", "Feuille de Bronze",
            "Éclat d'Acajou", "Chaleur de Châtaigne", "Braise Cuivrée", "Ambre Grillé", "Sienne Brûlée",
            "Blé Doré", "Éclat d'Érable", "Lumière Ocre", "Épice d'Automne", "Chaud Clou de Girofle",
            "Éclat Roux", "Bronze Mielleux", "Chaleur de Muscade", "Braise de Souci"
        ],
        ["Winter"] =
        [
            "Contraste Net", "Ton Bijou", "Lumière Givrée", "Noir Véritable", "Clarté Glaciale", "Bijou Profond",
            "Givre d'Hiver", "Nuit Saphir", "Givre Argenté", "Éclat d'Obsidienne", "Clarté Arctique",
            "Bijou de Minuit", "Profondeur d'Onyx", "Étoile Gelée", "Nuit Émeraude", "Givre de Platine",
            "Ardoise d'Hiver", "Bijou Rubis", "Lumière Glaciaire", "Améthyste Profonde", "Noir Encre",
            "Givre Cristallin", "Bijou d'Acier", "Clarté Polaire"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupTitles = new()
    {
        ["Spring"] =
        [
            "Blush corail", "Lèvres pêche", "Scintillement doré", "Éclat de rosée", "Blush abricot",
            "Eyeliner ensoleillé", "Lèvres fleuries", "Regard prairie", "Gloss miel", "Teinte cerise",
            "Scintillement pétale", "Highlighter rosée", "Paupière renoncule", "Lèvres pétale rosé",
            "Joues fleurs sauvages", "Scintillement narcisse", "Gloss eau de rose", "Blush aube",
            "Eyeliner pastel", "Highlighter lys", "Lèvres tulipe", "Eyeliner prairie verte", "Gloss abricot",
            "Joues rosée fraîche"
        ],
        ["Summer"] =
        [
            "Lèvres rose nude", "Blush poudré", "Scintillement lavande", "Éclat brumeux", "Eyeliner ardoise",
            "Blush poudre", "Lèvres brise", "Scintillement discret", "Eyeliner gris-bleu", "Voile pervenche",
            "Scintillement verre de mer", "Blush nuage", "Paupière glycine", "Eyeliner acier",
            "Highlighter écume de mer", "Lèvres rose nuageux", "Scintillement orchidée pâle",
            "Eyeliner bleu givré", "Blush pluie silencieuse", "Highlighter perle", "Paupière iris sourdine",
            "Eyeliner chambray", "Scintillement gris tourterelle", "Lèvres lilas poudré"
        ],
        ["Autumn"] =
        [
            "Lèvres terracotta", "Éclat doré", "Blush pêche", "Scintillement ambré", "Lèvres argile épicée",
            "Bronze chaud", "Eyeliner rouille", "Touche olive", "Lèvres cannelle", "Scintillement feuille de bronze",
            "Eyeliner acajou", "Blush châtaigne", "Paupière braise cuivrée", "Lèvres ambre grillé",
            "Eyeliner sienne brûlée", "Blush blé doré", "Scintillement érable", "Voile paupière ocre",
            "Eyeliner clou de girofle chaud", "Lèvres roux", "Joues bronze mielleux", "Scintillement muscade",
            "Highlighter souci", "Gloss sienne"
        ],
        ["Winter"] =
        [
            "Lèvres rouge vrai", "Highlighter glacé", "Regard bijou", "Blush givré", "Eyeliner onyx",
            "Lèvres baie", "Scintillement platine", "Fumé charbon", "Eyeliner saphir",
            "Scintillement givre argenté", "Cils obsidienne", "Lèvres prune de minuit",
            "Eyeliner profondeur d'onyx", "Highlighter étoile", "Eyeliner émeraude", "Paupière platine",
            "Regard fumé ardoise", "Lèvres rubis", "Highlighter glaciaire", "Eyeliner améthyste",
            "Cils noir encre", "Scintillement cristal", "Eyeliner bijou d'acier", "Lèvres polaire"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupSubtitles = new()
    {
        ["Spring"] =
        [
            "Joues fraîches de jour", "Nude agrumes doux", "Regard lumineux du matin", "Fini frais et lumineux",
            "Joues aux tons chauds", "Définition bronze léger", "Voile corail translucide",
            "Scintillement vert-or doux", "Lèvres dorées translucides", "Blush rose doux",
            "Voile rose léger pour les yeux", "Éclat frais peau de verre", "Voile jaune-or doux",
            "Teinte rosée translucide", "Blush multi-tons", "Paupière dorée lumineuse", "Lèvres roses nacrées",
            "Éclat pêche chaleureux", "Définition lilas douce", "Éclat frais et lumineux",
            "Corail-rose lumineux", "Définition sauge douce", "Brillance chaude translucide",
            "Blush naturel lumineux"
        ],
        ["Summer"] =
        [
            "Rose de jour sourdine", "Joues aux tons frais", "Regard doux et frais", "Fini translucide et rosé",
            "Définition fumée douce", "Blush rose frais", "Teinte mauve translucide", "Highlighter frais sourdine",
            "Définition fumée douce", "Scintillement paupière fraîche", "Highlighter frais irisé",
            "Blush frais à peine visible", "Voile violet-gris doux", "Définition gris frais",
            "Éclat irisé frais", "Teinte mauve sourdine", "Regard lavande-rose doux",
            "Définition subtile et fraîche", "Joues aux tons gris doux", "Éclat lumineux frais",
            "Voile violet frais", "Ton bleu jean doux", "Highlighter frais discret", "Teinte fraîche translucide"
        ],
        ["Autumn"] =
        [
            "Nude chaud · journée", "Regard champagne", "Blush crème", "Regard bronze", "Fini mat profond",
            "Joues dorées par le soleil", "Regard fumé chaud", "Ton terreux pour les yeux", "Nude chaud épicé",
            "Regard métallique chaud", "Définition chaude profonde", "Joues terracotta chaudes",
            "Scintillement métallique chaud", "Nude caramel chaud", "Définition chaude profonde",
            "Joues touchées de soleil", "Regard bronze chaud", "Ton chaud et terreux",
            "Définition brun épicé", "Brun-rouge chaud profond", "Blush doré chaud",
            "Ton épicé pour les yeux", "Éclat doré chaud", "Brillance terracotta chaude"
        ],
        ["Winter"] =
        [
            "Fini classique affirmé", "Scintillement frais et net", "Ton saphir profond",
            "Joues aux tons frais", "Ligne nette et précise", "Baie fraîche profonde",
            "Ton highlighter glacé", "Regard hiver dramatique", "Définition bijou profonde",
            "Highlighter métallique glacé", "Fini net et dramatique", "Baie-prune fraîche profonde",
            "Définition nette profonde", "Éclat glacé lumineux", "Définition bijou-vert profonde",
            "Scintillement métallique frais", "Fini dramatique frais", "Rouge bijou profond",
            "Éclat glacé frais", "Définition violette profonde", "Ligne de cils nette et précise",
            "Highlighter métallique glacé", "Définition métallique fraîche", "Rose givré frais"
        ]
    };

    public static readonly Dictionary<string, string[]> AccessoryPool = new()
    {
        ["Spring"] =
        [
            "Clous d'oreilles perle", "Cabas tressé", "Foulard fleuri", "Bracelet doré", "Chapeau de paille",
            "Bandeau pastel", "Créoles or rose", "Sac en toile", "Pince à cheveux marguerite", "Bracelet corail",
            "Foulard en lin", "Cabas jaune beurre", "Collier marguerite", "Nœud ruban rosé",
            "Pochette en paille tressée", "Bague émail pastel", "Barrette fleur de cerisier",
            "Foulard jaune beurre", "Boucles d'oreilles pendantes corail", "Chapeau de soleil en osier",
            "Pendentif quartz rose", "Foulard en lin pour cheveux", "Bracelet rose pétale", "Bague vigne dorée"
        ],
        ["Summer"] =
        [
            "Créoles argentées", "Veste en jean", "Foulard en lin", "Bague émail bleu", "Cabas en coton",
            "Bracelet de perles", "Étole chambray", "Pendentif verre de mer", "Collier de perles d'eau douce",
            "Écharpe cachemire grise", "Bracelet de cheville argenté", "Boucles d'oreilles verre de mer",
            "Chapeau bob en jean", "Collier coquillage", "Foulard en coton", "Bague topaze bleue",
            "Sac en raphia tressé", "Foulard bleu poudré", "Boucles d'oreilles coquillage argenté",
            "Ceinture en lin", "Chapeau de feutre gris", "Bracelet de cheville en perles", "Bandeau chambray",
            "Bague verre de mer"
        ],
        ["Autumn"] =
        [
            "Créoles dorées", "Foulard en soie", "Cabas en cuir", "Clous d'oreilles ambre", "Ceinture en daim",
            "Barrette écaille de tortue", "Manchette en cuivre", "Étole en laine", "Bottes en cuir châtaigne",
            "Sac panier tressé", "Boucles d'oreilles créoles en laiton", "Foulard en laine camel",
            "Bottines en daim", "Collier pendentif ambre", "Foulard en laine tressée",
            "Bracelet manchette bronze", "Sac bandoulière en cuir", "Boucles d'oreilles pendantes cuivre",
            "Chapeau de feutre camel", "Lunettes de soleil écaille de tortue", "Béret tricoté rouille",
            "Empilement de bagues en laiton", "Foulard en laine à carreaux", "Ceinture en cuir châtaigne"
        ],
        ["Winter"] =
        [
            "Clous d'oreilles argentés", "Foulard en velours", "Gants en cuir noir", "Bague onyx",
            "Béret en laine", "Boucles d'oreilles pendantes en cristal", "Étole en fausse fourrure",
            "Manchette en platine", "Manchette en argent sterling", "Ruban en satin noir",
            "Clous d'oreilles diamant", "Foulard en laine anthracite", "Étole en cachemire",
            "Boucles d'oreilles pendantes en perle", "Bandeau en velours noir",
            "Pendentif flocon de neige argenté", "Cache-oreilles en fausse fourrure", "Boutons de manchette onyx",
            "Gants en laine anthracite", "Épingle à cheveux en cristal", "Collier chaîne en platine",
            "Pochette en velours", "Créoles en argent sterling", "Foulard prune profond"
        ]
    };

    public static string BuildTitle(string contrast, string noun) => $"{noun} {ContrastWord[contrast]}";

    public static string BuildSubtitle(int variant, string family, string undertone, string contrast, string noun)
    {
        var mood = SeasonMood[family];
        var undertoneLower = UndertoneWord[undertone];
        var contrastLower = ContrastLevel[contrast];
        var familyLower = FamilyName[family];
        var nounLower = noun.ToLowerInvariant();
        var contrastDesc = ContrastDescriptor[contrast];

        return variant switch
        {
            0 => $"{mood} tons, ajustés pour ta palette {undertoneLower} à contraste {contrastLower}",
            1 => $"{Capitalize(contrastDesc)}, pour ta luminosité {undertoneLower}",
            2 => $"Énergie {noun}, propre à la palette {familyLower}",
            3 => $"Une sélection {undertoneLower}, à contraste {contrastLower}, inspirée par {nounLower}",
            4 => $"Le style du jour, inspiré par la saison {familyLower}, contraste {contrastLower}",
            _ => $"La sélection {undertoneLower} de Lumi, associée à {nounLower}"
        };
    }

    public static string BuildDescription(string family, string undertone, string contrast, string noun)
    {
        var moodLower = SeasonMoodFeminine[family];
        var nounLower = noun.ToLowerInvariant();
        var undertoneAdjective = UndertoneAdjective[undertone];
        var contrastDesc = ContrastDescriptor[contrast];

        return $"La lumière d'aujourd'hui est {moodLower} — Lumi a misé sur {nounLower} pour garder ta luminosité {undertoneAdjective} {contrastDesc}.";
    }

    private static string Capitalize(string value) =>
        string.IsNullOrEmpty(value) ? value : char.ToUpperInvariant(value[0]) + value[1..];
}
