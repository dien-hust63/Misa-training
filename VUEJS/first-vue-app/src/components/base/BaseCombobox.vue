<template>
  <div class="combobox-wraper">
    <div class="combobox__label" style="margin-bottom: 4px" v-if="label">
      {{ label }}
    </div>
    <div class="combobox" :class="{ 'combobox--show': isShow }">
      <input
        ref="input"
        type="text"
        class="combobox__input"
        placeholder="Chọn/Nhập thông tin"
        v-bind="$attrs"
        :value="value"
        v-on="inputListeners"
        :comboboxData="comboboxData"
      />
      <div class="combobox__input-cancel" @click="clearComboboxValue">
        <i class="fas fa-times-circle"></i>
      </div>
      <div class="combobox__dropdown" @click="toggleList">
        <i class="fas fa-chevron-down combobox__icon"></i>
      </div>
      <ul class="combobox__list">
        <li
          v-for="(item, index) in listData"
          :key="index"
          class="combobox__item"
          @click="chooseItem(item[keyValue], index)"
          :class="{ active: isSelectedItem(index) }"
        >
          <i class="fas fa-check checkmark"></i>
          <div class="combobox-item-text">{{ item[keyValue] }}</div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BaseCombobox",
  props: {
    value: {
      type: String,
      default() {
        return "";
      },
    },
    typeCombobox: {
      type: String,
      default() {
        return "";
      },
    },
    label: {
      type: String,
      default() {
        return "";
      },
    },
    apiUrl: {
      type: String,
      default() {
        return "";
      },
    },
    keyValue: {
      type: String,
      default() {
        return "";
      },
    },
    keyData: {
      type: String,
      default() {
        return "";
      },
    },
    mode: {
      type: String,
      default() {
        return "";
      },
    },
    listComboboxData: {
      type: Array,
      default() {
        return [];
      },
    },
  },

  data() {
    return {
      isShow: false,
      listData: [],
      selectedItem: -1,
    };
  },
  methods: {
    async getData() {
      //load dữ liệu và hiển thị
      try {
        await axios.get(this.apiUrl).then((response) => {
          this.listData = response.data;
        });
      } catch (error) {
        console.log(error);
      }
    },
    async toggleList() {
      if (this.isShow) {
        this.isShow = false;
      } else {
        if (this.mode == "manual") {
          this.listData = this.listComboboxData;
        } else {
          await this.getData();
        }
        this.isShow = true;
      }
    },
    clearComboboxValue() {
      this.$emit("clearComboboxValue", this.typeCombobox);
    },

    chooseItem(selectedValue, index) {
      this.isShow = false;
      this.selectedItem = index;
      this.$emit("updateComboboxValue", this.typeCombobox, selectedValue);
    },

    isSelectedItem(index) {
      return index == this.selectedItem;
    },
  },
  computed: {
    /**
     * lắng nghe các sự kiện từ input
     */
    inputListeners() {
      var vm = this;
      return Object.assign({}, this.$listeners, {
        input: function (event) {
          vm.$emit("input", event.target.value);
        },
        async focus() {
          if (vm.mode == "manual") {
            vm.listData = vm.listComboboxData;
          } else {
            await vm.getData();
          }
          vm.isShow = true;
        },
      });
    },

    comboboxData() {
      if (this.selectedItem != -1) {
        return this.listData[this.selectedItem][this.keyData];
      }
      return "";
    },
  },

  watch: {
    comboboxData: function (value) {
      this.$emit("updateComboboxData", this.typeCombobox, value);
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/combobox.css");
</style>