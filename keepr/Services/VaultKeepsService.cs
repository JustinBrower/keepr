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
        private readonly KeepsService _ks;

        public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, KeepsRepository kr, KeepsService ks)
        {
            _vkr = vkr;
            _vr = vr;
            _kr = kr;
            _ks = ks;
        }

        internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            return _vkr.GetKeepsByVaultId(id);
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            Vault vault = _vr.GetVaultById(vaultKeepData.VaultId);
            if (vaultKeepData.CreatorId != vault.CreatorId)
            {
                throw new Exception("Not yours");
            }
            _ks.AddKept(vaultKeepData.KeepId);
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