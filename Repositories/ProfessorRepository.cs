using Dapper;
using Microsoft.Data.SqlClient;
using Projeto02App.Entities;

namespace Projeto02App.Repositories
{
    public class ProfessorRepository
    {
        #region Atributos Privados

        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDTurma;Integrated Security=True;";

        #endregion

        #region Métodos

        public void Inserir(Professor professor)
        {
            var sql = """
                      INSERT INTO PROFESSORES(NOME, TELEFONE, EMAIL)
                      VALUES(@Nome, @Telefone, @Email)
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, professor);
            }
        }

        public void Atualizar(Professor professor)
        {
            var sql = """
                      UPDATE PROFESSORES
                      SET 
                            NOME = @Nome,
                            TELEFONE = @Telefone,
                            EMAIL = @Email
                      WHERE
                            ID = @Id
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, professor);
            }
        }

        public void Excluir(Guid id)
        {
            var sql = """
                      DELETE FROM PROFESSORES
                      WHERE ID = @Id
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { @Id = id });
            }
        }

        public List<Professor> ObterTodos()
        {
            var sql = """
                      SELECT * FROM PROFESSORES
                      ORDER BY NOME
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Professor>(sql).ToList();
            }
        }

        public Professor? ObterPorId(Guid id)
        {
            var sql = """
                      SELECT * FROM PROFESSORES
                      WHERE ID = @Id
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Professor>(sql, new { @Id = id });
            }
        }



        #endregion
    }
}
