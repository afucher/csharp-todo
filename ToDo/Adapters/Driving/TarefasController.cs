using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.Exceptions;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.Adapters.Driving
{
    [Route("/api/tarefas")]
    [ApiController]
    public class TarefasController : Controller
    {
        private readonly ServiçoTarefa _serviçoTarefa;

        public TarefasController(ServiçoTarefa serviçoTarefa)
        {
            _serviçoTarefa = serviçoTarefa;
        }
        
            
        [HttpGet]
        public ActionResult<IEnumerable<Object>> ObterTarefas()
        {
            return new ActionResult<IEnumerable<Object>>(_serviçoTarefa
                .ObterTarefas()
                .Select(tarefa => new {id = tarefa.Id, titulo = tarefa.Título, concluida = tarefa.EstáConcluída()}));
        }

        [HttpPost]
        public ActionResult<Object> CriarTarefa(TarefaDTO tarefaParaCriar)
        {
            try
            {
                var tarefa = _serviçoTarefa.CriaTarefa(tarefaParaCriar.titulo);
                return new ActionResult<Object>(new
                {
                    id = tarefa.Id,
                    titulo = tarefa.Título,
                    concluida = tarefa.EstáConcluída()
                });
            }
            catch (TítuloInválidoExceção e)
            {
                return UnprocessableEntity(tarefaParaCriar);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirTarefa(uint id)
        {
            _serviçoTarefa.ExcluirTarefa(id);
            return Ok();
        }

        [HttpPut("{id}/concluir")]
        public ActionResult ConcluirTarefa(uint id)
        {
            _serviçoTarefa.ConcluirTarefa(id);
            return Ok();
        }
        
    }

}