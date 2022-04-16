import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class ProfileService {

    async getProfileById(id) {
        const res = await api.get("api/profiles/" + id)
        logger.log(res.data)
        AppState.activeProfile = res.data
    }
}

export const profilesService = new ProfileService()