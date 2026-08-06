using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Business.Handlers.Analysis;

public static class MakeupLookSeedData
{
    public static List<MakeupLookDocument> All()
    {
        var looks = new List<MakeupLookDocument>();
        var order = 0;

        // ---------- Natural (5) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Bare Glow", "Çıplak Parıltı", "Éclat nu", "Brillo desnudo", "توهج طبيعي", "Естественное сияние"),
            Description = T(
                "Barely-there coverage with a dewy finish and a hint of warmth.",
                "Neredeyse görünmeyen bir kapatıcılıkla parlak bir bitiş ve hafif bir sıcaklık.",
                "Couvrance quasi invisible avec un fini éclatant et une touche de chaleur.",
                "Cobertura casi invisible con acabado luminoso y un toque de calidez.",
                "تغطية شبه معدومة بلمسة نهائية مشرقة ولمسة دافئة.",
                "Почти незаметное покрытие с сияющим финишем и тёплым акцентом."),
            Category = "Natural",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Your Lips But Better", "Sheer Rose Balm", "#C9847A"),
            Cheeks = Swatch("Barely Blush", "Sheer Peach Flush", "#E0B7A0"),
            Contour = Swatch("Soft Shadow", "Barely-There Contour", "#C9A88A"),
            Eyeshadow = Swatch("Skin Tone", "Bare Wash", "#D9C2AC"),
            Liner = Swatch("Soft Brown", "Barely Liner", "#8A6A54"),
            Brow = Swatch("Natural Brow", "Soft Brow Gel", "#7A5C42"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Dewy Skin Minimalist", "Işıltılı Cilt Minimalizmi", "Minimalisme peau lumineuse", "Minimalismo de piel luminosa", "مينيمالي ببشرة متوهجة", "Минимализм сияющей кожи"),
            Description = T(
                "A glass-skin base, cream blush, and clear brow gel — makeup that looks like skin, only better.",
                "Cam cilt görünümü veren bir taban, krem allık ve şeffaf kaş jeli — cilt gibi görünen ama daha iyi bir makyaj.",
                "Une base peau de verre, un fard à joues crème et un gel sourcils transparent — un maquillage qui ressemble à la peau, en mieux.",
                "Una base de piel de cristal, rubor en crema y gel de cejas transparente: un maquillaje que parece piel, solo que mejor.",
                "أساس بلمسة \"بشرة زجاجية\"، أحمر خدود كريمي، وجل حواجب شفاف — مكياج يبدو وكأنه بشرتك الحقيقية لكن أجمل.",
                "База в стиле «стеклянная кожа», кремовые румяна и прозрачный гель для бровей — макияж, который выглядит как кожа, только лучше."),
            Category = "Natural",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Your Lips But Better", "Sheer Petal Balm", "#C68C86"),
            Cheeks = Swatch("Cream Flush", "Dewy Cream Blush", "#E2A99B"),
            Contour = Swatch("Soft Bronze", "Barely-There Bronze", "#C9A387"),
            Eyeshadow = Swatch("Vanilla Wash", "Soft Vanilla Lid", "#E3CDB0"),
            Liner = Swatch("Soft Brown", "Whisper Liner", "#8A6A54"),
            Brow = Swatch("Clear Gel", "Natural Hold Brow Gel", "#7A5C42"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Sunkissed Natural", "Güneş Öpücüklü Doğal", "Naturel façon coup de soleil", "Natural besado por el sol", "طبيعي بلمسة الشمس", "Естественный образ «поцелованный солнцем»"),
            Description = T(
                "Warm bronzer swept across the cheeks and a golden peach lip for a just-back-from-vacation glow.",
                "Yanaklara sürülen sıcak bronzlaştırıcı ve altın şeftali dudak, tatilden yeni dönmüş gibi bir parlaklık verir.",
                "Un bronzer chaud balayé sur les joues et une lèvre pêche dorée pour un éclat façon retour de vacances.",
                "Un bronceador cálido sobre las mejillas y un labial durazno dorado para un brillo recién llegado de vacaciones.",
                "برونزر دافئ على الخدود وأحمر شفاه خوخي ذهبي لتوهج يبدو وكأنك عائد للتو من العطلة.",
                "Тёплый бронзер на скулах и персиково-золотистая помада — сияние, будто вы только вернулись из отпуска."),
            Category = "Natural",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel"],
            Lips = Swatch("Golden Peach", "Sunkissed Peach Balm", "#D68F6C"),
            Cheeks = Swatch("Sun Bronze", "Warm Sunkissed Bronzer", "#C6875F"),
            Contour = Swatch("Warm Bronze", "Sunkissed Contour", "#B87A56"),
            Eyeshadow = Swatch("Golden Sand", "Warm Sand Wash", "#D2A876"),
            Liner = Swatch("Warm Brown", "Sunkissed Liner", "#7A5236"),
            Brow = Swatch("Golden Brown", "Warm Natural Brow", "#6E4E32"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Cool Girl No-Makeup", "Soğuk Ton Makyajsız Görünüm", "Effet no make-up frais", "Efecto sin maquillaje fresco", "إطلالة \"بلا مكياج\" بلمسة باردة", "Прохладный образ «без макияжа»"),
            Description = T(
                "Sheer cool-pink flush and a satin mauve lip keep the focus on clear, even-toned skin.",
                "Şeffaf soğuk pembe allık ve saten mor dudak, odağı berrak ve eşit tonlu cilt üzerinde tutar.",
                "Un fard à joues rose froid léger et une lèvre mauve satinée gardent l'attention sur une peau claire et uniforme.",
                "Un rubor rosa frío translúcido y un labial malva satinado mantienen el foco en una piel clara y uniforme.",
                "أحمر خدود وردي بارد شفاف وأحمر شفاه موف ساتان يبقيان التركيز على بشرة صافية ومتجانسة اللون.",
                "Прозрачные холодно-розовые румяна и сатиновая мовая помада оставляют акцент на чистой, ровной коже."),
            Category = "Natural",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray"],
            Lips = Swatch("Satin Mauve", "Cool Mauve Balm", "#B27F87"),
            Cheeks = Swatch("Cool Pink", "Sheer Cool Flush", "#D69CA6"),
            Contour = Swatch("Cool Taupe", "Barely-There Cool Contour", "#B5A196"),
            Eyeshadow = Swatch("Dove Wash", "Soft Dove Lid", "#D6C7C2"),
            Liner = Swatch("Soft Gray-Brown", "Cool Whisper Liner", "#6E5A55"),
            Brow = Swatch("Ash Brown", "Cool Natural Brow", "#5C4E46"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Fresh-Faced Neutral", "Taze Görünümlü Nötr Makyaj", "Neutre au teint frais", "Neutro de rostro fresco", "مكياج نيوترال بإطلالة منتعشة", "Свежий нейтральный образ"),
            Description = T(
                "A universally flattering rosy-beige palette that works across undertones for a healthy, rested look.",
                "Her cilt alt tonuna uyan, evrensel olarak yakışan gül-bej bir palet, sağlıklı ve dinlenmiş bir görünüm sunar.",
                "Une palette rose-beige universellement flatteuse, adaptée à toutes les sous-tonalités, pour un look sain et reposé.",
                "Una paleta rosa-beige universalmente favorecedora que funciona en todos los subtonos, para un look saludable y descansado.",
                "باليت وردي-بيج يناسب جميع درجات البشرة الأساسية، يمنح إطلالة صحية ومرتاحة.",
                "Универсально выигрышная розово-бежевая палитра, подходящая для любого подтона кожи — свежий, отдохнувший вид."),
            Category = "Natural",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Rosy Beige", "Universal Rosy Beige", "#C08A80"),
            Cheeks = Swatch("Soft Rose Beige", "Universal Flush", "#D9A79B"),
            Contour = Swatch("Neutral Taupe", "Universal Contour", "#B69C89"),
            Eyeshadow = Swatch("Warm Beige", "Universal Beige Wash", "#D4B99C"),
            Liner = Swatch("Soft Brown", "Universal Liner", "#7A5C4C"),
            Brow = Swatch("Neutral Brown", "Universal Brow", "#6B5240"),
            SortOrder = order++
        });

        // ---------- Everyday (5) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Effortless Office", "Zahmetsiz Ofis Görünümü", "Bureau sans effort", "Oficina sin esfuerzo", "إطلالة مكتب بلا جهد", "Непринуждённый офисный образ"),
            Description = T(
                "Polished neutral tones and a defined-but-soft brow that read professional in five minutes flat.",
                "Cilalı nötr tonlar ve belirgin ama yumuşak bir kaş, beş dakikada profesyonel bir görünüm sunar.",
                "Des tons neutres soignés et un sourcil défini mais doux, pour un look professionnel en cinq minutes chrono.",
                "Tonos neutros pulidos y una ceja definida pero suave que lucen profesionales en cinco minutos.",
                "درجات نيوترال أنيقة وحاجب محدد لكنه ناعم يمنحان إطلالة مهنية خلال خمس دقائق فقط.",
                "Отточенные нейтральные тона и чёткие, но мягкие брови — профессиональный вид за пять минут."),
            Category = "Everyday",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Nude Rose", "Office Nude Rose", "#B98077"),
            Cheeks = Swatch("Soft Terracotta", "Everyday Warm Flush", "#C88B76"),
            Contour = Swatch("Taupe", "Everyday Contour", "#AD9078"),
            Eyeshadow = Swatch("Taupe Wash", "Everyday Neutral Lid", "#B39C86"),
            Liner = Swatch("Brown", "Soft Definition Liner", "#5C4433"),
            Brow = Swatch("Ash Brown", "Defined Soft Brow", "#5A4736"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Weekend Errand Run", "Hafta Sonu İşleri", "Courses du week-end", "Recados del fin de semana", "مشاوير نهاية الأسبوع", "Выходные по делам"),
            Description = T(
                "A tinted balm and cream blush stick that survive iced coffee, errands, and everything in between.",
                "Buzlu kahveye, işlere ve arasındaki her şeye dayanan renkli balsam ve krem allık stick.",
                "Un baume teinté et un stick de fard crème qui résistent au café glacé, aux courses et à tout le reste.",
                "Un bálsamo con color y un stick de rubor en crema que aguantan el café helado, los recados y todo lo demás.",
                "بلسم ملون وأحمر خدود كريمي على شكل ستيك يصمدان أمام القهوة المثلجة والمشاوير وكل ما بينهما.",
                "Тонирующий бальзам и кремовые румяна-стик, которые выдержат айс-кофе, дела и всё остальное."),
            Category = "Everyday",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Tinted Balm", "Sheer Coral Balm", "#C97768"),
            Cheeks = Swatch("Cream Stick", "Errand-Proof Cream Blush", "#D08972"),
            Contour = Swatch("Soft Bronze", "Quick Bronze Stick", "#C09370"),
            Eyeshadow = Swatch("Bare Wash", "Barely-There Wash", "#D6BFA4"),
            Liner = Swatch("Soft Brown", "Quick Liner", "#7A5A42"),
            Brow = Swatch("Natural Brow", "Errand Brow Gel", "#6E5038"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Coffee Date Casual", "Kahve Buluşması Rahatlığı", "Décontracté rendez-vous café", "Casual para una cita de café", "كاجوال لموعد القهوة", "Кэжуал на кофейное свидание"),
            Description = T(
                "A soft brown eyeshadow wash and a mauve-berry lip strike an easy balance between put-together and relaxed.",
                "Yumuşak kahverengi göz farı ve mürdüm-üzümsü dudak, derli toplu ve rahat arasında kolay bir denge kurar.",
                "Un fard à paupières brun doux et une lèvre mauve-baie trouvent l'équilibre parfait entre soigné et détendu.",
                "Una sombra marrón suave y un labial malva-baya logran un equilibrio fácil entre arreglado y relajado.",
                "ظلال بنية ناعمة وأحمر شفاه موف-توتي يحققان توازناً سهلاً بين الأناقة والاسترخاء.",
                "Мягкие коричневые тени и ягодно-мовая помада создают лёгкий баланс между собранностью и расслабленностью."),
            Category = "Everyday",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Brown", "Hazel"],
            Lips = Swatch("Mauve Berry", "Casual Mauve Berry", "#A65F6A"),
            Cheeks = Swatch("Soft Rose", "Casual Rose Flush", "#C88990"),
            Contour = Swatch("Soft Taupe", "Casual Contour", "#AE9280"),
            Eyeshadow = Swatch("Soft Brown Wash", "Coffee Date Wash", "#9C7C60"),
            Liner = Swatch("Espresso", "Coffee Date Liner", "#4A3324"),
            Brow = Swatch("Soft Brown", "Casual Brow", "#5C4736"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Quick Commute Face", "Hızlı Yolculuk Makyajı", "Look express pour le trajet", "Rostro rápido para el trayecto", "مكياج سريع لطريق العمل", "Быстрый образ по пути на работу"),
            Description = T(
                "A three-product routine — cream blush, brow gel, and tinted lip balm — for mornings with no time to spare.",
                "Zaman kaybetmeyen sabahlar için üç ürünlük rutin — krem allık, kaş jeli ve renkli dudak balsamı.",
                "Une routine en trois produits — fard crème, gel sourcils et baume à lèvres teinté — pour les matins sans une minute à perdre.",
                "Una rutina de tres productos —rubor en crema, gel de cejas y bálsamo labial con color— para las mañanas sin tiempo que perder.",
                "روتين من ثلاث منتجات فقط — أحمر خدود كريمي، وجل حواجب، وبلسم شفاه ملون — للصباحات التي لا وقت فيها للتأخير.",
                "Рутина из трёх продуктов — кремовые румяна, гель для бровей и тонирующий бальзам для губ — для утра без лишних минут."),
            Category = "Everyday",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Tinted Balm", "Commute Rose Balm", "#BE7A72"),
            Cheeks = Swatch("Cream Flush", "Quick Cream Blush", "#D3968A"),
            Contour = Swatch("Soft Taupe", "Minimal Contour", "#B29A82"),
            Eyeshadow = Swatch("Skin Wash", "Bare Minimal Wash", "#D8C4AC"),
            Liner = Swatch("Soft Brown", "Minimal Liner", "#7A5C46"),
            Brow = Swatch("Clear Gel", "Commute Brow Gel", "#6E5038"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Everyday Rosy Cheeks", "Günlük Pembemsi Yanaklar", "Joues roses du quotidien", "Mejillas sonrosadas de diario", "خدود وردية للاستخدام اليومي", "Румяные щёки на каждый день"),
            Description = T(
                "Rosy cheeks, a soft pink lip, and a hint of brown liner for definition without looking done-up.",
                "Pembemsi yanaklar, yumuşak pembe dudak ve abartısız belirginlik için hafif bir kahverengi eyeliner.",
                "Des joues roses, une lèvre rose doux et une pointe d'eyeliner brun pour la définition, sans effet trop maquillé.",
                "Mejillas sonrosadas, un labial rosa suave y un toque de delineador marrón para dar definición sin verse recargado.",
                "خدود وردية، أحمر شفاه وردي ناعم، ولمسة من محدد العيون البني لإضفاء تحديد دون مبالغة.",
                "Румяные щёки, мягкая розовая помада и лёгкий коричневый лайнер для чёткости без эффекта «накрашено»."),
            Category = "Everyday",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Soft Pink", "Everyday Pink Balm", "#C8858A"),
            Cheeks = Swatch("Rosy Flush", "Everyday Rosy Blush", "#D998A0"),
            Contour = Swatch("Soft Taupe", "Everyday Soft Contour", "#B29A88"),
            Eyeshadow = Swatch("Soft Rose Wash", "Everyday Rose Wash", "#D2ABA6"),
            Liner = Swatch("Soft Brown", "Everyday Brown Liner", "#6E4E3C"),
            Brow = Swatch("Soft Brown", "Everyday Brow", "#5C4636"),
            SortOrder = order++
        });

        // ---------- Soft Glam (5) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Champagne Soft Glam", "Şampanya Soft Glam", "Soft glam champagne", "Glam suave champán", "سوفت جلام بلون الشمبانيا", "Мягкий гламур в цвете шампанского"),
            Description = T(
                "A shimmering champagne lid, subtle winged liner, and a glossy nude lip for polished daytime shimmer.",
                "Işıltılı şampanya göz kapağı, ince kanat eyeliner ve parlak nude dudak, gündüz için cilalı bir ışıltı sunar.",
                "Une paupière champagne scintillante, un léger trait de liner et une lèvre nude glossy pour un éclat sophistiqué de jour.",
                "Un párpado champán con brillo, un delineado sutil y un labial nude con gloss para un brillo pulido de día.",
                "جفن شمبانيا لامع، محدد عيون خفيف بشكل الجناح، وأحمر شفاه نود لامع لتوهج أنيق نهاراً.",
                "Мерцающие веки цвета шампанского, тонкая стрелка и глянцевая нюдовая помада — изысканное дневное сияние."),
            Category = "SoftGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = ["Brown", "Green"],
            Lips = Swatch("Glossy Nude", "Champagne Gloss Nude", "#C99080"),
            Cheeks = Swatch("Soft Peach", "Champagne Glow Blush", "#D99B7E"),
            Contour = Swatch("Warm Bronze", "Soft Glam Contour", "#C08862"),
            Eyeshadow = Swatch("Shimmer Champagne", "Champagne Shimmer Lid", "#D9BC8C"),
            Liner = Swatch("Soft Brown Wing", "Subtle Wing Liner", "#5C4432"),
            Brow = Swatch("Golden Brown", "Soft Glam Brow", "#5E4530"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Rose Gold Soft Glam", "Rose Gold Soft Glam", "Soft glam or rose", "Glam suave oro rosa", "سوفت جلام روز غولد", "Мягкий гламур розовое золото"),
            Description = T(
                "Rose-gold shimmer on the lid and a mauve-pink lip give this look a romantic, luminous finish.",
                "Göz kapağındaki rose gold ışıltı ve mor-pembe dudak, bu görünüme romantik ve parlak bir bitiş kazandırır.",
                "Un fard or rose scintillant sur la paupière et une lèvre mauve-rose donnent à ce look une finition romantique et lumineuse.",
                "Un brillo oro rosa en el párpado y un labial malva-rosado dan a este look un acabado romántico y luminoso.",
                "توهج روز غولد على الجفن وأحمر شفاه موف-وردي يمنحان هذه الإطلالة لمسة نهائية رومانسية ومشرقة.",
                "Мерцание розового золота на веках и мовово-розовая помада придают образу романтичное, сияющее завершение."),
            Category = "SoftGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Blue", "Gray", "Hazel"],
            Lips = Swatch("Mauve Pink", "Rose Gold Mauve Lip", "#C2828A"),
            Cheeks = Swatch("Rose Shimmer", "Rose Gold Flush", "#D99AA0"),
            Contour = Swatch("Soft Rose Taupe", "Rose Gold Contour", "#B99486"),
            Eyeshadow = Swatch("Rose Gold Shimmer", "Rose Gold Lid", "#CBA084"),
            Liner = Swatch("Soft Brown", "Rose Gold Liner", "#6E4E42"),
            Brow = Swatch("Ash Brown", "Rose Gold Brow", "#5C4A40"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Bronze Soft Glam", "Bronz Soft Glam", "Soft glam bronze", "Glam suave bronce", "سوفت جلام برونزي", "Мягкий бронзовый гламур"),
            Description = T(
                "Warm bronze shimmer, sun-kissed contour, and a caramel lip for a sultry, sunlit softness.",
                "Sıcak bronz ışıltı, güneş öpücüklü kontur ve karamel dudak, baştan çıkarıcı ve güneşli bir yumuşaklık sunar.",
                "Un fard bronze chaud, un contouring façon coup de soleil et une lèvre caramel pour une douceur ensoleillée et envoûtante.",
                "Un brillo bronce cálido, un contorno besado por el sol y un labial caramelo para una suavidad sensual y soleada.",
                "توهج برونزي دافئ، كونتور بلمسة الشمس، وأحمر شفاه كراميل لإطلالة ناعمة ومشمسة وجذابة.",
                "Тёплый бронзовый шиммер, скульптурирование в стиле «поцелованное солнцем» и карамельная помада — соблазнительная солнечная мягкость."),
            Category = "SoftGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel", "Green"],
            Lips = Swatch("Caramel", "Bronze Glam Caramel", "#B96F52"),
            Cheeks = Swatch("Sun Bronze", "Bronze Glam Flush", "#C67F5C"),
            Contour = Swatch("Deep Bronze", "Sunlit Bronze Contour", "#A96C46"),
            Eyeshadow = Swatch("Bronze Shimmer", "Bronze Glam Lid", "#B37A44"),
            Liner = Swatch("Warm Brown", "Bronze Glam Liner", "#5E3E28"),
            Brow = Swatch("Warm Brown", "Bronze Glam Brow", "#553C26"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Pink Petal Soft Glam", "Pembe Yaprak Soft Glam", "Soft glam pétale rose", "Glam suave pétalo rosa", "سوفت جلام بتلة وردية", "Мягкий гламур «розовый лепесток»"),
            Description = T(
                "Soft pink shimmer and a berry-stained lip create a romantic, girl-next-door glam.",
                "Yumuşak pembe ışıltı ve meyveli dudak, romantik ve doğal bir glam görünüm yaratır.",
                "Un fard rose doux et une lèvre teintée baie créent un glam romantique et naturel.",
                "Un brillo rosa suave y un labial teñido de baya crean un glam romántico y natural.",
                "توهج وردي ناعم وأحمر شفاه بلون التوت يخلقان إطلالة جلام رومانسية وطبيعية.",
                "Мягкое розовое сияние и ягодная тонировка губ создают романтичный, естественный гламур."),
            Category = "SoftGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Green", "Blue"],
            Lips = Swatch("Berry Stain", "Pink Petal Berry", "#B8606A"),
            Cheeks = Swatch("Pink Shimmer", "Petal Pink Flush", "#DE96A0"),
            Contour = Swatch("Soft Rose Taupe", "Petal Soft Contour", "#BC968A"),
            Eyeshadow = Swatch("Pink Shimmer", "Petal Pink Lid", "#DDB0AC"),
            Liner = Swatch("Soft Plum", "Petal Plum Liner", "#5C3A42"),
            Brow = Swatch("Soft Brown", "Petal Brow", "#5A4438"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Golden Hour Soft Glam", "Altın Saat Soft Glam", "Soft glam heure dorée", "Glam suave hora dorada", "سوفت جلام الساعة الذهبية", "Мягкий гламур «золотой час»"),
            Description = T(
                "Warm gold shimmer washed across the lid with a honey-toned lip, made for golden-hour photos.",
                "Göz kapağına yayılan sıcak altın ışıltı ve bal tonlu dudak, altın saat fotoğrafları için tasarlanmıştır.",
                "Un fard doré chaud sur toute la paupière et une lèvre couleur miel, pensés pour les photos de l'heure dorée.",
                "Un brillo dorado cálido en todo el párpado y un labial tono miel, ideal para fotos de la hora dorada.",
                "توهج ذهبي دافئ يغطي الجفن بالكامل مع أحمر شفاه بلون العسل، مصمم لصور الساعة الذهبية.",
                "Тёплое золотое сияние на всём веке и медовая помада — идеальный образ для фото в «золотой час»."),
            Category = "SoftGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel"],
            Lips = Swatch("Honey", "Golden Hour Honey", "#C17E52"),
            Cheeks = Swatch("Golden Flush", "Golden Hour Blush", "#D08F62"),
            Contour = Swatch("Warm Gold Bronze", "Golden Hour Contour", "#BD8352"),
            Eyeshadow = Swatch("Gold Shimmer", "Golden Hour Lid", "#D2A85E"),
            Liner = Swatch("Warm Brown", "Golden Hour Liner", "#5E4026"),
            Brow = Swatch("Golden Brown", "Golden Hour Brow", "#5A3F24"),
            SortOrder = order++
        });

        // ---------- Full Glam (5) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Cut Crease Full Glam", "Cut Crease Full Glam", "Full glam cut crease", "Glam total cut crease", "فل جلام بتقنية الكت كريز", "Полный гламур cut crease"),
            Description = T(
                "A precise cut crease in deep plum with a matching bold liner and a rich berry lip for maximum drama.",
                "Derin mürdüm tonunda keskin bir cut crease, uyumlu belirgin eyeliner ve yoğun meyveli dudak, maksimum drama sunar.",
                "Un cut crease précis en prune profond, un eyeliner assorti prononcé et une lèvre baie intense pour un maximum de drame.",
                "Un cut crease preciso en ciruela intenso, un delineado audaz a juego y un labial baya profundo para el máximo drama.",
                "كت كريز دقيق بلون البرقوق العميق مع محدد عيون جريء متناسق وأحمر شفاه توتي غني لأقصى درجات الجاذبية.",
                "Чёткий cut crease в глубоком сливовом с яркой подводкой в тон и насыщенной ягодной помадой — максимальная драматичность."),
            Category = "FullGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Blue", "Gray", "Green"],
            Lips = Swatch("Deep Berry", "Full Glam Berry", "#8A2E42"),
            Cheeks = Swatch("Deep Rose", "Full Glam Rose Flush", "#B85C6E"),
            Contour = Swatch("Deep Cool Taupe", "Full Glam Contour", "#8A7268"),
            Eyeshadow = Swatch("Deep Plum", "Cut Crease Plum", "#5C2A48"),
            Liner = Swatch("Bold Black", "Cut Crease Liner", "#0E0E14"),
            Brow = Swatch("Deep Brown", "Full Glam Brow", "#3E2E24"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Red Carpet Glam", "Kırmızı Halı Glam", "Glam tapis rouge", "Glam alfombra roja", "جلام السجادة الحمراء", "Гламур для красной дорожки"),
            Description = T(
                "Full-coverage skin, sculpted contour, and a classic matte red lip built for camera flashes.",
                "Tam kapatıcılıklı cilt, heykelsi kontur ve kamera flaşları için tasarlanmış klasik mat kırmızı dudak.",
                "Une peau full coverage, un contouring sculpté et une lèvre rouge mate classique, pensés pour les flashs.",
                "Piel de cobertura total, contorno esculpido y un labial rojo mate clásico, hecho para los flashes.",
                "بشرة بتغطية كاملة، كونتور منحوت، وأحمر شفاه أحمر مطفي كلاسيكي مصمم لأضواء الكاميرات.",
                "Плотное покрытие кожи, скульптурный контуринг и классическая матовая красная помада — образ для вспышек камер."),
            Category = "FullGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Matte Red", "Red Carpet Matte Red", "#A81E28"),
            Cheeks = Swatch("Sculpted Rose", "Red Carpet Contour Blush", "#B8695E"),
            Contour = Swatch("Deep Bronze", "Red Carpet Sculpt", "#96674A"),
            Eyeshadow = Swatch("Bronze Smoke", "Red Carpet Lid", "#7A5636"),
            Liner = Swatch("Bold Black", "Red Carpet Liner", "#14141A"),
            Brow = Swatch("Deep Brown", "Red Carpet Brow", "#3A2A20"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Metallic Foil Glam", "Metalik Folyo Glam", "Glam métallique foil", "Glam metálico foil", "جلام فويل معدني", "Металлический фольгированный гламур"),
            Description = T(
                "A foiled metallic silver lid paired with a graphic black liner for an edgy, editorial finish.",
                "Folyo etkili metalik gümüş göz kapağı ve grafik siyah eyeliner, iddialı ve editoryal bir bitiş sunar.",
                "Une paupière argent métallique effet foil associée à un liner noir graphique pour une finition audacieuse et éditoriale.",
                "Un párpado plateado metálico efecto foil combinado con un delineado negro gráfico para un acabado atrevido y editorial.",
                "جفن فضي معدني بتأثير الفويل مع محدد عيون أسود جرافيكي لإطلالة جريئة تصلح للمجلات.",
                "Металлическое серебристое веко с эффектом фольги в сочетании с графичной чёрной подводкой — дерзкий, редакционный образ."),
            Category = "FullGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray"],
            Lips = Swatch("Nude Matte", "Foil Glam Nude", "#A97A6E"),
            Cheeks = Swatch("Cool Contour Rose", "Foil Glam Flush", "#B2727A"),
            Contour = Swatch("Cool Taupe", "Foil Glam Contour", "#8E7A72"),
            Eyeshadow = Swatch("Metallic Silver Foil", "Foil Silver Lid", "#C4C6CC"),
            Liner = Swatch("Graphic Black", "Foil Glam Liner", "#0E0E14"),
            Brow = Swatch("Ash Black", "Foil Glam Brow", "#2C2A2A"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Jewel Tone Glam", "Mücevher Tonu Glam", "Glam tons bijoux", "Glam tonos joya", "جلام بألوان الأحجار الكريمة", "Гламур в драгоценных тонах"),
            Description = T(
                "A rich emerald eyeshadow blended with charcoal in the crease, finished with a deep wine lip.",
                "Kıvrımda kömürle harmanlanmış zengin zümrüt göz farı ve koyu şarap dudakla tamamlanır.",
                "Un fard émeraude riche fondu avec du charbon dans le pli, sublimé par une lèvre vin profond.",
                "Una sombra esmeralda intensa difuminada con carbón en el pliegue, rematada con un labial vino profundo.",
                "ظلال زمردية غنية ممزوجة بالفحمي في تجعيد الجفن، مكتملة بأحمر شفاه نبيذي عميق.",
                "Насыщенные изумрудные тени, растушёванные угольным в складке века, и глубокая винная помада завершают образ."),
            Category = "FullGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Green", "Hazel", "Brown"],
            Lips = Swatch("Deep Wine", "Jewel Tone Wine", "#6E1E2E"),
            Cheeks = Swatch("Deep Berry Flush", "Jewel Tone Blush", "#A2596A"),
            Contour = Swatch("Deep Cool Taupe", "Jewel Tone Contour", "#847064"),
            Eyeshadow = Swatch("Rich Emerald", "Jewel Tone Emerald", "#0E5C42"),
            Liner = Swatch("Charcoal Black", "Jewel Tone Liner", "#1C1C22"),
            Brow = Swatch("Deep Brown", "Jewel Tone Brow", "#382820"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Sculpted Bronze Glam", "Heykelsi Bronz Glam", "Glam bronze sculpté", "Glam bronce esculpido", "جلام برونزي منحوت", "Скульптурный бронзовый гламур"),
            Description = T(
                "Heavy bronze contour, a warm smoked-out lid, and a glossy caramel lip for maximum-impact glam.",
                "Yoğun bronz kontur, sıcak dumanlı göz kapağı ve parlak karamel dudak, maksimum etkili bir glam sunar.",
                "Un contouring bronze intense, une paupière fumée chaude et une lèvre caramel glossy pour un glam à fort impact.",
                "Un contorno bronce intenso, un párpado ahumado cálido y un labial caramelo con gloss para un glam de máximo impacto.",
                "كونتور برونزي كثيف، جفن دخاني دافئ، وأحمر شفاه كراميل لامع لإطلالة جلام قوية التأثير.",
                "Насыщенный бронзовый контуринг, тёплый дымчатый смоки на веках и глянцевая карамельная помада — гламур максимального эффекта."),
            Category = "FullGlam",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel"],
            Lips = Swatch("Glossy Caramel", "Sculpted Bronze Caramel", "#B06B4A"),
            Cheeks = Swatch("Deep Bronze Flush", "Sculpted Bronze Blush", "#B87454"),
            Contour = Swatch("Heavy Bronze", "Sculpted Bronze Contour", "#8F5C3C"),
            Eyeshadow = Swatch("Warm Bronze Smoke", "Sculpted Bronze Lid", "#7C4E30"),
            Liner = Swatch("Deep Brown-Black", "Sculpted Bronze Liner", "#2A1E16"),
            Brow = Swatch("Warm Deep Brown", "Sculpted Bronze Brow", "#3E2C1E"),
            SortOrder = order++
        });

