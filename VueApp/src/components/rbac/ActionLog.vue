<template>
  <div>
    <div class="crumbs">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>
          <i class="el-icon-lx-cascades"></i> 操作记录
        </el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="container">
      <!-- <div class="handle-box">
        <el-select v-model="query.status" placeholder="状态" class="handle-select mr10">
          <el-option key="-1" label="全部" value="-1"></el-option>
          <el-option key="1" label="正常" value="1"></el-option>
          <el-option key="0" label="禁用" value="0"></el-option>
        </el-select>
        <el-input v-model="query.name" placeholder="输入关键字" class="handle-input mr10"></el-input>
        <el-button type="primary" icon="el-icon-search" @click="handleSearch">搜索</el-button>
      
      </div> -->

      <el-table :data="tableData" border style="width: 100%" class="spheight">
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column prop="apiUrl" label="地址" width="250"></el-table-column>
        <el-table-column prop="ip" label="IP" width="100"></el-table-column>
        <el-table-column prop="actionUserName" label="操作人"></el-table-column>
        <el-table-column prop="description" label="描述"></el-table-column>
       
        <el-table-column label="操作时间" prop="actionTime"></el-table-column>
       
        
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

  
  </div>
</template>

<script>
import {
  actionLog
} from "../../api/rbac/login";


export default {

  data() {
    return {
       query: {
        status: "",
        name: "",
        pageIndex: 1,
        pageSize: 10,
        totalCount: 0
      },
     tableData:[]
    };
  },
  mounted() {
    this.loadMenuList();
  },
 
  methods: {
   
    loadMenuList() {
      actionLog(this.query)
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
      this.query.pageIndex=val;
      this.loadMenuList();
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