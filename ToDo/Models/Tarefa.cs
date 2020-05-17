using System;

namespace ToDo.Models
{
    public class Tarefa
    {
        public string Título { get; }
        bool _concluída;

        public Tarefa(string título)
        {
            if(string.IsNullOrWhiteSpace(título)) throw new Exception("Título deve ter valor.");
            Título = título;
            _concluída = false;
        }

        public bool EstáConcluída() => _concluída;

        public void Concluir() => _concluída = true;
    }
}