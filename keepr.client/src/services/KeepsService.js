import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class KeepsService {

    async createKeep(body) {
        const res = await api.post("api/keeps/", body)
        AppState.keeps = [...AppState.keeps, res.data]
    }

    async getAllKeeps() {
        AppState.keeps = []
        const res = await api.get("api/keeps")
        AppState.keeps = res.data
    }

    async getThisUsersKeeps(id) {
        AppState.keeps = []
        const res = await api.get("api/profiles/" + id + "/keeps")
        AppState.keeps = res.data
    }

    async getKeepById(id) {
        AppState.activeKeep = {}
        const res = await api.get("api/keeps/" + id)
        AppState.activeKeep = res.data
    }

    async editKeep(id, body) {
        const res = await api.put("api/keeps/" + id, body)

        AppState.keeps = AppState.keeps.filter(k => k.id != id)
        AppState.keeps = [...AppState.keeps, res.data]
    }

    async addView(id) {
        const res = await api.get("api/keeps/" + id)
    }

    async deleteKeep(id) {
        await api.delete("api/keeps/" + id)
        AppState.keeps = AppState.keeps.filter(k => k.id == id)
    }

}

export const keepsService = new KeepsService()