using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class ProfilesService
    {

        private readonly ProfilesRepository _pr;

        public ProfilesService(ProfilesRepository pr)
        {
            _pr = pr;
        }

        internal Account GetProfileById(string id)
        {
            return _pr.GetProfileById(id);
        }

        internal List<Keep> GetKeepsByProfileId(string id)
        {
            return _pr.GetKeepsByProfileId(id);
        }

        internal List<Vault> GetVaultsByProfileId(string id)
        {
            return _pr.GetVaultsByProfileId(id);
        }
    }
}