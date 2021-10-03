using System;
using System.Data;
using System.Data.SQLite;

namespace Series
{
    class Banco
    {
        private static SQLiteConnection conexao;
        private static SQLiteConnection ConexaoBanco()
        {
            conexao = new SQLiteConnection("Data Source=C:\\Users\\Willian\\Desktop\\Séries\\Banco de dados\\series.db");
            conexao.Open();
            return conexao;
        }

        public static void NovoSerie(Novo s)
        {
            try
            {
                var cmd = ConexaoBanco().CreateCommand();
                cmd.CommandText = "INSERT INTO tb_series (S_NOME, S_GENERO, I_ANO) VALUES (@nome, @genero, @ano)";
                cmd.Parameters.AddWithValue("@nome", s.nome);
                cmd.Parameters.AddWithValue("@genero", s.genero);
                cmd.Parameters.AddWithValue("@ano", s.ano);
                cmd.ExecuteNonQuery();
                ConexaoBanco().Close();
            }
            catch (Exception ex)
            {
                throw ex;
                ConexaoBanco().Close();
            }
        }

        public static DataTable Todos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM tb_series";
                    da = new SQLiteDataAdapter(cmd.CommandText, ConexaoBanco());
                    da.Fill(dt);
                    ConexaoBanco().Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                ConexaoBanco().Close();
                throw ex;
            }
        }

        public static void AtualizarSerie(Novo s)
        {
            try
            {
                var cmd = ConexaoBanco().CreateCommand();
                cmd.CommandText = "UPDATE tb_series Set S_NOME=@nome, S_GENERO=@genero, I_ANO=@ano WHERE I_ID=@id";
                cmd.Parameters.AddWithValue("@nome", s.nome);
                cmd.Parameters.AddWithValue("@genero", s.genero);
                cmd.Parameters.AddWithValue("@ano", s.ano);
                cmd.Parameters.AddWithValue("@id", s.index);
                cmd.ExecuteNonQuery();
                ConexaoBanco().Close();
            }
            catch (Exception ex)
            {
                throw ex;
                ConexaoBanco().Close();
            }
        }

        public static void DeletarSerie(Novo s)
        {
            try
            {
                var cmd = ConexaoBanco().CreateCommand();
                cmd.CommandText = "DELETE FROM tb_series WHERE I_ID=@id";
                cmd.Parameters.AddWithValue("@id", s.index);
                cmd.ExecuteNonQuery();
                ConexaoBanco().Close();
            }
            catch (Exception ex)
            {
                throw ex;
                ConexaoBanco().Close();
            }
        }
    }
}
