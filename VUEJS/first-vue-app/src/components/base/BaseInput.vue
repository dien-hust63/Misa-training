<template>
  <div class="base-input">
    <label class="base-input__label">
      {{ label }} <span v-if="required">(<b class="text--red">*</b>)</span>
    </label>

    <div class="base-input__input">
      <input
        ref="input"
        v-bind="$attrs"
        :value="value"
        v-on="inputListeners"
        :tabindex="tabIndex"
      />
    </div>
    <div class="text--red"></div>
  </div>
</template>

<script>
export default {
  name: "BaseInput",
  inheritAttrs: false,
  data() {
    return {};
  },
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
        return "";
      },
    },
    tabIndex: {
      type: String,
      default() {
        return "";
      },
    },
    inputValue: {
      type: String,
      default() {
        return "";
      },
    },
    required: {
      type: Boolean,
      default() {
        return false;
      },
    },
  },

  mounted() {},

  methods: {
    /**
     * focus input từ parent
     */
    focusInput() {
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
    },
  },
  computed: {
    // We add all the listeners from the parent
    inputListeners: function () {
      var vm = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          vm.$emit("input", event.target.value);
        },
        blur: function (event) {
          console.log("blur form input");
          console.log(event);
        },
      });
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/input.css");
</style>

