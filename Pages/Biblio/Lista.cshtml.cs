using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Biblioteca.Pages.Biblio
{
    public class ListaModel : PageModel
    {
        public string errorMessage = "";
        public List<Libri> listaLibri = new List<Libri>();

        public void OnGet()
        {
            try
            {            
                using (SqlConnection conn = new SqlConnection(config.connectionString))
                {
                    conn.Open();
                    string sql = "SELECT Libri.Id, Libri.Titolo, " +
						"Autori.NomeAutore, Libri.Prezzo " +
                        "FROM " +
                        "Libri INNER JOIN Autori ON " +
                        "Libri.IdAutore = Autori.Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Libri libro = new Libri();
                                libro.Id = reader.GetInt32(0);
                                libro.Titolo = reader.GetString(1);
                                libro.NomeAutore = reader.GetString(2);
                                libro.Prezzo = reader.GetDecimal(3);
                                listaLibri.Add(libro);
                            }
                        }
                    }
                }
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

		}
    }
}
