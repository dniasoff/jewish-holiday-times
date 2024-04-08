
public class RequestOptions
{
   public bool DafYomi { get; set; }
    public bool MishnaYomi { get; set; }
    public bool NachYomi { get; set; }
    public bool TanakhYomi { get; set; }
    public bool DailyTehillim { get; set; }
    public bool SeferChofetzChaim { get; set; }
    public bool ShemirasHaLashon { get; set; }
    public bool DailyRambam { get; set; }
    public bool YerushalmiYomi { get; set; }
    public bool DafAWeek { get; set; }
    public bool DiasporaMode { get; set; }
    public bool MajorHolidays { get; set; }
    public bool YomTovOnly { get; set; }
    public bool MinorHolidays { get; set; }
    public bool RoshChodesh { get; set; }
    public bool MinorFasts { get; set; }
    public bool SpecialShabbatot { get; set; }
    public bool ParshatHaShavua { get; set; }
    public bool LeyningOff { get; set; }
    public bool HebrewDate { get    ; set; }
    public bool DaysOfOmer { get; set; }
    public bool YomKippurKatan { get; set; }
    public bool CandleLightingTimes { get; set; }
    public int CandleLightingMinutesBeforeSunset { get; set; }
    public bool HavdalahAtNightfall { get; set; }
    public int? HavdalahMinutesAfterSundown { get; set; }
    public bool UseElevationForLocation { get; set; }


    public RequestOptions()
    {
        MajorHolidays = true;
        MinorHolidays = true;
        DafYomi =true;
        MishnaYomi =true;
        NachYomi =true;
        TanakhYomi =true;
        DailyTehillim =true;
        SeferChofetzChaim =true;
        ShemirasHaLashon =true;
        DailyRambam =true;
        YerushalmiYomi =true;
        DafAWeek =true;
        DiasporaMode =true;
        MajorHolidays =true;
        YomTovOnly = false;
        MinorHolidays = true;
        RoshChodesh = true;
        MinorFasts = true;
        SpecialShabbatot = true;
        ParshatHaShavua =true;
        LeyningOff =true;
        HebrewDate = true;
        DaysOfOmer = true;
        YomKippurKatan = true;
        CandleLightingTimes =true;
        CandleLightingMinutesBeforeSunset = 15;
        HavdalahAtNightfall =true;
        HavdalahMinutesAfterSundown = null;
        UseElevationForLocation =true;
    }

    public string GetUrl (double latitude, double longitude, int elevation, string tzid) {
        string url = "https://www.hebcal.com/hebcal?v=1&cfg=json&lg=a";
        url += "&latitude=" + latitude.ToString();
        url += "&longitude=" + longitude.ToString();
        url += "&elev=" + elevation.ToString();
        url += "&tzid=" + tzid;
      
        if (DafYomi) {
            url += "&F=on";
        }
        if (MishnaYomi) {
            url += "&myomi=on";
        }
        if (NachYomi) {
            url += "&nyomi=on";
        }
        if (TanakhYomi) {
            url += "&dty=on";
        }
        if (DailyTehillim) {
            url += "&dps=on";
        }
        if (SeferChofetzChaim) {
            url += "&dcc=on";
        }
        if (ShemirasHaLashon) {
            url += "&dshl=on";
        }
        if (DailyRambam) {
            url += "&dr1=on";
        }
        if (YerushalmiYomi) {
            url += "&yyomi=on";
        }
        if (DafAWeek) {
            url += "&dw=on";
        }
        if (!DiasporaMode) {
            url += "&i=on";
        }
        if (MajorHolidays) {
            url += "&maj=on";
        }
        if (YomTovOnly) {
            url += "&yto=on";
        }
        if (MinorHolidays) {
            url += "&min=on";
        }
        if (RoshChodesh) {
            url += "&nx=on";
        }
        if (MinorFasts) {
            url += "&mf=on";
        }
        if (SpecialShabbatot) {
            url += "&s=on";
        }
        if (ParshatHaShavua) {
            url += "&s=on";
        }
        if (LeyningOff) {
            url += "&leyning=off";
        }
        if (HebrewDate) {
            url += "&d=on";
        }
        if (DaysOfOmer) {
            url += "&o=on";
        }
        if (YomKippurKatan) {
            url += "&ykk=on";
        }
        if (CandleLightingTimes) {
            url += "&c=on";
            url += "&b=" + CandleLightingMinutesBeforeSunset.ToString();
        }

        if (!HavdalahMinutesAfterSundown.HasValue && !HavdalahAtNightfall) {
             throw new InvalidOperationException("When HavdalahMinutesAfterSundown is null, HavdalahAtNightfall must be true");
        }

        if (HavdalahAtNightfall) {
            url += "&M=on";
        }
        else
        {
            url += "&m=" + HavdalahMinutesAfterSundown.ToString();
        }

        if (UseElevationForLocation) {
            url += "&ue=on";
        }

        return url;
    }
}

 public class HeDateParts
    {
        public string y { get; set; }
        public string m { get; set; }
        public string d { get; set; }
    }

