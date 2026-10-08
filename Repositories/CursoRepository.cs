using Dapper;
using Microsoft.Data.SqlClient;
using Projeto02App.Entities;

namespace Projeto02App.Repositories
{
    public class CursoRepository
    {
        #region Atributos Privados

        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDTurma;Integrated Security=True;";

        #endregion

        #region Métodos


        public void Inserir(Curso curso)
        {
            var sql = """
                      INSERT INTO CURSOS(IDCURSO, NOME, CARGAHORARIA)
                      VALUES(@IdCurso, @Nome, @CargaHoraria)
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, curso);
            }
        }

        public void Atualizar(Curso curso)
        {
            var sql = """
                      UPDATE CURSOS
                      SET
                            NOME = @Nome,
                            CARGAHORARIA = @CargaHoraria
                      WHERE 
                            IDCURSO = @IdCurso
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, curso);
            }
        }

        public void Excluir(Guid id)
        {
            var sql = """
                      DELETE FROM CURSOS
                      WHERE IDCURSO = @IdCurso
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { @IdCurso = id });
            }
        }

        public List<Curso> ObterTodos()
        {
            var sql = """
                      SELECT * FROM CURSOS
                      ORDER BY NOME
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Curso>(sql).ToList();
            }
        }

        public Curso? ObterPorId(Guid id)
        {
            var sql = """
                      SELECT * FROM CURSOS
                      WHERE IDCURSO = @IdCurso
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Curso>(sql, new { @IdCurso = id });
            }
        }


        #endregion
    }
}
