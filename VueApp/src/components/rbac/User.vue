<template>
  <div>
    <div class="crumbs">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>
          <i class="el-icon-lx-cascades"></i> 用户管理
        </el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="container">
      <div class="handle-box">
        <el-select v-model="query.status" placeholder="状态" class="handle-select mr10">
          <el-option key="-1" label="全部" value="-1"></el-option>
          <el-option key="1" label="正常" value="1"></el-option>
          <el-option key="0" label="禁用" value="0"></el-option>
        </el-select>
        <el-input v-model="query.name" placeholder="输入关键字" class="handle-input mr10"></el-input>
        <el-button type="primary" icon="el-icon-search" @click="handleSearch">搜索</el-button>
        <el-button type="primary" icon="el-icon-add" class="handle-del mr10" @click="addUser">新增</el-button>
      </div>

      <el-table :data="tableData" border style="width: 100%" class="spheight">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column prop="loginName" label="登录名" width="180"></el-table-column>
        <el-table-column prop="displayName" label="显示名" width="180"></el-table-column>
        <el-table-column label="状态">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.status==1?'success':'danger'"
            >{{scope.row.status==1?'正常':'禁用'}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="用户类型">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
            >{{scope.row.userType==0?'超级管理员':(scope.row.userType==1?'管理员':'普通用户')}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="是否删除">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.isDeleted==1?'danger':'success'"
            >{{scope.row.isDeleted==1?'是':'否'}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="创建时间" prop="createdTime"></el-table-column>
        <el-table-column label="创建人" prop="createdByUserName"></el-table-column>
        <el-table-column label="操作" align="center">
          <template slot-scope="scope">
            <el-button
              type="text"
              icon="el-icon-edit"
              title="编辑"
              @click="editUser(scope.$index, scope.row)"
            ></el-button>
            <el-button
              title="添加"
              type="text"
              icon="el-icon-delete"
              class="red"
              @click="handleDelete(scope.$index, scope.row)"
            ></el-button>
            <el-button
              title="分配角色"
              type="text"
              icon="el-icon-user-solid"
              class="red"
              @click="distributionRole(scope.$index, scope.row)"
            ></el-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="block">
        <span class="demonstration"></span>
        <el-pagination
          background
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="query.pageIndex"
          :page-size="query.pageSize"
          layout="prev, pager, next, jumper"
          :total="query.totalCount"
        ></el-pagination>
      </div>
    </div>

    <!--弹框添加编辑-->
    <el-drawer
      :title="formTitle"
      :visible.sync="addVisible"
      :direction="direction"
      :before-close="handleClose"
    >
      <el-form ref="form" :model="form" :rules="rules" label-width="70px">
        <el-form-item label="登录名" prop="loginName" label-width="80px">
          <el-input placeholder="请输入登录名" v-model="form.loginName" clearable></el-input>
        </el-form-item>
        <el-form-item label="昵称" prop="displayName" label-width="80px">
          <el-input placeholder="请输入昵称" v-model="form.displayName" clearable></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password" label-width="80px">
          <el-input placeholder="请输入密码" v-model="form.password" clearable></el-input>
        </el-form-item>
        <el-form-item label="用户状态">
          <el-switch
            v-model="form.status"
            active-value="1"
            inactive-value="0"
            active-color="#13ce66"
            inactive-color="#ff4949"
          ></el-switch>
        </el-form-item>
        <el-form-item label="描述">
          <el-input type="textarea" v-model="form.description"></el-input>
        </el-form-item>
        <div>
          <el-button @click="addVisible=false">取 消</el-button>
          <el-button
            type="primary"
            @click="onsubmit('form')"
            :loading="loading"
          >{{ loading ? '提交中 ...' : '确 定' }}</el-button>
        </div>
      </el-form>
    </el-drawer>

    <!-- 分配角色 -->

    <el-drawer title="角色分配" :visible.sync="isdistribution" direction="rtl" size="45%">
      <!-- 穿梭框 -->
<div>
          <el-button @click="isdistribution=false">取 消</el-button>
          <el-button
            type="primary"
            @click="Save"
            :loading="loading"
          >{{ loading ? '提交中 ...' : '保存' }}</el-button>
        </div>
      <el-transfer ref="transfer" v-model="selectRole" :titles="['未分配', '已分配']" :data="roleList"></el-transfer>
    
    </el-drawer>
  </div>
</template>

<script>
import {
  getUserList,
  cteateUser,
  getUserById,
  editUser,
  deleteUser,
  saveRoles
} from "../../api/rbac/user";
import {
  getRoleList,
  getRolesByUserId,
  getAllRolesByUserId
} from "../../api/rbac/role";
// import { Message } from "element-ui";
export default {
  data() {
    return {
      query: {
        status: "",
        name: "",
        pageIndex: 1,
        pageSize: 20,
        totalCount: 0
      },
      form: {
        id: "",
        loginName: "",
        displayName: "",
        password: "",
        avatar: "/img/img.146655c9.jpg",
        userType: 2, //0超级管理员 1管理员 2普通用户
        isLocked: 0,
        status: "1",
        isDeleted: 0,
        description: ""
      },
      rules: {
        //验证规则
        loginName: [
          { required: true, message: "请输入登录名称", trigger: "blur" },
          { min: 5, max: 20, message: "长度在 5 到 20 个字符", trigger: "blur" }
        ],
        displayName: [
          { required: true, message: "请输入昵称", trigger: "blur" },
          { min: 5, max: 20, message: "长度在 5 到 20 个字符", trigger: "blur" }
        ],
        password: [
          { required: true, message: "请输入登录密码", trigger: "blur" },
          { min: 6, max: 20, message: "长度在 6 到 20 个字符", trigger: "blur" }
        ]
      },
      tableData: [],
      addVisible: false,
      direction: "rtl", //展开方式默认从右往左
      loading: false,
      formMode: "create",
      //角色分配
      isdistribution: false,
      roleList: [], //所有列表
      selectRole: [], //已选的
      currentId:''
    };
  },
  mounted() {
    this.loadUserList();
  },
  computed: {
    formTitle() {
      if (this.formMode === "create") {
        return "创建用户";
      } else if (this.formMode === "edit") {
        return "编辑用户";
      }
      return "";
    }
  },
  methods: {
    addUser() {
      //添加
      this.formMode = "create";
      this.addVisible = true;
    },
    editUser(index, row) {
      //添加
      this.formMode = "edit";
      getUserById(row.id)
        .then(res => {
          if (res.data.code === 200) {
            for (let item in this.form) {
              if (item === "status") {
                this.form[item] = res.data.data[item] + "";
              } else {
                this.form[item] = res.data.data[item];
              }
            }

            console.log(this.form);
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          console.log(err);
        });
      this.addVisible = true;
    },
    handleSearch() {
      this.loadUserList();
    },
    onsubmit(formName) {
      this.$confirm("确定要提交表单吗？").then(_ => {
        //表单验证
        this.loading = true;
        this.$refs[formName].validate(valid => {
          if (!valid) {
            this.$message.error("请检查表单");
            return false; //无法阻止，会继续向下执行
          } else {
            //验证通过

            if (this.formMode === "create") {
              this.doCreateUser(formName);
            } else if (this.formMode === "edit") {
              this.doEditUser();
            }
          }
        });
        this.loading = false;
      });
    },
    doCreateUser(formName) {
      cteateUser(this.form)
        .then(res => {
          if (res.data.code == 200) {
            this.$message.success(res.data.message);
            this.addVisible = false; //关闭弹框
            //重置表单
            this.loadUserList(); //刷新当前数据
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          console.log(err.response.data);
          if (err.response.status == 400) {
            var res = err.response.data;
            for (var item in res) {
              //  for (let index = 0; index < res[item].length; index++) {
              //        this.$message.error(res[item][index]);

              //  }
              this.$message.error(res[item][0]);
              return;
            }
          }
        });
    },
    doEditUser() {
      editUser(this.form.id, this.form)
        .then(res => {
          if (res.data.code == 200) {
            this.$message.success(res.data.message);
            this.addVisible = false; //关闭弹框

            this.loadUserList(); //刷新当前数据
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    handleDelete(index, row) {
      this.$confirm("确定要删除用户:" + row.displayName + " 吗?", "提示")
        .then(_ => {
          deleteUser(row.id, {})
            .then(res => {
              if (res.data.code == 200) {
                this.$message.success(res.data.message);
                this.loadUserList();
              } else {
                this.$message.error(res.data.message);
              }
            })
            .catch(err => {});
        })
        .catch(_ => {
          this.$message.warning(_);
        });
    },
    loadUserList() {
      getUserList({})
        .then(res => {
          if (res.data.code == 200) {
            this.tableData = res.data.data;
            this.query.totalCount = res.data.totalCount;
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          this.$message.error(err.Message);
        });
    },
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`);
    },
    handleClose(done) {
      done(); //关闭
      this.$refs["form"].resetFields();
    },
    distributionRole(index, row) {
      this.isdistribution = true;
      // //获取所有角色
      //   getRoleList({}).then(res=>{
      //     this.roleList=res.data.data
      //   })
      // //获取用户已有的角色
      //   getRolesByUserId(row.id).then(res=>{
      //       this.selectRole=res.data.data
      //   })
      this.currentId=row.id
      getAllRolesByUserId(row.id).then(res => {
        this.selectRole = res.data.data.assignedRoles;
        this.roleList = res.data.data.rolelist;
      });
    },
    Save() {
      //this.$refs['transfer'].
      
     saveRoles({userId:this.currentId,roleIds:this.selectRole}).then(res=>{
       this.$message.success(res.data.message);
       this.isdistribution=false;
     }).catch(err=>{
        console.log(err);
     })
    }
  }
};
</script>

<style scoped>
.handle-box {
  margin-bottom: 20px;
}

.handle-select {
  width: 120px;
}

.handle-input {
  width: 300px;
  display: inline-block;
}
.table {
  width: 100%;
  font-size: 14px;
}
.red {
  color: #ff0000;
}
.mr10 {
  margin-right: 10px;
}
.table-td-thumb {
  display: block;
  margin: auto;
  width: 40px;
  height: 40px;
}
.spheight tr {
  height: 20px;
}
.block {
  text-align: right;
}
</style>