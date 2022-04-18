<template>
  <div class="d-flex justify-content-center">
    <img
      data-bs-toggle="modal"
      :data-bs-target="'#keep' + keep.id"
      class="keepImg hoverable"
      :src="keep.img"
      alt="keep_img"
    />
    <p class="keepText">{{ keep.name }}</p>
    <div>
      <router-link :to="{ name: 'Profile', params: { id: keep.creatorId } }">
        <img
          v-if="keep.creator"
          class="mini-pic hoverable"
          :src="keep.creator.picture"
          alt=""
        />
      </router-link>
    </div>
  </div>

  <Modal :id="'keep' + keep.id">
    <template #title> {{ keep.name }} </template>
    <template #body>
      <div class="row justify-content-center">
        <div class="col-5">
          <div>{{ keep.description }}</div>
        </div>
        <div class="col-5">
          <img
            class="keepModalImg image-fluid"
            :src="keep.img"
            alt="keep_img"
          />
        </div>
      </div>
      <!-- FIXME ADD IN v-if="account" -->
      <div class="row">
        <div class="col-12">
          <form>
            <div>
              <label class="form-label me-2">Choose Vault:</label>
              <!-- <select required class="px-2" v-model="editable.vaultId"> -->
              <!-- <option v-for="v in vaults" :key="v.id">
                <a class="dropdown-item" href="#">{{ v.name }}</a>
              </option> -->
              <!-- </select> -->
            </div>
            <button @click="addToVault" class="btn btn-warning">
              Add To Vault
            </button>
            <!-- FIXME ADD IN v-if="account" -->
            <button @click="removeKeep" class="btn btn-danger ms-4">
              Delete
            </button>
          </form>
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
export default {
  props: {
    keep: {
      typeof: Object,
      required: true
    },
    setup(props) {
      const editable = ref({})
      onMounted(() => {
        try {
          vaultsService.getMyVaults()
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      })
      return {
        editable,
        async addtoVault() {
          try {
            Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).hide()
            editable.value.keepId = props.keep.id
            vaultKeepsService.createVaultKeep(editable.value)
          } catch (error) {
            logger.error(error)
            Pop.toast(error.message, 'error')
          }
        },
        async removeKeep() {
          try {
            logger.log("hello")
            if (await Pop.confirm()) {
              Modal.getOrCreateInstance(document.getElementById('keep' + props.keep.id)).hide()
              keepsService.deleteKeep(props.keep.id)
            }
          } catch (error) {
            logger.error(error)
            Pop.toast(error.message, 'error')
          }
        },
        account: computed(() => AppState.account),
        vaults: computed(() => AppState.vaults)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keepImg {
  opacity: 80%;
  max-height: 200px;
  width: auto;
  padding: 1rem;
}

// .keepModalImg {
//   max-height: 50vh;
//   width: auto;
// }

.keepText {
  font-weight: 800;
  //   position: relative;
  //   bottom: -103px;
  //   right: 93px;
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