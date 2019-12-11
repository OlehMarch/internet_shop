﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using internet_shop.Models;
using internet_shop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace internet_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromosController : Controller
    {
        private readonly PromosService _promosService;
       
        public PromosController(PromosService promosService)
        {
            _promosService = promosService;
        }
        // GET: api/Promos
        [HttpGet]
        public IEnumerable<Promos> Get()
        {
            return _promosService.GetAllPromos();
        }

        // GET: api/Promos/5
        [HttpGet("{id}", Name = "Get")]

        public IActionResult Get(int id)
        {
            Promos promos = _promosService.GetPromoById(id);
            if (promos == null)
            {
                return NotFound($"No promo found with id: {id}");
            }
            else
            {
                return Ok(promos);
            }            
        }


        // POST: Promos/Create
        [HttpPost]
        public IActionResult Post([FromBody] Promos value)
        {
            var data = _promosService.AddPromo(value.Name, value.Value);

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }



        // DELETE: Promos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _promosService.DeletePromoById(id);
            if (data.exception != null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data.result);
            }
        }
        // PUT: api/User
        [HttpPut]
        public IActionResult Put([FromBody] Promos value)
        {
            var data = _promosService.Updatepromos(value);
            if (data.promos == null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data);
            }
        }
    }
}