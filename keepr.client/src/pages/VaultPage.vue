<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h1>Keepr</h1>
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
import { onMounted } from '@vue/runtime-core'
export default {
  name: 'Vault',
  setup() {
    onMounted(() => {

    })
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>