using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MyWebAppMVC.Contracts.Common;
using MyWebAppMVC.Contracts.ViewModels.Departments;
using MyWebAppMVC.Extensions;
using MyWebAppMVC.Models;
using MyWebAppMVC.Service;

namespace MyWebAppMVC.Controllers.Api
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsApiController(IGenericService<Department> service) : ControllerBase
    {
        // GET: api/departments?pageNumber=1&pageSize=10
        [HttpGet]
        [ProducesResponseType<PagedResult<DepartmentDto>>(StatusCodes.Status200OK)]
        public ActionResult<PagedResult<DepartmentDto>> GetAll(
            [FromQuery] PaginationQuery query,
            [FromServices] IMapper mapper)
        {

            //var paged = service.GetPaged(query);

            //var result = new PagedResult<DepartmentDto>(
            //    paged.Items.Select(d => new DepartmentDto(d.Id, d.Name)),
            //    paged.TotalCount,
            //    query);

            var result = service.GetAllQueryable()
                .ProjectTo<DepartmentDto>(mapper.ConfigurationProvider)
                .ToPagedResult(query);

            return Ok(result);
        }

        // GET: api/departments/5
        [HttpGet("{id:int}")]
        [ProducesResponseType<DepartmentDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DepartmentDto> GetById(int id)
        {
            var department = service.GetById(id);
            if (department is null)
                return NotFound();

            return Ok(new DepartmentDto(department.Id, department.Name));
        }

        // POST: api/departments
        [HttpPost]
        [ProducesResponseType<DepartmentDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DepartmentDto> Create([FromBody] DepartmentCreateDto dto)
        {
            var department = new Department { Name = dto.Name };
            var created = service.Create(department);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new DepartmentDto(created.Id, created.Name));
        }

        // PUT: api/departments/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] DepartmentDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!service.Exists(id))
                return NotFound();

            service.Update(new Department { Id = dto.Id, Name = dto.Name });
            return NoContent();
        }

        // DELETE: api/departments/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (!service.Exists(id))
                return NotFound();

            service.Delete(id);
            return NoContent();
        }
    }
}