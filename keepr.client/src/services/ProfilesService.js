import { AppState } from "../AppState"
import { api } from "./AxiosService"


class ProfileService {

    async getProfileById(id) {
        const res = await api.get("api/profiles/" + id)
        AppState.activeProfile = res.data
    }
}

export const profilesService = new ProfileService()