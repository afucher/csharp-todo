using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Flurl.Http;
using Newtonsoft.Json;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Adapters
{
    public class TarefasAPI : IFonteDadosTarefas
    {
        private readonly HttpClient _httpClient;

        public TarefasAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IReadOnlyCollection<Tarefa> ObterTarefas()
        {
            var resultado = _httpClient
                .GetStringAsync("http://localhost:5000/api/Tarefas")
                .Result;
                
            return JsonConvert.DeserializeObject<dynamic[]>(resultado)
                .Select(tarefa => new Tarefa((uint)tarefa.id, (string)tarefa.titulo, (bool)tarefa.concluida))
                .ToArray();
        }

        public Tarefa CriarTarefa(Tarefa tarefa)
        {
            throw new System.NotImplementedException();
        }

        public void ExcluirTarefa(uint id)
        {
            _httpClient.DeleteAsync($"http://localhost:5000/api/Tarefas/{id}");
        }

        public void ConcluirTarefa(uint id)
        {
            throw new System.NotImplementedException();
        }
    }
}