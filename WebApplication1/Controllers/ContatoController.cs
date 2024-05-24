using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace celiaRedes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {

        string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NAguentoMais;Integrated Security=True;Connect Timeout=30;Encrypt=False;"; 

        [HttpGet(Name = "GeTCurriculo")]

        public IEnumerable<Contato> Get()
        {
            List<Contato> contatos = new List<Contato>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string querry = "SELECT * FROM Contato";
                SqlCommand command = new SqlCommand(querry, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Contato contato = new Contato()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        Email = reader["Email"].ToString(),
                        NomeArquivo = reader["NomeArquivo"].ToString(),
                        Arquivo = reader["Arquivo"].ToString(),
                    };

                    contatos.Add(contato);
                }

                reader.Close();

            }

            return contatos;
        }

        [HttpPost]

        public ActionResult CreateContato(Contato contato)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            {
                string querry = "INSERT INTO Contato (Nome, Email, NomeArquivo, Arquivo) VALUES (@Nome, @Email, @NomeArquivo, @Arquivo)";
                SqlCommand command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@Nome", contato.Nome);
                command.Parameters.AddWithValue("@Email", contato.Email);
                command.Parameters.AddWithValue("@NomeArquivo", contato.NomeArquivo);
                command.Parameters.AddWithValue("@Arquivo", contato.Arquivo);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

    }
}