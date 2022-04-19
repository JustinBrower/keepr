<template>
  <div class="card bg-dark text-white">
    <img class="card-img" :src="keep.img" alt="keep_img" />
    <div @click="openModal" class="card-img-overlay hoverable">
      <h5 class="card-title weight">
        {{ keep.name }}
      </h5>
      <p class="card-text weight">{{ keep.description }}</p>
      <p class="card-text"></p>
      <div
        @click.stop="goTo('Profile', keep.creatorId)"
        class="d-flex align-items-end"
        style="height: 20px; width: 10px"
      >
        <img
          v-if="keep.creator"
          class="mini-pic hoverable"
          :src="keep.creator.picture"
          alt="p_pic"
        />
      </div>
    </div>
  </div>

  <Modal :id="'keep' + keep.id">
    <template #title> {{ keep.name }} -- {{ keep.creator.name }} </template>
    <template #body>
      <div class="container-fluid p-0">
        <div class="row">
          <div class="col-6">
            <div class="row" style="height: 20px">
              <div class="col-6">
                <p>Views: {{ keep.views }}</p>
              </div>
              <div class="col-6">
                <p>Keeps: {{ keep.kept }}</p>
              </div>
            </div>
            <div class="row mt-3">
              <div
                class="col-12 d-flex justify-content-start"
                style="border-top: 2px solid black"
              >
                <p class="mt-1 keepText">{{ keep.description }}</p>
              </div>
            </div>
            <div
              class="row align-items-end"
              style="min-height: 45vh; max-height: 80%"
              v-if="user.isAuthenticated"
            >
              <div>
                <form @submit.prevent="addToVault">
                  <label v-if="vaults.length > 0" class="form-label"
                    >Choose Vault:</label
                  >
                  <div v-if="vaults.length > 0">
                    <select required class="px-2" v-model="editable.vaultId">
                      <option v-for="v in vaults" :key="v.id" :value="v.id">
                        <a class="dropdown-item" href="#">{{ v.name }}</a>
                      </option>
                    </select>
                  </div>
                  <div v-if="vaults.length == 0">
                    <p>Please create a vault to use this feature</p>
                  </div>
                  <button v-if="vaults.length > 0" class="btn btn-primary mt-3">
                    KeepIt
                  </button>
                  <button
                    type="button"
                    v-if="account.id == keep.creatorId"
                    @click="removeKeep"
                    class="btn btn-danger mt-3 ms-1"
                  >
                    Delete
                  </button>
                  <button
                    type="button"
                    v-if="route.name == 'Vault' && profile.id == account.id"
                    @click="deleteVaultKeep"
                    class="btn btn-warning mt-3 ms-1"
                  >
                    UnKeep
                  </button>
                </form>
              </div>
            </div>
          </div>

          <!-- IMAGE START -->
          <div class="col-6 d-flex justify-content-end">
            <img
              class="keepModalImg shadow rounded"
              :src="keep.img"
              alt="keep_img"
            />
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import { router } from '../router'
import { useRoute } from 'vue-router'
export default {
  props: {
    keep: {
      typeof: Object,
      required: true
    },
  },
  setup(props) {
    const editable = ref({})
    const route = useRoute();
    onMounted(async () => {
      try {
        if (AppState.user.isAuthenticated) {
          await vaultsService.getMyVaults()
        }
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      editable,
      route,
      async addToVault() {
        try {
          Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).hide()
          editable.value.keepId = props.keep.id
          await vaultKeepsService.createVaultKeep(editable.value)
          Pop.toast("Added to Vault", 'success')
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).hide()
            await keepsService.deleteKeep(props.keep.id)
            Pop.toast("Keep Delorted", 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async goTo(page, userId) {
        try {
          Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).hide()
          router.push({ name: page, params: { id: userId } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async openModal() {
        try {
          Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).show()
          if (props.keep.creatorId != this.account.id) {
            await keepsService.addView(props.keep.id)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async deleteVaultKeep() {
        try {
          if (await Pop.confirm()) {
            Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).hide()
            await vaultKeepsService.deleteVaultKeep(props.keep.vaultKeepId)
            Pop.toast('Removed from Vault', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      profile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
    }
  }
}

</script>


<style lang="scss" scoped>
.keepImg {
  max-width: 200px;
  height: auto;
  margin: 20px;
}

.keepModalImg {
  // object-fit: fill;
  height: 100%;
  max-width: 400px;
  width: auto;
}

.weight {
  font-weight: 800;
}

.keepText {
  word-wrap: break-word;
}

.hoverable:hover {
  cursor: pointer;
}

.mini-pic {
  width: 30px;
  height: 30px;
  border-radius: 50%;
}
</style>