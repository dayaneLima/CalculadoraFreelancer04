using CalcFreelancer.Models;
using CalcFreelancer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Services
{
    public class ProjetoService
    {
        private readonly AzureProjetoRepository ProjetoRepository;

        public ProjetoService()
        {
            ProjetoRepository = new AzureProjetoRepository();
        }

        public void Inserir(Projeto projeto)
        {
            ProjetoRepository.Insert(projeto);
        }
    }
}
