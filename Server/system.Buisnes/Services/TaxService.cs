using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class TaxService : ITaxService
{
    private readonly ITaxRepository _taxRepository;

    public TaxService(ITaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }

    public async Task<List<Tax>> GetTaxes()
    {
        return await _taxRepository.Get();
    }

    public async Task<int> CreateTax(Tax tax)
    {
        return await _taxRepository.Create(tax);
    }

    public async Task<int> UpdateTax(int id, string name, decimal? rate, string type)
    {
        return await _taxRepository.Update(id, name, rate, type);
    }

    public async Task<int> DeleteTax(int id)
    {
        return await _taxRepository.Delete(id);
    }
}
