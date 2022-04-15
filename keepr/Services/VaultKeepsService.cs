using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkr;
        private readonly VaultsRepository _vr;
        private readonly KeepsRepository _kr;

        public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, KeepsRepository kr)
        {
            _vkr = vkr;
            _vr = vr;
            _kr = kr;
        }

        internal List<Keep> GetKeepsByVaultId(int id)
        {
            return _kr.GetKeepsByVaultIdNow(id);
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            Vault vault = _vr.GetVaultById(vaultKeepData.VaultId);
            if (vaultKeepData.CreatorId != vault.CreatorId)
            {
                throw new Exception("Not yours");
            }
            return _vkr.CreateVaultKeeps(vaultKeepData);
        }

        internal string RemoveVaultKeep(int id, Account userInfo)
        {
            VaultKeep vaultKeep = _vkr.GetVaultKeepById(id);
            Vault vault = _vr.GetVaultById(vaultKeep.VaultId);
            if (vault.CreatorId != userInfo.Id)
            {
                throw new Exception("You aren't allowed to Delort this");
            }
            return _vkr.RemoveVaultKeep(id);
        }
    }
}