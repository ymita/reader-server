using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Reader.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : Controller
    {
        IEnumerable<object> GetUserIds()
        {
            using (var db = new SqliteConnection("Filename=words.db"))
            {
                db.Open();

                var command = new SqliteCommand("select id, spelling, meaning, text from words", db);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return reader.GetValue(0);
                }
            }
        }

        // GET api/words
        [HttpGet]
        public ActionResult<IEnumerable<Word>> Get()
        {
            //
            //using (var db = new SqliteConnection("Filename=words.db"))
            //{
            //    db.Open();

            //    var command = new SqliteCommand();
            //    command.Connection = db;

            //    string sql = "create table words (id int, spelling text, meaning text, text text)";
            //    command.CommandText = sql;
            //    command.ExecuteNonQuery();

            //    command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    command.Parameters.AddWithValue("@id", 1);
            //    command.Parameters.AddWithValue("@spelling", "ameliorate");
            //    command.Parameters.AddWithValue("@meaning", "改善する");
            //    command.Parameters.AddWithValue("@text", "They offered some compromises in an effort to ameliorate the situation.");
            //    command.ExecuteNonQuery();
            //}

            var userIds = GetUserIds();
            //userIds.ToList().ForEach(Console.WriteLine);

            foreach(var item in userIds.ToList())
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
            //
            List<Word> words = new List<Word>();

            words.Add(new Word { Id = 1, Spelling = "ameliorate", Meaning = "改善する", Text = "They offered some compromises in an effort to ameliorate the situation." });

            return words;
        }

        // GET api/words/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/words
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/words/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/words/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
