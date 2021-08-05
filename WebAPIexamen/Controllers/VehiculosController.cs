using Clasesexamen.Data;
using Clasesexamen.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIexamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly EjercicioEvaluacionContext _context;
        public VehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }
        // GET: api/<VehiculosController>

        [Route("ListaVehiculos")]
        [HttpGet]
        public List<Vehiculo> Get()
        {
            return _context.Vehiculos.ToList();
        }

        // GET api/<VehiculosController>/5
        [HttpGet("{id}")]
        public Vehiculo Get(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(v => v.Codigo == id).FirstOrDefault();
            return vehiculo;
        }

        // POST api/<VehiculosController>
        [HttpPost]
        public int Post([FromBody] Vehiculo vehiculo)
        {
            int respuesta = 0;
            try
            {
                _context.Add(vehiculo);
                _context.SaveChanges();
                respuesta = 1;
            }
            catch (Exception)
            {
                respuesta = 0;
            }
            return respuesta;
        }

        // PUT api/<VehiculosController>/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Vehiculo vehiculo)
        {
            int respuesta = 0;
            try
            {
                vehiculo = _context.Vehiculos.Where(c => c.Codigo == id).FirstOrDefault();
                _context.Update(vehiculo);
                _context.SaveChanges();
                respuesta = 1;
            }
            catch (Exception)
            {
                respuesta = 0;
            }
            return respuesta;
        }

        // DELETE api/<VehiculosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
