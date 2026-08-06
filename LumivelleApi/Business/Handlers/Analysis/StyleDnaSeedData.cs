using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Analysis;

public static class StyleDnaSeedData
{
    public static List<StyleDnaDocument> All()
    {
        var dnas = new List<StyleDnaDocument>();
        var order = 0;

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Warm Editorial Minimalism", "Sıcak Editoryal Minimalizm", "Minimalisme éditorial chaud", "Minimalismo editorial cálido", "بساطة تحريرية دافئة", "Тёплый редакционный минимализм"),
            Description = T(
                "Relaxed luxury with warm earthy tones and clean silhouettes.",
                "Sıcak toprak tonları ve temiz siluetlerle rahat lüks.",
                "Un luxe décontracté aux tons terreux chauds et aux silhouettes épurées.",
                "Lujo relajado con tonos terrosos cálidos y siluetas limpias.",
                "أناقة مريحة بألوان ترابية دافئة وخطوط نظيفة.",
                "Расслабленная роскошь с тёплыми землистыми тонами и чистыми силуэтами."),
            CompatibleSeasons = ["Soft Autumn", "Warm Autumn"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#E7D7B8", "#C7A36B", "#7A6A58", "#5A4638"],
            SignaturePieces = TList(
                new[] { "Camel blazer", "Cream knit top", "Straight-leg trousers", "Gold jewelry", "Leather tote" },
                new[] { "Deve tüyü blazer", "Krem örgü üst", "Düz paça pantolon", "Altın takı", "Deri tote çanta" },
                new[] { "Blazer camel", "Haut en maille crème", "Pantalon droit", "Bijoux dorés", "Sac tote en cuir" },
                new[] { "Blazer camel", "Top de punto crema", "Pantalón recto", "Joyería dorada", "Bolso tote de cuero" },
                new[] { "بليزر بلون الجمل", "قميص كريمي محبوك", "بنطال مستقيم", "مجوهرات ذهبية", "حقيبة توت جلدية" },
                new[] { "Пиджак цвета верблюжьей шерсти", "Кремовый вязаный топ", "Прямые брюки", "Золотые украшения", "Кожаная сумка-тоут" }),
            Keywords = TList(
                new[] { "editorial", "minimal", "quiet luxury", "warm" },
                new[] { "editoryal", "minimal", "sessiz lüks", "sıcak" },
                new[] { "éditorial", "minimal", "luxe discret", "chaud" },
                new[] { "editorial", "minimalista", "lujo silencioso", "cálido" },
                new[] { "تحريري", "بسيط", "فخامة هادئة", "دافئ" },
                new[] { "редакционный", "минимализм", "тихая роскошь", "тёплый" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Clean Girl Warm", "Sıcak Clean Girl", "Clean girl chaud", "Clean girl cálida", "كلين جيرل الدافئة", "Тёплый стиль clean girl"),
            Description = T(
                "Dewy skin, slicked hair, and effortless golden-hour basics.",
                "Parlak cilt, geriye taranmış saç ve zahmetsiz altın saat temelleri.",
                "Peau lumineuse, cheveux lissés et essentiels dorés sans effort.",
                "Piel luminosa, cabello alisado y básicos dorados sin esfuerzo.",
                "بشرة مشرقة، شعر أملس، وأساسيات ذهبية بلا مجهود.",
                "Сияющая кожа, гладко зачёсанные волосы и лёгкие золотистые базовые вещи."),
            CompatibleSeasons = ["Warm Spring", "Light Spring"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#F2E4CE", "#E8C49A", "#C9A46A", "#8A6A4A"],
            SignaturePieces = TList(
                new[] { "White tank top", "Gold hoops", "Straight jeans", "Slicked bun", "Tan tote" },
                new[] { "Beyaz atlet", "Altın halka küpe", "Düz kesim kot", "Geriye taranmış topuz", "Ten rengi tote" },
                new[] { "Débardeur blanc", "Créoles dorées", "Jean droit", "Chignon lissé", "Sac tote beige" },
                new[] { "Camiseta blanca", "Aros dorados", "Jeans rectos", "Moño alisado", "Bolso tote beige" },
                new[] { "قميص أبيض بلا أكمام", "أقراط ذهبية دائرية", "جينز مستقيم", "كعكة شعر ملساء", "حقيبة توت بيج" },
                new[] { "Белый топ", "Золотые кольца-серьги", "Прямые джинсы", "Гладкий пучок", "Бежевая сумка-тоут" }),
            Keywords = TList(
                new[] { "clean girl", "dewy", "effortless", "warm" },
                new[] { "clean girl", "parlak", "zahmetsiz", "sıcak" },
                new[] { "clean girl", "lumineux", "sans effort", "chaud" },
                new[] { "clean girl", "luminoso", "sin esfuerzo", "cálido" },
                new[] { "كلين جيرل", "مشرق", "بلا مجهود", "دافئ" },
                new[] { "clean girl", "сияющий", "лёгкий", "тёплый" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Old Money", "Eski Zenginlik Şıklığı", "Vieil argent", "Dinero antiguo", "الثراء العريق", "Стиль старых денег"),
            Description = T(
                "Understated heritage wealth: tailored coats, pearls, and a palette that never shouts.",
                "Sessiz miras zenginliği: kesim ceketler, inciler ve asla bağırmayan bir palet.",
                "Une richesse discrète et patrimoniale : manteaux tailleur, perles et une palette qui ne crie jamais.",
                "Riqueza heredada y discreta: abrigos entallados, perlas y una paleta que nunca grita.",
                "ثراء موروث بلا استعراض: معاطف مفصّلة، لؤلؤ، وألوان لا ترتفع صوتها أبداً.",
                "Сдержанное родовое богатство: приталенные пальто, жемчуг и палитра, которая никогда не кричит."),
            CompatibleSeasons = ["Cool Summer", "Soft Summer"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#0B2545", "#8D99AE", "#D9D9D9", "#B08488"],
            SignaturePieces = TList(
                new[] { "Tailored wool coat", "Pearl strand necklace", "Pleated midi skirt", "Loafers", "Structured handbag" },
                new[] { "Kesim yün palto", "İnci kolye", "Pilili midi etek", "Makosen ayakkabı", "Yapılandırılmış el çantası" },
                new[] { "Manteau en laine ajusté", "Collier de perles", "Jupe midi plissée", "Mocassins", "Sac à main structuré" },
                new[] { "Abrigo de lana entallado", "Collar de perlas", "Falda midi plisada", "Mocasines", "Bolso estructurado" },
                new[] { "معطف صوف مفصّل", "عقد لؤلؤ", "تنورة ميدي مطوية", "حذاء لوفر", "حقيبة يد مهيكلة" },
                new[] { "Приталенное шерстяное пальто", "Жемчужное колье", "Плиссированная юбка миди", "Лоферы", "Структурированная сумка" }),
            Keywords = TList(
                new[] { "heritage", "tailored", "quiet wealth", "refined" },
                new[] { "miras", "kesim", "sessiz zenginlik", "zarif" },
                new[] { "patrimoine", "tailleur", "richesse discrète", "raffiné" },
                new[] { "herencia", "entallado", "riqueza silenciosa", "refinado" },
                new[] { "إرث", "مفصّل", "ثراء هادئ", "راقٍ" },
                new[] { "наследие", "приталенный", "тихое богатство", "изысканный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Parisian Soft", "Parisyen Yumuşaklık", "Douceur parisienne", "Suavidad parisina", "الرقة الباريسية", "Парижская мягкость"),
            Description = T(
                "Effortless French ease: soft knits, straight denim, and a barely-there beauty routine.",
                "Zahmetsiz Fransız rahatlığı: yumuşak triko, düz kesim kot ve neredeyse görünmeyen bir güzellik rutini.",
                "L'aisance française sans effort : mailles douces, denim droit et une routine beauté presque invisible.",
                "Facilidad francesa sin esfuerzo: punto suave, denim recto y una rutina de belleza casi invisible.",
                "أناقة فرنسية بلا مجهود: صوف ناعم، جينز مستقيم، وروتين تجميل بالكاد يُلاحظ.",
                "Непринуждённая французская лёгкость: мягкий трикотаж, прямой деним и почти незаметный бьюти-ритуал."),
            CompatibleSeasons = ["Summer", "Light Summer"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#EFE9E1", "#C9D6DF", "#E8B4B8", "#9AA5B1"],
            SignaturePieces = TList(
                new[] { "Breton striped top", "Straight-leg jeans", "Silk scarf", "Ballet flats", "Trench coat" },
                new[] { "Çizgili Breton üst", "Düz paça kot", "İpek eşarp", "Bale ayakkabısı", "Trençkot" },
                new[] { "Marinière rayée", "Jean droit", "Foulard en soie", "Ballerines", "Trench" },
                new[] { "Top de rayas bretón", "Jeans rectos", "Pañuelo de seda", "Bailarinas", "Trench" },
                new[] { "قميص مقلم بريتون", "جينز مستقيم", "وشاح حريري", "حذاء باليه مسطح", "معطف ترنش" },
                new[] { "Полосатый топ в стиле Бретань", "Прямые джинсы", "Шёлковый платок", "Балетки", "Тренч" }),
            Keywords = TList(
                new[] { "Parisian", "effortless", "classic", "soft" },
                new[] { "Parisyen", "zahmetsiz", "klasik", "yumuşak" },
                new[] { "parisien", "sans effort", "classique", "doux" },
                new[] { "parisino", "sin esfuerzo", "clásico", "suave" },
                new[] { "باريسي", "بلا مجهود", "كلاسيكي", "ناعم" },
                new[] { "парижский", "лёгкий", "классический", "мягкий" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Scandinavian Minimal", "İskandinav Minimalizmi", "Minimalisme scandinave", "Minimalismo escandinavo", "البساطة الاسكندنافية", "Скандинавский минимализм"),
            Description = T(
                "Cool, pared-back Nordic style: crisp whites, structured outerwear, and icy accents.",
                "Soğuk ve sade İskandinav tarzı: keskin beyazlar, yapılandırılmış dış giyim ve buzul detaylar.",
                "Un style nordique épuré et frais : blancs nets, vêtements d'extérieur structurés et touches glacées.",
                "Estilo nórdico frío y depurado: blancos nítidos, prendas de abrigo estructuradas y toques glaciales.",
                "أسلوب اسكندنافي بارد ومُختزل: بياض نقي، ملابس خارجية مهيكلة، ولمسات جليدية.",
                "Прохладный, лаконичный скандинавский стиль: чёткий белый цвет, структурная верхняя одежда и ледяные акценты."),
            CompatibleSeasons = ["Cool Winter", "Clear Winter"],
            CompatibleContrast = ["Medium", "High"],
            Palette = ["#F5F7FA", "#9BA4B4", "#1C1F26", "#5C7A99"],
            SignaturePieces = TList(
                new[] { "Oversized wool coat", "Crisp white shirt", "Straight trousers", "Silver jewelry", "Minimalist sneakers" },
                new[] { "Oversize yün palto", "Keskin beyaz gömlek", "Düz kesim pantolon", "Gümüş takı", "Minimalist spor ayakkabı" },
                new[] { "Manteau en laine oversize", "Chemise blanche impeccable", "Pantalon droit", "Bijoux en argent", "Baskets minimalistes" },
                new[] { "Abrigo de lana oversize", "Camisa blanca impecable", "Pantalón recto", "Joyería de plata", "Zapatillas minimalistas" },
                new[] { "معطف صوف واسع", "قميص أبيض ناصع", "بنطال مستقيم", "مجوهرات فضية", "حذاء رياضي بسيط" },
                new[] { "Oversize пальто из шерсти", "Безупречная белая рубашка", "Прямые брюки", "Серебряные украшения", "Минималистичные кроссовки" }),
            Keywords = TList(
                new[] { "Nordic", "minimal", "cool", "structured" },
                new[] { "İskandinav", "minimal", "soğuk", "yapılandırılmış" },
                new[] { "nordique", "minimal", "frais", "structuré" },
                new[] { "nórdico", "minimalista", "frío", "estructurado" },
                new[] { "اسكندنافي", "بسيط", "بارد", "مهيكل" },
                new[] { "скандинавский", "минимализм", "холодный", "структурный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Romantic Earthy", "Romantik Toprak Tonları", "Romantique terreux", "Romántico terroso", "الرومانسية الترابية", "Романтика земных тонов"),
            Description = T(
                "Flowing silhouettes and warm terracotta tones for a softly romantic, grounded look.",
                "Yumuşak romantik ve toprağa bağlı bir görünüm için akıcı siluetler ve sıcak toprak tonları.",
                "Des silhouettes fluides et des tons terracotta chauds pour un look doucement romantique et ancré.",
                "Siluetas fluidas y tonos terracota cálidos para un look suavemente romántico y arraigado.",
                "خطوط انسيابية وألوان تراكوتا دافئة لإطلالة رومانسية هادئة ومتجذّرة.",
                "Плавные силуэты и тёплые терракотовые тона для мягкого, романтичного и приземлённого образа."),
            CompatibleSeasons = ["Autumn", "Deep Autumn"],
            CompatibleContrast = ["Medium", "High"],
            Palette = ["#B5502F", "#6B6E3A", "#8C5A3C", "#3E2A1E"],
            SignaturePieces = TList(
                new[] { "Flowy midi dress", "Suede ankle boots", "Layered gold necklaces", "Wide-brim hat", "Fringed shawl" },
                new[] { "Akıcı midi elbise", "Süet bilekte bot", "Katmanlı altın kolyeler", "Geniş kenarlı şapka", "Püsküllü şal" },
                new[] { "Robe midi fluide", "Bottines en daim", "Colliers dorés superposés", "Chapeau à large bord", "Châle à franges" },
                new[] { "Vestido midi fluido", "Botines de gamuza", "Collares dorados superpuestos", "Sombrero de ala ancha", "Chal con flecos" },
                new[] { "فستان ميدي انسيابي", "حذاء بوت سويدي", "عقود ذهبية متعددة الطبقات", "قبعة واسعة الحواف", "شال مزين بالشراشيب" },
                new[] { "Струящееся платье миди", "Замшевые ботильоны", "Многослойные золотые цепочки", "Шляпа с широкими полями", "Шаль с бахромой" }),
            Keywords = TList(
                new[] { "romantic", "earthy", "flowing", "warm" },
                new[] { "romantik", "toprak tonu", "akıcı", "sıcak" },
                new[] { "romantique", "terreux", "fluide", "chaud" },
                new[] { "romántico", "terroso", "fluido", "cálido" },
                new[] { "رومانسي", "ترابي", "انسيابي", "دافئ" },
                new[] { "романтичный", "земляной", "струящийся", "тёплый" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Soft Feminine", "Yumuşak Feminen", "Douceur féminine", "Femenino suave", "الأنوثة الناعمة", "Мягкая женственность"),
            Description = T(
                "Blurred pastels, delicate lace, and rounded silhouettes for a gentle, feminine mood.",
                "Nazik ve feminen bir hava için birbirine karışan pastel tonlar, ince dantel ve yuvarlak siluetler.",
                "Des pastels fondus, une dentelle délicate et des silhouettes arrondies pour une ambiance douce et féminine.",
                "Pasteles difuminados, encaje delicado y siluetas redondeadas para un ambiente suave y femenino.",
                "درجات باستيل متداخلة، دانتيل رقيق، وخطوط دائرية لأجواء أنثوية لطيفة.",
                "Размытые пастельные тона, деликатное кружево и округлые силуэты для нежного, женственного настроения."),
            CompatibleSeasons = ["Soft Summer", "Summer"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#E9D5DA", "#C6B7C8", "#F0C9B0", "#A9758C"],
            SignaturePieces = TList(
                new[] { "Lace-trim blouse", "Pleated tea dress", "Pearl drop earrings", "Mary Jane flats", "Soft cardigan" },
                new[] { "Dantel detaylı bluz", "Pilili tea elbise", "İnci sallantılı küpe", "Mary Jane ayakkabı", "Yumuşak hırka" },
                new[] { "Chemisier à dentelle", "Robe tea plissée", "Boucles d'oreilles pendantes en perles", "Ballerines Mary Jane", "Cardigan doux" },
                new[] { "Blusa con encaje", "Vestido tea plisado", "Pendientes de perlas colgantes", "Merceditas", "Cárdigan suave" },
                new[] { "بلوزة بحواف دانتيل", "فستان تي مطوي", "أقراط لؤلؤ متدلية", "حذاء ماري جين", "سترة صوفية ناعمة" },
                new[] { "Блузка с кружевом", "Плиссированное чайное платье", "Серьги-подвески с жемчугом", "Туфли Мэри Джейн", "Мягкий кардиган" }),
            Keywords = TList(
                new[] { "feminine", "soft", "pastel", "delicate" },
                new[] { "feminen", "yumuşak", "pastel", "ince" },
                new[] { "féminin", "doux", "pastel", "délicat" },
                new[] { "femenino", "suave", "pastel", "delicado" },
                new[] { "أنثوي", "ناعم", "باستيل", "رقيق" },
                new[] { "женственный", "мягкий", "пастельный", "деликатный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Modern Classic", "Modern Klasik", "Classique moderne", "Clásico moderno", "الكلاسيكية العصرية", "Современная классика"),
            Description = T(
                "Sharp lines, high contrast, and timeless pieces reworked with a modern edge.",
                "Keskin çizgiler, yüksek kontrast ve modern bir dokunuşla yeniden yorumlanmış zamansız parçalar.",
                "Des lignes nettes, un contraste marqué et des pièces intemporelles revisitées avec une touche moderne.",
                "Líneas definidas, alto contraste y piezas atemporales reinterpretadas con un toque moderno.",
                "خطوط حادة، تباين عالٍ، وقطع خالدة أُعيد تقديمها بلمسة عصرية.",
                "Чёткие линии, высокий контраст и вневременные вещи с современным акцентом."),
            CompatibleSeasons = ["Winter", "Deep Winter"],
            CompatibleContrast = ["Medium", "High"],
            Palette = ["#0A0A0A", "#FFFFFF", "#9B111E", "#4B4B4D"],
            SignaturePieces = TList(
                new[] { "Structured blazer", "Crisp white shirt", "Tailored trousers", "Statement red lip", "Pointed pumps" },
                new[] { "Yapılandırılmış blazer", "Keskin beyaz gömlek", "Kesim pantolon", "İddialı kırmızı ruj", "Sivri burunlu topuklu" },
                new[] { "Blazer structuré", "Chemise blanche impeccable", "Pantalon tailleur", "Rouge à lèvres rouge affirmé", "Escarpins pointus" },
                new[] { "Blazer estructurado", "Camisa blanca impecable", "Pantalón de sastre", "Labial rojo statement", "Zapatos de tacón puntiagudos" },
                new[] { "بليزر مهيكل", "قميص أبيض ناصع", "بنطال كلاسيكي مفصّل", "أحمر شفاه أحمر جريء", "حذاء بكعب مدبب" },
                new[] { "Структурированный пиджак", "Безупречная белая рубашка", "Классические брюки со стрелками", "Яркая красная помада", "Туфли-лодочки с острым носом" }),
            Keywords = TList(
                new[] { "classic", "modern", "sharp", "contrast" },
                new[] { "klasik", "modern", "keskin", "kontrast" },
                new[] { "classique", "moderne", "net", "contraste" },
                new[] { "clásico", "moderno", "definido", "contraste" },
                new[] { "كلاسيكي", "عصري", "حاد", "تباين" },
                new[] { "классика", "современный", "чёткий", "контраст" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Coastal Autumn", "Kıyı Sonbaharı", "Automne côtier", "Otoño costero", "خريف الساحل", "Прибрежная осень"),
            Description = T(
                "Weathered linen and sea-glass tones bring beachy ease to a warm autumn wardrobe.",
                "Yıpranmış keten ve deniz camı tonları, sıcak sonbahar gardırobuna sahil rahatlığı katıyor.",
                "Le lin patiné et les tons de verre de mer apportent une aisance balnéaire à une garde-robe automnale chaude.",
                "El lino desgastado y los tonos de vidrio marino aportan soltura playera a un armario otoñal cálido.",
                "كتان بملمس بحري وألوان زجاج البحر تمنح خزانة الخريف الدافئة إحساساً ساحلياً مريحاً.",
                "Потёртый лён и оттенки морского стекла придают тёплому осеннему гардеробу пляжную непринуждённость."),
            CompatibleSeasons = ["Warm Autumn", "Autumn"],
            CompatibleContrast = ["Medium"],
            Palette = ["#3F6E63", "#D9C4A3", "#8A6E52", "#C1613D"],
            SignaturePieces = TList(
                new[] { "Linen wide-leg pants", "Woven raffia bag", "Faded denim jacket", "Layered shell necklace", "Suede espadrilles" },
                new[] { "Keten bol paça pantolon", "Örgü hasır çanta", "Solmuş kot ceket", "Katmanlı deniz kabuğu kolye", "Süet espadril" },
                new[] { "Pantalon large en lin", "Sac en raphia tressé", "Veste en jean délavé", "Collier de coquillages superposé", "Espadrilles en daim" },
                new[] { "Pantalón ancho de lino", "Bolso tejido de rafia", "Chaqueta vaquera desteñida", "Collar de conchas superpuesto", "Alpargatas de gamuza" },
                new[] { "بنطال كتان واسع", "حقيبة راف مضفورة", "جاكيت جينز باهت", "عقد أصداف متعدد الطبقات", "حذاء إسبادريل سويدي" },
                new[] { "Широкие льняные брюки", "Плетёная сумка из рафии", "Выцветшая джинсовая куртка", "Многослойное ожерелье из ракушек", "Замшевые эспадрильи" }),
            Keywords = TList(
                new[] { "coastal", "weathered", "warm", "relaxed" },
                new[] { "kıyı", "yıpranmış", "sıcak", "rahat" },
                new[] { "côtier", "patiné", "chaud", "décontracté" },
                new[] { "costero", "desgastado", "cálido", "relajado" },
                new[] { "ساحلي", "متآكل", "دافئ", "مريح" },
                new[] { "прибрежный", "потёртый", "тёплый", "расслабленный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Bold Winter Glamour", "Cesur Kış Göz Alıcılığı", "Glamour hivernal audacieux", "Glamour invernal audaz", "بريق الشتاء الجريء", "Смелый зимний гламур"),
            Description = T(
                "High-shine fabrics and jewel-toned drama built for maximum contrast and impact.",
                "Maksimum kontrast ve etki için parlak kumaşlar ve mücevher tonlu dramatik detaylar.",
                "Des tissus brillants et un drame aux tons de pierres précieuses pour un contraste et un impact maximal.",
                "Tejidos brillantes y dramatismo en tonos joya para lograr máximo contraste e impacto.",
                "أقمشة لامعة ودراما بألوان الأحجار الكريمة لأقصى تباين وتأثير.",
                "Блестящие ткани и драматичные драгоценные тона для максимального контраста и эффекта."),
            CompatibleSeasons = ["Deep Winter", "Winter"],
            CompatibleContrast = ["High"],
            Palette = ["#0D3B2E", "#000000", "#A6192E", "#C0C0C0"],
            SignaturePieces = TList(
                new[] { "Velvet evening gown", "Statement diamond earrings", "Sequined clutch", "Faux fur stole", "Stiletto heels" },
                new[] { "Kadife gece elbisesi", "İddialı pırlanta küpe", "Payetli el çantası", "Yapay kürk stola", "Stiletto topuk" },
                new[] { "Robe de soirée en velours", "Boucles d'oreilles diamant affirmées", "Pochette pailletée", "Étole en fausse fourrure", "Talons aiguilles" },
                new[] { "Vestido de noche de terciopelo", "Pendientes statement de diamantes", "Clutch con lentejuelas", "Estola de piel sintética", "Tacones de aguja" },
                new[] { "فستان سهرة مخملي", "أقراط ألماس جريئة", "حقيبة يد مزينة بالترتر", "وشاح فرو صناعي", "حذاء كعب رفيع" },
                new[] { "Бархатное вечернее платье", "Эффектные серьги с бриллиантами", "Клатч с пайетками", "Палантин из искусственного меха", "Туфли на шпильке" }),
            Keywords = TList(
                new[] { "glamour", "bold", "jewel-toned", "dramatic" },
                new[] { "göz alıcı", "cesur", "mücevher tonu", "dramatik" },
                new[] { "glamour", "audacieux", "ton pierre précieuse", "dramatique" },
                new[] { "glamour", "audaz", "tono joya", "dramático" },
                new[] { "بريق", "جريء", "لون الأحجار الكريمة", "درامي" },
                new[] { "гламур", "смелый", "драгоценные тона", "драматичный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Deep Autumn Opulence", "Derin Sonbahar Görkemi", "Opulence automnale profonde", "Opulencia otoñal profunda", "فخامة الخريف العميق", "Роскошь глубокой осени"),
            Description = T(
                "Saturated bronze and oxblood tones layered for a richly warm, high-impact wardrobe.",
                "Zengin ve sıcak, yüksek etkili bir gardırop için katmanlanmış doygun bronz ve öküz kanı tonları.",
                "Des tons bronze et bordeaux saturés superposés pour une garde-robe richement chaude et percutante.",
                "Tonos bronce y burdeos saturados en capas para un armario cálido, rico y de gran impacto.",
                "ألوان برونزية ونبيذية غنية متراكمة لخزانة دافئة وفاخرة وذات تأثير قوي.",
                "Насыщенные бронзовые и бордовые тона в многослойных сочетаниях для по-настоящему тёплого, эффектного гардероба."),
            CompatibleSeasons = ["Deep Autumn", "Autumn"],
            CompatibleContrast = ["High", "Medium"],
            Palette = ["#5C3A1E", "#5E1914", "#1F3B2C", "#B8860B"],
            SignaturePieces = TList(
                new[] { "Leather trench coat", "Oxblood ankle boots", "Chunky gold chain", "Velvet blazer", "Structured tote" },
                new[] { "Deri trençkot", "Öküz kanı rengi bilekte bot", "Kalın altın zincir", "Kadife blazer", "Yapılandırılmış tote çanta" },
                new[] { "Trench en cuir", "Bottines bordeaux", "Grosse chaîne dorée", "Blazer en velours", "Sac tote structuré" },
                new[] { "Trench de cuero", "Botines burdeos", "Cadena dorada gruesa", "Blazer de terciopelo", "Bolso tote estructurado" },
                new[] { "معطف ترنش جلدي", "حذاء بوت بلون النبيذي", "سلسلة ذهبية سميكة", "بليزر مخملي", "حقيبة توت مهيكلة" },
                new[] { "Кожаный тренч", "Бордовые ботильоны", "Массивная золотая цепь", "Бархатный пиджак", "Структурированная сумка-тоут" }),
            Keywords = TList(
                new[] { "opulent", "deep", "bronze", "rich" },
                new[] { "görkemli", "derin", "bronz", "zengin" },
                new[] { "opulent", "profond", "bronze", "riche" },
                new[] { "opulento", "profundo", "bronce", "rico" },
                new[] { "فاخر", "عميق", "برونزي", "غني" },
                new[] { "роскошный", "глубокий", "бронзовый", "насыщенный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Clear Spring Vivid", "Net Bahar Canlılığı", "Printemps clair vif", "Primavera clara vívida", "ربيع نقي زاهي", "Яркая чистая весна"),
            Description = T(
                "Primary-bright colors and crisp contrast for a clear, high-energy spring palette.",
                "Net ve yüksek enerjili bir bahar paleti için ana renklerin parlaklığı ve keskin kontrast.",
                "Des couleurs primaires éclatantes et un contraste net pour une palette printanière claire et énergique.",
                "Colores primarios brillantes y contraste nítido para una paleta primaveral clara y enérgica.",
                "ألوان أساسية زاهية وتباين حاد لباقة ربيعية نقية وعالية الطاقة.",
                "Яркие основные цвета и чёткий контраст для чистой, энергичной весенней палитры."),
            CompatibleSeasons = ["Clear Spring", "Spring"],
            CompatibleContrast = ["High"],
            Palette = ["#E4312B", "#0057B7", "#FFD23F", "#FFFFFF"],
            SignaturePieces = TList(
                new[] { "Cobalt blue trench", "Red patent flats", "Sunny yellow tote", "White crop top", "Statement hoop earrings" },
                new[] { "Kobalt mavisi trençkot", "Kırmızı rugan babet", "Sarı tote çanta", "Beyaz crop üst", "İddialı halka küpe" },
                new[] { "Trench bleu cobalt", "Ballerines vernies rouges", "Sac tote jaune soleil", "Crop top blanc", "Créoles affirmées" },
                new[] { "Trench azul cobalto", "Bailarinas de charol rojo", "Bolso tote amarillo sol", "Top corto blanco", "Aros statement" },
                new[] { "ترنش أزرق كوبالت", "حذاء لامع أحمر", "حقيبة توت صفراء زاهية", "توب أبيض قصير", "أقراط دائرية جريئة" },
                new[] { "Тренч кобальтового цвета", "Красные лакированные балетки", "Ярко-жёлтая сумка-тоут", "Белый укороченный топ", "Крупные серьги-кольца" }),
            Keywords = TList(
                new[] { "vivid", "clear", "bright", "energetic" },
                new[] { "canlı", "net", "parlak", "enerjik" },
                new[] { "vif", "clair", "lumineux", "énergique" },
                new[] { "vívido", "claro", "brillante", "enérgico" },
                new[] { "زاهي", "نقي", "لامع", "نشيط" },
                new[] { "яркий", "чистый", "солнечный", "энергичный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Cool Summer Ethereal", "Serin Yaz Esintisi", "Été frais éthéré", "Verano frío etéreo", "صيف بارد أثيري", "Прохладное эфемерное лето"),
            Description = T(
                "Hazy blues and cool lilacs float together for a light, dreamlike summer look.",
                "Puslu maviler ve serin leylak tonları, hafif ve rüya gibi bir yaz görünümü için bir araya geliyor.",
                "Des bleus voilés et des lilas frais se mêlent pour un look estival léger et onirique.",
                "Azules brumosos y lilas fríos se combinan para un look veraniego ligero y de ensueño.",
                "أزرق ضبابي وأرجواني بارد يمتزجان لإطلالة صيفية خفيفة وحالمة.",
                "Дымчатая синева и прохладная сирень сливаются в лёгкий, мечтательный летний образ."),
            CompatibleSeasons = ["Cool Summer", "Summer"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#B7C9E2", "#C9B6D9", "#A9AFBC", "#E3B6C1"],
            SignaturePieces = TList(
                new[] { "Chiffon midi dress", "Lilac silk scarf", "Pearl stud earrings", "Pale blue cardigan", "Suede sling-back flats" },
                new[] { "Şifon midi elbise", "Leylak rengi ipek eşarp", "İnci küpe", "Açık mavi hırka", "Süet arkası açık babet" },
                new[] { "Robe midi en mousseline", "Foulard en soie lilas", "Puces d'oreilles en perles", "Cardigan bleu pâle", "Ballerines en daim ouvertes à l'arrière" },
                new[] { "Vestido midi de gasa", "Pañuelo de seda lila", "Pendientes de perla", "Cárdigan azul pálido", "Bailarinas de gamuza abiertas atrás" },
                new[] { "فستان ميدي من الشيفون", "وشاح حريري بلون الليلك", "أقراط لؤلؤ صغيرة", "سترة زرقاء فاتحة", "حذاء سويدي مفتوح من الخلف" },
                new[] { "Платье миди из шифона", "Сиреневый шёлковый платок", "Серьги-пусеты с жемчугом", "Бледно-голубой кардиган", "Замшевые туфли с открытой пяткой" }),
            Keywords = TList(
                new[] { "ethereal", "cool", "dreamy", "light" },
                new[] { "esintili", "serin", "rüya gibi", "hafif" },
                new[] { "éthéré", "frais", "onirique", "léger" },
                new[] { "etéreo", "frío", "soñador", "ligero" },
                new[] { "أثيري", "بارد", "حالم", "خفيف" },
                new[] { "эфемерный", "прохладный", "мечтательный", "лёгкий" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Warm Spring Radiant", "Sıcak Bahar Işıltısı", "Printemps chaud radieux", "Primavera cálida radiante", "ربيع دافئ متألق", "Сияющая тёплая весна"),
            Description = T(
                "Sun-warmed coral and turquoise pair with golden skin for a radiant, energized glow.",
                "Güneşle ısınmış mercan ve turkuaz tonları, altın rengi bir cilt ile birleşerek ışıltılı ve enerjik bir görünüm yaratıyor.",
                "Le corail et le turquoise réchauffés par le soleil s'associent à une peau dorée pour un éclat radieux et énergique.",
                "El coral y el turquesa cálidos se combinan con piel dorada para un brillo radiante y enérgico.",
                "مرجاني وفيروزي دافئان يمتزجان مع بشرة ذهبية لإشراقة متألقة ونشيطة.",
                "Согретые солнцем кораллово-бирюзовые тона в сочетании с золотистой кожей создают сияющий, энергичный образ."),
            CompatibleSeasons = ["Warm Spring", "Spring"],
            CompatibleContrast = ["Medium", "High"],
            Palette = ["#FF7F50", "#FFC845", "#2FB6A8", "#FFB08A"],
            SignaturePieces = TList(
                new[] { "Coral wrap dress", "Turquoise statement necklace", "Gold hoop earrings", "Woven straw bag", "Espadrille wedges" },
                new[] { "Mercan rengi kruvaze elbise", "Turkuaz iddialı kolye", "Altın halka küpe", "Örgü hasır çanta", "Espadril dolgu topuk" },
                new[] { "Robe portefeuille corail", "Collier turquoise affirmé", "Créoles dorées", "Sac en paille tressée", "Espadrilles compensées" },
                new[] { "Vestido cruzado coral", "Collar turquesa statement", "Aros dorados", "Bolso de paja tejida", "Cuñas de esparto" },
                new[] { "فستان ملفوف مرجاني", "عقد فيروزي جريء", "أقراط ذهبية دائرية", "حقيبة قش مضفورة", "حذاء إسبادريل بكعب مرتفع" },
                new[] { "Платье на запах кораллового цвета", "Эффектное бирюзовое колье", "Золотые серьги-кольца", "Плетёная соломенная сумка", "Танкетка-эспадрильи" }),
            Keywords = TList(
                new[] { "radiant", "warm", "sunny", "energized" },
                new[] { "ışıltılı", "sıcak", "güneşli", "enerjik" },
                new[] { "radieux", "chaud", "ensoleillé", "énergique" },
                new[] { "radiante", "cálido", "soleado", "enérgico" },
                new[] { "متألق", "دافئ", "مشمس", "نشيط" },
                new[] { "сияющий", "тёплый", "солнечный", "энергичный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Soft Summer Romantic", "Yumuşak Yaz Romantizmi", "Romantisme d'été doux", "Romance de verano suave", "رومانسية صيفية ناعمة", "Мягкий летний романтизм"),
            Description = T(
                "Muted mauves and misty blues drape softly for an unhurried, romantic summer feel.",
                "Yumuşak, romantik ve sakin bir yaz hissi için soluk mor ve puslu mavi tonları yumuşakça sarmalıyor.",
                "Des mauves atténués et des bleus brumeux qui drapent en douceur pour une ambiance estivale romantique et sans hâte.",
                "Malvas apagados y azules brumosos que caen con suavidad para una sensación veraniega romántica y sin prisas.",
                "أرجواني باهت وأزرق ضبابي يتدفقان برقة لإحساس صيفي رومانسي وهادئ.",
                "Приглушённая мальва и туманная синева мягко драпируются для неторопливого, романтичного летнего настроения."),
            CompatibleSeasons = ["Soft Summer", "Light Summer"],
            CompatibleContrast = ["Low"],
            Palette = ["#B99CA4", "#A9B4A0", "#AEBBC4", "#E8CBCB"],
            SignaturePieces = TList(
                new[] { "Draped satin slip dress", "Mauve silk kimono", "Delicate layered necklaces", "Soft ballet flats", "Woven clutch" },
                new[] { "Drape saten askılı elbise", "Mor ipek kimono", "İnce katmanlı kolyeler", "Yumuşak bale ayakkabısı", "Örgü el çantası" },
                new[] { "Robe nuisette drapée en satin", "Kimono en soie mauve", "Colliers fins superposés", "Ballerines douces", "Pochette tressée" },
                new[] { "Vestido slip drapeado de satén", "Kimono de seda malva", "Collares finos superpuestos", "Bailarinas suaves", "Clutch tejido" },
                new[] { "فستان ساتان منسدل", "كيمونو حريري أرجواني", "عقود رفيعة متعددة الطبقات", "حذاء باليه ناعم", "حقيبة يد مضفورة" },
                new[] { "Драпированное сатиновое платье-комбинация", "Шёлковое кимоно цвета мальвы", "Тонкие многослойные цепочки", "Мягкие балетки", "Плетёный клатч" }),
            Keywords = TList(
                new[] { "romantic", "soft", "muted", "unhurried" },
                new[] { "romantik", "yumuşak", "soluk", "sakin" },
                new[] { "romantique", "doux", "atténué", "tranquille" },
                new[] { "romántico", "suave", "apagado", "tranquilo" },
                new[] { "رومانسي", "ناعم", "باهت", "هادئ" },
                new[] { "романтичный", "мягкий", "приглушённый", "неторопливый" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Clear Winter Graphic", "Net Kış Grafikliği", "Hiver clair graphique", "Invierno claro gráfico", "شتاء نقي بخطوط جرافيكية", "Чёткая графичная зима"),
            Description = T(
                "Stark black-and-white blocks punctuated by a single vivid pop for maximum clarity.",
                "Maksimum netlik için tek bir canlı renkle vurgulanan sert siyah beyaz bloklar.",
                "Des blocs noir et blanc tranchants ponctués d'une touche de couleur vive pour une clarté maximale.",
                "Bloques nítidos de blanco y negro puntuados por un solo toque de color vivo para máxima claridad.",
                "كتل حادة من الأبيض والأسود تتخللها لمسة لون واحدة زاهية لأقصى وضوح.",
                "Резкие чёрно-белые блоки, разбавленные единственным ярким акцентом для максимальной чёткости."),
            CompatibleSeasons = ["Clear Winter", "Winter"],
            CompatibleContrast = ["High"],
            Palette = ["#FFFFFF", "#0B0B0B", "#D6006D", "#0033A0"],
            SignaturePieces = TList(
                new[] { "Colorblock coat", "Graphic white sneakers", "Fuchsia clutch", "Black leather gloves", "Geometric earrings" },
                new[] { "Renk bloklu palto", "Grafik beyaz spor ayakkabı", "Fuşya el çantası", "Siyah deri eldiven", "Geometrik küpe" },
                new[] { "Manteau color-block", "Baskets blanches graphiques", "Pochette fuchsia", "Gants en cuir noir", "Boucles d'oreilles géométriques" },
                new[] { "Abrigo colorblock", "Zapatillas blancas gráficas", "Clutch fucsia", "Guantes de cuero negro", "Pendientes geométricos" },
                new[] { "معطف بألوان متباينة", "حذاء رياضي أبيض جرافيكي", "حقيبة يد فوشيا", "قفازات جلدية سوداء", "أقراط هندسية" },
                new[] { "Пальто в стиле колорблок", "Графичные белые кроссовки", "Клатч цвета фуксии", "Чёрные кожаные перчатки", "Геометрические серьги" }),
            Keywords = TList(
                new[] { "graphic", "clear", "bold", "striking" },
                new[] { "grafik", "net", "cesur", "çarpıcı" },
                new[] { "graphique", "clair", "audacieux", "frappant" },
                new[] { "gráfico", "claro", "audaz", "llamativo" },
                new[] { "جرافيكي", "نقي", "جريء", "لافت" },
                new[] { "графичный", "чёткий", "смелый", "яркий" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Light Spring Bloom", "Açık Bahar Çiçeklenmesi", "Floraison printanière légère", "Floración de primavera ligera", "ازدهار ربيعي فاتح", "Лёгкое весеннее цветение"),
            Description = T(
                "Featherlight pastels and blossom pinks for a fresh, delicately luminous spring look.",
                "Taze ve narin ışıltılı bir bahar görünümü için tüy gibi hafif pasteller ve çiçek pembeleri.",
                "Des pastels légers comme une plume et des roses fleuris pour un look printanier frais et délicatement lumineux.",
                "Pasteles ligeros como plumas y rosas florales para un look primaveral fresco y delicadamente luminoso.",
                "باستيل خفيف كالريشة ووردي زهري لإطلالة ربيعية منعشة ومضيئة برقة.",
                "Лёгкие, как пёрышко, пастельные тона и цветочно-розовый для свежего, деликатно светящегося весеннего образа."),
            CompatibleSeasons = ["Light Spring", "Spring"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#FFD9C0", "#B8E3C9", "#FFF3D6", "#FF9AA2"],
            SignaturePieces = TList(
                new[] { "Floral sundress", "Pastel cardigan", "Woven espadrilles", "Delicate charm bracelet", "Straw crossbody bag" },
                new[] { "Çiçekli yazlık elbise", "Pastel hırka", "Örgü espadril", "İnce şarm bilekliği", "Hasır çapraz askılı çanta" },
                new[] { "Robe d'été fleurie", "Cardigan pastel", "Espadrilles tressées", "Bracelet à breloques délicat", "Sac bandoulière en paille" },
                new[] { "Vestido veraniego floral", "Cárdigan pastel", "Alpargatas tejidas", "Pulsera de dijes delicada", "Bolso cruzado de paja" },
                new[] { "فستان صيفي بأزهار", "سترة باستيل", "حذاء إسبادريل مضفور", "سوار تعليقات رقيق", "حقيبة قش متقاطعة" },
                new[] { "Летнее платье с цветочным принтом", "Пастельный кардиган", "Плетёные эспадрильи", "Тонкий браслет с подвесками", "Соломенная сумка через плечо" }),
            Keywords = TList(
                new[] { "bloom", "light", "fresh", "delicate" },
                new[] { "çiçeklenme", "açık", "taze", "ince" },
                new[] { "floraison", "léger", "frais", "délicat" },
                new[] { "floración", "ligero", "fresco", "delicado" },
                new[] { "ازدهار", "فاتح", "منعش", "رقيق" },
                new[] { "цветение", "лёгкий", "свежий", "деликатный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Cool Winter Frost", "Serin Kış Buzu", "Givre d'hiver froid", "Escarcha de invierno frío", "صقيع شتاء بارد", "Морозная холодная зима"),
            Description = T(
                "Icy lavenders and deep charcoal meet for a crisp, frost-toned winter statement.",
                "Keskin, buz tonlu bir kış ifadesi için buzul leylak ve koyu antrasit tonları bir araya geliyor.",
                "Des lavandes glacées et un anthracite profond se rencontrent pour une déclaration hivernale nette et givrée.",
                "Lavandas heladas y carbón profundo se encuentran para una declaración invernal nítida y escarchada.",
                "أرجواني جليدي وفحمي داكن يلتقيان لإطلالة شتوية حادة بلمسة صقيعية.",
                "Ледяная лаванда и глубокий графит сходятся в чётком, морозном зимнем образе."),
            CompatibleSeasons = ["Cool Winter", "Winter"],
            CompatibleContrast = ["Medium", "High"],
            Palette = ["#D6D9F0", "#2E2E38", "#F4F6FA", "#1B3A6B"],
            SignaturePieces = TList(
                new[] { "Wool overcoat", "Cashmere scarf", "Sapphire drop earrings", "Charcoal turtleneck", "Leather ankle boots" },
                new[] { "Yün palto", "Kaşmir atkı", "Safir sallantılı küpe", "Antrasit balıkçı yaka kazak", "Deri bilekte bot" },
                new[] { "Pardessus en laine", "Écharpe en cachemire", "Boucles d'oreilles pendantes en saphir", "Col roulé anthracite", "Bottines en cuir" },
                new[] { "Abrigo de lana", "Bufanda de cachemira", "Pendientes colgantes de zafiro", "Cuello alto antracita", "Botines de cuero" },
                new[] { "معطف صوف طويل", "وشاح كشمير", "أقراط زفير متدلية", "كنزة برقبة عالية فحمية", "حذاء بوت جلدي" },
                new[] { "Шерстяное пальто", "Кашемировый шарф", "Серьги-подвески с сапфиром", "Графитовая водолазка", "Кожаные ботильоны" }),
            Keywords = TList(
                new[] { "frost", "cool", "crisp", "icy" },
                new[] { "buz", "serin", "keskin", "buzul" },
                new[] { "givre", "froid", "net", "glacé" },
                new[] { "escarcha", "frío", "nítido", "helado" },
                new[] { "صقيع", "بارد", "حاد", "جليدي" },
                new[] { "мороз", "холодный", "чёткий", "ледяной" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Soft Autumn Bohemian", "Yumuşak Sonbahar Bohemi", "Bohème automnal doux", "Bohemio otoñal suave", "بوهيمي خريفي ناعم", "Мягкая осенняя богема"),
            Description = T(
                "Muted sage and warm taupe in flowing, textured layers for an easy bohemian mood.",
                "Rahat bir bohem hava için soluk adaçayı ve sıcak taupe tonlarının akıcı, dokulu katmanları.",
                "Sauge atténuée et taupe chaude en couches fluides et texturées pour une ambiance bohème détendue.",
                "Salvia apagada y taupe cálido en capas fluidas y con textura para un ambiente bohemio relajado.",
                "أخضر مريمية باهت وبني رمادي دافئ في طبقات انسيابية وملمس غني لأجواء بوهيمية مريحة.",
                "Приглушённый шалфейный и тёплый серо-коричневый в струящихся текстурных слоях для непринуждённого богемного настроения."),
            CompatibleSeasons = ["Soft Autumn", "Autumn"],
            CompatibleContrast = ["Low", "Medium"],
            Palette = ["#8A9A78", "#B39B7E", "#D98E73", "#E8DCC4"],
            SignaturePieces = TList(
                new[] { "Crochet maxi dress", "Layered beaded necklaces", "Suede fringe bag", "Wide-brim felt hat", "Woven leather sandals" },
                new[] { "Kroşe maksi elbise", "Katmanlı boncuklu kolyeler", "Süet püsküllü çanta", "Geniş kenarlı fötr şapka", "Örgü deri sandalet" },
                new[] { "Robe longue au crochet", "Colliers de perles superposés", "Sac en daim à franges", "Chapeau en feutre à large bord", "Sandales en cuir tressé" },
                new[] { "Vestido largo de crochet", "Collares de cuentas superpuestos", "Bolso de gamuza con flecos", "Sombrero de fieltro de ala ancha", "Sandalias de cuero trenzado" },
                new[] { "فستان طويل بالكروشيه", "عقود خرزية متعددة الطبقات", "حقيبة سويدية مزينة بالشراشيب", "قبعة لباد واسعة الحواف", "صندل جلدي مضفور" },
                new[] { "Вязаное макси-платье", "Многослойные бусы", "Замшевая сумка с бахромой", "Фетровая шляпа с широкими полями", "Плетёные кожаные сандалии" }),
            Keywords = TList(
                new[] { "bohemian", "soft", "earthy", "textured" },
                new[] { "bohem", "yumuşak", "toprak tonu", "dokulu" },
                new[] { "bohème", "doux", "terreux", "texturé" },
                new[] { "bohemio", "suave", "terroso", "texturizado" },
                new[] { "بوهيمي", "ناعم", "ترابي", "ذو ملمس" },
                new[] { "богемный", "мягкий", "земляной", "текстурный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Monochrome Cool Winter", "Monokrom Serin Kış", "Hiver froid monochrome", "Invierno frío monocromático", "شتاء بارد أحادي اللون", "Монохромная холодная зима"),
            Description = T(
                "Black, white, and cool grey layered in tonal blocks for a sleek, architectural edge.",
                "Zarif ve mimari bir görünüm için ton ton bloklar halinde katmanlanmış siyah, beyaz ve soğuk gri.",
                "Noir, blanc et gris froid superposés en blocs tonals pour une allure élégante et architecturale.",
                "Negro, blanco y gris frío en capas de bloques tonales para un estilo elegante y arquitectónico.",
                "أسود وأبيض ورمادي بارد في طبقات متدرجة اللون لمظهر أنيق ومعماري.",
                "Чёрный, белый и холодный серый в тональных блоках для элегантного, архитектурного образа."),
            CompatibleSeasons = ["Cool Winter", "Clear Winter"],
            CompatibleContrast = ["High"],
            Palette = ["#000000", "#FFFFFF", "#7C8592", "#A9C6E8"],
            SignaturePieces = TList(
                new[] { "Monochrome wool suit", "Architectural leather bag", "Silver cuff bracelet", "Pointed-toe boots", "Structured turtleneck" },
                new[] { "Monokrom yün takım", "Mimari deri çanta", "Gümüş bilezik", "Sivri burunlu bot", "Yapılandırılmış balıkçı yaka" },
                new[] { "Tailleur en laine monochrome", "Sac en cuir architectural", "Bracelet manchette en argent", "Bottes à bout pointu", "Col roulé structuré" },
                new[] { "Traje de lana monocromático", "Bolso de cuero arquitectónico", "Brazalete de plata", "Botas de punta afilada", "Cuello alto estructurado" },
                new[] { "بدلة صوف أحادية اللون", "حقيبة جلدية بتصميم معماري", "سوار فضي", "حذاء بوت مدبب", "كنزة برقبة عالية مهيكلة" },
                new[] { "Монохромный шерстяной костюм", "Сумка архитектурного кроя", "Серебряный браслет-манжета", "Сапоги с острым носом", "Структурная водолазка" }),
            Keywords = TList(
                new[] { "monochrome", "cool", "sleek", "architectural" },
                new[] { "monokrom", "serin", "zarif", "mimari" },
                new[] { "monochrome", "froid", "élégant", "architectural" },
                new[] { "monocromático", "frío", "elegante", "arquitectónico" },
                new[] { "أحادي اللون", "بارد", "أنيق", "معماري" },
                new[] { "монохромный", "холодный", "элегантный", "архитектурный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Light Summer Watercolor", "Açık Yaz Suluboyası", "Aquarelle d'été léger", "Acuarela de verano ligero", "ألوان مائية صيفية فاتحة", "Акварельное лёгкое лето"),
            Description = T(
                "Soft periwinkle and seafoam blend like watercolor for an airy, gentle summer palette.",
                "Hafif ve nazik bir yaz paleti için yumuşak lavanta mavisi ve deniz köpüğü tonları suluboya gibi birbirine karışıyor.",
                "Le bleu pervenche doux et l'écume de mer se mêlent comme à l'aquarelle pour une palette estivale aérienne et douce.",
                "El azul lavanda suave y la espuma marina se mezclan como acuarela para una paleta veraniega ligera y suave.",
                "أزرق بنفسجي ناعم وأخضر رغوة البحر يمتزجان كالألوان المائية لباقة صيفية خفيفة ولطيفة.",
                "Мягкий барвинковый и пенно-морской оттенки смешиваются, как акварель, создавая воздушную, нежную летнюю палитру."),
            CompatibleSeasons = ["Light Summer", "Summer"],
            CompatibleContrast = ["Low"],
            Palette = ["#C7D3EA", "#F0D3DC", "#D9DEE3", "#C3E4DA"],
            SignaturePieces = TList(
                new[] { "Printed chiffon blouse", "Wide-leg linen pants", "Pearl bracelet", "Pastel espadrille flats", "Soft structured tote" },
                new[] { "Baskılı şifon bluz", "Bol paça keten pantolon", "İnci bilezik", "Pastel espadril babet", "Yumuşak yapılandırılmış tote çanta" },
                new[] { "Chemisier imprimé en mousseline", "Pantalon large en lin", "Bracelet de perles", "Espadrilles pastel", "Sac tote souple structuré" },
                new[] { "Blusa estampada de gasa", "Pantalón ancho de lino", "Pulsera de perlas", "Alpargatas pastel", "Bolso tote suave estructurado" },
                new[] { "بلوزة شيفون مطبوعة", "بنطال كتان واسع", "سوار لؤلؤ", "حذاء إسبادريل باستيل", "حقيبة توت ناعمة مهيكلة" },
                new[] { "Шифоновая блузка с принтом", "Широкие льняные брюки", "Жемчужный браслет", "Пастельные эспадрильи", "Мягкая структурированная сумка-тоут" }),
            Keywords = TList(
                new[] { "watercolor", "light", "airy", "gentle" },
                new[] { "suluboya", "açık", "hava gibi hafif", "nazik" },
                new[] { "aquarelle", "léger", "aérien", "doux" },
                new[] { "acuarela", "ligero", "aéreo", "suave" },
                new[] { "ألوان مائية", "فاتح", "خفيف", "لطيف" },
                new[] { "акварель", "лёгкий", "воздушный", "нежный" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Warm Autumn Harvest", "Sıcak Sonbahar Hasadı", "Récolte automnale chaude", "Cosecha de otoño cálida", "حصاد الخريف الدافئ", "Тёплый осенний урожай"),
            Description = T(
                "Pumpkin, mustard, and chestnut layered like a harvest table for cozy warmth.",
                "Rahat bir sıcaklık için hasat sofrası gibi katmanlanmış balkabağı, hardal ve kestane tonları.",
                "Citrouille, moutarde et châtaigne superposées comme une table de récolte pour une chaleur douillette.",
                "Calabaza, mostaza y castaña en capas como una mesa de cosecha para una calidez acogedora.",
                "قرع العسل والخردل والكستناء متراكمة كمائدة حصاد لدفء مريح.",
                "Тыквенный, горчичный и каштановый оттенки, наслоенные, как урожайный стол, для уютного тепла."),
            CompatibleSeasons = ["Warm Autumn", "Autumn"],
            CompatibleContrast = ["Medium"],
            Palette = ["#D9772B", "#D9A441", "#5C3A21", "#F2E3C6"],
            SignaturePieces = TList(
                new[] { "Corduroy jacket", "Chunky knit sweater", "Mustard scarf", "Chestnut leather boots", "Woven basket bag" },
                new[] { "Kadife pantolon kumaşı ceket", "Kalın örgü kazak", "Hardal rengi atkı", "Kestane rengi deri bot", "Örgü sepet çanta" },
                new[] { "Veste en velours côtelé", "Pull en grosse maille", "Écharpe moutarde", "Bottes en cuir châtaigne", "Sac panier tressé" },
                new[] { "Chaqueta de pana", "Suéter de punto grueso", "Bufanda mostaza", "Botas de cuero castaño", "Bolso cesta tejido" },
                new[] { "جاكيت من قماش الكورد", "سترة صوف سميكة", "وشاح خردلي", "حذاء بوت جلدي بلون الكستناء", "حقيبة سلة مضفورة" },
                new[] { "Вельветовый жакет", "Крупная вязка свитер", "Горчичный шарф", "Кожаные ботинки цвета каштана", "Плетёная сумка-корзина" }),
            Keywords = TList(
                new[] { "harvest", "warm", "cozy", "earthy" },
                new[] { "hasat", "sıcak", "rahat", "toprak tonu" },
                new[] { "récolte", "chaud", "douillet", "terreux" },
                new[] { "cosecha", "cálido", "acogedor", "terroso" },
                new[] { "حصاد", "دافئ", "مريح", "ترابي" },
                new[] { "урожай", "тёплый", "уютный", "земляной" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Spring Garden Fresh", "Bahar Bahçesi Tazeliği", "Fraîcheur de jardin printanier", "Frescura de jardín primaveral", "نضارة حديقة الربيع", "Свежесть весеннего сада"),
            Description = T(
                "Grass greens and tulip pinks bring a crisp, garden-fresh energy to everyday dressing.",
                "Çim yeşilleri ve lale pembeleri, günlük giyime canlı, bahçe tazeliğinde bir enerji katıyor.",
                "Des verts d'herbe et des roses tulipe apportent une énergie fraîche de jardin à l'habillement quotidien.",
                "Verdes césped y rosas tulipán aportan una energía fresca de jardín al vestir cotidiano.",
                "أخضر عشبي ووردي التوليب يمنحان الملابس اليومية طاقة نضرة كحديقة.",
                "Травянисто-зелёный и тюльпаново-розовый привносят свежую садовую энергию в повседневный образ."),
            CompatibleSeasons = ["Spring", "Warm Spring"],
            CompatibleContrast = ["Medium"],
            Palette = ["#5FA463", "#F26D85", "#FFCB3C", "#7FC1E0"],
            SignaturePieces = TList(
                new[] { "Printed shirt dress", "Green canvas sneakers", "Woven straw hat", "Pink crossbody bag", "Layered enamel bangles" },
                new[] { "Baskılı gömlek elbise", "Yeşil kanvas spor ayakkabı", "Örgü hasır şapka", "Pembe çapraz askılı çanta", "Katmanlı emaye bilezikler" },
                new[] { "Robe chemise imprimée", "Baskets en toile vertes", "Chapeau en paille tressée", "Sac bandoulière rose", "Bracelets émaillés superposés" },
                new[] { "Vestido camisero estampado", "Zapatillas de lona verdes", "Sombrero de paja tejida", "Bolso cruzado rosa", "Brazaletes esmaltados superpuestos" },
                new[] { "فستان قميصي مطبوع", "حذاء رياضي أخضر من القماش", "قبعة قش مضفورة", "حقيبة وردية متقاطعة", "أساور مطلية بالمينا متعددة" },
                new[] { "Платье-рубашка с принтом", "Зелёные текстильные кеды", "Плетёная соломенная шляпа", "Розовая сумка через плечо", "Многослойные эмалированные браслеты" }),
            Keywords = TList(
                new[] { "garden", "fresh", "crisp", "playful" },
                new[] { "bahçe", "taze", "canlı", "oyunbaz" },
                new[] { "jardin", "frais", "net", "enjoué" },
                new[] { "jardín", "fresco", "nítido", "juguetón" },
                new[] { "حديقة", "نضر", "حيوي", "مرح" },
                new[] { "сад", "свежий", "чёткий", "игривый" }),
            SortOrder = order++
        });

        dnas.Add(new StyleDnaDocument
        {
            Name = T("Golden Hour Warm Autumn", "Altın Saat Sıcak Sonbaharı", "Automne chaud à l'heure dorée", "Otoño cálido de hora dorada", "خريف دافئ بلحظة الغروب الذهبية", "Тёплая осень золотого часа"),
            Description = T(
                "Amber light and burnished bronze tones evoke a warm, glowing autumn sunset.",
                "Kehribar ışığı ve cilalanmış bronz tonları, sıcak ve ışıltılı bir sonbahar gün batımını çağrıştırıyor.",
                "La lumière ambrée et les tons bronze bruni évoquent un coucher de soleil automnal chaud et lumineux.",
                "La luz ámbar y los tonos bronce bruñido evocan un atardecer otoñal cálido y resplandeciente.",
                "ضوء العنبر وألوان البرونز اللامع يستحضران غروب خريف دافئ ومتوهج.",
                "Янтарный свет и полированные бронзовые тона напоминают о тёплом, сияющем осеннем закате."),
            CompatibleSeasons = ["Warm Autumn", "Deep Autumn"],
            CompatibleContrast = ["Medium", "High"],
            Palette = ["#D98C2B", "#A64B2A", "#8C6A3F", "#4B2E39"],
            SignaturePieces = TList(
                new[] { "Suede trench coat", "Amber statement ring", "Bronze metallic heels", "Silk wrap blouse", "Leather crossbody bag" },
                new[] { "Süet trençkot", "Kehribar iddialı yüzük", "Bronz metalik topuklu", "İpek kruvaze bluz", "Deri çapraz askılı çanta" },
                new[] { "Trench en daim", "Bague ambre affirmée", "Talons métallisés bronze", "Chemisier portefeuille en soie", "Sac bandoulière en cuir" },
                new[] { "Trench de gamuza", "Anillo statement ámbar", "Tacones metálicos bronce", "Blusa cruzada de seda", "Bolso cruzado de cuero" },
                new[] { "ترنش سويدي", "خاتم عنبري جريء", "حذاء كعب معدني برونزي", "بلوزة حريرية ملفوفة", "حقيبة جلدية متقاطعة" },
                new[] { "Замшевый тренч", "Эффектное кольцо цвета янтаря", "Бронзовые металлизированные туфли", "Шёлковая блузка на запах", "Кожаная сумка через плечо" }),
            Keywords = TList(
                new[] { "golden", "warm", "glowing", "bronze" },
                new[] { "altın", "sıcak", "ışıltılı", "bronz" },
                new[] { "doré", "chaud", "lumineux", "bronze" },
                new[] { "dorado", "cálido", "resplandeciente", "bronce" },
                new[] { "ذهبي", "دافئ", "متوهج", "برونزي" },
                new[] { "золотой", "тёплый", "сияющий", "бронзовый" }),
            SortOrder = order++
        });

        return dnas;
    }

    private static Dictionary<string, string> T(string en, string tr, string fr, string es, string ar, string ru) => new()
    {
        ["en"] = en, ["tr"] = tr, ["fr"] = fr, ["es"] = es, ["ar"] = ar, ["ru"] = ru
    };

    private static Dictionary<string, string[]> TList(
        string[] en, string[] tr, string[] fr, string[] es, string[] ar, string[] ru) => new()
    {
        ["en"] = en, ["tr"] = tr, ["fr"] = fr, ["es"] = es, ["ar"] = ar, ["ru"] = ru
    };
}
