import { AppState } from "../AppState"
import { api } from "./AxiosService"


class VaultKeepsService {


    async createVaultKeep(body) {
        const res = await api.post("api/vaultkeeps/", body)
        AppState.vaultKeeps = [...AppState.vaultKeeps, res.data]
    }
    async getTheseVaultKeeps(id) {
        AppState.keeps = []
        const res = await api.get("api/vaults/" + id + "/keeps")
        AppState.keeps = res.data
    }

    async deleteVaultKeep(id) {
        await api.delete("api/vaultkeeps/" + id)
        AppState.vaults = AppState.vaults.filter(k => k.id == id)
    }
}

export const vaultKeepsService = new VaultKeepsService()