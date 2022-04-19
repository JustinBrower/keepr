import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class ProfileService {

    async getProfileById(id) {
        const res = await api.get("api/profiles/" + id)
        AppState.activeProfile = res.data
        logger.log("got active profile")
    }
}

export const profilesService = new ProfileService()