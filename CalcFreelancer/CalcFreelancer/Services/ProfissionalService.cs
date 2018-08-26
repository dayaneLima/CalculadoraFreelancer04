using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Services
{
    public class ProfissionalService
    {
        private readonly AzureRepository ProfissionalRepository;

        public ProfissionalService()
        {
            ProfissionalRepository  = new AzureRepository();
        }

        public void Inserir(Profissional profissional)
        {
            ProfissionalRepository.Insert(profissional);
        }
    }
}
