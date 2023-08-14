namespace Application.DTOs
{
  public class PositionDto
  {
    public int Id { get; set; }
    public string Symbol { get; set; }
    public float SharesOwned { get; set; }
    public float AverageCostBasis { get; set; }
    public float CurrentTotalValue => SharesOwned * AverageCostBasis;
    public string SectorName { get; set; }
    public string IndustryName { get; set; }
  }
}