<template>
  <div class="container-fluid">
    <div class="row mt-4">
      <div class="col-3 justify-content-center">
        <img :src="profile.picture" alt="prof_pic" />
      </div>
      <div class="col-3 d-flex justify-content-start">
        <h1>{{ profile.name }}</h1>
      </div>
      <div class="col-6"></div>
    </div>
    <div class="row mt-5 pt-5"></div>
    <div class="row">
      <h2>Vaults</h2>
    </div>
    <div class="row">
      <div class="col-2" v-for="v in vaults" :key="v.id">
        <Vault :vault="v" />
      </div>
    </div>
    <div class="row">
      <h2>Keeps</h2>
    </div>
    <div class="row">
      <div class="col-2" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      try {
        profilesService.getProfileById(route.params.id)
        keepsService.getThisUsersKeeps(route.params.id)
        vaultsService.getThisUsersVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>