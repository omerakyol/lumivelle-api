using System.Collections.Generic;

namespace Business.Handlers.Recommendations.WordBanks;

internal static class TrPresetWordBanks
{
    public static readonly Dictionary<string, string> SeasonMood = new()
    {
        ["Spring"] = "Taze ve parlak",
        ["Summer"] = "Yumuşak ve serin",
        ["Autumn"] = "Sıcak ve zengin",
        ["Winter"] = "Berrak ve net"
    };

    public static readonly Dictionary<string, string> ContrastWord = new()
    {
        ["Low"] = "Yumuşak",
        ["Medium"] = "Dengeli",
        ["High"] = "Cesur"
    };

    public static readonly Dictionary<string, string> ContrastDescriptor = new()
    {
        ["Low"] = "yumuşak ve kolay",
        ["Medium"] = "zarif ve doğal",
        ["High"] = "çarpıcı ve net"
    };

    public static readonly Dictionary<string, string> UndertoneAdjective = new()
    {
        ["Warm"] = "güneş öpücüklü",
        ["Cool"] = "ayaz dokunuşlu",
        ["Neutral"] = "zahmetsizce dengeli"
    };

    public static readonly Dictionary<string, string> UndertoneWord = new()
    {
        ["Warm"] = "sıcak",
        ["Cool"] = "serin",
        ["Neutral"] = "nötr"
    };

    public static readonly Dictionary<string, string> ContrastLevel = new()
    {
        ["Low"] = "düşük",
        ["Medium"] = "orta",
        ["High"] = "yüksek"
    };

    public static readonly Dictionary<string, string> FamilyName = new()
    {
        ["Spring"] = "İlkbahar",
        ["Summer"] = "Yaz",
        ["Autumn"] = "Sonbahar",
        ["Winter"] = "Kış"
    };

