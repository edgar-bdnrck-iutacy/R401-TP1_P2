using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientConvertisseurV1.Models;

namespace ClientConvertisseurV1.Services
{
    public interface IService
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}

