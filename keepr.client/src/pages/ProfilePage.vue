<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-3">
        <img :src="profile.picture" alt="prof_pic" />
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
export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      try {
        profilesService.getProfileById(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>