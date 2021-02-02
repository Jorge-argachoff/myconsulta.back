using Domain.Dtos;
using Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Infra.Interfaces;
using Application.Dtos;
using Application.Enums;
using RabbitMQ.Client;

namespace Domain.Services
{
    public class ConsultaServices : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaServices(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task CancelConsulta(int idConsulta)
        {
            await _consultaRepository.ChangeStatusConsulta(idConsulta, StatusConsultaEnum.Cancelada);
        }

        public async Task CreateComentario(ComentarioDto comentario)
        {
            if (string.IsNullOrWhiteSpace(comentario.Comentario)
                || comentario.Comentario.Length > 500) { throw new Exception("Comentario deve ter entre 1 até 500 caracteres."); }

            await _consultaRepository.CreateComentario(comentario);
        }

        public async Task CreateConsulta(ConsultaDto consulta)
        {
            if (consulta.Detalhes.Length > 800) { throw new Exception("Detalhes mão pode conter mais de 800 caracteres. Caracteres digitados:" + consulta.Detalhes.Length); }

            await _consultaRepository.CreateConsulta(consulta);
        }

        public Task<IEnumerable<ComentarioDto>> GetComentsByPersonId(int id)
        {
            return _consultaRepository.GetComentsByPersonId(id);
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByCpf(string cpf)
        {
            return await _consultaRepository.GetConsultasByCpf(cpf);
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasByData(string data)
        {
            return await _consultaRepository.GetConsultasByData(data);
        }

        public async Task<ConsultaDto> GetConsultasById(int id)
        {
            return await _consultaRepository.GetConsultasById(id);
        }

        public async Task<IEnumerable<ConsultaDto>> GetNextConsultas()
        {
            return await _consultaRepository.GetNextConsultas();
        }

        public Task<bool> sendMessage(ComentarioDto comentario)
        {
           
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "chatMessage",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    string message = comentario.Comentario;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                        routingKey: "chatBot",
                                        basicProperties: null,
                                        body: body);
                }


                return Task.FromResult<bool>(true);           

        }
    }
}
