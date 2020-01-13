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
        <el-button type="primary" icon="el-icon-add" class="handle-del mr10" @click="addRole">新增</el-button>
      </div>

      <el-table :data="tableData" border style="width: 100%" class="spheight">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column prop="name" label="角色名称" width="180"></el-table-column>
        <el-table-column label="状态">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.status==1?'success':'danger'"
            >{{scope.row.status==1?'正常':'禁用'}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="内置">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.isBuiltin?'success':'danger'"
            >{{scope.row.isBuiltin?"是":"否"}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="超管" align="center">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.isSuperAdministrator?'success':'danger'"
            >{{scope.row.isSuperAdministrator?"是":"否"}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="创建时间" prop="createdTime"></el-table-column>
        <el-table-column label="创建人" prop="createdByUserName"></el-table-column>
        <el-table-column label="操作" width="180" align="center">
          <template slot-scope="scope">
            <el-button type="text" icon="el-icon-edit" @click="editRole(scope.$index, scope.row)">编辑</el-button>
            <el-button
              type="text"
              icon="el-icon-delete"
              class="red"
              @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button>
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

    <!--弹框-->
    <el-drawer
      :title="formTitle"
      :visible.sync="addVisible"
      :direction="direction"
      :before-close="handleClose"
    >
      <el-form ref="form" :model="form" :rules="rules" label-width="70px">
        <el-form-item label="角色名称" prop="name" label-width="80px">
          <el-input placeholder="请输入角色名称" v-model="form.name" clearable></el-input>
        </el-form-item>
        <el-form-item label="角色状态">
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
  </div>
</template>

<script>
import {
  getRoleList,
  cteateRole,
  getRoleById,
  editRole,
  deleteRole
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
        name: "",
        description: "",
        status: "1",
        isSuperAdministrator: false,
        isDeleted: 0,
        isBuiltin: false
      },
      rules: {
        //验证规则
        name: [
          { required: true, message: "请输入角色名称", trigger: "blur" },
          { min: 3, max: 20, message: "长度在 3 到 20 个字符", trigger: "blur" }
        ]
      },
      tableData: [],
      addVisible: false,
      direction: "rtl", //展开方式默认从右往左
      loading: false,
      formMode: "create"
    };
  },
  mounted() {
    this.loadRoleList();
  },
  computed: {
    formTitle() {
      if (this.formMode === "create") {
        return "创建角色";
      } else if (this.formMode === "edit") {
        return "编辑角色";
      }
      return "";
    }
  },
  methods: {
    addRole() {
      //添加
      this.formMode = "create";
      this.addVisible = true;
    },
    editRole(index, row) {
      //添加
      this.formMode = "edit";
      getRoleById(row.id)
        .then(res => {
          if (res.data.code === 200) {
            let entity = res.data.data;
            this.form.status = entity.status + "";
            this.form.name = entity.name;
            this.form.description = entity.description;
            this.form.isSuperAdministrator = entity.isSuperAdministrator;
            this.form.isDeleted = entity.isDeleted;
            this.form.isBuiltin = entity.isBuiltin;
            this.form.id = entity.id;
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
      this.loadRoleList();
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
              this.doCreateRole(formName);
            } else if (this.formMode === "edit") {
              this.doEditRole();
            }
          }
        });
        this.loading = false;
      });
    },
    doCreateRole(formName) {
      cteateRole(this.form)
        .then(res => {
          if (res.data.code == 200) {
            this.$message.success(res.data.message);
            this.addVisible = false; //关闭弹框
            //重置表单
            this.loadRoleList(); //刷新当前数据
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          this.$message.error("网络错误");
        });
    },
    doEditRole() {
      editRole(this.form.id, this.form)
        .then(res => {
          if (res.data.code == 200) {
            this.$message.success(res.data.message);
            this.addVisible = false; //关闭弹框

            this.loadRoleList(); //刷新当前数据
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    handleDelete(index, row) {
      this.$confirm("确定要删除角色:" + row.name + " 吗?", "提示")
        .then(_ => {
          deleteRole(row.id, {})
            .then(res => {
              if (res.data.code == 200) {
                this.$message.success(res.data.message);
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
    loadRoleList() {
      getRoleList({})
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