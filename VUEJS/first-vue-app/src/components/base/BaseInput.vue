<template>
  <div class="base-input">
    <label>
      {{ label }}
    </label>
    <div>
      <input v-bind="$attrs" v-on="inputListeners" />
    </div>
  </div>
</template>

<script>
export default {
  name: "BaseInput",
  props: {
    label: {
      type: String,
      default() {
        return "";
      },
    },
    value: {
      type: String,
      default() {
        return "NV001";
      },
    },
  },
  computed: {
    inputListeners: function () {
      var vm = this;
      // `Object.assign` merges objects together to form a new object
      return Object.assign(
        {},
        // We add all the listeners from the parent
        this.$listeners,
        // Then we can add custom listeners or override the
        // behavior of some listeners.
        {
          // This ensures that the component works with v-model
          input: function (event) {
            vm.$emit("input", event.target.value);
          },
        }
      );
    },
  },
};
</script>

<style scoped>
</style>

