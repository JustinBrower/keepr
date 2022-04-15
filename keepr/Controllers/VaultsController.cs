using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;

        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Vault>> GetVaultById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetVaultById(id);
                if (vault.IsPrivate == true && vault.CreatorId != userInfo?.Id)
                {
                    return BadRequest("This Vault is private.");
                }
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vs.CreateVault(vaultData);
                vault.Creator = userInfo;
                return Created($"api/vaults/{vault.Id}", vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault([FromBody] Vault editedVault, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                editedVault.Id = id;
                Vault vault = _vs.EditVault(editedVault, userInfo);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> RemoveVault(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_vs.RemoveVault(id, userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // NOTE BELOW IS THE VAULTKEEPS STUFF


        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetVaultById(id);
                if (vault.IsPrivate == true && vault.CreatorId != userInfo?.Id)
                {
                    return BadRequest("This Vault is private.");
                }
                List<Keep> keeps = _vks.GetKeepsByVaultId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}