import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultKeepsService {


    async createVaultKeep(body) {
        const res = await api.post("api/vaultkeeps/", body)
    }
    async getTheseVaultKeeps(id) {
        logger.log("id used to get vaultkeeps...", id)
        const res = await api.get("api/vaults/" + id + "/keeps")
        logger.log("vaulkeeps are...", res.data)
        AppState.vaultKeeps = res.data
    }

    async deleteVaultKeep(id) {
        await api.delete("api/vaultkeeps/" + id)
        AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id != id)
    }
}

export const vaultKeepsService = new VaultKeepsService()