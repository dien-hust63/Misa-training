<template>
  <div class="content">
    <employee-list @showEmployeeDetail1="showEmployeeDetail"/>
    <employee-detail :class="{'isHide': isHide}" />
  </div>
</template>

<style scoped>
@import url("../../css/layout/content.css");
</style>

<script>
import EmployeeList from '../../view/employee/EmployeeList.vue';
import EmployeeDetail from '../../view/employee/EmployeeDetail.vue'
export default {
  name: 'TheContent',
  components: {
    EmployeeList,
    EmployeeDetail,
  },
  data(){
    return{
      isHide:true,
    }
  },
  methods: {
    showEmployeeDetail(){
      this.isHide = false;
    }
  },
}
</script>