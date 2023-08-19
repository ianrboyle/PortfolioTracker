namespace Domain.Models.FinancialModelingPrep
{
  public class StockQuote
  {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double ChangesPercentage { get; set; }
    public double Change { get; set; }
    public double DayLow { get; set; }
    public double DayHigh { get; set; }
    public double YearHigh { get; set; }
    public double YearLow { get; set; }
    public long MarketCap { get; set; }
    public double PriceAvg50 { get; set; }
    public double PriceAvg200 { get; set; }
    public string Exchange { get; set; }
    public int Volume { get; set; }
    public int AvgVolume { get; set; }
    public double Open { get; set; }
    public int PreviousClose { get; set; }
    public double Eps { get; set; }
    public double Pe { get; set; }
    public string EarningsAnnouncement { get; set; }
    public long SharesOutstanding { get; set; }
    public int Timestamp { get; set; }
  }

}