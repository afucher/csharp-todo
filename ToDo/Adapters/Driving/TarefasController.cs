using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }

}