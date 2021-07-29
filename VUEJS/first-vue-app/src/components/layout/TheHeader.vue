<template>
   <div class="header">
        <div class="header-left">
            <div class="toggle"></div>
            <div class="logo"></div>
        </div>
        <div class="header-right">
            <div class="dropdown dropdown-restaurant">
                <div class="dropdown-value"></div>
                <i class="fas fa-chevron-down"></i>
                <ul class="dropdown-list">
                </ul>
            </div>
            <div class="user">
                <div class="user-avatar"></div>
                <div class="user-name">
                    Nguyễn Văn Diện
                </div>
                <div class="user-option"></div>
            </div>
        </div>
    </div>
</template>

<style scoped>
@import url('../../css/layout/header.css');
</style>

<script>
export default {
    name: 'TheHeader',
}
</script>