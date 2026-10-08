using Dapper;
using Microsoft.Data.SqlClient;
using Projeto02App.Entities;

namespace Projeto02App.Repositories
{
    public class TurmaRepository
    {

        #region Atributos Privados

        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDTurma;Integrated Security=True;";

        #endregion

        #region Métodos

        public void Inserir(Turma turma)
        {

            var sql = """
                      INSERT INTO TURMAS(IDTURMA, NOME, DATAINICIO, HORARIO, IDPROFESSOR, IDCURSO)
                      VALUES(@IdTurma, @Nome, @DataInicio, @Horario, @IdProfessor, @IdCurso)
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    @IdTurma = turma.IdTurma,
                    @Nome = turma.Nome,
                    @DataInicio = turma.DataInicio,
                    @Horario = turma.Horario,
                    @IdProfessor = turma.Professor.IdProfessor,
                    @IdCurso = turma.Curso.IdCurso
                });
            }

        }

        public void Atualizar(Turma turma)
        {
            var sql = """
                      UPDATE TURMAS
                      SET
                            NOME = @Nome,
                            DATAINICIO = @DataInicio,
                            HORARIO = @Horario
                      WHERE
                            IDTURMA = @IdTurma
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, turma);
            }
        }

        public void Excluir(Guid id)
        {
            var sql = """
                      DELETE FROM TURMAS
                      WHERE IDTURMA = @IdTurma
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { @IdTurma = id });
            }
        }

        public List<Turma> ObterTodas()
        {
            var sql = """
                      SELECT * FROM TURMAS
                      ORDER BY NOME
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Turma>(sql).ToList();
            }
        }

        public Turma? ObterPorId(Guid id)
        {
            var sql = """
                      SELECT * FROM TURMAS
                      WHERE IDTURMA = @IdTurma
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Turma>(sql, new { @IdTurma = id });
            }
        }

        public Turma? ObterExportar(Guid id)
        {
            var sql = """
                      SELECT
                            T.IDTURMA, T.NOME, T.DATAINICIO, T.HORARIO,
                            P.IDPROFESSOR, P.NOME, P.TELEFONE, P.EMAIL,
                            C.IDCURSO, C.NOME, C.CARGAHORARIA
                      FROM TURMAS T
                      INNER JOIN PROFESSORES P ON P.IDPROFESSOR = T.IDPROFESSOR
                      INNER JOIN CURSOS C ON C.IDCURSO = T.IDCURSO
                      WHERE T.IDTURMA = @IdTurma
                      """;

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Turma, Professor, Curso, Turma>(
                    sql,
                    (turma, professor, curso) =>
                    {
                        turma.Professor = professor;
                        turma.Curso = curso;
                        return turma;
                    },
                    new { @IdTurma = id },
                    splitOn: "IDPROFESSOR,IDCURSO"
                ).FirstOrDefault();
            }
        }

        #endregion
    }
}
