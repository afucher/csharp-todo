using System;
using ToDo.Exceptions;

namespace ToDo.Models
{
    public class Tarefa
    {
        public uint? Id { get; }
        public string Título { get; }
        bool _concluída;

        public Tarefa(string título)
        {
            if(string.IsNullOrWhiteSpace(título)) throw new TítuloInválidoExceção();
            Título = título;
            _concluída = false;
        }

        public Tarefa(uint id, string título) : this(título)
        {
            Id = id;
        }

        public bool EstáConcluída() => _concluída;

        public void Concluir() => _concluída = true;
    }
}