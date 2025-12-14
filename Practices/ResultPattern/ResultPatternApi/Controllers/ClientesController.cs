using Microsoft.AspNetCore.Mvc;
using ResultPattern.Application.Clientes;
using ResultPattern.Application.Common;
using ResultPattern.Application.Dtos.Clientes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResultPattern.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _svc;

        public ClientesController(IClienteService svc)
        {
            _svc = svc;

        }
        [HttpGet("GetByDni")]
        public async Task<Result<ClienteDto>> GetByDni(int dni, CancellationToken ct)
        {
            return await _svc.GetByDniAsync(dni, ct);
        }

        [HttpGet()]
        public async Task<Result<List<ClienteDto>>> Listado(CancellationToken ct)
        {
            return await _svc.GetAllAsync(ct);
        }

        // GET: api/<ClientesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{

        //}

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
