using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly AddressService _addressService;

        public App(IConfiguration config, AddressService addressService)
        {
            _config = config;
            _addressService = addressService;
        }
        public async Task Run()
        {
            await _addressService.GetCoordinates();
        }
    }
}
