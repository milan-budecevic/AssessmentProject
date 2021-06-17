using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
    public interface IDataRepository
    {
        Task<ParsedData> GetDataFromCsv();
    }
}
