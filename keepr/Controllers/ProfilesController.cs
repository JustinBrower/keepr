using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;

        public ProfilesController(ProfilesService ps)
        {
            _ps = ps;
        }

        [HttpGet("{id}")]

        public ActionResult<Account> GetProfileById(string id)
        {
            try
            {
                Account profile = _ps.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
        {
            try
            {
                List<Keep> keeps = _ps.GetKeepsByProfileId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]

        public ActionResult<List<Vault>> GetVaultsByProfileId(string id)
        {
            try
            {
                List<Vault> vaults = _ps.GetVaultsByProfileId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}