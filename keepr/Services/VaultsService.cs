using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vr;

        public VaultsService(VaultsRepository vr)
        {
            _vr = vr;
        }

        internal Vault GetVaultById(int id)
        {
            return _vr.GetVaultById(id);
        }

        internal Vault CreateVault(Vault vaultData)
        {
            return _vr.CreateVault(vaultData);
        }

        internal Vault EditVault(Vault editedVault, Account userInfo)
        {


            Vault original = GetVaultById(editedVault.Id);
            if (editedVault.CreatorId != userInfo.Id)
            {
                throw new Exception("You aren't allowed to edit this");
            }

            original.Name = editedVault.Name != null ? editedVault.Name : original.Name;
            original.Description = editedVault.Description != null ? editedVault.Description : original.Description;
            original.IsPrivate = editedVault.IsPrivate != editedVault.IsPrivate ? editedVault.IsPrivate : original.IsPrivate;
            _vr.EditVault(original);
            return original;
        }

        internal string RemoveVault(int id, Account userInfo)
        {
            Vault vault = _vr.GetVaultById(id);
            if (vault.CreatorId != userInfo.Id)
            {
                throw new Exception("You aren't allowed to Delort this");
            }
            return _vr.RemoveVault(id);
        }
    }
}