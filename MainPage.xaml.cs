using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Exchange5
{
    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //DateTime dzis = DateTime.Now;
            //Date.MaximumDate = dzis;
        }

        private void ExClicked(object sender, EventArgs e)
        {
            string dateToday = DateTime.Today.ToString("yyyy-MM-dd");
            string dateYesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            string USDUrl = $"https://api.nbp.pl/api/exchangerates/rates/c/USD/{dateToday}/?format=json";
            string EURUrl = $"https://api.nbp.pl/api/exchangerates/rates/c/EUR/{dateToday}/?format=json";
            string GBPUrl = $"https://api.nbp.pl/api/exchangerates/rates/c/GBP/{dateToday}/?format=json";

            string USDJson, EURJson, GBPJson;
            using (var webClient = new WebClient())
            {
                USDJson = webClient.DownloadString(USDUrl);
                EURJson = webClient.DownloadString(EURUrl);
                GBPJson = webClient.DownloadString(GBPUrl);
            }
            Currency USD = JsonSerializer.Deserialize<Currency>(USDJson);
            Currency EUR = JsonSerializer.Deserialize<Currency>(EURJson);
            Currency GBP = JsonSerializer.Deserialize<Currency>(GBPJson);

            USDUrl = $"https://api.nbp.pl/api/exchangerates/rates/c/USD/{dateYesterday}/?format=json";
            EURUrl = $"https://api.nbp.pl/api/exchangerates/rates/c/EUR/{dateYesterday}/?format=json";
            GBPUrl = $"https://api.nbp.pl/api/exchangerates/rates/c/GBP/{dateYesterday}/?format=json";
            using (var webClient = new WebClient())
            {
                USDJson = webClient.DownloadString(USDUrl);
                EURJson = webClient.DownloadString(EURUrl);
                GBPJson = webClient.DownloadString(GBPUrl);
            }
            Currency USDOld = JsonSerializer.Deserialize<Currency>(USDJson);
            Currency EUROld = JsonSerializer.Deserialize<Currency>(EURJson);
            Currency GBPOld = JsonSerializer.Deserialize<Currency>(GBPJson);


            string s = $"Nazwa waluty: {USD.currency} \n"; ;
            s += $"Cena Skupu: {USD.rates[0].bid} zł\n";
            s += $"Cena sprzedaży: {USD.rates[0].ask} zł\n";
            if (USD.rates[0].ask < USDOld.rates[0].ask)
                //s += "Spadek\n";
                Image1.Source = "down.png";
            else
                Image1.Source = "up.png";
            //s += "Wzrost\n";
            USDDisplay.Text = s;
            s = $"Nazwa waluty: {EUR.currency} \n"; ;
            s += $"Cena Skupu: {EUR.rates[0].bid} zł\n";
            s += $"Cena sprzedaży: {EUR.rates[0].ask} zł\n";
            if (EUR.rates[0].ask < EUROld.rates[0].ask)
                //s += "Spadek\n";
                Image2.Source = "down.png";
            else
                Image2.Source = "up.png";
            //s += "Wzrost\n";
            EURDisplay.Text = s;
            s = $"Nazwa waluty: {GBP.currency} \n"; ;
            s += $"Cena Skupu: {GBP.rates[0].bid} zł\n";
            s += $"Cena sprzedaży: {GBP.rates[0].ask} zł\n";
            if (GBP.rates[0].ask < GBPOld.rates[0].ask)
                //s += "Spadek\n";
                Image3.Source = "down.png";
            else
                Image3.Source = "up.png";
                //s += "Wzrost\n";
            GBPDisplay.Text = s;
        }

    }
}