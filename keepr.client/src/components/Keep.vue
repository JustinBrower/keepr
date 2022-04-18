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
      <div class="row">
        <div class="col-6 d-flex justify-content-start">
          <div class="row" style="height: 20px">
            <div class="col-4">
              <p>Views: {{ keep.views }}</p>
            </div>
            <div class="col-4">
              <p>Keeps: {{ keep.kept }}</p>
            </div>
            <div class="row">
              <div class="col-5 d-flex justify-content-center">
                <p>{{ keep.description }}</p>
              </div>
            </div>
          </div>
          <div></div>
        </div>
        <div class="col-6 d-flex justify-content-end image-fluid">
          <img
            class="keepModalImg image-fluid"
            :src="keep.img"
            alt="keep_img"
          />
        </div>
      </div>
      <div class="row mt-3" v-if="account">
        <div class="col-12 d-flex">
          <form @submit.prevent="addToVault">
            <label class="form-label me-2">Choose Vault:</label>
            <select required class="px-2" v-model="editable.vaultId">
              <option v-for="v in vaults" :key="v.id" :value="v.id">
                <a class="dropdown-item" href="#">{{ v.name }}</a>
              </option>
            </select>
            <button class="btn btn-warning ms-3">Add To Vault</button>
          </form>
          <button
            v-if="account.id == keep.creatorId"
            @click="removeKeep"
            class="btn btn-danger ms-4 justify-content-end"
          >
            Delete
          </button>
          <button
            v-if="route.name == 'Vault' && profile.id == account.id"
            @click="deleteVaultKeep"
            class="btn btn-danger ms-3"
          >
            Remove From Vault
          </button>
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
        await vaultsService.getMyVaults()
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
            await vaultKeepsService.deleteVaultKeep(props.keep.vaultKeepId)
            Pop.toast('Removed from Vault', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.activeProfile)
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
  max-height: 512px;
  max-width: 512px;
  width: auto;
}

.weight {
  font-weight: 800;
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