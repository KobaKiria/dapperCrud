using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SuperHeroController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllSuperHeros()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("defaultCOnnection"));
            IEnumerable<SuperHero> heros = await SelectAllHeroes(connection);
            return Ok(heros);
        }



        [HttpGet("{heroId}")]
        public async Task<ActionResult<SuperHero>> GetHeroById(int heroId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("defaultCOnnection"));
            var hero = await connection.QueryFirstAsync<SuperHero>("select * from Heros where id = @Id",
                new {Id= heroId });
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero hero)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("defaultCOnnection"));
            await connection.ExecuteAsync("insert into Heros (name, firstname, lastname, place) values (@Name, @FirstName, @LastName, @Place)", hero);
            return Ok(await SelectAllHeroes(connection));
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("defaultConnection"));
            await connection.ExecuteAsync("update Heros set name = @Name, firstname = @FirstName, lastname = @LastName, place = @Place where id = @Id", hero);
            return Ok(await SelectAllHeroes(connection));
        }
        [HttpDelete("{heroId}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int heroId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("defaultCOnnection"));
            await connection.ExecuteAsync("Delete from  Heros where id = @Id", new { id = heroId });
            return Ok(await SelectAllHeroes(connection));
        }
        private static async Task<IEnumerable<SuperHero>> SelectAllHeroes(SqlConnection connection)
        {
            return await connection.QueryAsync<SuperHero>("select * from Heros");
        }


    }
}
