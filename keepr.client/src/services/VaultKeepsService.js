import { AppState } from "../AppState"
import { api } from "./AxiosService"


class VaultKeepsService {


    async createVaultKeep(body) {
        const res = await api.post("api/vaultkeeps/", body)
        AppState.vaultKeeps = [...AppState.vaultKeeps, res.data]
    }
    async getTheseVaultKeeps(id) {
        const res = await api.get("api/vault/" + id + "/vaultkeeps")
        AppState.vaults = res.data
    }

    async deleteVaultKeep(id) {
        await api.delete("api/vaultkeeps/" + id)
        AppState.vaults = AppState.vaults.filter(k => k.id == id)
    }
}

export const vaultKeepsService = new VaultKeepsService()