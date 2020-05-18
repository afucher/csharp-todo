using System;

namespace ToDo.Exceptions
{
    public class TítuloInválidoExceção : Exception
    {
        public TítuloInválidoExceção() : base("Título deve ter valor.")
        {
            
        }
    }
}