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
      <div class="row">
        <div class="col-12">
          <!-- FIXME THIS ACC CHECK DOESN'T WORK -->
          <button v-if="account" class="btn btn-warning">Add To Vault</button>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
  props: {
    keep: {
      typeof: Object,
      required: true
    },
    setup() {
      return {
        account: computed(() => AppState.account)
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