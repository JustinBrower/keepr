<template>
  <div
    class="modal fade"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
    id="form-modal"
  >
    <div class="modal-dialog modal-xl" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            <slot name="title"><!--title slot--></slot>
          </h5>
          <button
            type="button"
            class="btn-close bg-primary"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <slot name="body"><!--body slot--></slot>
        </div>
        <div class="">
          <slot name="footer"><!--footer slot--></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped>
</style>