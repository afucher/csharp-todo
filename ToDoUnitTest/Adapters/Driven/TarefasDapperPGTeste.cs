using System.Data;
using Dapper;
using NSubstitute;
using NUnit.Framework;
using ToDo.Adapters;

namespace ToDoUnitTest.Adapters
{
    public class TarefasDapperPGTeste
    {
        [Test]
        public void AindaNãoSei()
        {
            var conn = Substitute.For<IDbConnection>();
            var repo = new TarefasDapperPG(conn);

            conn.
        }
    }
}