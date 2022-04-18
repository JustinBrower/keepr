import { AppState } from "../AppState"
import { api } from "./AxiosService"


class VaultsService {

    async createVault(body) {
        const res = await api.post("api/vaults/", body)
        AppState.vaults = [...AppState.vaults, res.data]
    }

    async getMyVaults() {
        AppState.vaults = []
        const res = await api.get("account/vaults")
        AppState.vaults = res.data
    }

    async getThisUsersVaults(id) {
        AppState.vaults = []
        const res = await api.get("api/profiles/" + id + "/vaults")
        AppState.vaults = res.data

    }

    async getVaultById(id) {
        AppState.activeVault = {}
        const res = await api.get("api/vaults/" + id)
        AppState.activeVault = res.data
    }

    async editVault(id, body) {
        const res = await api.put("api/vaults/" + id, body)

        AppState.vaults = AppState.vaults.filter(k => k.id != id)
        AppState.vaults = [...AppState.vaults, res.data]
    }

    async deleteVault(id) {
        await api.delete("api/vaults/" + id)
        AppState.vaults = AppState.vaults.filter(k => k.id == id)
    }

}

export const vaultsService = new VaultsService()