<template>
  <div>
    <div class="crumbs">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>
          <i class="el-icon-lx-cascades"></i> 权限管理
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
        <el-button
          type="primary"
          icon="el-icon-add"
          class="handle-del mr10"
          @click="addPermission"
        >新增</el-button>
      </div>

      <el-table :data="tableData" border style="width: 100%" class="spheight">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column prop="name" label="权限名" width="120"></el-table-column>
        <el-table-column prop="menuId" label="关联菜单" width="100"></el-table-column>
        <el-table-column prop="actionCode" label="操作码"></el-table-column>
        <el-table-column label="状态" align="center">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.status==1?'success':'danger'"
            >{{scope.row.status==1?'正常':'禁用'}}</el-tag>
          </template>
        </el-table-column>

        <el-table-column label="类型" align="center">
          <template slot-scope="scope">
            <el-tag
              effect="dark"
              :type="scope.row.type==0?'info':'success'"
            >{{scope.row.type==0?'按钮':'菜单'}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="创建时间" prop="createdTime"></el-table-column>
        <el-table-column label="创建人" prop="createdByUserName"></el-table-column>
        <el-table-column label="操作" width="180" align="center">
          <template slot-scope="scope">
            <el-button
              type="text"
              icon="el-icon-edit"
              @click="editPermission(scope.$index, scope.row)"
            >编辑</el-button>
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
        <el-form-item label="权限名称" prop="name" label-width="80px">
          <el-input placeholder="请输入权限名称" v-model="form.name" clearable></el-input>
        </el-form-item>
        <el-form-item label="操作码" prop="actionCode" label-width="80px">
          <el-input placeholder="请输入操作码" v-model="form.actionCode" clearable></el-input>
        </el-form-item>

        <el-form-item label="关联菜单" prop="menuId" label-width="80px">
          <tree-select :data="menuOptions" @change="selectChange" :checkedKeys="selectkey"></tree-select>
        </el-form-item>
        <el-form-item label="类型" prop="type" label-width="80px">
          <el-select v-model="form.type" placeholder="请选择">
            <el-option label="按钮" value="0"></el-option>
            <el-option label="菜单" value="1"></el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="状态">
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
  getPermissionList,
  cteatePermission,
  getPermissionById,
  editPermission,
  deletePermission,
  getMenuTree
} from "../../api/rbac/permission";
import treeSelect from "../../components/common/selecteDemo";
import { timingSafeEqual } from "crypto";
// import { ColorPicker } from "element-ui";
// import { Message } from "element-ui";
export default {
  components: {
    treeSelect
  },
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
        menuId: "",
        name: "",
        actionCode: "",
        icon: "",
        description: "",
        status: "1",
        isDelete: 0,
        type: "0"
      },
      rules: {
        //验证规则
        name: [
          { required: true, message: "请输入权限名称", trigger: "blur" },
          { min: 2, max: 10, message: "长度在 2 到 10 个字符", trigger: "blur" }
        ],
        menuId: [
          { required: true, message: "请选择菜单", trigger: "blur" }
        ],
        type: [
          { required: true, message: "请选择类型", trigger: "blur" },
        
        ]
      },
      tableData: [],
      addVisible: false,
      direction: "rtl", //展开方式默认从右往左
      loading: false,
      formMode: "create",
      menuOptions: [],
      selectkey: []
    };
  },
  mounted() {
    this.loadPermissionList();
  },
  computed: {
    formTitle() {
      if (this.formMode === "create") {
        return "创建权限";
      } else if (this.formMode === "edit") {
        return "编辑权限";
      }
      return "";
    }
  },
  methods: {
    selectChange(data) {
      this.selectkey = [];
      this.selectkey.push(data[0].value);
     
      this.form.menuId = data[0].value;
      console.log(data)
    },
    addPermission() {
      //添加
      this.formMode = "create";
      this.loadMenuTree();
      this.addVisible = true;
    },
    editPermission(index, row) {
    
      //编辑
      this.formMode = "edit";
      getPermissionById(row.id)
        .then(res => {
        
          if (res.data.code === 200) {
            for (let item in this.form) {
              if (item === "status") {
                this.form[item] = res.data.data[item] + "";
              } else if (item === "menuId") {
                if (res.data.data[item]) {
                  this.selectkey = [];
                  this.selectkey.push(res.data.data[item]);
                    this.form.menuId=res.data.data[item]
                }
              } else {
                this.form[item] = res.data.data[item];
              }
            }
            this.form.type=this.form.type+''
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          console.log(err);
        });
      this.loadMenuTree();
      this.addVisible = true;
    },
    handleSearch() {
      this.loadPermissionList();
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
              this.doCreatePermission(formName);
            } else if (this.formMode === "edit") {
              this.doEditPermission();
            }
          }
        });
        this.loading = false;
      });
    },
    doCreatePermission(formName) {
      cteatePermission(this.form)
        .then(res => {
          if (res.data.code == 200) {
            this.$message.success(res.data.message);
            this.addVisible = false; //关闭弹框
            //重置表单
            this.loadPermissionList(); //刷新当前数据
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
    doEditPermission() {
      editPermission(this.form.id, this.form)
        .then(res => {
          if (res.data.code == 200) {
            this.$message.success(res.data.message);
            this.addVisible = false; //关闭弹框

            this.loadPermissionList(); //刷新当前数据
          } else {
            this.$message.error(res.data.message);
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    handleDelete(index, row) {
      this.$confirm("确定要删除:" + row.name + " 吗?", "提示")
        .then(_ => {
          deletePermission(row.id, {})
            .then(res => {
              if (res.data.code == 200) {
                this.$message.success(res.data.message);
                this.loadPermissionList();
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
    loadPermissionList() {
      getPermissionList({})
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
    loadMenuTree() {
      //加载菜单树
      getMenuTree("8eb66204-8023-c1eb-6162-39f22ef35f5f")
        .then(res => {
          if (res.data.code == 200) {
            let dataJson = JSON.stringify(res.data.data);
            dataJson = dataJson
              .replace(/title/g, "label")
              .replace(/childen/g, "children");
            this.menuOptions = JSON.parse(dataJson);
          }
        })
        .catch(err => {
          console.log(err);
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