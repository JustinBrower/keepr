<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h1>Keepr</h1>
        <h2>
          {{ vault.name }}
          <button @click="deleteVault" class="btn btn-danger">Delete</button>
        </h2>
      </div>
      <h2>Keeps</h2>
      <div class="masonry-with-columns">
        <div v-for="k in keeps" :key="k.id">
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
import { router } from '../router'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
        await vaultKeepsService.getTheseVaultKeeps(route.params.id)
        await vaultsService.getVaultById(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      async deleteVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(route.params.id)
            router.push({ name: 'Home' })
            Pop.toast('Vault Delorted', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>