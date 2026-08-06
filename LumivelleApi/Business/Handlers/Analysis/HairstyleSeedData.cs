using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Analysis;

public static class HairstyleSeedData
{
    public static List<HairstyleDocument> All()
    {
        var styles = new List<HairstyleDocument>();
        var order = 0;

        // ===== BaseCut (20) =====

        styles.Add(new HairstyleDocument
        {
            Title = T("Long Layers", "Uzun Katlar", "Longs dégradés", "Capas largas", "طبقات طويلة", "Длинные слои"),
            Description = T(
                "Soft, face-framing layers that add movement without losing length.",
                "Uzunluğu kaybetmeden hareket katan yumuşak, yüz çevresi katmanları.",
                "Des dégradés doux qui encadrent le visage et ajoutent du mouvement sans perdre en longueur.",
                "Capas suaves que enmarcan el rostro y aportan movimiento sin perder longitud.",
                "طبقات ناعمة تؤطر الوجه وتضيف حركة دون فقدان الطول.",
                "Мягкие слои, обрамляющие лицо и добавляющие движение без потери длины."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oval", "Round"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Blunt Bob", "Düz Kesim Bob", "Carré net", "Bob recto", "بوب حاد", "Прямое каре"),
            Description = T(
                "A precise, one-length bob at the jaw with a sharp horizontal line for a bold, modern shape.",
                "Çene hizasında keskin yatay bir çizgiyle cesur, modern bir görünüm sunan hassas, tek boy bob.",
                "Un carré précis, à une seule longueur au niveau de la mâchoire, avec une ligne horizontale nette pour une silhouette audacieuse et moderne.",
                "Un bob preciso, de una sola longitud a la altura de la mandíbula, con una línea horizontal marcada para una forma atrevida y moderna.",
                "قصة بوب دقيقة بطول واحد عند الفك بخط أفقي حاد لمظهر جريء وعصري.",
                "Точное каре одной длины на уровне челюсти с чёткой горизонтальной линией — смелая, современная форма."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Square", "Oblong"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Long Bob (Lob)", "Uzun Bob (Lob)", "Long carré (Lob)", "Bob largo (Lob)", "بوب طويل", "Длинное каре (лоб)"),
            Description = T(
                "A collarbone-length cut that keeps versatility while subtly slimming a fuller lower face.",
                "Çok yönlülüğü koruyan, dolgun bir alt yüz hattını hafifçe inceltilmiş gösteren köprücük kemiği boyunda kesim.",
                "Une coupe à hauteur des clavicules, polyvalente, qui affine subtilement un bas de visage plus plein.",
                "Un corte a la altura de la clavícula que mantiene la versatilidad mientras afina sutilmente un rostro más lleno en la parte inferior.",
                "قصة بطول عظمة الترقوة تحافظ على المرونة مع تنحيف خفي للجزء السفلي الممتلئ من الوجه.",
                "Стрижка длиной до ключиц, сохраняющая универсальность и слегка облегчающая более полный овал в нижней части лица."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oval", "Heart"],
            CompatibleJawlines = ["Medium", "Soft"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Pixie Crop", "Pixie Kesim", "Coupe pixie", "Corte pixie", "قصة بيكسي", "Стрижка пикси"),
            Description = T(
                "A short, close-cropped cut with soft layers at the crown for a bold, low-maintenance shape.",
                "Tepede yumuşak katmanlarla cesur, bakımı kolay bir görünüm sunan kısa, sık kesim.",
                "Une coupe courte et près de la tête, avec des dégradés doux au sommet pour une silhouette audacieuse et facile à entretenir.",
                "Un corte corto y ceñido con capas suaves en la coronilla, para una silueta atrevida y de bajo mantenimiento.",
                "قصة قصيرة ملاصقة للرأس بطبقات ناعمة عند التاج، لمظهر جريء وسهل العناية.",
                "Короткая, плотно прилегающая стрижка с мягкими слоями на макушке — смелый силуэт, не требующий сложного ухода."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oval", "Heart"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Shag Cut", "Shag Kesim", "Coupe shag", "Corte shag", "قصة شاغ", "Стрижка шэг"),
            Description = T(
                "Choppy, heavily layered mid-length cut with a lived-in fringe that breaks up strong angles.",
                "Belirgin hatları yumuşatan dağınık kahküllü, yoğun katmanlı orta boy kesim.",
                "Une coupe mi-longue très dégradée et effilée, avec une frange décontractée qui casse les angles marqués.",
                "Corte semilargo muy texturizado y en capas, con flequillo desenfadado que suaviza los ángulos marcados.",
                "قصة متوسطة الطول كثيفة الطبقات وغير منتظمة، مع غرة عفوية تكسر حدة الزوايا.",
                "Стрижка средней длины с обильным слоением и небрежной чёлкой, смягчающей резкие черты."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Round", "Square"],
            CompatibleJawlines = ["Medium", "Angular"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("A-Line Bob", "A-Kesim Bob", "Carré plongeant", "Bob asimétrico A-line", "بوب أنيق متدرج", "Каре с удлинением"),
            Description = T(
                "A bob that's shorter in back and gently angles longer toward the chin, elongating the face.",
                "Arkada daha kısa, çeneye doğru yumuşakça uzayan, yüzü uzatan bob kesim.",
                "Un carré plus court à l'arrière qui s'allonge en douceur vers le menton, allongeant le visage.",
                "Un bob más corto en la nuca que se alarga suavemente hacia la barbilla, alargando el rostro.",
                "قصة بوب أقصر من الخلف وتطول تدريجيًا نحو الذقن، مما يمنح الوجه مظهرًا أطول.",
                "Каре, короче сзади и плавно удлиняющееся к подбородку, визуально вытягивающее лицо."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Round", "Oval"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Layered Bob", "Katmanlı Bob", "Carré dégradé", "Bob en capas", "بوب متدرج", "Каре с многослойностью"),
            Description = T(
                "A chin-to-shoulder bob with internal layering that adds width at the jaw for balance.",
                "Çeneden omuza uzanan, çenede genişlik katan iç katmanlı dengeleyici bob.",
                "Un carré du menton aux épaules avec des dégradés internes qui ajoutent de la largeur au niveau de la mâchoire pour équilibrer le visage.",
                "Un bob del mentón a los hombros con capas internas que aportan anchura en la mandíbula para equilibrar el rostro.",
                "قصة بوب من الذقن إلى الكتف بطبقات داخلية تضيف عرضًا عند الفك لتحقيق التوازن.",
                "Каре от подбородка до плеч с внутренним слоением, добавляющим ширину у линии челюсти для баланса."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Diamond", "Heart"],
            CompatibleJawlines = ["Medium", "Soft"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Classic Crop", "Klasik Kısa Kesim", "Coupe classique courte", "Corte clásico corto", "قصة كلاسيكية قصيرة", "Классическая короткая стрижка"),
            Description = T(
                "A tailored short cut with clean, structured lines that highlight sharp bone structure.",
                "Belirgin kemik yapısını ön plana çıkaran, temiz ve yapılandırılmış hatlara sahip kısa kesim.",
                "Une coupe courte structurée aux lignes nettes qui met en valeur une ossature marquée.",
                "Un corte corto y estructurado, de líneas limpias, que resalta una estructura ósea marcada.",
                "قصة قصيرة أنيقة بخطوط نظيفة ومحددة تبرز تكوين العظام الحاد.",
                "Аккуратная короткая стрижка с чёткими линиями, подчёркивающая выразительные черты лица."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Square", "Diamond"],
            CompatibleJawlines = ["Angular"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Graduated Bob", "Kademeli Bob", "Carré dégradé graduel", "Bob escalonado", "بوب متدرج تصاعديًا", "Каскадное каре"),
            Description = T(
                "A stacked bob with graduated layers at the back that add width and soften a long jawline.",
                "Arkada kademeli katmanlarla genişlik katan ve uzun çeneyi yumuşatan yığılı bob.",
                "Un carré étagé avec des dégradés graduels à l'arrière qui ajoutent de la largeur et adoucissent une mâchoire allongée.",
                "Un bob escalonado con capas graduadas en la parte trasera que aportan anchura y suavizan una mandíbula alargada.",
                "قصة بوب متدرجة بطبقات متصاعدة من الخلف تضيف عرضًا وتلطف الفك الطويل.",
                "Каскадное каре с градуированными слоями сзади, добавляющими ширину и смягчающими вытянутую линию челюсти."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oblong", "Square"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Feathered Layers", "Tüylü Katmanlar", "Dégradés effilés", "Capas plumeadas", "طبقات ريشية", "Перистые слои"),
            Description = T(
                "Soft, feather-tipped layers throughout that create gentle movement and lift at the crown.",
                "Tüm boyunca yumuşak, tüy uçlu katmanlarla tepe kısmında hafif hareket ve dolgunluk yaratır.",
                "Des dégradés doux effilés sur toute la longueur, créant du mouvement et du volume au sommet.",
                "Capas suaves y afiladas en toda la melena que generan movimiento delicado y volumen en la coronilla.",
                "طبقات ناعمة بأطراف ريشية على كامل الشعر تخلق حركة لطيفة وارتفاعًا عند التاج.",
                "Мягкие, «перистые» слои по всей длине создают лёгкое движение и объём у макушки."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Round", "Oval"],
            CompatibleJawlines = ["Soft"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Blunt Lob", "Düz Kesim Lob", "Long carré net", "Lob recto", "لوب حاد", "Прямое удлинённое каре"),
            Description = T(
                "A straight-across, shoulder-length cut with a crisp hemline that adds visual width.",
                "Omuz boyunda, düz kesimli ve netlik katan bir alt hat ile görsel genişlik sağlar.",
                "Une coupe droite à hauteur d'épaules avec une ligne nette qui ajoute de la largeur visuelle.",
                "Un corte recto a la altura de los hombros con un contorno definido que aporta anchura visual.",
                "قصة مستقيمة بطول الكتف بخط حاد يضيف عرضًا بصريًا.",
                "Прямая стрижка длиной до плеч с чёткой линией среза, визуально добавляющая ширину."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oblong", "Diamond"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Textured Crop", "Dokulu Kısa Kesim", "Coupe courte texturée", "Corte corto texturizado", "قصة قصيرة بملمس", "Короткая текстурированная стрижка"),
            Description = T(
                "A short, piecey crop with textured ends that softens a pointed chin and adds fullness.",
                "Sivri çeneyi yumuşatan ve dolgunluk katan, dokulu uçlara sahip kısa kesim.",
                "Une coupe courte et effilée aux pointes texturées qui adoucit un menton pointu et ajoute du volume.",
                "Un corte corto y despuntado con puntas texturizadas que suaviza una barbilla puntiaguda y añade cuerpo.",
                "قصة قصيرة متناثرة الأطراف بملمس يلطف الذقن المدببة ويضيف كثافة.",
                "Короткая рваная стрижка с текстурированными кончиками, смягчающая заострённый подбородок и придающая объём."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Heart", "Round"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Long Layered Cut", "Uzun Katmanlı Kesim", "Coupe longue dégradée", "Corte largo en capas", "قصة طويلة متدرجة", "Длинная многослойная стрижка"),
            Description = T(
                "Long length kept intact with soft layers starting below the chin to add width at the sides.",
                "Uzunluk korunarak çenenin altından başlayan yumuşak katmanlarla yanlarda genişlik katılır.",
                "Une longueur préservée avec des dégradés doux commençant sous le menton pour ajouter de la largeur sur les côtés.",
                "Se conserva la longitud con capas suaves que comienzan bajo la barbilla para añadir anchura a los lados.",
                "طول محافظ عليه مع طبقات ناعمة تبدأ أسفل الذقن لإضافة عرض عند الجانبين.",
                "Длина сохранена, мягкие слои начинаются ниже подбородка, добавляя ширину по бокам."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oblong", "Oval"],
            CompatibleJawlines = ["Medium", "Soft"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Asymmetric Bob", "Asimetrik Bob", "Carré asymétrique", "Bob asimétrico", "بوب غير متماثل", "Асимметричное каре"),
            Description = T(
                "An angled bob, shorter on one side, that draws the eye diagonally and softens symmetry.",
                "Bir tarafı daha kısa olan, göze diyagonal bir çizgi çizen ve simetriyi yumuşatan asimetrik bob.",
                "Un carré asymétrique, plus court d'un côté, qui attire le regard en diagonale et adoucit la symétrie.",
                "Un bob asimétrico, más corto de un lado, que dirige la mirada en diagonal y suaviza la simetría.",
                "قصة بوب غير متماثلة، أقصر من جهة، توجه النظر بشكل قطري وتلطف التناظر.",
                "Асимметричное каре, короче с одной стороны, уводит взгляд по диагонали и смягчает симметрию."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Square", "Diamond"],
            CompatibleJawlines = ["Angular"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Chin-Length Bob", "Çene Boyu Bob", "Carré au menton", "Bob a la altura del mentón", "بوب بطول الذقن", "Каре по линии подбородка"),
            Description = T(
                "A rounded bob that ends right at the chin, drawing focus to the cheekbones.",
                "Tam çene hizasında biten, elmacık kemiklerine odaklanan yuvarlak bob.",
                "Un carré arrondi qui s'arrête exactement au menton, attirant l'attention sur les pommettes.",
                "Un bob redondeado que termina justo en la barbilla, dirigiendo la atención a los pómulos.",
                "قصة بوب دائرية تنتهي عند الذقن مباشرة، لتوجيه الانتباه نحو عظام الخد.",
                "Округлое каре, заканчивающееся точно на уровне подбородка, акцентирует скулы."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Round", "Heart"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Face-Framing Long Cut", "Yüz Çevresi Uzun Kesim", "Coupe longue encadrant le visage", "Corte largo que enmarca el rostro", "قصة طويلة تؤطر الوجه", "Длинная стрижка с обрамлением лица"),
            Description = T(
                "Long layers concentrated around the face that widen visually at the temples and cheeks.",
                "Şakak ve elmacık kemiklerinde görsel genişlik yaratan, yüz çevresine yoğunlaşan uzun katmanlar.",
                "Des dégradés longs concentrés autour du visage qui élargissent visuellement les tempes et les joues.",
                "Capas largas concentradas alrededor del rostro que ensanchan visualmente las sienes y las mejillas.",
                "طبقات طويلة تتركز حول الوجه وتوسّع بصريًا منطقة الصدغين والخدين.",
                "Длинные слои, сосредоточенные вокруг лица, визуально расширяют виски и скулы."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Diamond", "Heart"],
            CompatibleJawlines = ["Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Blunt Crop", "Düz Kısa Kesim", "Coupe courte nette", "Corte corto recto", "قصة قصيرة حادة", "Прямая короткая стрижка"),
            Description = T(
                "A short, straight-cut crop with minimal layering for a clean, geometric silhouette.",
                "Minimal katmanlarla temiz ve geometrik bir siluet sunan kısa, düz kesim.",
                "Une coupe courte et droite, peu dégradée, pour une silhouette nette et géométrique.",
                "Un corte corto y recto con muy poco degradado para una silueta limpia y geométrica.",
                "قصة قصيرة ومستقيمة بأقل قدر من الطبقات لمظهر نظيف وهندسي.",
                "Короткая прямая стрижка с минимумом слоёв — чёткий, геометричный силуэт."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Square", "Round"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Layered Pixie", "Katmanlı Pixie", "Pixie dégradée", "Pixie en capas", "بيكسي متدرجة", "Пикси с многослойностью"),
            Description = T(
                "A pixie with longer layers on top that can be styled forward to soften angular features.",
                "Öne doğru şekillendirilerek keskin hatları yumuşatabilen, üstte uzun katmanlı pixie.",
                "Une coupe pixie avec des mèches plus longues sur le dessus, coiffables vers l'avant pour adoucir des traits anguleux.",
                "Un pixie con mechones más largos en la parte superior que pueden peinarse hacia adelante para suavizar rasgos angulosos.",
                "قصة بيكسي بطبقات أطول من الأعلى يمكن تصفيفها للأمام لتلطيف الملامح الحادة.",
                "Пикси с более длинными прядями сверху, которые можно уложить вперёд, смягчая угловатые черты."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Oval", "Diamond"],
            CompatibleJawlines = ["Soft"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Undercut Bob", "Dipten Kesim Bob", "Carré undercut", "Bob con undercut", "بوب بقاعدة محلوقة", "Каре с андеркатом"),
            Description = T(
                "A bob with a shaved or closely cropped undersection for a striking, structured contrast.",
                "Çarpıcı, yapılandırılmış bir kontrast için traşlı ya da sık kesilmiş alt kısma sahip bob.",
                "Un carré avec une base rasée ou coupée très court pour un contraste marqué et structuré.",
                "Un bob con la base rapada o muy corta para un contraste llamativo y estructurado.",
                "قصة بوب بقاعدة محلوقة أو مقصوصة قصيرًا جدًا لتباين لافت ومنظم.",
                "Каре с выбритым или очень коротко состриженным затылком — эффектный структурный контраст."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Square", "Oblong"],
            CompatibleJawlines = ["Angular"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Soft Layers", "Yumuşak Katmanlar", "Dégradés doux", "Capas suaves", "طبقات ناعمة", "Мягкие слои"),
            Description = T(
                "Gentle, blended layers throughout that keep length while softening the overall outline.",
                "Uzunluğu koruyarak genel hatları yumuşatan, tüm boyunca yumuşak ve kaynaşık katmanlar.",
                "Des dégradés doux et fondus sur toute la longueur qui préservent la longueur tout en adoucissant le contour général.",
                "Capas suaves y difuminadas en toda la melena que conservan la longitud mientras suavizan el contorno general.",
                "طبقات ناعمة ومتدرجة بانسيابية على كامل الشعر تحافظ على الطول مع تلطيف الخطوط العامة.",
                "Мягкие, плавно переходящие слои по всей длине сохраняют длину, смягчая общий контур."),
            Category = "BaseCut",
            CompatibleFaceShapes = ["Heart", "Oval"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        // ===== Bangs (15) =====

        styles.Add(new HairstyleDocument
        {
            Title = T("Curtain Bangs", "Perde Kahküller", "Frange rideau", "Flequillo cortina", "غرة الستارة", "Челка-занавес"),
            Description = T(
                "Center-parted, face-framing bangs that soften a strong forehead.",
                "Ortadan ayrılan, güçlü bir alnı yumuşatan yüz çevresi kahküller.",
                "Frange centrée qui encadre le visage et adoucit un front marqué.",
                "Flequillo centrado que enmarca el rostro y suaviza una frente marcada.",
                "غرة مفروقة من المنتصف تؤطر الوجه وتلطف الجبهة البارزة.",
                "Челка с прямым пробором, смягчающая выразительный лоб."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Square", "Diamond", "Heart"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Wispy Bangs", "Hafif Kahküller", "Frange effilée légère", "Flequillo ligero", "غرة خفيفة", "Тонкая чёлка"),
            Description = T(
                "Thin, feathered bangs with visible gaps that add texture without overwhelming the forehead.",
                "Alnı boğmadan doku katan, aralıklı ve tüy gibi ince kahküller.",
                "Une frange fine et effilée, avec des mèches espacées qui ajoutent de la texture sans alourdir le front.",
                "Un flequillo fino y desfilado, con mechones separados que aportan textura sin sobrecargar la frente.",
                "غرة رفيعة ومتناثرة بفراغات واضحة تضيف ملمسًا دون إثقال الجبهة.",
                "Тонкая, филированная чёлка с просветами между прядями — текстура без утяжеления лба."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oval", "Round"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Full Blunt Bangs", "Dolgun Düz Kahkül", "Frange épaisse et nette", "Flequillo recto tupido", "غرة كثيفة مستقيمة", "Густая прямая чёлка"),
            Description = T(
                "A dense, straight-across fringe that shortens the appearance of a long forehead.",
                "Uzun bir alnın görünümünü kısaltan, sık ve düz kesimli kahkül.",
                "Une frange dense et droite qui raccourcit visuellement un front allongé.",
                "Un flequillo denso y recto que acorta visualmente una frente alargada.",
                "غرة كثيفة ومستقيمة تقصّر مظهر الجبهة الطويلة.",
                "Густая прямая чёлка, зрительно укорачивающая длинный лоб."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oblong", "Square"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Side-Swept Bangs", "Yana Taranmış Kahkül", "Frange balayée sur le côté", "Flequillo peinado hacia un lado", "غرة مسدولة جانبيًا", "Косая чёлка"),
            Description = T(
                "A diagonal fringe swept to one side that covers a wider forehead and narrows the top of the face.",
                "Geniş bir alnı örten ve yüzün üst kısmını daraltan, bir yana taranan diyagonal kahkül.",
                "Une frange diagonale rabattue d'un côté, qui couvre un front plus large et affine le haut du visage.",
                "Un flequillo diagonal peinado hacia un lado que cubre una frente más ancha y estrecha la parte superior del rostro.",
                "غرة مائلة تُمشّط إلى جانب واحد تغطي الجبهة الواسعة وتُضيّق الجزء العلوي من الوجه.",
                "Диагональная чёлка, зачёсанная набок, скрывает широкий лоб и сужает верхнюю часть лица."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Heart", "Diamond"],
            CompatibleJawlines = ["Medium", "Angular"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Micro Bangs", "Mikro Kahkül", "Micro-frange", "Microflequillo", "غرة قصيرة جدًا", "Микро-чёлка"),
            Description = T(
                "A very short, high-set fringe that sits above the brows for a graphic, confident statement.",
                "Kaşların üzerinde duran, çok kısa ve yüksek konumlu, cesur bir görünüm sunan kahkül.",
                "Une frange très courte, placée haut au-dessus des sourcils, pour un effet graphique et affirmé.",
                "Un flequillo muy corto y alto, situado por encima de las cejas, para un efecto gráfico y seguro.",
                "غرة قصيرة جدًا ومرتفعة فوق الحاجبين لإطلالة جريئة وواضحة.",
                "Очень короткая, высоко посаженная чёлка над бровями — графичное, уверенное заявление."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oval", "Round"],
            CompatibleJawlines = ["Soft"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Long Side Bangs", "Uzun Yan Kahkül", "Longue frange sur le côté", "Flequillo lateral largo", "غرة جانبية طويلة", "Длинная косая чёлка"),
            Description = T(
                "An elongated fringe blended into the length on one side, softening a strong jaw diagonally.",
                "Bir yana doğru uzunluğa karışan, güçlü bir çeneyi diyagonal olarak yumuşatan uzun kahkül.",
                "Une frange allongée fondue dans la longueur d'un côté, qui adoucit une mâchoire marquée en diagonale.",
                "Un flequillo alargado que se funde con la melena de un lado, suavizando en diagonal una mandíbula marcada.",
                "غرة طويلة تندمج مع طول الشعر من جانب واحد، ما يلطف الفك القوي بشكل قطري.",
                "Удлинённая чёлка, плавно переходящая в основную длину с одной стороны, по диагонали смягчает выраженную челюсть."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Square", "Oblong"],
            CompatibleJawlines = ["Angular"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Feathered Bangs", "Tüylü Kahkül", "Frange effilée", "Flequillo plumeado", "غرة ريشية", "Перистая чёлка"),
            Description = T(
                "Layered, airy bangs that taper at the edges for a soft, blended frame around the eyes.",
                "Kenarlarda incelen, gözler etrafında yumuşak bir çerçeve oluşturan katmanlı ve hafif kahkül.",
                "Une frange légère et dégradée, effilée sur les bords, pour un encadrement doux autour des yeux.",
                "Un flequillo ligero y en capas, afinado en los bordes, para un marco suave alrededor de los ojos.",
                "غرة خفيفة ومتدرجة تنحف عند الأطراف لتشكل إطارًا ناعمًا حول العينين.",
                "Слоистая, лёгкая чёлка с истончёнными краями создаёт мягкое обрамление вокруг глаз."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Round", "Heart"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Baby Bangs", "Kısa Retro Kahkül", "Frange très courte", "Flequillo muy corto", "غرة قصيرة جدًا فوق الحاجب", "Ретро-чёлка"),
            Description = T(
                "A blunt, brow-skimming fringe cut straight across for a striking retro-inspired look.",
                "Kaşları hafifçe geçen, düz kesilmiş retro esintili çarpıcı kahkül.",
                "Une frange droite et nette, effleurant les sourcils, pour un look rétro saisissant.",
                "Un flequillo recto y neto, que roza las cejas, para un look retro llamativo.",
                "غرة مستقيمة تلامس الحاجبين بقصة حادة لإطلالة ملفتة مستوحاة من الطراز القديم.",
                "Прямая, чёткая чёлка чуть выше бровей — эффектный ретро-образ."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oval", "Diamond"],
            CompatibleJawlines = ["Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Choppy Bangs", "Dağınık Kahkül", "Frange effilée irrégulière", "Flequillo desfilado irregular", "غرة غير منتظمة", "Рваная чёлка"),
            Description = T(
                "Uneven, razor-cut bangs with irregular edges that break up strong horizontal lines.",
                "Belirgin yatay hatları bölen, düzensiz kenarlı, jiletle kesilmiş kahkül.",
                "Une frange effilée au rasoir, aux bords irréguliers, qui casse les lignes horizontales marquées.",
                "Un flequillo cortado a navaja, de bordes irregulares, que rompe las líneas horizontales marcadas.",
                "غرة مقصوصة بالموس بحواف غير منتظمة تكسر الخطوط الأفقية القوية.",
                "Рваная чёлка, состриженная бритвой, с неровными краями — разбивает жёсткие горизонтальные линии."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Square", "Round"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Piecey Fringe", "Tutamlı Kahkül", "Frange texturée séparée", "Flequillo texturizado separado", "غرة منفصلة الخصل", "Разделённая чёлка"),
            Description = T(
                "A separated, textured fringe styled in defined pieces that adds width across the forehead.",
                "Alına genişlik katan, belirgin tutamlar halinde şekillendirilen ayrık ve dokulu kahkül.",
                "Une frange texturée et séparée, coiffée en mèches définies, qui ajoute de la largeur au front.",
                "Un flequillo texturizado y separado, peinado en mechones definidos, que añade anchura a la frente.",
                "غرة ذات ملمس ومنفصلة إلى خصل محددة تضيف عرضًا عبر الجبهة.",
                "Разделённая на пряди текстурированная чёлка добавляет ширины в области лба."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oblong", "Oval"],
            CompatibleJawlines = ["Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Rounded Bangs", "Yuvarlak Kahkül", "Frange arrondie", "Flequillo redondeado", "غرة مقوسة", "Округлая чёлка"),
            Description = T(
                "A gently curved fringe that follows the brow line and rounds off angular upper features.",
                "Kaş çizgisini takip eden ve köşeli üst hatları yuvarlaklaştıran hafifçe kavisli kahkül.",
                "Une frange légèrement incurvée qui suit la ligne des sourcils et arrondit des traits supérieurs anguleux.",
                "Un flequillo ligeramente curvado que sigue la línea de las cejas y redondea rasgos superiores angulosos.",
                "غرة منحنية بلطف تتبع خط الحاجب وتُدوّر الملامح العلوية الحادة.",
                "Слегка изогнутая чёлка, повторяющая линию бровей, смягчает угловатые верхние черты лица."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Heart", "Square"],
            CompatibleJawlines = ["Soft", "Angular"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Layered Fringe", "Katmanlı Kahkül", "Frange dégradée", "Flequillo en capas", "غرة متدرجة", "Многослойная чёлка"),
            Description = T(
                "A multi-length fringe blended in layers that narrows a wide forehead without heaviness.",
                "Geniş bir alnı ağırlık katmadan daraltan, katmanlı ve çok uzunluklu kahkül.",
                "Une frange multi-longueurs, fondue en dégradé, qui affine un front large sans effet lourd.",
                "Un flequillo de varias longitudes, difuminado en capas, que estrecha una frente ancha sin pesadez.",
                "غرة متعددة الأطوال ومتدرجة تُضيّق الجبهة الواسعة دون ثقل.",
                "Многослойная чёлка разной длины сужает широкий лоб, не утяжеляя образ."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Diamond", "Round"],
            CompatibleJawlines = ["Medium", "Soft"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("See-Through Bangs", "Şeffaf Kahkül", "Frange transparente", "Flequillo transparente", "غرة شفافة", "Прозрачная чёлка"),
            Description = T(
                "A sheer, lightweight fringe with visible skin beneath, adding softness without bulk.",
                "Altındaki cildi hafifçe gösteren, ince ve hafif, hacim katmadan yumuşaklık veren kahkül.",
                "Une frange légère et transparente, laissant entrevoir la peau, pour de la douceur sans volume.",
                "Un flequillo ligero y transparente que deja entrever la piel, aportando suavidad sin volumen.",
                "غرة خفيفة وشفافة تُظهر البشرة تحتها، تضيف نعومة دون كثافة زائدة.",
                "Лёгкая, полупрозрачная чёлка, сквозь которую проглядывает кожа, — мягкость без объёма."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oval", "Heart"],
            CompatibleJawlines = ["Soft"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Angled Bangs", "Açılı Kahkül", "Frange en diagonale", "Flequillo en diagonal", "غرة قطرية", "Диагональная чёлка"),
            Description = T(
                "A diagonally cut fringe, longer on one side, that echoes and complements sharp cheekbones.",
                "Bir yanı daha uzun olan, keskin elmacık kemiklerini tamamlayan diyagonal kesimli kahkül.",
                "Une frange coupée en diagonale, plus longue d'un côté, qui fait écho à des pommettes marquées.",
                "Un flequillo cortado en diagonal, más largo de un lado, que resalta unos pómulos marcados.",
                "غرة مقصوصة بشكل قطري، أطول من جهة واحدة، تنسجم مع عظام الخد الحادة.",
                "Диагонально состриженная чёлка, длиннее с одной стороны, перекликается с чёткими скулами."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Square", "Diamond"],
            CompatibleJawlines = ["Angular"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Textured Fringe", "Dokulu Kahkül", "Frange texturée", "Flequillo texturizado", "غرة ذات ملمس", "Текстурированная чёлка"),
            Description = T(
                "A soft, brushed-out fringe with irregular texture that adds fullness across a narrow forehead.",
                "Dar bir alına dolgunluk katan, düzensiz dokulu ve fırçalanmış yumuşak kahkül.",
                "Une frange douce et texturée, brossée de façon irrégulière, qui apporte du volume à un front étroit.",
                "Un flequillo suave y texturizado, cepillado de forma irregular, que aporta cuerpo a una frente estrecha.",
                "غرة ناعمة ذات ملمس غير منتظم ومصففة بالفرشاة تضيف كثافة لجبهة ضيقة.",
                "Мягкая текстурированная чёлка с неровной укладкой добавляет объёма узкому лбу."),
            Category = "Bangs",
            CompatibleFaceShapes = ["Oblong", "Round"],
            CompatibleJawlines = ["Medium", "Soft"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        // ===== Texture (10) =====

        styles.Add(new HairstyleDocument
        {
            Title = T("Beach Waves", "Plaj Dalgaları", "Vagues plage", "Ondas de playa", "تموجات الشاطئ", "Пляжные волны"),
            Description = T(
                "Loose, undone texture that adds volume and a relaxed finish.",
                "Hacim ve rahat bir bitiş katan gevşek, düzensiz doku.",
                "Une texture relâchée et naturelle qui ajoute du volume pour une finition décontractée.",
                "Textura suelta y natural que aporta volumen con un acabado relajado.",
                "ملمس منسدل وطبيعي يضيف حجمًا بلمسة نهائية مريحة.",
                "Небрежная текстура, добавляющая объём и создающая расслабленный вид."),
            Category = "Texture",
            CompatibleFaceShapes = ["Oval", "Heart", "Round"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Natural Curls", "Doğal Bukleler", "Boucles naturelles", "Rizos naturales", "تجعيدات طبيعية", "Натуральные локоны"),
            Description = T(
                "Curls enhanced in their natural pattern, defined and separated for bounce without frizz.",
                "Doğal deseninde belirginleştirilen, uçuşmadan hareketli görünen tanımlı ve ayrık bukleler.",
                "Des boucles rehaussées dans leur motif naturel, définies et séparées pour du rebond sans frisottis.",
                "Rizos realzados en su patrón natural, definidos y separados para dar rebote sin encrespamiento.",
                "تجعيدات مُعززة بنمطها الطبيعي، محددة ومنفصلة لحيوية دون تجعد مزعج.",
                "Локоны, подчёркнутые в естественном узоре, чётко разделённые — упругость без пушистости."),
            Category = "Texture",
            CompatibleFaceShapes = ["Round", "Oval"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Sleek Straight", "Parlak Düz Saç", "Lisse brillant", "Liso brillante", "استقامة ناعمة لامعة", "Гладкая прямая укладка"),
            Description = T(
                "Smooth, glass-flat strands with a polished finish that elongate the overall line of the face.",
                "Yüzün genel hattını uzatan, pürüzsüz ve cam gibi düz, cilalı bir bitişe sahip teller.",
                "Des mèches lisses, plates et brillantes, au fini poli, qui allongent la ligne générale du visage.",
                "Mechones lisos y planos como el cristal, con un acabado pulido que alarga la línea general del rostro.",
                "خصل ملساء ومستقيمة كالزجاج بلمسة نهائية لامعة تُطيل الخط العام للوجه.",
                "Гладкие, идеально прямые пряди с зеркальным блеском визуально вытягивают линию лица."),
            Category = "Texture",
            CompatibleFaceShapes = ["Square", "Oblong"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Voluminous Blowout", "Hacimli Fön", "Brushing volumateur", "Secado con volumen", "تصفيف بحجم كبير", "Объёмная укладка феном"),
            Description = T(
                "A round-brush blowout that lifts at the roots and adds fullness through the mid-lengths.",
                "Diplerde dolgunluk yaratan ve orta uzunluklarda hacim katan, yuvarlak fırçayla şekillendirilen fön.",
                "Un brushing volume, réalisé à la brosse ronde, qui soulève les racines et apporte du corps aux longueurs médianes.",
                "Un secado con volumen realizado con cepillo redondo, que levanta las raíces y aporta cuerpo a las medias longitudes.",
                "تصفيف بالفرشاة المستديرة يرفع الجذور ويضيف كثافة عند منتصف الشعر.",
                "Укладка круглой щёткой с приподнятыми корнями добавляет объёма по длине волос."),
            Category = "Texture",
            CompatibleFaceShapes = ["Heart", "Diamond"],
            CompatibleJawlines = ["Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Tousled Waves", "Dağınık Dalgalar", "Vagues décoiffées", "Ondas despeinadas", "تموجات عفوية", "Растрёпанные волны"),
            Description = T(
                "Loosely waved strands with a windswept finish that softens straighter, angular lines.",
                "Rüzgarda savrulmuş gibi görünen, daha düz ve köşeli hatları yumuşatan gevşek dalgalı teller.",
                "Des mèches légèrement ondulées, au fini décoiffé, qui adoucissent des lignes plus droites et anguleuses.",
                "Mechones ligeramente ondulados, con un acabado despeinado, que suavizan líneas más rectas y angulosas.",
                "خصل متموجة بخفة بلمسة عفوية كأنها من الريح، تلطف الخطوط المستقيمة والحادة.",
                "Слегка волнистые пряди с эффектом «растрёпанных ветром» смягчают прямые, угловатые линии."),
            Category = "Texture",
            CompatibleFaceShapes = ["Oval", "Square"],
            CompatibleJawlines = ["Angular", "Medium"],
            CompatibleDensities = ["Low", "Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Defined Curls", "Belirgin Bukleler", "Boucles définies", "Rizos definidos", "تجعيدات محددة", "Чёткие локоны"),
            Description = T(
                "Tight, well-formed ringlets that add height and structure at the crown.",
                "Tepe kısmında yükseklik ve yapı katan, sıkı ve iyi şekillenmiş bukle halkaları.",
                "Des anglaises bien formées et serrées, qui apportent hauteur et structure au sommet.",
                "Rizos apretados y bien formados que aportan altura y estructura en la coronilla.",
                "تجعيدات حلزونية محكمة ومُشكّلة جيدًا تضيف ارتفاعًا وبنية عند التاج.",
                "Плотные, чётко оформленные локоны-колечки добавляют высоты и структуры на макушке."),
            Category = "Texture",
            CompatibleFaceShapes = ["Round", "Diamond"],
            CompatibleJawlines = ["Soft"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Glass Hair Straight", "Cam Saç Efekti", "Cheveux effet miroir", "Cabello efecto espejo", "استقامة بلمعان زجاجي", "Стеклянный блеск"),
            Description = T(
                "An ultra-smooth, reflective straight finish achieved with a flat iron for a mirror-like shine.",
                "Düzleştirici ile elde edilen, ayna gibi parlayan, son derece pürüzsüz düz bitiş.",
                "Un fini lisse ultra réfléchissant, obtenu au lisseur, pour un éclat miroir.",
                "Un acabado ultraliso y reflectante, logrado con la plancha, para un brillo espejo.",
                "لمسة نهائية فائقة الملاسة وعاكسة للضوء تُحقق بمكواة التمليس لبريق يشبه المرآة.",
                "Сверхгладкая, зеркально блестящая укладка утюжком."),
            Category = "Texture",
            CompatibleFaceShapes = ["Oblong", "Oval"],
            CompatibleJawlines = ["Medium"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Braided Texture", "Örgü Dokusu", "Texture tressée", "Textura trenzada", "ملمس الضفائر", "Текстура из кос"),
            Description = T(
                "Loosely braided-out waves that create soft crimped texture and gentle volume.",
                "Yumuşak kıvrımlı doku ve hafif hacim yaratan, gevşek örgülerden açılmış dalgalar.",
                "Des ondulations obtenues en défaisant des tresses lâches, créant une texture douce et crêpée avec du volume.",
                "Ondas obtenidas al deshacer trenzas sueltas, creando una textura suave y ondulada con volumen.",
                "تموجات ناتجة عن فك ضفائر فضفاضة تخلق ملمسًا ناعمًا مموجًا وحجمًا لطيفًا.",
                "Волны, полученные из распущенных свободных косичек, создают мягкую гофрированную текстуру и лёгкий объём."),
            Category = "Texture",
            CompatibleFaceShapes = ["Heart", "Round"],
            CompatibleJawlines = ["Soft", "Medium"],
            CompatibleDensities = ["Medium"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Crimped Texture", "Kırışık Doku", "Texture crêpée", "Textura ondulada crespa", "ملمس متعرج", "Гофрированная текстура"),
            Description = T(
                "Fine, zigzag crimped texture throughout that adds visual volume without heaviness.",
                "Görsel hacim katan, ağırlık yaratmayan ince zikzak kıvrımlı doku.",
                "Une texture crêpée fine en zigzag sur toute la chevelure, qui ajoute du volume visuel sans lourdeur.",
                "Una textura ondulada fina en zigzag por toda la melena, que aporta volumen visual sin pesadez.",
                "ملمس متموج رفيع بشكل متعرج على كامل الشعر يضيف حجمًا بصريًا دون ثقل.",
                "Мелкая зигзагообразная гофрированная текстура по всей длине добавляет визуального объёма без утяжеления."),
            Category = "Texture",
            CompatibleFaceShapes = ["Square", "Diamond"],
            CompatibleJawlines = ["Angular"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        styles.Add(new HairstyleDocument
        {
            Title = T("Wet-Look Waves", "Islak Görünümlü Dalgalar", "Vagues effet mouillé", "Ondas efecto mojado", "تموجات بمظهر مبلل", "Волны с мокрым эффектом"),
            Description = T(
                "Slicked, gel-defined waves with a glossy, second-day finish that hug the head closely.",
                "Başa yakın oturan, jölesiyle belirginleştirilmiş, parlak ve ikinci gün etkisi veren ıslak görünümlü dalgalar.",
                "Des vagues lissées au gel, à l'effet mouillé et brillant, qui épousent la tête de près.",
                "Ondas alisadas con gel, de efecto mojado y brillante, que se ciñen a la cabeza.",
                "تموجات ملساء محددة بالجل بلمسة لامعة تشبه المظهر المبلل وتلتصق بالرأس عن قرب.",
                "Гладкие, зафиксированные гелем волны с эффектом «мокрых волос» и глянцевым блеском, плотно облегающие голову."),
            Category = "Texture",
            CompatibleFaceShapes = ["Oblong", "Heart"],
            CompatibleJawlines = ["Medium", "Soft"],
            CompatibleDensities = ["Medium", "High"],
            SortOrder = order++
        });

        return styles;
    }

    private static Dictionary<string, string> T(string en, string tr, string fr, string es, string ar, string ru) => new()
    {
        ["en"] = en, ["tr"] = tr, ["fr"] = fr, ["es"] = es, ["ar"] = ar, ["ru"] = ru
    };
}
