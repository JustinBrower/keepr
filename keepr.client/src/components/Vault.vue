<template>
  <div>
    <router-link v-if="vault.isPrivate == true" :to="{ name: 'Home' }">
      <div class="mt-3">
        <h3 class="d-flex">
          {{ vault.name }}
          <p class="mdi mdi-lock"></p>
        </h3>
      </div>
    </router-link>
    <router-link
      v-if="vault.isPrivate == false"
      :to="{ name: 'Vault', params: { id: vault.id } }"
    >
      <div class="mt-3">
        <h3>
          {{ vault.name }}
        </h3>
      </div>
    </router-link>
  </div>
</template>


<script>
export default {
  props: {
    vault: {
      typeof: Object,
      required: true,
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>