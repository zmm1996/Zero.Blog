<template>
  <div class="sidebar">
    <el-menu
      class="sidebar-el-menu"
      :default-active="onRoutes"
      :collapse="collapse"
      background-color="#324157"
      text-color="#bfcbd9"
      active-text-color="#20a0ff"
      unique-opened
      router
    >
      <template v-for="item in items">
        <template v-if="item.subs">
          <el-submenu :index="item.index" :key="item.index">
            <template slot="title">
              <i :class="item.icon"></i>
              <span slot="title">{{ item.title }}</span>
            </template>
            <template v-for="subItem in item.subs">
              <el-submenu v-if="subItem.subs" :index="subItem.index" :key="subItem.index">
                <template slot="title">{{ subItem.title }}</template>
                <el-menu-item
                  v-for="(threeItem,i) in subItem.subs"
                  :key="i"
                  :index="threeItem.index"
                >{{ threeItem.title }}</el-menu-item>
              </el-submenu>
              <el-menu-item v-else :index="subItem.index" :key="subItem.index">{{ subItem.title }}</el-menu-item>
            </template>
          </el-submenu>
        </template>
        <template v-else>
          <el-menu-item :index="item.index" :key="item.index">
            <i :class="item.icon"></i>
            <span slot="title">{{ item.title }}</span>
          </el-menu-item>
        </template>
      </template>
    </el-menu>
  </div>
</template>

<script>
import { getInitMenuTree } from "../../api/rbac/menu";
import bus from "../common/bus";
export default {
  data() {
    return {
      collapse: false,
      items: [
        {
          icon: "el-icon-s-home",
          index: "dashboard",
          title: "系统首页"
        },
        // {
        //   icon: "el-icon-s-custom",
        //   index: "2",
        //   title: "用户权限管理",
        //   subs: [
        //     {
        //       icon: "el-icon-setting",
        //       index: "menu",
        //       title: "菜单管理"
        //     },
        //     {
        //       icon: "el-icon-setting",
        //       index: "role",
        //       title: "角色管理"
        //     },
        //     {
        //       icon: "el-icon-setting",
        //       index: "user",
        //       title: "用户管理"
        //     },
        //     {
        //       icon: "el-icon-setting",
        //       index: "permission",
        //       title: "权限管理"
        //     },
        //     {
        //       icon: "el-icon-setting",
        //       index: "rolepermission",
        //       title: "角色权限分配"
        //     }
        //   ]
        // },
        // {
        //   icon: "el-icon-lx-cascades",
        //   index: "table",
        //   title: "基础表格"
        // },
        // {
        //   icon: "el-icon-lx-copy",
        //   index: "tabs",
        //   title: "tab选项卡"
        // },
        // {
        //   icon: "el-icon-lx-calendar",
        //   index: "3",
        //   title: "表单相关",
        //   subs: [
        //     {
        //       index: "form",
        //       title: "基本表单"
        //     },
        //     {
        //       index: "3-2",
        //       title: "三级菜单",
        //       subs: [
        //         {
        //           index: "editor",
        //           title: "富文本编辑器"
        //         },
        //         {
        //           index: "markdown",
        //           title: "markdown编辑器"
        //         }
        //       ]
        //     },
        //     {
        //       index: "upload",
        //       title: "文件上传"
        //     }
        //   ]
        // },
        // {
        //   icon: "el-icon-lx-emoji",
        //   index: "icon",
        //   title: "自定义图标"
        // },
        // {
        //   icon: "el-icon-pie-chart",
        //   index: "charts",
        //   title: "schart图表"
        // },
        // {
        //   icon: "el-icon-rank",
        //   index: "6",
        //   title: "拖拽组件",
        //   subs: [
        //     {
        //       index: "drag",
        //       title: "拖拽列表"
        //     },
        //     {
        //       index: "dialog",
        //       title: "拖拽弹框"
        //     }
        //   ]
        // },
        // {
        //   icon: "el-icon-lx-global",
        //   index: "i18n",
        //   title: "国际化功能"
        // },
        // {
        //   icon: "el-icon-s-help",
        //   index: "7",
        //   title: "错误处理",
        //   subs: [
        //     {
        //       index: "permission1",
        //       title: "权限测试"
        //     },
        //     {
        //       index: "404",
        //       title: "404页面"
        //     }
        //   ]
        // },
        // {
        //   icon: "el-icon-lx-redpacket_fill",
        //   index: "/donate",
        //   title: "支持作者"
        // },

        // {
        //   icon: "el-icon-tickets",
        //   index: "/actionlog",
        //   title: "操作记录"
        // }
      ]
    };
  },
  computed: {
    onRoutes() {
      //alert(this.$route.path)
      //当前路由，返回当前选中项
      return this.$route.path.replace("/", "");
    }
  },
  mounted() {
    this.initMenu();
  },
  created() {
    // 通过 Event Bus 进行组件间通信，来折叠侧边栏
    bus.$on("collapse", msg => {
      this.collapse = msg;
      bus.$emit("collapse-content", msg);
    });
  },
  methods: {
    initMenu() {

   let cacheMenu= window.localStorage.getItem("zore_menu")

    if(!cacheMenu){
      getInitMenuTree()
        .then(res => {
       
        var ss=  this.digui(res.data);

       window.localStorage.setItem("zore_menu",JSON.stringify(ss))
       this.items=  this.items.concat(ss);
      
        })
        .catch(err => {
          this.$message.error("初始化菜单失败");
        });
        }else{
            console.log("cache")
           this.items=  this.items.concat(JSON.parse(cacheMenu));
        }
    },
    digui(data) {

      var aa=[];
      data.forEach(element => {
        let o = {
          icon: element.icon,
          index:  element.url,
          title: element.name,
          hideInMenu:element.hideInMenu,
          subs:element.children.length==0?null: this.digui(element.children)
        };
      if(!o.subs)
      {
        delete o.subs
      }
       // console.log(o.index)
        aa.push(o);
      });

      return aa;
    }
  }
};
</script>

<style scoped>
.sidebar {
  display: block;
  position: absolute;
  left: 0;
  top: 70px;
  bottom: 0;
  overflow-y: scroll;
}
.sidebar::-webkit-scrollbar {
  width: 0;
}
.sidebar-el-menu:not(.el-menu--collapse) {
  width: 250px;
}
.sidebar > ul {
  height: 100%;
}
</style>
