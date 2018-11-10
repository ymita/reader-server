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
        void addWord(Word word)
        {
            using (var db = new SqliteConnection("Filename=words.db"))
            {
                db.Open();

                var command = new SqliteCommand();
                command.Connection = db;

                command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
                command.Parameters.AddWithValue("@id", word.Id);
                command.Parameters.AddWithValue("@spelling", word.Spelling);
                command.Parameters.AddWithValue("@meaning", word.Meaning);
                command.Parameters.AddWithValue("@text", word.Text);
                command.ExecuteNonQuery();
            }
        }
        IEnumerable<Word> getWords()
        {
            try
            {
                using (var db = new SqliteConnection("Filename=words.db"))
                {
                    List<Word> words = new List<Word>();

                    db.Open();

                    var command = new SqliteCommand("select id, spelling, meaning, text from words", db);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Word word = new Word();
                        word.Id = Convert.ToInt32(reader["id"]);
                        word.Spelling = (string)reader["spelling"];
                        word.Meaning = (string)reader["meaning"];
                        word.Text = (string)reader["text"];

                        words.Add(word);
                    }
                    return words;
                }
            }
            catch(Exception ex)
            {
                List<Word> words = new List<Word>();
                words.Add(new Word { Id = 999, Meaning = ex.Message });
                return words;
            }
        }

        // GET api/words
        [HttpGet]
        public ActionResult<IEnumerable<Word>> Get()
        {
            #region adding records
            //using (var db = new SqliteConnection("Filename=words.db"))
            //{
            //    db.Open();

            //    var command = new SqliteCommand();
            //    command.Connection = db;

            //    //    string sql = "create table words (id int, spelling text, meaning text, text text)";
            //    //    command.CommandText = sql;
            //    //    command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 2);
            //    //command.Parameters.AddWithValue("@spelling", "atrophy");
            //    //command.Parameters.AddWithValue("@meaning", "萎縮");
            //    //command.Parameters.AddWithValue("@text", "Atrophy is the partial or complete wasting away of a part of the body.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 3);
            //    //command.Parameters.AddWithValue("@spelling", "precinct");
            //    //command.Parameters.AddWithValue("@meaning", "選挙区、境界");
            //    //command.Parameters.AddWithValue("@text", "A precinct is a space enclosed by the walls or other boundaries of a particular place or building, or by an arbitrary and imaginary line drawn around it.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 4);
            //    //command.Parameters.AddWithValue("@spelling", "allay");
            //    //command.Parameters.AddWithValue("@meaning", "静める、和らげる");
            //    //command.Parameters.AddWithValue("@text", "Usually you can allay the negativity by providing (or asking members to provide) clear and concise instructions and documentation.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 5);
            //    //command.Parameters.AddWithValue("@spelling", "endemic");
            //    //command.Parameters.AddWithValue("@meaning", "特有の、土着の");
            //    //command.Parameters.AddWithValue("@text", "This isn't a platform thing, it's a problem that is endemic to the web.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 6);
            //    //command.Parameters.AddWithValue("@spelling", "hydrolysis");
            //    //command.Parameters.AddWithValue("@meaning", "加水分解");
            //    //command.Parameters.AddWithValue("@text", "Usually hydrolysis is a chemical process in which a molecule of water is added to a substance. Sometimes this addition causes both substance and water molecule to split into two parts.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 7);
            //    //command.Parameters.AddWithValue("@spelling", "debacle");
            //    //command.Parameters.AddWithValue("@meaning", "総崩れ、大失敗");
            //    //command.Parameters.AddWithValue("@text", "These include the White House’s U.S. Digital Service 18F taskforce, launched in response to the healthcare.gov debacle.");
            //    //command.ExecuteNonQuery();

            //    //ここから下は未登録
            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 8);
            //    //command.Parameters.AddWithValue("@spelling", "opulent");
            //    //command.Parameters.AddWithValue("@meaning", "贅沢な、豊富な");
            //    //command.Parameters.AddWithValue("@text", "She was also known for her opulent lifestyle.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 9);
            //    //command.Parameters.AddWithValue("@spelling", "stout");
            //    //command.Parameters.AddWithValue("@meaning", "丈夫な、頑丈な");
            //    //command.Parameters.AddWithValue("@text", "Campers prefer stout vessels, sticks and cloth.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 10);
            //    //command.Parameters.AddWithValue("@spelling", "circumvent");
            //    //command.Parameters.AddWithValue("@meaning", "回避する");
            //    //command.Parameters.AddWithValue("@text", "If the destination server filters content based on the origin of the request, the use of a proxy can circumvent this filter.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 11);
            //    //command.Parameters.AddWithValue("@spelling", "circumvent");
            //    //command.Parameters.AddWithValue("@meaning", "回避する");
            //    //command.Parameters.AddWithValue("@text", "The concept of a weak reference was developed to circumvent these situations in Perl 5.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 12);
            //    //command.Parameters.AddWithValue("@spelling", "boulder");
            //    //command.Parameters.AddWithValue("@meaning", "大きな石");
            //    //command.Parameters.AddWithValue("@text", "In geology, a boulder is a rock fragment with size greater than 25.6 centimetres (10.1 in) in diameter.");
            //    //command.ExecuteNonQuery();

            //    //command.CommandText = "insert into words(id, spelling, meaning, text) values (@id, @spelling, @meaning, @text);";
            //    //command.Parameters.AddWithValue("@id", 13);
            //    //command.Parameters.AddWithValue("@spelling", "scour");
            //    //command.Parameters.AddWithValue("@meaning", "(不要なものを)取り除く、こすって洗う");
            //    //command.Parameters.AddWithValue("@text", "We scoured the web for some of this week's most interesting open source-related news stories so you don't have to.");
            //    //command.ExecuteNonQuery();
            //}
            #endregion

            List<Word> words = new List<Word>();

            var wordsList = getWords();
            foreach(var word in wordsList.ToList())
            {
                words.Add(word);
            }

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
        public IActionResult Post([FromBody] Word value)
        {
            this.addWord(value);
            return new CreatedResult("api/words/" + value.Id, value);
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
