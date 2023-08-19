namespace Domain.Models.FinancialModelingPrep
{
  public class FinancialStatement
  {
    public string Date { get; set; }
    public string Symbol { get; set; }
    public string ReportedCurrency { get; set; }
    public string Cik { get; set; }
    public string FillingDate { get; set; }
    public string AcceptedDate { get; set; }
    public string CalendarYear { get; set; }
    public string Period { get; set; }
    public long Revenue { get; set; }
    public long CostOfRevenue { get; set; }
    public long GrossProfit { get; set; }
    public double GrossProfitRatio { get; set; }
    public long ResearchAndDevelopmentExpenses { get; set; }
    public int GeneralAndAdministrativeExpenses { get; set; }
    public int SellingAndMarketingExpenses { get; set; }
    public long SellingGeneralAndAdministrativeExpenses { get; set; }
    public int OtherExpenses { get; set; }
    public long OperatingExpenses { get; set; }
    public long CostAndExpenses { get; set; }
    public long InterestIncome { get; set; }
    public long InterestExpense { get; set; }
    public long DepreciationAndAmortization { get; set; }
    public long Ebitda { get; set; }
    public double EbitdaRatio { get; set; }
    public long OperatingIncome { get; set; }
    public double OperatingIncomeRatio { get; set; }
    public int TotalOtherIncomeExpensesNet { get; set; }
    public long IncomeBeforeTax { get; set; }
    public double IncomeBeforeTaxRatio { get; set; }
    public long IncomeTaxExpense { get; set; }
    public long NetIncome { get; set; }
    public double NetIncomeRatio { get; set; }
    public double Eps { get; set; }
    public double EpsDiluted { get; set; }
    public long WeightedAverageShsOut { get; set; }
    public long WeightedAverageShsOutDil { get; set; }
    public string Link { get; set; }
    public string FinalLink { get; set; }
  }

}