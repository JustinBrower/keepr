<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div>{{ vault.name }}</div>
  </router-link>
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