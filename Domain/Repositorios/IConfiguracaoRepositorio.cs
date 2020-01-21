﻿using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorios
{
    public interface IConfiguracaoRepositorio
    {
        Task<IEnumerable<HorarioDto>> GetAllHours();
        Task CreateHorario(HorarioDto horario);
        Task CreateNome(NomeConsultaDto nome);
        Task deleteHorario(int id);
        Task deleteNome(int id);
        Task<IEnumerable<NomeConsultaDto>> GetAllNomes();
    }
}
