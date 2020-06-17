using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Flurl.Http;
using Newtonsoft.Json;
using ToDo.Models;
using ToDo.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
                
            return JsonConvert.DeserializeObject<TarefaDTO[]>(resultado)
                .Select(tarefa => tarefa.Tarefa())
                .ToArray();
        }

        public Tarefa CriarTarefa(Tarefa tarefa)
        {
            var tarefaJson = JsonSerializer.Serialize(new {titulo = tarefa.Título});
            var conteúdo = new StringContent(tarefaJson, Encoding.Default, "application/json");
            var resultado = _httpClient
                .PostAsync("http://localhost:5000/api/Tarefas", conteúdo)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .Result;
            var tarefaCriada = JsonConvert.DeserializeObject<TarefaDTO>(resultado);
            return tarefaCriada.Tarefa();

        }

        public void ExcluirTarefa(uint id)
        {
            _httpClient.DeleteAsync($"http://localhost:5000/api/Tarefas/{id}");
        }

        public void ConcluirTarefa(uint id)
        {
            _httpClient.PutAsync($"http://localhost:5000/api/Tarefas/{id}/concluir",new StringContent(""));
        }
    }
}