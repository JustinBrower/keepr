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
    }
}