public class Item
{
    public string title { get; set; }
    public string date { get; set; }
    public string hdate { get; set; }
    public string category { get; set; }
    public string subcat { get; set; }
    public bool yomtov { get; set; }
    public string title_orig { get; set; }
    public string hebrew { get; set; }
    public HeDateParts heDateParts { get; set; }
    public string link { get; set; }
    public string memo { get; set; }
    public Omer omer { get; set; }
}

public class Location
{
    public string title { get; set; }
    public string city { get; set; }
    public string tzid { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public int elevation { get; set; }
    public string geo { get; set; }
}

public class Range
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class Root
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public Location location { get; set; }
        public Range range { get; set; }
        public List<Item> items { get; set; }
    }


public class Day
{
    public string Title { get; set; }
    public DateTime FirstDayOfWeek { get; set; }
    public string HebrewDateInEnglish { get; set; }
    public string HebrewDateInEnglishSaying { get; set; }
    public string HebrewDate { get; set; }
    public string HebrewDateYear { get; set; }
    public string HebrewDateMonth { get; set; }
    public string HebrewDateDay { get; set; }
    public DateTime CandleLighting { get; set; }
    public DateTime Havdalah { get; set; }
    public Boolean YomTov { get; set; }

    public string Holiday { get; set; }
    public string HolidayEnglish { get; set; }

    public string RoshChodesh { get; set; }
    public string RoshChodeshEnglish { get; set; }

    public string Omer { get; set; }
    public string OmerEnglish { get; set; }


    public CategoryData DafYomi { get; set; }
    public CategoryData MishnaYomi { get; set; }
    public CategoryData NachYomi { get; set; }
    public CategoryData TnachYomi { get; set; }
    public CategoryData DailyRambam { get; set; }
    public CategoryData Yerushalmi { get; set; }
    public CategoryData ChofetzChaim { get; set; }
    public CategoryData ShemirasHaLashon { get; set; }
    public CategoryData Tehillim { get; set; }

    public Omer OmerDetails { get; set; }

    public List<Body> Zmanim { get; set; }

    public Day(DateTime firstDayOfWeek)
    {
        FirstDayOfWeek = firstDayOfWeek;
        YomTov = false;
        Zmanim = new List<Body>();
    }

}

public class Week
{
    public string Parsha { get; set; }
    public string ParshaEnglish { get; set; }
    public DateTime FirstDayOfWeek { get; set; }

    public Week(DateTime firstDayOfWeek)
    {
        FirstDayOfWeek = firstDayOfWeek;
    }

}



public enum Category
{
    hebdate,
    dafyomi,
    dafweekly,
    mishnayomi,
    nachyomi,
    tanakhYomi,
    dailyRambam1,
    yerushalmi,
    chofetzChaim,
    shemiratHaLashon,
    dailyPsalms,
    omer,
    roshchodesh,
    parashat,
    candles,
    havdalah,
    zmanim,
    holiday
}

public class CategoryData
{
    public string Name {get; set; }
    public string NameEnglish { get; set; }
    public string Description { get; set; }
    public string Link {  get; set; }

} 



public class Omer
{
    public Body count { get; set; }
    public Sefira sefira { get; set; }
}

public class Sefira
{
    public string he { get; set; }
    public string translit { get; set; }
    public string en { get; set; }
}
public class Body
{
    public string he { get; set; }
    public string en { get; set; }

    public Body()
    {

    }

    public Body (string he, string en)
    {
        this.he = he;
        this.en = en;
    }
}
