<template>
  <div class="index">
    <el-container>
      <!-- 左侧 -->
      <el-aside width="200px">
      
        <el-menu
         router
          default-active="/home"
          class="el-menu-vertical-demo"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#ffd04b"
        >

          <el-menu-item index="/home">
            <i class="el-icon-document"></i>
            <span slot="title">首页</span>
          </el-menu-item>
          <el-submenu index="1">
            <template slot="title">
              <i class="el-icon-location"></i>
              <span>权限管理</span>
            </template>
            <el-menu-item index="/role">角色</el-menu-item>
            <el-menu-item index="/user">用户</el-menu-item>
          </el-submenu>
      
          <el-menu-item index="3" disabled>
            <i class="el-icon-document"></i>
            <span slot="title">导航三</span>
          </el-menu-item>

          <el-menu-item index="4">
            <i class="el-icon-setting"></i>
            <span slot="title">导航四</span>
          </el-menu-item>

          <el-submenu index="5">
            <template slot="title">
              <i class="el-icon-setting"></i>
              <span slot="title">导航五</span>
            </template>

            <el-submenu index="5-1">
              <template slot="title">
                <i class="el-icon-setting"></i>
                <span slot="title">选项1</span>
              </template>

              <el-menu-item index="5-1">选项1</el-menu-item>
            </el-submenu>
          </el-submenu>
        </el-menu>
      </el-aside>

      <el-container>
        <!-- 头部 -->
        <el-header>
          <el-menu
        
            class="el-menu-demo"
            mode="horizontal"
         
            background-color="#545c64"
            text-color="#fff"
            active-text-color="#ffd04b"
          
          >

          <h2>头部</h2>
          </el-menu>
        </el-header>

        <!-- 主页 -->
        <el-main>Main

        <router-view></router-view>

        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
export default {
    data() {
      return {
        activeIndex: '1',
        activeIndex2: '1'
      };
    }
};
</script>
<style  >
.el-aside {
display: block;
position: relative;
overflow-y: scroll;
background-color: #545c64 !important;
}
</style>