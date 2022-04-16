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
  </div>
  <Modal :id="'keep' + keep.id">
    <template #title> {{ keep.name }} </template>
    <template #body>
      <div>{{ keep.description }}</div>
    </template>
  </Modal>
</template>


<script>
export default {
  props: {
    keep: {
      typeof: Object,
      required: true
    }
  },
  setup() {
    return {}
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

.keepText {
  font-weight: 800;
  position: relative;
  bottom: -103px;
  right: 93px;
}

.hoverable:hover {
  cursor: pointer;
}
</style>