using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppProveedoresAPI.Models;

namespace AppProveedoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly AppProveedoresBdContext _context;

        public ProveedoresController(AppProveedoresBdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var proveedores = await _context.TblProvProveedores.ToListAsync();

            return Ok(proveedores);
        }
    }
}