        // ---------- Smokey (5) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Classic Smokey", "Klasik Dumanlı", "Smoky classique", "Ahumado clásico", "دخاني كلاسيكي", "Классический смоки"),
            Description = T(
                "A deep charcoal-to-black smokey eye with a matte nude lip to balance the intensity.",
                "Yoğunluğu dengelemek için mat nude bir dudakla derin kömür-siyah dumanlı göz.",
                "Un smoky yeux charbon à noir profond avec une lèvre nude mate pour équilibrer.",
                "Un ahumado de carbón a negro profundo con labio nude mate para equilibrar.",
                "عيون دخانية عميقة من الفحمي إلى الأسود مع شفاه نود مطفية لموازنة الكثافة.",
                "Глубокий смоки от угольного до чёрного с матовой нюдовой помадой для баланса."),
            Category = "Smokey",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Blue", "Gray", "Green"],
            Lips = Swatch("Matte Nude", "Cool Nude Matte", "#B08478"),
            Cheeks = Swatch("Soft Rose", "Cool Rose Flush", "#C08A88"),
            Contour = Swatch("Cool Taupe", "Cool Contour", "#9C8A82"),
            Eyeshadow = Swatch("Charcoal", "Deep Charcoal Smoke", "#3A3A3F"),
            Liner = Swatch("Black", "Deep Black Liner", "#16161A"),
            Brow = Swatch("Ash Brown", "Cool Ash Brow", "#5C5248"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Bronze Smokey", "Bronz Dumanlı Göz", "Smoky bronze", "Ahumado bronce", "دخاني برونزي", "Бронзовый смоки"),
            Description = T(
                "Warm bronze and copper tones smoked through the crease with a soft nude lip to keep it wearable.",
                "Kıvrım boyunca dumanlanan sıcak bronz ve bakır tonları, giyilebilirliği korumak için yumuşak nude dudakla dengelenir.",
                "Des tons bronze et cuivre chauds fumés dans le pli, équilibrés par une lèvre nude douce pour rester portable.",
                "Tonos bronce y cobre cálidos difuminados en el pliegue, equilibrados con un labial nude suave para que sea versátil.",
                "درجات برونزية ونحاسية دافئة مموهة عبر تجعيد الجفن مع أحمر شفاه نود ناعم لإبقاء الإطلالة قابلة للارتداء.",
                "Тёплые бронзовые и медные оттенки, растушёванные в складке века, уравновешены мягкой нюдовой помадой для повседневной носки."),
            Category = "Smokey",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel", "Green"],
            Lips = Swatch("Soft Nude", "Bronze Smokey Nude", "#B98070"),
            Cheeks = Swatch("Warm Terracotta", "Bronze Smokey Flush", "#C17E5E"),
            Contour = Swatch("Warm Bronze", "Bronze Smokey Contour", "#A96E48"),
            Eyeshadow = Swatch("Copper Bronze", "Bronze Smokey Lid", "#8C5430"),
            Liner = Swatch("Deep Brown", "Bronze Smokey Liner", "#3A2618"),
            Brow = Swatch("Warm Brown", "Bronze Smokey Brow", "#4A3220"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Plum Smokey", "Erik Moru Dumanlı Göz", "Smoky prune", "Ahumado ciruela", "دخاني بلون البرقوق", "Сливовый смоки"),
            Description = T(
                "Deep plum and aubergine blended through the socket with a rose-brown lip for a moody evening look.",
                "Kıvrımda harmanlanan koyu erik moru ve patlıcan tonları, hüzünlü bir akşam görünümü için gül-kahverengi dudakla tamamlanır.",
                "Une prune profonde et de l'aubergine fondues dans le creux de l'œil, complétées par une lèvre brun-rosé pour un look de soirée intense.",
                "Un ciruela profundo y berenjena difuminados en la cuenca, completados con un labial marrón rosado para un look de noche intenso.",
                "برقوق عميق وباذنجاني ممزوجان في تجويف العين مع أحمر شفاه بني وردي لإطلالة مسائية غامضة.",
                "Глубокий сливовый и баклажановый оттенки, растушёванные в складке века, дополнены розово-коричневой помадой для загадочного вечернего образа."),
            Category = "Smokey",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Green", "Hazel", "Blue"],
            Lips = Swatch("Rose Brown", "Plum Smokey Lip", "#8A5652"),
            Cheeks = Swatch("Deep Rose", "Plum Smokey Flush", "#A2646E"),
            Contour = Swatch("Cool Taupe", "Plum Smokey Contour", "#8E7870"),
            Eyeshadow = Swatch("Deep Plum", "Plum Smokey Lid", "#4A2438"),
            Liner = Swatch("Aubergine Black", "Plum Smokey Liner", "#241420"),
            Brow = Swatch("Deep Ash Brown", "Plum Smokey Brow", "#3E342E"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Green Smokey", "Yeşil Dumanlı Göz", "Smoky vert", "Ahumado verde", "دخاني أخضر", "Зелёный смоки"),
            Description = T(
                "Deep forest green smoked through charcoal edges, worn with a muted rose lip to let the eyes lead.",
                "Kömür kenarlarla dumanlanan koyu orman yeşili, gözlerin öne çıkması için mat gül dudakla giyilir.",
                "Un vert forêt profond fumé avec des bords charbon, porté avec une lèvre rose mat pour laisser les yeux dominer.",
                "Un verde bosque profundo ahumado con bordes carbón, combinado con un labial rosa mate para que los ojos protagonicen.",
                "أخضر غابي عميق مموه بحواف فحمية، مع أحمر شفاه وردي باهت ليبقى التركيز على العينين.",
                "Глубокий лесной зелёный, растушёванный угольными краями, дополнен приглушённой розовой помадой, чтобы акцент оставался на глазах."),
            Category = "Smokey",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Green", "Hazel", "Brown"],
            Lips = Swatch("Muted Rose", "Green Smokey Lip", "#A8726C"),
            Cheeks = Swatch("Soft Rose", "Green Smokey Flush", "#B77C78"),
            Contour = Swatch("Cool Taupe", "Green Smokey Contour", "#8E7A70"),
            Eyeshadow = Swatch("Forest Green", "Green Smokey Lid", "#2E4A32"),
            Liner = Swatch("Charcoal Black", "Green Smokey Liner", "#1A1A1E"),
            Brow = Swatch("Ash Brown", "Green Smokey Brow", "#4A3E34"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Soft Daytime Smokey", "Yumuşak Gündüz Dumanlı Göz", "Smoky doux de jour", "Ahumado suave de día", "دخاني ناعم للنهار", "Мягкий дневной смоки"),
            Description = T(
                "A diffused taupe-brown smokey with soft edges, wearable enough for daylight and desk lighting.",
                "Yumuşak kenarlı, dağınık taupe-kahverengi bir dumanlı göz, gün ışığı ve ofis ışığında rahatlıkla giyilebilir.",
                "Un smoky taupe-brun diffus aux contours doux, portable en plein jour comme sous la lumière du bureau.",
                "Un ahumado taupe-marrón difuso de bordes suaves, ideal para la luz del día y la oficina.",
                "دخاني بني-تاوبي منتشر بحواف ناعمة، مناسب لضوء النهار وإضاءة المكتب على حد سواء.",
                "Растушёванный тауп-коричневый смоки с мягкими границами — подходит и для дневного света, и для офисного освещения."),
            Category = "Smokey",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Soft Nude Brown", "Daytime Smokey Nude", "#B08272"),
            Cheeks = Swatch("Soft Taupe Rose", "Daytime Smokey Flush", "#C08C82"),
            Contour = Swatch("Soft Taupe", "Daytime Smokey Contour", "#AC9280"),
            Eyeshadow = Swatch("Taupe Brown", "Daytime Smokey Lid", "#7C6452"),
            Liner = Swatch("Soft Brown-Black", "Daytime Smokey Liner", "#2E2420"),
            Brow = Swatch("Soft Ash Brown", "Daytime Smokey Brow", "#4E4038"),
            SortOrder = order++
        });

        // ---------- Bridal (5) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Timeless Bridal", "Zamansız Gelin", "Mariée intemporelle", "Novia atemporal", "عروس خالدة", "Вечная невеста"),
            Description = T(
                "Soft-focus skin, rose-flushed cheeks, and a long-wear rose nude lip built to photograph beautifully.",
                "Yumuşak odaklı cilt, gül tonlu yanaklar ve fotoğrafta güzel görünen kalıcı gül nude dudak.",
                "Peau floutée, joues rosées et une lèvre nude rosée longue tenue, pensée pour la photo.",
                "Piel de enfoque suave, mejillas sonrosadas y labio nude rosado de larga duración, ideal en fotos.",
                "بشرة ناعمة، وخدود وردية، وشفاه نود وردية طويلة الثبات تبدو رائعة في الصور.",
                "Мягкая кожа, розовые щёки и стойкая розовая нюдовая помада, идеальная для фото."),
            Category = "Bridal",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Rose Nude", "Long-Wear Rose Nude", "#C08A82"),
            Cheeks = Swatch("Rose Flush", "Bridal Rose Flush", "#D9A0A0"),
            Contour = Swatch("Soft Bronze", "Bridal Soft Contour", "#C2977C"),
            Eyeshadow = Swatch("Champagne Pink", "Bridal Champagne", "#E0C4B2"),
            Liner = Swatch("Soft Brown", "Bridal Soft Liner", "#7A5C4E"),
            Brow = Swatch("Natural Brow", "Bridal Brow", "#6E5440"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Boho Bridal", "Boho Gelin", "Mariée bohème", "Novia boho", "عروس بوهيمية", "Богемная невеста"),
            Description = T(
                "Sun-warmed skin, a soft peach flush, and a barely-there terracotta lip for an outdoor, free-spirited bride.",
                "Güneş ışıltılı cilt, yumuşak şeftali allık ve doğa düğünü için özgür ruhlu bir gelin görünümüne uygun hafif terracotta dudak.",
                "Une peau dorée par le soleil, un fard pêche doux et une lèvre terracotta subtile pour une mariée en plein air, à l'esprit libre.",
                "Piel dorada por el sol, un rubor durazno suave y un labial terracota casi imperceptible para una novia al aire libre y de espíritu libre.",
                "بشرة دافئة بلمسة شمسية، أحمر خدود خوخي ناعم، وأحمر شفاه تراكوتا خفيف جداً لعروس الحفلات الخارجية ذات الروح الحرة.",
                "Тёплая, будто согретая солнцем кожа, мягкий персиковый румянец и едва заметная терракотовая помада — для невесты на природе со свободным духом."),
            Category = "Bridal",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Sheer Terracotta", "Boho Bridal Lip", "#B87360"),
            Cheeks = Swatch("Soft Peach", "Boho Bridal Flush", "#D89A78"),
            Contour = Swatch("Sun Bronze", "Boho Bridal Contour", "#C1875E"),
            Eyeshadow = Swatch("Warm Sand", "Boho Bridal Lid", "#D2B48A"),
            Liner = Swatch("Soft Brown", "Boho Bridal Liner", "#6E4E36"),
            Brow = Swatch("Warm Brown", "Boho Bridal Brow", "#5C4028"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Classic Glam Bridal", "Klasik Glam Gelin", "Mariée glam classique", "Novia glam clásica", "عروس جلام كلاسيكي", "Классическая гламурная невеста"),
            Description = T(
                "Defined eyes with a soft brown smoke, full lashes, and a satin rose lip for a bride who wants more drama.",
                "Yumuşak kahverengi dumanla belirgin gözler, dolgun kirpikler ve daha fazla drama isteyen bir gelin için saten gül dudak.",
                "Des yeux définis avec un fumé brun doux, des cils fournis et une lèvre rose satinée pour une mariée qui veut plus de glamour.",
                "Ojos definidos con un ahumado marrón suave, pestañas voluminosas y un labial rosa satinado para una novia que busca más drama.",
                "عيون محددة بدخان بني ناعم، رموش كثيفة، وأحمر شفاه وردي ساتان لعروس تريد إطلالة أكثر جرأة.",
                "Выразительные глаза с мягким коричневым смоки, объёмные ресницы и сатиновая розовая помада — для невесты, которая хочет больше драмы."),
            Category = "Bridal",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Satin Rose", "Classic Glam Bridal Lip", "#B8747C"),
            Cheeks = Swatch("Soft Rose Flush", "Classic Glam Bridal Blush", "#D0949A"),
            Contour = Swatch("Soft Bronze", "Classic Glam Bridal Contour", "#BE9070"),
            Eyeshadow = Swatch("Soft Brown Smoke", "Classic Glam Bridal Lid", "#8C6C50"),
            Liner = Swatch("Soft Brown-Black", "Classic Glam Bridal Liner", "#3A2A20"),
            Brow = Swatch("Soft Brown", "Classic Glam Bridal Brow", "#5A4432"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Minimalist Bridal", "Minimalist Gelin", "Mariée minimaliste", "Novia minimalista", "عروس مينيمالية", "Минималистичная невеста"),
            Description = T(
                "Skin-first makeup with a whisper of blush and a your-lips-but-better tint for a bride who wants to look like herself.",
                "Kendisi gibi görünmek isteyen bir gelin için hafif bir allık dokunuşu ve dudak tonuyla cilt odaklı bir makyaj.",
                "Un maquillage centré sur la peau, une touche de blush et une teinte façon lèvres naturelles pour une mariée qui veut se ressembler.",
                "Un maquillaje centrado en la piel, un toque de rubor y un tinte labial natural para una novia que quiere verse ella misma.",
                "مكياج يركز على نضارة البشرة مع لمسة خفيفة من الأحمر الخدود وتلوين شفاه طبيعي لعروس تريد أن تبدو على طبيعتها.",
                "Макияж с акцентом на кожу, лёгкий намёк на румяна и естественный тон губ — для невесты, которая хочет остаться собой."),
            Category = "Bridal",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Your Lips But Better", "Minimalist Bridal Lip", "#C2877E"),
            Cheeks = Swatch("Whisper Blush", "Minimalist Bridal Flush", "#DBA79C"),
            Contour = Swatch("Barely-There Contour", "Minimalist Bridal Contour", "#C6A98E"),
            Eyeshadow = Swatch("Bare Wash", "Minimalist Bridal Lid", "#DCC7AE"),
            Liner = Swatch("Soft Brown", "Minimalist Bridal Liner", "#7C5C46"),
            Brow = Swatch("Natural Brow", "Minimalist Bridal Brow", "#6E5238"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Destination Bridal", "Yurt Dışı Düğünü Gelin", "Mariée destination", "Novia de destino", "عروس حفل زفاف في الخارج", "Невеста для свадьбы за границей"),
            Description = T(
                "Waterproof, humidity-proof formulas in a dewy rose palette built to survive heat, tears, and dancing till dawn.",
                "Su geçirmez ve neme dayanıklı formüllerle ışıltılı gül paleti, sıcaklığa, gözyaşlarına ve sabaha kadar dansa dayanacak şekilde tasarlandı.",
                "Des formules waterproof et résistantes à l'humidité dans une palette rose lumineuse, pensée pour résister à la chaleur, aux larmes et à la danse jusqu'à l'aube.",
                "Fórmulas resistentes al agua y a la humedad en una paleta rosa luminosa, hecha para resistir el calor, las lágrimas y el baile hasta el amanecer.",
                "تركيبات مقاومة للماء والرطوبة بباليت وردي متوهج مصمم ليصمد أمام الحرارة والدموع والرقص حتى الفجر.",
                "Водостойкие, устойчивые к влажности формулы в сияющей розовой палитре — выдержат жару, слёзы и танцы до рассвета."),
            Category = "Bridal",
            CompatibleSeasons = [],
            CompatibleUndertones = ["Warm", "Cool", "Neutral"],
            CompatibleEyeColors = [],
            Lips = Swatch("Waterproof Rose", "Destination Bridal Lip", "#BE7E7C"),
            Cheeks = Swatch("Dewy Rose", "Destination Bridal Flush", "#D99CA0"),
            Contour = Swatch("Sunlit Bronze", "Destination Bridal Contour", "#C2916C"),
            Eyeshadow = Swatch("Rose Champagne", "Destination Bridal Lid", "#D6B4A0"),
            Liner = Swatch("Waterproof Brown", "Destination Bridal Liner", "#5C4230"),
            Brow = Swatch("Waterproof Brow", "Destination Bridal Brow", "#5A4030"),
            SortOrder = order++
        });

        // ---------- Seasonal (20) ----------

        looks.Add(new MakeupLookDocument
        {
            Title = T("Soft Autumn Glow", "Yumuşak Sonbahar Parıltısı", "Éclat automne doux", "Brillo suave de otoño", "توهج الخريف الناعم", "Мягкое осеннее сияние"),
            Description = T(
                "Champagne lids, terracotta lips, and a warm bronze flush tuned to Soft Autumn.",
                "Şampanya göz kapakları, terracotta dudaklar ve Yumuşak Sonbahar'a uygun sıcak bronz allık.",
                "Paupières champagne, lèvres terracotta et un fard bronze chaud adapté à l'automne doux.",
                "Párpados champán, labios terracota y un rubor bronce cálido para el otoño suave.",
                "جفون شمبانيا وشفاه تراكوتا مع توهج برونزي دافئ يناسب الخريف الناعم.",
                "Веки шампань, терракотовые губы и тёплый бронзовый румянец для мягкой осени."),
            Category = "Seasonal",
            CompatibleSeasons = ["Soft Autumn"],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = ["Green", "Brown", "Hazel"],
            Lips = Swatch("Terracotta", "Warm Terracotta Lip", "#B8665C"),
            Cheeks = Swatch("Bronze Flush", "Warm Bronze Blush", "#C9846A"),
            Contour = Swatch("Warm Taupe", "Soft Contour", "#A8846A"),
            Eyeshadow = Swatch("Champagne", "Warm Champagne Lid", "#C9A46A"),
            Liner = Swatch("Cocoa", "Warm Cocoa Liner", "#6E4A3C"),
            Brow = Swatch("Soft Brown", "Warm Brow", "#5B4434"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Spring Awakening", "Bahar Uyanışı", "Réveil printanier", "Despertar de primavera", "صحوة الربيع", "Весеннее пробуждение"),
            Description = T(
                "Coral lips and a golden-green eyeshadow wash bring Spring's warm, sunlit clarity to life.",
                "Mercan dudaklar ve altın-yeşil göz farı, Bahar'ın sıcak ve güneşli berraklığını hayata geçirir.",
                "Des lèvres corail et un fard doré-vert donnent vie à la clarté chaude et ensoleillée du printemps.",
                "Labios coral y una sombra dorado-verde dan vida a la claridad cálida y soleada de la primavera.",
                "شفاه مرجانية وظلال ذهبية-خضراء تُحيي وضوح الربيع الدافئ والمشمس.",
                "Коралловые губы и золотисто-зелёные тени оживляют тёплую, солнечную ясность весны."),
            Category = "Seasonal",
            CompatibleSeasons = ["Spring"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Green", "Hazel", "Brown"],
            Lips = Swatch("Warm Coral", "Spring Coral Lip", "#F2846B"),
            Cheeks = Swatch("Peach Flush", "Spring Peach Blush", "#F4B183"),
            Contour = Swatch("Warm Camel", "Spring Contour", "#D9B48C"),
            Eyeshadow = Swatch("Golden Green", "Spring Green Wash", "#8FBF5A"),
            Liner = Swatch("Warm Brown", "Spring Liner", "#7A5236"),
            Brow = Swatch("Golden Brown", "Spring Brow", "#6E4E30"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Light Spring Bloom", "Açık Bahar Çiçeklenmesi", "Éclosion printemps clair", "Florecer de primavera clara", "تفتح الربيع الفاتح", "Цветение светлой весны"),
            Description = T(
                "Pale coral cheeks and a soft butter-yellow lid keep the palette light, warm, and delicate.",
                "Soluk mercan yanaklar ve yumuşak tereyağı sarısı göz kapağı, paleti hafif, sıcak ve narin tutar.",
                "Des joues corail pâle et une paupière beurre doux gardent la palette légère, chaude et délicate.",
                "Mejillas coral pálido y un párpado suave color mantequilla mantienen la paleta ligera, cálida y delicada.",
                "خدود مرجانية فاتحة وجفن بلون الزبدة الناعم يحافظان على الباليت خفيفاً ودافئاً ورقيقاً.",
                "Бледно-коралловые щёки и мягкие сливочно-жёлтые веки сохраняют палитру лёгкой, тёплой и нежной."),
            Category = "Seasonal",
            CompatibleSeasons = ["Light Spring"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Blue", "Green", "Hazel"],
            Lips = Swatch("Light Coral", "Light Spring Lip", "#F5A18C"),
            Cheeks = Swatch("Pale Coral", "Light Spring Blush", "#F0B9A0"),
            Contour = Swatch("Light Camel", "Light Spring Contour", "#E0C39F"),
            Eyeshadow = Swatch("Butter Yellow", "Light Spring Lid", "#F5D98A"),
            Liner = Swatch("Soft Brown", "Light Spring Liner", "#8A6A54"),
            Brow = Swatch("Light Golden Brown", "Light Spring Brow", "#8A6A4A"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Warm Spring Sunlit", "Sıcak Bahar Güneşi", "Printemps chaud ensoleillé", "Primavera cálida soleada", "ربيع دافئ ومشرق", "Тёплая солнечная весна"),
            Description = T(
                "Tomato-red lips and a marigold lid bring bold, sun-warmed brightness to Warm Spring coloring.",
                "Domates kırmızısı dudaklar ve kadife çiçeği göz kapağı, Sıcak Bahar tenine cesur ve güneşli bir parlaklık katar.",
                "Des lèvres rouge tomate et une paupière souci apportent une luminosité audacieuse et ensoleillée au printemps chaud.",
                "Labios rojo tomate y un párpado caléndula aportan un brillo audaz y soleado a la primavera cálida.",
                "شفاه حمراء طماطمية وجفن بلون الأقحوان يمنحان إشراقة جريئة ومشمسة لألوان الربيع الدافئ.",
                "Помидорно-красные губы и веки цвета календулы придают смелую, солнечную яркость тёплой весне."),
            Category = "Seasonal",
            CompatibleSeasons = ["Warm Spring"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel", "Green"],
            Lips = Swatch("Warm Tomato", "Warm Spring Lip #1", "#E8583B"),
            Cheeks = Swatch("Coral Pink", "Warm Spring Blush #1", "#F0837E"),
            Contour = Swatch("Warm Taupe", "Warm Spring Contour #1", "#B39B7C"),
            Eyeshadow = Swatch("Marigold", "Warm Spring Lid #1", "#F0A93B"),
            Liner = Swatch("Warm Brown", "Warm Spring Liner #1", "#6E4426"),
            Brow = Swatch("Warm Golden Brown", "Warm Spring Brow #1", "#5E3E22"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Warm Spring Meadow", "Sıcak Bahar Çayırı", "Prairie printemps chaud", "Pradera de primavera cálida", "مروج الربيع الدافئ", "Луг тёплой весны"),
            Description = T(
                "Grass-green liner and a turquoise-flecked lid pair with a warm coral lip for a fresh, garden-bright look.",
                "Çim yeşili eyeliner ve turkuaz nüanslı göz kapağı, taze ve bahçe canlılığında bir görünüm için sıcak mercan dudakla eşleşir.",
                "Un liner vert gazon et une paupière parsemée de turquoise s'associent à une lèvre corail chaude pour un look frais façon jardin.",
                "Un delineador verde hierba y un párpado con toques turquesa se combinan con un labial coral cálido para un look fresco de jardín.",
                "محدد عيون أخضر عشبي وجفن بلمسات فيروزية يتناسقان مع أحمر شفاه مرجاني دافئ لإطلالة منعشة بروح الحدائق.",
                "Травянисто-зелёная подводка и веки с бирюзовыми бликами сочетаются с тёплой коралловой помадой для свежего, «садового» образа."),
            Category = "Seasonal",
            CompatibleSeasons = ["Warm Spring"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Green", "Hazel"],
            Lips = Swatch("Warm Coral", "Warm Spring Lip #2", "#F0837E"),
            Cheeks = Swatch("Golden Beige Flush", "Warm Spring Blush #2", "#D9BC8C"),
            Contour = Swatch("Golden Taupe", "Warm Spring Contour #2", "#B39B7C"),
            Eyeshadow = Swatch("Warm Turquoise", "Warm Spring Lid #2", "#3FB6A8"),
            Liner = Swatch("Grass Green", "Warm Spring Liner #2", "#5E7A2E"),
            Brow = Swatch("Warm Brown", "Warm Spring Brow #2", "#5E3E22"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Clear Spring Vivid", "Berrak Bahar Canlılığı", "Printemps clair vif", "Primavera clara vívida", "ربيع صافٍ زاهٍ", "Яркая чистая весна"),
            Description = T(
                "A vivid emerald liner and a true-red lip bring high-contrast, jewel-bright energy to Clear Spring.",
                "Canlı zümrüt eyeliner ve gerçek kırmızı dudak, Berrak Bahar'a yüksek kontrastlı ve mücevher parlaklığında bir enerji katar.",
                "Un liner émeraude vif et une lèvre rouge vrai apportent une énergie contrastée et éclatante au printemps clair.",
                "Un delineador esmeralda vívido y un labial rojo verdadero aportan una energía de alto contraste y brillo joya a la primavera clara.",
                "محدد عيون زمردي زاهٍ وأحمر شفاه أحمر نقي يمنحان طاقة عالية التباين ولمعان الأحجار الكريمة للربيع الصافي.",
                "Яркая изумрудная подводка и настоящая красная помада придают контрастную, драгоценно-яркую энергию чистой весне."),
            Category = "Seasonal",
            CompatibleSeasons = ["Clear Spring"],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = ["Green", "Blue", "Brown"],
            Lips = Swatch("True Red", "Clear Spring Lip", "#E0342B"),
            Cheeks = Swatch("Hot Pink Flush", "Clear Spring Blush", "#EF488F"),
            Contour = Swatch("Warm Navy Taupe", "Clear Spring Contour", "#CBBBA3"),
            Eyeshadow = Swatch("Clear Emerald", "Clear Spring Lid", "#28A870"),
            Liner = Swatch("Clear Emerald Liner", "Clear Spring Liner", "#0E5C3E"),
            Brow = Swatch("Warm Deep Brown", "Clear Spring Brow", "#4A3020"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Summer Haze", "Yaz Pusu", "Brume d'été", "Bruma de verano", "ضباب الصيف", "Летняя дымка"),
            Description = T(
                "Powder blue liner and a cool rose lip capture Summer's soft, hazy elegance.",
                "Pudra mavisi eyeliner ve soğuk gül dudak, Yaz'ın yumuşak ve puslu zarafetini yakalar.",
                "Un liner bleu poudré et une lèvre rose froid capturent l'élégance douce et voilée de l'été.",
                "Un delineador azul empolvado y un labial rosa frío capturan la elegancia suave y brumosa del verano.",
                "محدد عيون أزرق بودرة وأحمر شفاه وردي بارد يعكسان أناقة الصيف الناعمة والضبابية.",
                "Пудрово-голубая подводка и холодная розовая помада передают мягкую, туманную элегантность лета."),
            Category = "Seasonal",
            CompatibleSeasons = ["Summer"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray"],
            Lips = Swatch("Cool Rose", "Summer Lip", "#D89AAE"),
            Cheeks = Swatch("Cool Beige Flush", "Summer Blush", "#D4C7B8"),
            Contour = Swatch("Dove Gray", "Summer Contour", "#B8B4AE"),
            Eyeshadow = Swatch("Powder Blue", "Summer Lid", "#A8C4D9"),
            Liner = Swatch("Slate Gray", "Summer Liner", "#5C6670"),
            Brow = Swatch("Cool Ash Brown", "Summer Brow", "#5C5248"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Light Summer Whisper", "Açık Yaz Fısıltısı", "Murmure d'été clair", "Susurro de verano claro", "همسة الصيف الفاتح", "Шёпот светлого лета"),
            Description = T(
                "Pale lilac lids and a soft powder-pink lip keep the look airy, cool, and understated.",
                "Soluk leylak göz kapakları ve yumuşak pudra pembe dudak, görünümü hafif, soğuk ve sade tutar.",
                "Des paupières lilas pâle et une lèvre rose poudré doux gardent le look aérien, froid et discret.",
                "Párpados lila pálido y un labial rosa empolvado suave mantienen el look ligero, frío y discreto.",
                "جفون ليلكية فاتحة وأحمر شفاه وردي بودرة ناعم يحافظان على الإطلالة خفيفة وباردة وهادئة.",
                "Бледно-лиловые веки и мягкая пудрово-розовая помада сохраняют образ лёгким, прохладным и сдержанным."),
            Category = "Seasonal",
            CompatibleSeasons = ["Light Summer"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray", "Green"],
            Lips = Swatch("Powder Pink", "Light Summer Lip", "#E5C2CE"),
            Cheeks = Swatch("Soft Ivory Flush", "Light Summer Blush", "#EFE9DE"),
            Contour = Swatch("Light Cool Taupe", "Light Summer Contour", "#C9BFB2"),
            Eyeshadow = Swatch("Soft Lilac", "Light Summer Lid", "#CBBEDD"),
            Liner = Swatch("Soft Gray", "Light Summer Liner", "#7C7C82"),
            Brow = Swatch("Light Ash Brown", "Light Summer Brow", "#7A6E64"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Cool Summer Twilight", "Soğuk Yaz Alacakaranlığı", "Crépuscule d'été frais", "Crepúsculo de verano fresco", "غسق الصيف البارد", "Сумерки прохладного лета"),
            Description = T(
                "Periwinkle liner and a deep cool rose lip give Cool Summer a moody, dusky elegance.",
                "Erguvan mavisi eyeliner ve derin soğuk gül dudak, Soğuk Yaz'a hüzünlü ve alacakaranlık bir zarafet katar.",
                "Un liner périwinkle et une lèvre rose froid profond donnent à l'été frais une élégance intense et crépusculaire.",
                "Un delineador periwinkle y un labial rosa frío profundo dan al verano fresco una elegancia intensa y crepuscular.",
                "محدد عيون بنفسجي مزرق وأحمر شفاه وردي بارد عميق يمنحان الصيف البارد أناقة غامضة تشبه الغسق.",
                "Барвинковая подводка и глубокая холодно-розовая помада придают прохладному лету загадочную, сумеречную элегантность."),
            Category = "Seasonal",
            CompatibleSeasons = ["Cool Summer"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray", "Green"],
            Lips = Swatch("Deep Cool Rose", "Cool Summer Lip #1", "#C77E90"),
            Cheeks = Swatch("Cool Taupe Flush", "Cool Summer Blush #1", "#B0A198"),
            Contour = Swatch("Soft Charcoal", "Cool Summer Contour #1", "#5C5C63"),
            Eyeshadow = Swatch("Periwinkle", "Cool Summer Lid #1", "#8C9AD9"),
            Liner = Swatch("Cool Charcoal", "Cool Summer Liner #1", "#2E2E36"),
            Brow = Swatch("Cool Ash Brown", "Cool Summer Brow #1", "#4A423C"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Cool Summer Plum", "Soğuk Yaz Eriği", "Prune été frais", "Ciruela de verano fresco", "برقوق الصيف البارد", "Слива прохладного лета"),
            Description = T(
                "Soft plum lids and a sea-green liner balance cool depth with a fresh, watery finish.",
                "Yumuşak erik moru göz kapakları ve deniz yeşili eyeliner, soğuk derinliği taze ve sulu bir bitişle dengeler.",
                "Des paupières prune douces et un liner vert marin équilibrent une profondeur froide avec une finition fraîche et aquatique.",
                "Párpados ciruela suaves y un delineador verde marino equilibran la profundidad fría con un acabado fresco y acuoso.",
                "جفون بلون البرقوق الناعم ومحدد عيون أخضر بحري يوازنان العمق البارد بلمسة نهائية منعشة ومائية.",
                "Мягкие сливовые веки и морско-зелёная подводка уравновешивают холодную глубину свежим, водянистым финишем."),
            Category = "Seasonal",
            CompatibleSeasons = ["Cool Summer"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Green", "Blue", "Gray"],
            Lips = Swatch("Cool Mauve", "Cool Summer Lip #2", "#8C5A79"),
            Cheeks = Swatch("Cool Rose Flush", "Cool Summer Blush #2", "#C77E90"),
            Contour = Swatch("Cool Taupe", "Cool Summer Contour #2", "#B0A198"),
            Eyeshadow = Swatch("Soft Plum", "Cool Summer Lid #2", "#8C5A79"),
            Liner = Swatch("Sea Green", "Cool Summer Liner #2", "#3E6A5E"),
            Brow = Swatch("Cool Ash Brown", "Cool Summer Brow #2", "#4A423C"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Soft Summer Mist", "Yumuşak Yaz Sisi", "Brume d'été douce", "Bruma de verano suave", "ضباب الصيف الناعم", "Дымка мягкого лета"),
            Description = T(
                "Muted mauve and sage tones blend into a soft, misty look built for Soft Summer's low-contrast palette.",
                "Mat mor ve adaçayı tonları, Yumuşak Yaz'ın düşük kontrastlı paleti için yumuşak ve puslu bir görünümde harmanlanır.",
                "Des tons mauve et sauge mats se fondent dans un look doux et voilé, adapté à la palette peu contrastée de l'été doux.",
                "Tonos malva y salvia apagados se mezclan en un look suave y brumoso, ideal para la paleta de bajo contraste del verano suave.",
                "درجات موف وأخضر مريمية باهتة تمتزج في إطلالة ناعمة وضبابية تناسب باليت الصيف الناعم منخفض التباين.",
                "Приглушённые тона мовы и шалфея сливаются в мягкий, туманный образ, созданный для низкоконтрастной палитры мягкого лета."),
            Category = "Seasonal",
            CompatibleSeasons = ["Soft Summer"],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Gray", "Green", "Blue"],
            Lips = Swatch("Soft Mauve", "Soft Summer Lip", "#A88CA0"),
            Cheeks = Swatch("Dusty Rose", "Soft Summer Blush", "#C79A96"),
            Contour = Swatch("Soft Greige", "Soft Summer Contour", "#C2B8AC"),
            Eyeshadow = Swatch("Muted Sage", "Soft Summer Lid", "#93A889"),
            Liner = Swatch("Soft Charcoal", "Soft Summer Liner", "#524E4A"),
            Brow = Swatch("Muted Ash Brown", "Soft Summer Brow", "#665C52"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Harvest Autumn", "Hasat Sonbaharı", "Automne des récoltes", "Otoño de cosecha", "خريف الحصاد", "Осень урожая"),
            Description = T(
                "Rust liner, mustard-washed lids, and a terracotta lip channel Autumn's rich, earthy warmth.",
                "Pas eyeliner, hardal yıkamalı göz kapakları ve terracotta dudak, Sonbahar'ın zengin ve toprak tonlu sıcaklığını yansıtır.",
                "Un liner rouille, une paupière moutarde et une lèvre terracotta expriment la chaleur riche et terreuse de l'automne.",
                "Un delineador óxido, párpados color mostaza y un labial terracota reflejan la calidez rica y terrosa del otoño.",
                "محدد عيون صدئي، جفون بلون الخردل، وأحمر شفاه تراكوتا يعكسون دفء الخريف الغني والترابي.",
                "Ржавая подводка, горчичные веки и терракотовая помада передают насыщенное, землистое тепло осени."),
            Category = "Seasonal",
            CompatibleSeasons = ["Autumn"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel", "Green"],
            Lips = Swatch("Warm Terracotta", "Autumn Lip", "#B8665C"),
            Cheeks = Swatch("Warm Camel Flush", "Autumn Blush", "#B08A5A"),
            Contour = Swatch("Deep Espresso", "Autumn Contour", "#5B4434"),
            Eyeshadow = Swatch("Warm Mustard", "Autumn Lid", "#C99A2E"),
            Liner = Swatch("Rust", "Autumn Liner", "#8A3E24"),
            Brow = Swatch("Deep Warm Brown", "Autumn Brow", "#4A3020"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Soft Autumn Harvest Moon", "Yumuşak Sonbahar Hasat Ayı", "Lune des récoltes automne doux", "Luna de cosecha otoño suave", "قمر حصاد الخريف الناعم", "Урожайная луна мягкой осени"),
            Description = T(
                "Muted olive on the lid and a soft bronze lip give Soft Autumn a second, moodier finish for evening.",
                "Göz kapağındaki mat zeytin yeşili ve yumuşak bronz dudak, Yumuşak Sonbahar'a akşamlar için ikinci, daha hüzünlü bir bitiş kazandırır.",
                "Une paupière olive mate et une lèvre bronze douce offrent à l'automne doux une seconde finition plus intense pour le soir.",
                "Un párpado oliva apagado y un labial bronce suave dan al otoño suave un segundo acabado más intenso para la noche.",
                "جفن زيتوني باهت وأحمر شفاه برونزي ناعم يمنحان الخريف الناعم لمسة نهائية ثانية أكثر غموضاً للمساء.",
                "Приглушённо-оливковые веки и мягкая бронзовая помада дают мягкой осени второй, более таинственный вечерний вариант."),
            Category = "Seasonal",
            CompatibleSeasons = ["Soft Autumn"],
            CompatibleUndertones = ["Warm", "Neutral"],
            CompatibleEyeColors = ["Green", "Hazel", "Brown"],
            Lips = Swatch("Soft Bronze", "Soft Autumn Lip #2", "#8A6A2B"),
            Cheeks = Swatch("Muted Warm Flush", "Soft Autumn Blush #2", "#9B8572"),
            Contour = Swatch("Soft Espresso", "Soft Autumn Contour #2", "#5B4434"),
            Eyeshadow = Swatch("Muted Olive", "Soft Autumn Lid #2", "#6E6E2E"),
            Liner = Swatch("Soft Espresso Liner", "Soft Autumn Liner #2", "#3E2E20"),
            Brow = Swatch("Soft Warm Brown", "Soft Autumn Brow #2", "#4A3624"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Warm Autumn Ember", "Sıcak Sonbahar Köz", "Braise automne chaud", "Brasa de otoño cálido", "جمر الخريف الدافئ", "Тлеющий уголёк тёплой осени"),
            Description = T(
                "Pumpkin-orange lids and a brick-red lip glow like embers for Warm Autumn's rich intensity.",
                "Balkabağı turuncusu göz kapakları ve tuğla kırmızısı dudak, Sıcak Sonbahar'ın zengin yoğunluğu için köz gibi parlar.",
                "Des paupières orange citrouille et une lèvre rouge brique brillent comme des braises pour l'intensité riche de l'automne chaud.",
                "Párpados naranja calabaza y un labial rojo ladrillo brillan como brasas para la rica intensidad del otoño cálido.",
                "جفون برتقالية بلون اليقطين وأحمر شفاه أحمر آجري يتوهجان كالجمر ليعكسا كثافة الخريف الدافئ الغنية.",
                "Тыквенно-оранжевые веки и кирпично-красная помада светятся, как угли, отражая насыщенную интенсивность тёплой осени."),
            Category = "Seasonal",
            CompatibleSeasons = ["Warm Autumn"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel"],
            Lips = Swatch("Warm Brick Red", "Warm Autumn Lip", "#A8462E"),
            Cheeks = Swatch("Golden Camel Flush", "Warm Autumn Blush", "#B08040"),
            Contour = Swatch("Warm Chocolate", "Warm Autumn Contour", "#4A3324"),
            Eyeshadow = Swatch("Deep Pumpkin", "Warm Autumn Lid", "#D97C34"),
            Liner = Swatch("Warm Chestnut", "Warm Autumn Liner", "#5A3420"),
            Brow = Swatch("Deep Warm Brown", "Warm Autumn Brow", "#3E2818"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Deep Autumn Twilight", "Derin Sonbahar Alacakaranlığı", "Crépuscule automne profond", "Crepúsculo de otoño profundo", "غسق الخريف العميق", "Сумерки глубокой осени"),
            Description = T(
                "Deep rust liner and a rich aubergine crease bring bold, saturated warmth to Deep Autumn.",
                "Derin pas eyeliner ve zengin patlıcan tonlu kıvrım, Derin Sonbahar'a cesur ve doygun bir sıcaklık katar.",
                "Un liner rouille profond et un pli aubergine riche apportent une chaleur audacieuse et saturée à l'automne profond.",
                "Un delineador óxido profundo y un pliegue berenjena rico aportan una calidez audaz y saturada al otoño profundo.",
                "محدد عيون صدئي عميق وتجعيد جفن باذنجاني غني يمنحان الخريف العميق دفئاً جريئاً ومشبعاً.",
                "Глубокая ржавая подводка и насыщенная баклажанная складка века придают глубокой осени смелое, насыщенное тепло."),
            Category = "Seasonal",
            CompatibleSeasons = ["Deep Autumn"],
            CompatibleUndertones = ["Warm"],
            CompatibleEyeColors = ["Brown", "Hazel"],
            Lips = Swatch("Deep Rust", "Deep Autumn Lip", "#9C4126"),
            Cheeks = Swatch("Deep Bronze Flush", "Deep Autumn Blush", "#7A5A20"),
            Contour = Swatch("Deep Espresso", "Deep Autumn Contour", "#4A342A"),
            Eyeshadow = Swatch("Deep Aubergine", "Deep Autumn Lid", "#4A2E3A"),
            Liner = Swatch("Deep Rust Liner", "Deep Autumn Liner", "#6A2E1A"),
            Brow = Swatch("Deep Espresso Brow", "Deep Autumn Brow", "#3A281E"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Classic Winter Frost", "Klasik Kış Kırağısı", "Givre d'hiver classique", "Escarcha de invierno clásica", "صقيع الشتاء الكلاسيكي", "Классический зимний иней"),
            Description = T(
                "A true red lip and cool emerald liner deliver Winter's crisp, high-contrast drama.",
                "Gerçek kırmızı dudak ve soğuk zümrüt eyeliner, Kış'ın keskin ve yüksek kontrastlı dramasını sunar.",
                "Une lèvre rouge vrai et un liner émeraude froid livrent le drame net et contrasté de l'hiver.",
                "Un labial rojo verdadero y un delineador esmeralda frío ofrecen el drama nítido y contrastado del invierno.",
                "أحمر شفاه أحمر نقي ومحدد عيون زمردي بارد يقدمان دراما الشتاء الحادة وعالية التباين.",
                "Настоящая красная помада и холодная изумрудная подводка передают чёткую, контрастную драму зимы."),
            Category = "Seasonal",
            CompatibleSeasons = ["Winter"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray", "Brown"],
            Lips = Swatch("Cool True Red", "Winter Lip", "#C81E2E"),
            Cheeks = Swatch("Fuchsia Flush", "Winter Blush", "#C82E8A"),
            Contour = Swatch("Cool Charcoal", "Winter Contour", "#3A3A3F"),
            Eyeshadow = Swatch("Cool Emerald", "Winter Lid", "#0E7A54"),
            Liner = Swatch("True Black", "Winter Liner", "#0E0E14"),
            Brow = Swatch("Cool Deep Brown", "Winter Brow", "#2E241C"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Deep Winter Midnight", "Derin Kış Geceyarısı", "Minuit hiver profond", "Medianoche de invierno profundo", "منتصف ليل الشتاء العميق", "Полночь глубокой зимы"),
            Description = T(
                "Wine-stained lips and a deep navy liner give Deep Winter a saturated, midnight-luxe finish.",
                "Şarap tonlu dudaklar ve derin lacivert eyeliner, Derin Kış'a doygun ve gece yarısı lüksü bir bitiş kazandırır.",
                "Des lèvres teintées vin et un liner marine profond donnent à l'hiver profond une finition saturée façon minuit luxueux.",
                "Labios teñidos de vino y un delineador azul marino profundo dan al invierno profundo un acabado saturado de lujo nocturno.",
                "شفاه بلون النبيذ ومحدد عيون كحلي عميق يمنحان الشتاء العميق لمسة نهائية مشبعة وفاخرة بروح منتصف الليل.",
                "Винные губы и глубокая тёмно-синяя подводка придают глубокой зиме насыщенный, роскошный полуночный финиш."),
            Category = "Seasonal",
            CompatibleSeasons = ["Deep Winter"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Brown", "Gray"],
            Lips = Swatch("Deep Wine", "Deep Winter Lip", "#6E1E30"),
            Cheeks = Swatch("Deep Fuchsia Flush", "Deep Winter Blush", "#8A1E5C"),
            Contour = Swatch("Cool Deep Taupe", "Deep Winter Contour", "#4A423C"),
            Eyeshadow = Swatch("Deep Navy", "Deep Winter Lid", "#101E42"),
            Liner = Swatch("True Black", "Deep Winter Liner", "#0E0E14"),
            Brow = Swatch("Deep Charcoal Brow", "Deep Winter Brow", "#242024"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Cool Winter Ice", "Soğuk Kış Buzu", "Glace hiver frais", "Hielo de invierno frío", "جليد الشتاء البارد", "Лёд прохладной зимы"),
            Description = T(
                "Icy blue lids and a magenta lip capture Cool Winter's crisp, jewel-bright chill.",
                "Buzlu mavi göz kapakları ve macenta dudak, Soğuk Kış'ın keskin ve mücevher parlaklığındaki soğukluğunu yakalar.",
                "Des paupières bleu glacé et une lèvre magenta capturent le froid net et éclatant de l'hiver frais.",
                "Párpados azul hielo y un labial magenta capturan el frío nítido y brillante del invierno frío.",
                "جفون زرقاء جليدية وأحمر شفاه ماجنتا يعكسان برودة الشتاء البارد الحادة واللامعة كالأحجار الكريمة.",
                "Ледяные голубые веки и пурпурная помада передают чёткий, драгоценно-яркий холод прохладной зимы."),
            Category = "Seasonal",
            CompatibleSeasons = ["Cool Winter"],
            CompatibleUndertones = ["Cool"],
            CompatibleEyeColors = ["Blue", "Gray"],
            Lips = Swatch("Cool Magenta", "Cool Winter Lip", "#C81E8A"),
            Cheeks = Swatch("Cool Red Flush", "Cool Winter Blush", "#C81E2E"),
            Contour = Swatch("Cool Charcoal", "Cool Winter Contour", "#3A3A3F"),
            Eyeshadow = Swatch("Icy Blue", "Cool Winter Lid", "#7EA8C4"),
            Liner = Swatch("Cool Charcoal Liner", "Cool Winter Liner", "#1E1E24"),
            Brow = Swatch("Cool Ash Brown", "Cool Winter Brow", "#3A342E"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Clear Winter Vivid", "Berrak Kış Canlılığı", "Hiver clair vif", "Invierno claro vívido", "شتاء صافٍ زاهٍ", "Яркая чистая зима"),
            Description = T(
                "A vivid royal-purple lid and true black liner bring Clear Winter's high-contrast brilliance to full drama.",
                "Canlı kraliyet moru göz kapağı ve gerçek siyah eyeliner, Berrak Kış'ın yüksek kontrastlı parlaklığını tam dramaya taşır.",
                "Une paupière violet royal vif et un liner noir vrai portent la brillance ultra-contrastée de l'hiver clair à son maximum.",
                "Un párpado morado real vívido y un delineador negro verdadero llevan el brillo de alto contraste del invierno claro a todo su drama.",
                "جفن بنفسجي ملكي زاهٍ ومحدد عيون أسود نقي يرفعان بريق الشتاء الصافي عالي التباين إلى أقصى درجات الجاذبية.",
                "Яркие королевско-фиолетовые веки и настоящая чёрная подводка доводят контрастный блеск чистой зимы до полного драматизма."),
            Category = "Seasonal",
            CompatibleSeasons = ["Clear Winter"],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Blue", "Green", "Brown"],
            Lips = Swatch("Vivid Red", "Clear Winter Lip #1", "#E0142E"),
            Cheeks = Swatch("Hot Pink Flush", "Clear Winter Blush #1", "#EF4B8F"),
            Contour = Swatch("Cool Navy Contour", "Clear Winter Contour #1", "#101E42"),
            Eyeshadow = Swatch("Royal Purple", "Clear Winter Lid #1", "#4A1E9C"),
            Liner = Swatch("True Black", "Clear Winter Liner #1", "#0E0E14"),
            Brow = Swatch("True Black Brow", "Clear Winter Brow #1", "#1A1A1E"),
            SortOrder = order++
        });

        looks.Add(new MakeupLookDocument
        {
            Title = T("Clear Winter Turquoise", "Berrak Kış Turkuazı", "Turquoise hiver clair", "Turquesa de invierno claro", "فيروز الشتاء الصافي", "Бирюза чистой зимы"),
            Description = T(
                "Clear turquoise liner against a hot-pink lip creates the striking, saturated contrast Clear Winter wears best.",
                "Berrak turkuaz eyeliner ve pembe dudak, Berrak Kış'ın en iyi taşıdığı çarpıcı ve doygun kontrastı yaratır.",
                "Un liner turquoise vif contre une lèvre rose vif crée le contraste saisissant et saturé que l'hiver clair porte le mieux.",
                "Un delineador turquesa vívido junto a un labial rosa fuerte crea el contraste llamativo y saturado que mejor luce el invierno claro.",
                "محدد عيون فيروزي صافٍ مع أحمر شفاه وردي فاقع يخلقان التباين اللافت والمشبع الذي يليق بالشتاء الصافي.",
                "Чистая бирюзовая подводка в паре с ярко-розовой помадой создаёт эффектный, насыщенный контраст, который так идёт чистой зиме."),
            Category = "Seasonal",
            CompatibleSeasons = ["Clear Winter"],
            CompatibleUndertones = ["Cool", "Neutral"],
            CompatibleEyeColors = ["Green", "Blue"],
            Lips = Swatch("Clear Hot Pink", "Clear Winter Lip #2", "#EF4B8F"),
            Cheeks = Swatch("Cool White Flush", "Clear Winter Blush #2", "#F0F2F5"),
            Contour = Swatch("Cool Navy Contour", "Clear Winter Contour #2", "#101E42"),
            Eyeshadow = Swatch("Clear Turquoise", "Clear Winter Lid #2", "#0EA8A0"),
            Liner = Swatch("Clear Turquoise Liner", "Clear Winter Liner #2", "#086E68"),
            Brow = Swatch("True Black Brow", "Clear Winter Brow #2", "#1A1A1E"),
            SortOrder = order++
        });

        return looks;
    }

    private static Dictionary<string, string> T(string en, string tr, string fr, string es, string ar, string ru) => new()
    {
        ["en"] = en, ["tr"] = tr, ["fr"] = fr, ["es"] = es, ["ar"] = ar, ["ru"] = ru
    };

    private static ColorSwatch Swatch(string name, string code, string hex) => new() { Name = name, Code = code, Hex = hex };
}
