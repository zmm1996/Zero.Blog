<template>
  <div>
    <div class="crumbs">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>
          <i class="el-icon-lx-cascades"></i> 角色权限分配
        </el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="container">
      <div class="main">
        <div class="tableRole">
          <el-table
            :data="roleListData"
            style="width: 200px"
            class="roleList"
            border
            size="mini"
            highlight-current-row
            @current-change="handleCurrentChange"
          >
            <el-table-column align="center">
              <template slot="header">
                <h3>角色列表</h3>
                <el-button
                  type="success"
                  icon="el-icon-refresh"
                  circle
                  size="mini"
                  @click="loadRoleList"
                ></el-button>
              </template>
              <template slot-scope="scope">
                <span>{{scope.row.name}}</span>
              </template>
            </el-table-column>
          </el-table>
        </div>

        <div class="treePermission">
          <div>
            <el-tag type="info" style="width:80%">权限列表</el-tag>
            <el-button class="savebutton" size="mini" type="success" @click="onSave">保存</el-button>
          </div>
          <el-tree
          ref="tree"
            :data="permissionTree"
            show-checkbox
            node-key="id"
            default-expand-all
            :default-checked-keys="defaultCheckedKeys"
            :props="defaultProps"
          
          ></el-tree>
        </div>
      </div>
    </div>

    <div :v-loading="loading"  element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
    element-loading-background="rgba(0, 0, 0, 0.8)">

    </div>
  </div>
</template>

<script>
import { getPermissonTreeByRoleId } from "../../api/rbac/permission";
import { getRoleList ,assignPermission} from "../../api/rbac/role";
export default {
  data() {
    return {
      roleListData: [],
      permissionTree: [
        
      ],
      defaultProps: {
        children: "children",
        label: "title"
      },
      currentRow: null, //角色当前选中值,
      defaultCheckedKeys:[],//权限树默认选中
      defaultExpandedKeys:[],//默认张开
loading:false
    };
  },
  mounted() {
    this.loadRoleList();
  },
  methods: {
    handleCurrentChange(val) {
      //选中角色
      this.currentRow = val;
      this.loadPermissonTree();
    },
    loadRoleList() {
      //加载角色
      getRoleList({})
        .then(res => {
          if (res.data.code === 200) {
            this.roleListData = res.data.data;
            this.permissionTree=[]
          }
        })
        .catch(err => {
          console.log(err);
          this.$message.error("获取角色失败");
        });
    },
    loadPermissonTree() {
      //加载权限
      if (this.currentRow) {
        getPermissonTreeByRoleId(this.currentRow.id)
          .then(res => {
            if (res.data.code === 200) {
              this.permissionTree = res.data.data.tree;
            
            if(res.data.data.isSuperAdministrator)
            {
             
              this.defaultCheckedKeys=res.data.data.allPermisson
            }else{
                this.defaultCheckedKeys=res.data.data.selectedPermissions
            }
            }
          })
          .catch(err => {
            this.$message.error("加载出错");
          });
      }
    },
    onSave(){
      this.loading=true;
      // console.log(this.$refs.tree.getCheckedKeys());
       console.log(this.$refs.tree.getCheckedKeys());
      let permissionArr=this.$refs.tree.getCheckedKeys();
      if(!this.currentRow)
      {
          this.$message.error("请选择角色")
          return ;
      }
      assignPermission({roleId:this.currentRow.id,permissions:permissionArr}).then(res=>{
        this.$message.success(res.data.message);
         this.loadPermissonTree();
      }).catch(err=>{
        console.log(err);
      })
      this.loading=false;
    }

  }
};
</script>

<style scoped>
.tableRole {
  float: left;
}
.treePermission {
  width: 80%;
  float: left;
}
.main::after {
  content: "";
  display: block;
  clear: both;
}

.roleList {
  cursor: pointer;
}
.savebutton {
  margin-left: 0em;
}
</style>