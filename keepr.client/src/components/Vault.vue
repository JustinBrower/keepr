<template>
  <div>
    <div v-if="vault.isPrivate == true && account.id != profile.id">
      <router-link :to="{ name: 'Home' }" @click="takeHome">
        <div class="mt-3">
          <h3 class="d-flex">
            {{ vault.name }}
            <p>private/notmine</p>
            <p class="mdi mdi-lock"></p>
          </h3>
        </div>
      </router-link>
    </div>
    <div v-if="vault.isPrivate == true && account.id == profile.id">
      <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
        <div class="mt-3">
          <h3 class="d-flex">
            {{ vault.name }}
            <p>private and mine</p>
            <p class="mdi mdi-lock"></p>
          </h3>
        </div>
      </router-link>
    </div>
    <div v-if="vault.isPrivate == false">
      <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
        <div class="mt-3">
          <h3>
            {{ vault.name }}
            <p>notprivate</p>
          </h3>
        </div>
      </router-link>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
export default {
  props: {
    vault: {
      typeof: Object,
      required: true,
    }
  },
  setup() {
    return {
      async takeHome() {
        try {
          Pop.toast('That Vault is Private', 'error')
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>