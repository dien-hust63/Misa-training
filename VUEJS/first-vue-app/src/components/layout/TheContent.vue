<template>
  <div class="content">
    <employee-list @showFormStaff="showEmployeeDetail"  @showEditForm="showEditForm"/>
    <employee-detail :class="{'isHide': isHide}" @closeFormStaff="closeEmployeeDetail" :employeeCode="employeeCode"/>
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
      employeeCode: "",
    }
  },
  methods: {
    showEmployeeDetail(employeeCode){
      this.isHide = false;
      this.employeeCode = employeeCode;
    },
    closeEmployeeDetail(){
      this.isHide = true;
    },
    showEditForm(){
      this.isHide = false;  
    }
  },
}
</script>