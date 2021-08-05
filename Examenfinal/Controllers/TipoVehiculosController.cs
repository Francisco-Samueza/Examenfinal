using Clasesexamen.Data;
using Clasesexamen.Model;
using Clasesexamen.ViewData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenfinal.Controllers
{
    [Authorize]
    public class TipoVehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public TipoVehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }
        public void Combox()
        {
            ViewData["CodigoVehiculo"] = new SelectList(_context.Vehiculos.Select(x => new ViewDataVehiculo
            {
                Codvehiculo = x.Codigo,
                Nombrevehiculo = x.Nombre,
                Estado = x.Estado,
            }
             ).Where(e => e.Estado == 1).ToList(), "Codvehiculo", "Nombrevehiculo");
        }
        [Authorize(Roles = "Admin,User")]
        // GET: TipoVehiculosController
        public ActionResult Index()
        {
            List<ViewDataVehiculo> lsttipovehiculo = new List<ViewDataVehiculo>();
            lsttipovehiculo = _context.TipoVehiculos.Select(x => new ViewDataVehiculo
            {
                Nombrevehiculo=x.CodigoVehiculoNavigation.Nombre,
                Estado=x.Estado,
                DescripcionV=x.Descripcion,
                Codvehiculo=x.Codigo
            }).ToList();
            return View(lsttipovehiculo);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: TipoVehiculosController/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(t => t.Codigo == id).FirstOrDefault();
            Combox();
            return View(tipoVehiculo);
        }
        [Authorize(Roles = "Admin")]
        // GET: TipoVehiculosController/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: TipoVehiculosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipoVehiculo)
        {
            try
            {
                tipoVehiculo.Estado = 1;
                _context.Add(tipoVehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: TipoVehiculosController/Edit/5
        public ActionResult Edit(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(t => t.Codigo == id).FirstOrDefault();
            Combox();
            return View(tipoVehiculo);
        }
        [Authorize(Roles = "Admin")]
        // POST: TipoVehiculosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipovehiculo)
        {
            if(tipovehiculo.Codigo!=id)
            {
                return RedirectToAction("Index");
            }
            try
            {
                Combox();
                _context.Update(tipovehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipovehiculo);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Activar(int id)
        {
            TipoVehiculo vehiculo = _context.TipoVehiculos.Where(v => v.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Desactivar(int id)
        {
            TipoVehiculo vehiculo = _context.TipoVehiculos.Where(v => v.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
