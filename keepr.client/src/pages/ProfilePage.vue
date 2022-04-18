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
      <div>
        <button
          data-bs-toggle="modal"
          data-bs-target="#addVault"
          v-if="account.id == profile.id"
          class="btn btn-success"
        >
          Add Vault
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col-2" v-for="v in vaults" :key="v.id">
        <Vault :vault="v" />
      </div>
    </div>
    <div class="row">
      <h2>Keeps</h2>
      <div>
        <button
          data-bs-toggle="modal"
          data-bs-target="#addKeep"
          v-if="(account.id = profile.id)"
          class="btn btn-success mb-3"
        >
          Add Keep
        </button>
      </div>
    </div>
    <div class="masonry-with-columns">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
  <Modal id="addKeep">
    <template #title>Add Keep</template>
    <template #body>
      <div class="row">
        <div class="col-12">
          <form @submit.prevent="addKeep">
            <div class="col-md-4 mb-2">
              <label for="" class="form-label">Name: </label>
              <input
                v-model="editable.name"
                required
                max="255"
                type="text"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Name..."
              />
            </div>
            <div class="col-md-4 mb-2">
              <label class="form-label">Image: </label>
              <input
                max="255"
                v-model="editable.img"
                required
                type="text"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Image Link..."
              />
            </div>
            <div class="col-md-4 mb-2">
              <label class="form-label">Description: </label>
              <input
                v-model="editable.description"
                required
                max="255"
                type="text"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Description..."
              />
            </div>
            <div class="col-12 d-flex justify-content-end">
              <button class="btn btn-primary">Create</button>
            </div>
          </form>
        </div>
      </div>
    </template>
  </Modal>
  <Modal id="addVault">
    <template #title>Add Vault</template>
    <template #body>
      <div class="row">
        <div class="col-12">
          <form @submit.prevent="addVault">
            <div class="col-md-4 mb-2">
              <label for="" class="form-label">Name: </label>
              <input
                v-model="editable.name"
                required
                max="255"
                type="text"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Name..."
              />
            </div>
            <div class="col-md-4 mb-2">
              <label class="form-label">Description: </label>
              <input
                v-model="editable.description"
                required
                max="255"
                type="text"
                class="form-control"
                aria-describedby="helpId"
                placeholder="Description..."
              />
            </div>
            <div class="col-md-4">
              <label class="form-label mt-4 me-2">Visibility: </label>
              <select required class="px-2" v-model="editable.isPrivate">
                <option value="true">
                  <a class="dropdown-item" href="#">Private</a>
                </option>
                <option value="false">
                  <a class="dropdown-item" href="#">Public</a>
                </option>
              </select>
            </div>
            <div class="col-12 d-flex justify-content-end">
              <button class="btn btn-primary">Create</button>
            </div>
          </form>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
export default {
  setup() {
    const route = useRoute();
    const editable = ref({})
    const account = AppState.account
    onMounted(async () => {
      try {
        await profilesService.getProfileById(route.params.id)
        logger.log('profile is...', AppState.activeProfile)
        await keepsService.getThisUsersKeeps(route.params.id)
        logger.log('keeps are...', AppState.keeps)
        await vaultsService.getThisUsersVaults(route.params.id)
        if (account.id == route.params.id) {
          await vaultsService.getMyVaults()
        }
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      editable,
      async addKeep() {
        try {
          Modal.getOrCreateInstance(document.getElementById('addKeep')).hide()
          await keepsService.createKeep(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async addVault() {
        try {
          if (editable.value.isPrivate == "true") {
            editable.value.isPrivate = true
          } else {
            editable.value.isPrivate = false
          }
          logger.log(editable.value)
          Modal.getOrCreateInstance(document.getElementById('addVault')).hide()
          await vaultsService.createVault(editable.value)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account)
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