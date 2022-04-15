using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkr;

        public VaultKeepsService(VaultKeepsRepository vkr)
        {
            _vkr = vkr;
        }
    }
}