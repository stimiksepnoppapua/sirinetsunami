using MobileBMKG.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileBMKG.Services
{
    public interface IDataStore<T>
    {
        Task<T> GetAutoGempaAsync();
        Task<T> LastGempaDirasakanAsync();
        Task<List<T>> GetGempaTerkiniAsync();
        Task<List<T>> GetGempaDirasakanAsync();
        Task<Infotsunami> GetLastTsunamiAsync();
    }
}
