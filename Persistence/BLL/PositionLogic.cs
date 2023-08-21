using Application.DTOs;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Persistence.DAL;
using Application.Services;
using Domain.Models.FinancialModelingPrep;

namespace Persistence.BLL
{
  public class PositionLogic : IPositionLogic
  {
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserLogic _userLogic;
    private readonly IRedisClient _redisClient;
    private readonly FinancialModelingPrepApiService _fmpService;
    private readonly ISectorRepository _sectorRepository;
    private readonly IIndustryRepository _industryRepository;
    public PositionLogic(IPositionRepository repository, IMapper mapper, IUserLogic userLogic, IRedisClient redisClient, FinancialModelingPrepApiService fmpService, ISectorRepository sectorRepository, IIndustryRepository industryRepository)
    {
      _industryRepository = industryRepository;
      _sectorRepository = sectorRepository;
      _fmpService = fmpService;
      _redisClient = redisClient;
      _userLogic = userLogic;
      _mapper = mapper;
      _repository = repository;

    }

    public async Task AddPosition(Position position)
    {
      CompanyInformation companyInfo = await GetCompanyInformation(position);
      position.SectorId = companyInfo.SectorId;
      position.IndustryId = companyInfo.IndustryId;
      await _repository.AddNewPosition(position);
    }

    public async Task<PositionDto> GetPositionById(int positionId)
    {

      var position = await _repository.GetPositionById(positionId);
      if (position == null || position.Id == 0)
      {
        string exceptionString = $"Position with ID: {positionId} not found.";
        CustomException ex = new CustomException(exceptionString, 404);
        throw ex;
      }
      return _mapper.Map<Position, PositionDto>(position);

    }

    public async Task<List<PositionDto>> GetUserPositions(int appUserId)
    {
      var positions = await _repository.GetUserPositions(appUserId);
      if (!positions.Any())
      {
        //check if user exists
        var user = await _userLogic.GetUserById(appUserId);
      }
      return _mapper.Map<List<Position>, List<PositionDto>>(positions);
    }

    private async Task<CompanyInformation> GetCompanyInformation(Position position)
    {
      CompanyInformation companyInfo = await _redisClient.GetCompanyInfoBySymbol(position.Symbol);

      if (companyInfo == null || companyInfo.Symbol == null)
      {
        // get company profile
        var stockQuotes = await _fmpService.GetStockQuote(position.Symbol);
        // var profiles = await _fmpService.GetCompanyProfile(symbol);
        var profiles = new List<CompanyProfile>();
        var prof = new CompanyProfile
        {
          Sector = "Technology",
          Industry = "Consumer Electronics",
          CompanyName = "Apple Inc",
          Country = "USA"
        };
        profiles.Add(prof);
        var stockQuote = stockQuotes.FirstOrDefault();
        var profile = profiles.FirstOrDefault();
        await _sectorRepository.AddSector(profile.Sector);
        Sector sector = await _sectorRepository.GetSectorBySectorName(profile.Sector);
        await _industryRepository.AddIndustry(profile.Industry, sector.Id);
        Industry industry = await _industryRepository.GetIndustryByIndustryName(profile.Industry);
        companyInfo.Symbol = position.Symbol;
        companyInfo.CompanyName = profile.CompanyName;
        companyInfo.CurrentPrice = stockQuote.Price;
        companyInfo.SectorId = sector.Id;
        companyInfo.IndustryId = industry.Id;
        companyInfo.Country = profile.Country;
        await _redisClient.AddCompanyInfoToRedis(companyInfo);
      }

      return companyInfo;
    }
  }
}
