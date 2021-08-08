<template>
  <div class="base-input">
    <!-- <ValidationProvider :name="label" ref="provider" :rules="rules" v-slot="{ errors }"> -->
    <label class="base-input__label">
      {{ label }} <span v-if="required">(<b class="text--red">*</b>)</span>
    </label>

    <div
      class="base-input__input"
      :class="{ 'base-input__input--error': isError }"
    >
      <input
        ref="input"
        v-bind="$attrs"
        :value="value"
        v-on="inputListeners"
        :tabindex="tabIndex"
      />
    </div>
    <div class="text--red" style="margin-top: 2px">{{ errors }}</div>
    <!-- </ValidationProvider> -->
  </div>
</template>

<script>
export default {
  name: "BaseInput",
  inheritAttrs: false,
  data() {
    return {
      errors: "",
      isError: false,
    };
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
    // rules: {
    //   type: String,
    //   default() {
    //     return "";
    //   },
    // },
    required: {
      type: Boolean,
      default() {
        return false;
      },
    },
    inputCheck: {
      type: Boolean,
      default() {
        return false;
      },
    },
  },

  methods: {
    /**
     * focus input từ parent
     */
    focusInput() {
      this.$nextTick(() => {
        this.$refs.input.focus();
      });
    },

    /**
     * validate email đúng định dạng
     * @param {String} email xâu email người dùng nhập vào
     * @returns {Boolean} true nếu đúng định dạng
     * author: nvdien(8/8/2021)
     * modified: nvdien(8/8/2021)
     */
    validEmail: function (email) {
      var re =
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
    },

    validateInput(self) {
      console.log(self.value);
      if (self.required && (self.value === null || self.value === "")) {
        console.log("alpo");
        self.errors = "Trường này bắt buộc nhập";
        self.isError = true;
      }
      if (self.$refs.input.type == "email" && self.value != null) {
        if (!self.validEmail(self.value)) {
          self.errors = "Sai định dạng email";
          self.isError = true;
        }
      }
    },
  },
  computed: {
    // We add all the listeners from the parent
    inputListeners: function () {
      var self = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          self.$emit("input", event.target.value);
          self.errors = "";
          self.isError = false;
        },
        blur: function () {
          self.validateInput(self);
        },
      });
    },
  },

  watch: {
    inputCheck: function () {
      this.validateInput(this);
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/input.css");
</style>

