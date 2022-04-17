<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h1>Keepr</h1>
        <h2>{{ vault.name }}</h2>
      </div>
      <div class="row">
        <div class="col-2" v-for="k in keeps" :key="k.id">
          <Keep :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute();
    onMounted(() => {
      try {
        vaultKeepsService.getTheseVaultKeeps(route.params.id)
        vaultsService.getVaultById(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>