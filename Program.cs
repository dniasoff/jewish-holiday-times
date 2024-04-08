// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.InteropServices.JavaScript.JSType;



RequestOptions requestOptions = new RequestOptions();

string baseUrl = requestOptions.GetUrl(53.51299751174321, -2.251411602288692,79,"Europe/London");

using var client = new HttpClient();

IDictionary<DateTime, Week> weeks = new Dictionary<DateTime, Week>();
IDictionary<DateTime, Day> days = new Dictionary<DateTime, Day>();

foreach (string weekRange in EachWeek(10))
{

    string startDate = weekRange.Split(',')[0];
    string endDate = weekRange.Split(",")[1];

    string url = baseUrl;

    url += "&start=" + startDate;
    url += "&end=" + endDate;

    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();
    var responseBody = await response.Content.ReadAsStringAsync();

    File.WriteAllText(@"C:\Users\DanielNiasoff\Downloads\response.json", responseBody);

    Root r = JsonConvert.DeserializeObject<Root>(responseBody);

    DateTime FirstDayOfWeek = DateTime.Parse(r.range.start);
    Week Week = new Week(FirstDayOfWeek);

    weeks.Add(FirstDayOfWeek, Week);

    for (int i = 0; i < 7; i++)
    {
        DateTime CurrentDay = FirstDayOfWeek.AddDays(i);
        Day day = new Day(FirstDayOfWeek);
        days.Add(CurrentDay, day);
    }

    foreach (var i in r.items)
    {
        Category category = (Category)Enum.Parse(typeof(Category), i.category, true);
        DateTime date = DateTime.Parse(i.date);
        DateTime day = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);


        switch (category)
        {
            case Category.candles:
                days[day].CandleLighting = date;
                break;
            case Category.chofetzChaim:
                // code block
                break;
            case Category.dafyomi:
                // code block
                break;
            case Category.dailyPsalms:
                // code block
                break;
            case Category.dailyRambam1:
                // code block
                break;
            case Category.havdalah:
                days[day].Havdalah = date;
                break;
            case Category.hebdate:
                days[day].HebrewDate = i.hebrew + " " + i.heDateParts.y;
                days[day].HebrewDateInEnglish = i.hdate;
                days[day].HebrewDateDay = i.heDateParts.d;
                days[day].HebrewDateMonth = i.heDateParts.m;
                days[day].HebrewDateYear = i.heDateParts.y;
                days[day].HebrewDateInEnglishSaying = i.title + " " + i.hdate.Split(' ')[2];
                // code block
                break;
            case Category.holiday:               
                days[day].Holiday = i.hebrew;
                days[day].HolidayEnglish = i.title;
                days[day].YomTov = i.yomtov;
                break;
            case Category.mishnayomi:
                // code block
                break;
            case Category.nachyomi:
                // code block
                break;
            case Category.omer:
                days[day].Omer = i.hebrew;
                days[day].OmerEnglish = i.title;
                days[day].OmerDetails = i.omer;
                break;
            case Category.parashat:
                weeks[FirstDayOfWeek].Parsha = i.hebrew;
                weeks[FirstDayOfWeek].ParshaEnglish = i.title;
                break;
            case Category.roshchodesh:
                days[day].RoshChodesh = i.hebrew;
                days[day].RoshChodesh = i.title;
                break;
            case Category.shemiratHaLashon:
                // code block
                break;
            case Category.tanakhYomi:
                // code block
                break;
            case Category.yerushalmi:
                // code block
                break;
            case Category.zmanim:
                days[day].Zmanim.Add(new Body(i.hebrew, i.title));
                break;
            default:
                // code block
                break;
        }
    }

}

IEnumerable<string> EachWeek(int NoOfYears)
{

    CultureInfo myCI = new CultureInfo("en-US");
    Calendar myCal = myCI.Calendar;
    CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;

    for (int i = 0; i < NoOfYears; i++)
    {

        int Year = int.Parse(DateTime.Now.AddYears(i).ToString("yyyy"));

        DateTime FirstDayOfYear = new System.DateTime(Year, 1, 1);
        DateTime LastDayOfYear = new System.DateTime(Year, 12, 31 );
 
        int DaysToFirstSundayOfYear = 0;
        
        if (FirstDayOfYear.DayOfWeek != DayOfWeek.Sunday )
        {
            DaysToFirstSundayOfYear = 7 - (int)FirstDayOfYear.DayOfWeek;
        }

        DateTime FirstSundayOfYear = FirstDayOfYear.AddDays(DaysToFirstSundayOfYear);

        DateTime FirstDayOfWeek = FirstSundayOfYear;

        int counter = 0;

        while (FirstDayOfWeek.Date <= LastDayOfYear.Date)
        {
            
            DateTime LastDayOfWeek = FirstDayOfWeek.AddDays(6);

            string StartDateString = FirstDayOfWeek.ToString("yyyy-MM-dd");
            string EndDateString = LastDayOfWeek.ToString("yyyy-MM-dd");

    
            yield return StartDateString + "," + EndDateString;
            

            counter++;
            FirstDayOfWeek = FirstSundayOfYear.AddDays(counter * 7);
        }            
    }
}