    public static readonly Dictionary<string, string[]> SeasonNouns = new()
    {
        ["Spring"] =
        [
            "Turunçgil Çiçeği", "Mercan Çiçeği", "Şeftali Işıltısı", "Berrak Işık", "Taze Çiçeklenme",
            "Altın Çiçek", "Parlak Çayır", "Yaprak Pembeliği", "Sabah Çiği", "Kiraz Çiçeği", "Bal Rengi Işık",
            "Bahar Çayırı", "Düğün Çiçeği Işıltısı", "Pembe Yaprak", "Çiy Taneli Çiçek", "Kayısı Işığı",
            "Kır Çiçeği", "Yumuşak Nergis", "Gül Suyu Işıltısı", "Gün Doğumu Çiçeği", "Pastel Çiçek",
            "Yeşil Çayır Işığı", "Zambak Işıltısı", "Lale Pembeliği"
        ],
        ["Summer"] =
        [
            "Toz Pembe", "Arduvaz Mavisi", "Yumuşak Lavanta", "Serin Esinti", "Puslu Mavi", "Pudra Pembe",
            "Sessiz Sis", "Deniz Camı", "Bulut Grisi", "Devetabanı Pusu", "Yumuşak Sis", "Gümüş Pus",
            "Pudra Mavisi", "Mor Salkım Pusu", "Serin Alacakaranlık", "Puslu Leylak", "Yumuşak Çelik",
            "Deniz Köpüğü", "Bulutlu Pembe", "Soluk Orkide", "Buzlu Mavi", "Sessiz Yağmur", "Gri İnci",
            "Soluk Süsen"
        ],
        ["Autumn"] =
        [
            "Altın Saat", "Terrakota Sıcaklığı", "Kehribar Işıltısı", "Baharatlı Kil", "Pas ve Zeytin",
            "Toprak Rengi Işık", "Sonbahar Korları", "Tarçın Baharatı", "Hasat Altını", "Bronz Yaprak",
            "Maun Işıltısı", "Kestane Sıcaklığı", "Bakır Kor", "Kavrulmuş Kehribar", "Yanık Sienna",
            "Altın Buğday", "Akçaağaç Işıltısı", "Okr Işığı", "Sonbahar Baharatı", "Sıcak Karanfil",
            "Kızıl Kahve Işıltısı", "Bal Rengi Bronz", "Muskat Sıcaklığı", "Kadife Çiçeği Koru"
        ],
        ["Winter"] =
        [
            "Keskin Kontrast", "Mücevher Tonu", "Buzlu Işık", "Gerçek Siyah", "Buzul Berraklığı", "Derin Mücevher",
            "Kış Ayazı", "Safir Gece", "Gümüş Ayaz", "Obsidyen Parıltısı", "Kutup Berraklığı",
            "Gece Yarısı Mücevheri", "Oniks Derinliği", "Donmuş Yıldız Işığı", "Zümrüt Gece", "Platin Ayaz",
            "Kış Arduvazı", "Yakut Mücevher", "Buzul Işığı", "Derin Ametist", "Mürekkep Siyahı", "Kristal Ayaz",
            "Çelik Mücevher", "Kutup Netliği"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupTitles = new()
    {
        ["Spring"] =
        [
            "Mercan allık", "Şeftali dudak", "Altın parıltı", "Çiy ışıltısı", "Kayısı allık", "Güneşli eyeliner",
            "Çiçek dudak", "Çayır göz", "Bal parlatıcı", "Kiraz tonu", "Yaprak parıltısı", "Çiy vurgusu",
            "Düğün çiçeği göz kapağı", "Pembe yaprak dudak", "Kır çiçeği yanak", "Nergis parıltısı",
            "Gül suyu parlatıcı", "Gün doğumu allık", "Pastel eyeliner", "Zambak vurgusu", "Lale dudak",
            "Yeşil çayır eyeliner", "Kayısı parlatıcı", "Taze çiy yanak"
        ],
        ["Summer"] =
        [
            "Gül nude dudak", "Toz allık", "Lavanta parıltısı", "Puslu ışıltı", "Arduvaz eyeliner", "Pudra allık",
            "Esinti dudak", "Sessiz parıltı", "Gri-mavi eyeliner", "Devetabanı dokunuş", "Deniz camı parıltısı",
            "Bulut allık", "Mor salkım göz kapağı", "Çelik eyeliner", "Deniz köpüğü vurgusu",
            "Bulutlu gül dudak", "Soluk orkide parıltısı", "Buzlu mavi eyeliner", "Sessiz yağmur allık",
            "İnci vurgusu", "Soluk süsen göz kapağı", "Kot mavisi eyeliner", "Güvercin grisi parıltı",
            "Pudra leylak dudak"
        ],
        ["Autumn"] =
        [
            "Terrakota dudak", "Altın parıltı", "Şeftali allık", "Kehribar parıltısı", "Baharatlı kil dudak",
            "Sıcak bronz", "Pas eyeliner", "Zeytin vurgusu", "Tarçın dudak", "Bronz yaprak parıltısı",
            "Maun eyeliner", "Kestane allık", "Bakır kor göz kapağı", "Kavrulmuş kehribar dudak",
            "Yanık sienna eyeliner", "Altın buğday allık", "Akçaağaç parıltısı", "Okr göz kapağı dokunuşu",
            "Sıcak karanfil eyeliner", "Kızıl kahve dudak", "Bal rengi bronz yanak", "Muskat parıltısı",
            "Kadife çiçeği vurgusu", "Sienna parlatıcı"
        ],
        ["Winter"] =
        [
            "Gerçek kırmızı dudak", "Buzlu vurgu", "Mücevher göz", "Buzlu allık", "Oniks eyeliner",
            "Böğürtlen dudak", "Platin parıltısı", "Kömür dumanı", "Safir eyeliner", "Gümüş ayaz parıltısı",
            "Obsidyen kirpik", "Gece yarısı eriği dudak", "Oniks derinliği eyeliner", "Yıldız ışığı vurgusu",
            "Zümrüt eyeliner", "Platin göz kapağı", "Arduvaz duman göz", "Yakut dudak", "Buzul vurgusu",
            "Ametist eyeliner", "Mürekkep siyahı kirpik", "Kristal parıltı", "Çelik mücevher eyeliner",
            "Kutup dudak"
        ]
    };

    public static readonly Dictionary<string, string[]> MakeupSubtitles = new()
    {
        ["Spring"] =
        [
            "Taze gündüz yanağı", "Yumuşak turunçgil nude", "Parlak sabah gözü", "Işıltılı taze bitiş",
            "Sıcak tonlu yanak", "Hafif bronz belirginlik", "Şeffaf mercan dokunuşu",
            "Yumuşak yeşil-altın parıltı", "Şeffaf altın dudak", "Yumuşak pembe allık",
            "Hafif pembe göz dokunuşu", "Taze cam cilt ışıltısı", "Yumuşak sarı-altın dokunuş",
            "Şeffaf gül tonu", "Çok tonlu allık", "Parlak altın göz kapağı", "Nemli pembe dudak",
            "Sıcak şeftali ışıltısı", "Yumuşak leylak belirginlik", "Taze ışıltılı parlaklık",
            "Parlak mercan-pembe", "Yumuşak adaçayı belirginlik", "Şeffaf sıcak parlaklık",
            "Işıltılı doğal allık"
        ],
        ["Summer"] =
        [
            "Soluk gündüz gülü", "Serin tonlu yanak", "Yumuşak serin göz", "Şeffaf nemli bitiş",
            "Yumuşak dumanlı belirginlik", "Serin pembe allık", "Şeffaf mor-pembe ton", "Soluk serin vurgu",
            "Yumuşak dumanlı belirginlik", "Serin göz kapağı parıltısı", "Sedefli serin vurgu",
            "Neredeyse yok denecek serin allık", "Yumuşak mor-gri dokunuş", "Serin gri belirginlik",
            "Serin sedefli ışıltı", "Soluk mor-pembe ton", "Yumuşak lavanta-pembe göz",
            "Serin hafif belirginlik", "Yumuşak gri tonlu yanak", "Serin ışıltılı parlaklık",
            "Serin mor dokunuş", "Yumuşak kot mavisi ton", "Sessiz serin vurgu", "Şeffaf serin ton"
        ],
        ["Autumn"] =
        [
            "Sıcak nude · gündüz", "Şampanya gözü", "Krem allık", "Bronz göz görünümü", "Derin mat bitiş",
            "Güneş öpücüklü yanaklar", "Dumanlı sıcak göz", "Toprak tonlu göz", "Baharatlı sıcak nude",
            "Metalik sıcak göz", "Derin sıcak belirginlik", "Sıcak terrakota yanak", "Sıcak metalik parıltı",
            "Sıcak karamel nude", "Derin sıcak belirginlik", "Güneş dokunuşlu sıcak yanak", "Sıcak bronz göz",
            "Toprak tonlu sıcaklık", "Baharatlı kahverengi belirginlik", "Derin sıcak kızıl-kahve",
            "Sıcak altın allık", "Sıcak baharatlı göz tonu", "Sıcak altın parıltı", "Sıcak terrakota parlaklığı"
        ],
        ["Winter"] =
        [
            "Cesur klasik bitiş", "Keskin serin parıltı", "Derin safir tonu", "Serin tonlu yanak",
            "Keskin belirgin çizgi", "Derin serin böğürtlen", "Buzlu vurgu tonu", "Dramatik kış gözü",
            "Derin mücevher belirginliği", "Buzlu metalik vurgu", "Keskin dramatik bitiş",
            "Derin serin erik tonu", "Derin keskin belirginlik", "Buzlu ışıltılı parlaklık",
            "Derin zümrüt-yeşil belirginlik", "Serin metalik parıltı", "Serin dramatik bitiş",
            "Derin mücevher kırmızısı", "Buzlu serin ışıltı", "Derin mor belirginlik",
            "Keskin belirgin kirpik çizgisi", "Buzlu metalik vurgu", "Serin metalik belirginlik",
            "Serin buzlu pembe"
        ]
    };

    public static readonly Dictionary<string, string[]> AccessoryPool = new()
    {
        ["Spring"] =
        [
            "İnci küpe", "Örgü çanta", "Çiçekli eşarp", "Altın bilezik", "Hasır şapka", "Pastel saç bandı",
            "Rose gold halka küpe", "Kanvas çanta", "Papatya saç tokası", "Mercan bileklik",
            "Keten elbise eşarbı", "Tereyağı sarısı çanta", "Papatya kolye", "Pembe kurdele fiyonk",
            "Örgü hasır el çantası", "Pastel emaye yüzük", "Kiraz çiçeği toka", "Tereyağı sarısı eşarp",
            "Mercan sarkan küpe", "Hasır güneş şapkası", "Pembe kuvars kolye", "Keten başörtüsü",
            "Yaprak pembesi bileklik", "Altın sarmaşık yüzük"
        ],
        ["Summer"] =
        [
            "Gümüş halka küpe", "Kot ceket", "Keten eşarp", "Mavi emaye yüzük", "Pamuk çanta", "İnci bileklik",
            "Kot mavisi şal", "Deniz camı kolye", "Tatlı su incisi kolye", "Gri kaşmir eşarp", "Gümüş halhal",
            "Deniz camı küpe", "Kot bucket şapka", "Deniz kabuğu kolye", "Pamuk başörtüsü", "Mavi topaz yüzük",
            "Örgü hasır çanta", "Pudra mavisi eşarp", "Gümüş kabuk küpe", "Keten kemer", "Gri keçe şapka",
            "İnci halhal", "Kot mavisi saç bandı", "Deniz camı yüzük"
        ],
        ["Autumn"] =
        [
            "Altın halka küpe", "İpek eşarp", "Deri çanta", "Kehribar küpe", "Süet kemer", "Bağa desenli toka",
            "Bakır bilezik", "Yün şal", "Kestane rengi deri bot", "Örgü sepet çanta", "Pirinç halka küpe",
            "Deve tüyü yün eşarp", "Süet bot", "Kehribar kolye", "Örgü yün eşarp", "Bronz bilezik",
            "Deri çapraz çanta", "Bakır sarkan küpe", "Deve tüyü keçe şapka", "Bağa desenli güneş gözlüğü",
            "Pas rengi örgü bere", "Pirinç yüzük seti", "Yün ekose eşarp", "Kestane rengi deri kemer"
        ],
        ["Winter"] =
        [
            "Gümüş küpe", "Kadife eşarp", "Siyah deri eldiven", "Oniks yüzük", "Yün bere", "Kristal sarkan küpe",
            "Suni kürk stola", "Platin bilezik", "Gümüş bilezik", "Siyah saten kurdele", "Pırlanta küpe",
            "Kömür grisi yün eşarp", "Kaşmir şal", "İnci sarkan küpe", "Siyah kadife saç bandı",
            "Gümüş kar tanesi kolye", "Suni kürk kulaklık", "Oniks kol düğmesi", "Kömür grisi yün eldiven",
            "Kristal saç tokası", "Platin zincir kolye", "Kadife el çantası", "Gümüş halka küpe",
            "Koyu erik rengi eşarp"
        ]
    };

    public static string BuildTitle(string contrast, string noun) => $"{ContrastWord[contrast]} {noun}";

    public static string BuildSubtitle(int variant, string family, string undertone, string contrast, string noun)
    {
        var mood = SeasonMood[family];
        var undertoneLower = UndertoneWord[undertone];
        var contrastLower = ContrastLevel[contrast];
        var familyLower = FamilyName[family].ToLowerInvariant();
        var nounLower = noun.ToLowerInvariant();
        var contrastDesc = ContrastDescriptor[contrast];

        return variant switch
        {
            0 => $"{mood} tonları, {undertoneLower} {contrastLower} kontrastlı paletine göre ayarlandı",
            1 => $"{Capitalize(contrastDesc)} tonlar, {undertoneLower} parlaklığın için",
            2 => $"{noun} enerjisi, {familyLower} paletine özel",
            3 => $"{Capitalize(undertoneLower)}, {contrastLower} kontrastlı, {nounLower} esinli bir seçki",
            4 => $"{FamilyName[family]} esintili, {contrastLower} kontrastlı bugünün stili",
            _ => $"Lumi'nin {undertoneLower} tonlu seçimi, {nounLower} ile katmanlandı"
        };
    }

    public static string BuildDescription(string family, string undertone, string contrast, string noun)
    {
        var moodLower = SeasonMood[family].ToLowerInvariant();
        var nounLower = noun.ToLowerInvariant();
        var undertoneAdjective = UndertoneAdjective[undertone];
        var contrastDesc = ContrastDescriptor[contrast];

        return $"Bugünün ışığı {moodLower} — Lumi, {undertoneAdjective} parlaklığını {contrastDesc} tutmak için {nounLower} tonuna yöneldi.";
    }

    private static string Capitalize(string value) =>
        string.IsNullOrEmpty(value) ? value : char.ToUpperInvariant(value[0]) + value[1..];
}
