<template>
  <div class="combobox" :class="{ 'combobox--show': isShow }">
    <input
      type="text"
      class="combobox__input"
      placeholder="Chọn/Nhập thông tin"
      v-bind="$attrs"
      :value="value"
      v-on="inputListeners"
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
        @click="chooseItem(item['DepartmentName'], index)"
        :class="{ active: isSelectedItem(index) }"
      >
        <i class="fas fa-check checkmark"></i>
        <div class="combobox-item-text">{{ item["DepartmentName"] }}</div>
      </li>
    </ul>
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
      console.log("get data");
      //load dữ liệu và hiển thị
      await axios
        .get("http://cukcuk.manhnv.net/api/Department")
        .then((response) => {
          this.listData = response.data;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    async toggleList() {
      if (this.isShow) {
        this.isShow = false;
      } else {
        await this.getData();
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
      console.log(index == this.selectedItem);
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
          await vm.getData();
          vm.isShow = true;
        },
      });
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/combobox.css");
</style>