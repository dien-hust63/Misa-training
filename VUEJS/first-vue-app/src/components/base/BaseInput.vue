<template>
  <div class="base-input">
    <label class = "base-input__label">
      {{ label }}
    </label>
    <div class="base-input__input">
      <input v-bind="$attrs" v-on="inputListeners" :tabindex="tabIndex" :value="inputValue"/>
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
    tabIndex:{
      type:String,
      default(){
        return "";
      }
    },
    inputValue:{
      type:String,
      default(){
        return "";
      }
    }
  },
  computed: {
    // We add all the listeners from the parent
    inputListeners: function () {
      var vm = this;
      return Object.assign(
        {},
        this.$listeners,
        {
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
  .base-input__input{
    height: 40px;
    margin:4px 0 0 0;
    padding:0;
  }

  input{
    width:100%;
    height: 40px;
    border:1px solid #bbb;
    border-radius: 4px;
    padding-left: 16px;
    padding-right: 16px;
    outline: none;
    box-sizing: border-box;
    width: 100%;
  }

  input:focus{
    border-color: #019160;
  }
</style>

