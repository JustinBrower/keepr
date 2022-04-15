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
    }
}