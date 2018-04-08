//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BeerOn.Data.DbModels;
//using BeerOn.Data.ModelsDto;
//using BeerOn.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace BeerOn.API.Controllers
//{
//    public class RolesController : Controller
//    {
//        private readonly IRoleService _roleService;
//        private readonly IMapper _mapper;

//        public RolesController(IRoleService roleService, IMapper mapper)
//        {
//            _roleService = roleService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var roles = _roleService.GetAll();

//            return Ok(roles);
//        }

//        [HttpGet("{id}")]
//        public IActionResult Get(long id)
//        {
//            var role = _roleService.GetRole(id);

//            if (role == null)
//                return BadRequest();

//            return Ok(role);
//        }

//        [HttpPost]
//        public IActionResult Create([FromBody]RoleDto role)
//        {
//            if (role == null)
//                return BadRequest();

//            var roleMapped = _mapper.Map<RoleDto, Role>(role);

//            _roleService.InsertRole(roleMapped);

//            return Ok(roleMapped);
//        }
//    }
//}
