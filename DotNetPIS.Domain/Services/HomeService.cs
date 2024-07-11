using DotNetPIS.Domain.Interfaces;
using DotNetPIS.Domain.Models.GTFS;

namespace DotNetPIS.Domain.Services;

public class HomeService
{
    private readonly IRepository<Source, int> _sourceRepo;

    public HomeService(IRepository<Source, int> sourceRepo)
    {
        _sourceRepo = sourceRepo;
    }

    public List<Source> GetInfoSources()
    {
        return _sourceRepo.GetAll().ToList();
    }

    public Source GetSourceById(int id)
    {
        return _sourceRepo.GetById(id);
    }
    
}