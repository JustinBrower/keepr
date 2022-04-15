using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _kr;

        public KeepsService(KeepsRepository kr)
        {
            _kr = kr;
        }

        internal List<Keep> GetAllKeeps()
        {
            return _kr.GetAllKeeps();
        }

        internal Keep CreateKeep(Keep keepData)
        {
            return _kr.CreateKeep(keepData);
        }

        internal Keep EditKeep(Keep editedKeep, Account userInfo)
        {
            Keep original = GetKeepById(editedKeep.Id);

            if(original.CreatorId != userInfo.Id){
                throw new Exception("You may not edit this.");

            }

            original.Name = editedKeep.Name != null ? editedKeep.Name : original.Name;
            original.Description = editedKeep.Description != null ? editedKeep.Description : original.Description;
            original.Img = editedKeep.Img != null ? editedKeep.Img : original.Img;

            // FIXME CANT NULL INTS I GUESS
            original.Views = editedKeep.Views != 0 ? editedKeep.Views : original.Views;
            original.Kept = editedKeep.Kept != 0 ? editedKeep.Kept : original.Kept;
            _kr.EditKeep(original);
            return original;
        }

        internal Keep GetKeepById(int id)
        {
            return _kr.GetKeepById(id);
        }

        internal string RemoveKeep(int id, Account userInfo)
        {
            Keep keep = _kr.GetKeepById(id);
            if (keep == null)
            {
                // FIXME NEED THIS TO RETURN 204 CODE
                return null;
            }
            if (keep.CreatorId != userInfo.Id)
            {
                throw new Exception("You may not Delort this.");
            }
            return _kr.RemoveKeep(id);
        }
    }
}