<template>
  <div class="login-wrap">
    <div class="ms-login">
      <div class="ms-title">后台管理系统</div>
      <el-form :model="param" :rules="rules" ref="login" label-width="0px" class="ms-content">
        <el-form-item prop="username">
          <el-input v-model="param.username" placeholder="username">
            <el-button slot="prepend" icon="el-icon-lx-people"></el-button>
          </el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input
            type="password"
            placeholder="password"
            v-model="param.password"
            @keyup.enter.native="submitForm()"
          >
            <el-button slot="prepend" icon="el-icon-lx-lock"></el-button>
          </el-input>
        </el-form-item>
        <div class="login-btn">
          <el-button
            :loading="loading"
            type="primary"
            @click="submitForm()"
          >{{loading?"玩命登录中...":"登录"}}</el-button>
        </div>
        <p class="login-tips">Tips : 用户名和密码随便填。</p>
      </el-form>
    </div>
  </div>
</template>

<script>
import { login } from "../../api/rbac/login";
import ass from "../../router/get-routes.js";
import router from "../../router/index.js";
export default {
  data: function() {
    return {
      param: {
        username: "pengyuyan",
        password: "123123"
      },
      rules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" }
        ],
        password: [{ required: true, message: "请输入密码", trigger: "blur" }]
      },
      loading: false
    };
  },
  methods: {
    submitForm() {
      this.loading = true;

      this.$refs.login.validate(valid => {
        if (valid) {
          login(this.param)
            .then(res => {
              if (res.data.code == 200) {
                this.$message.success("登录成功");
                localStorage.setItem("ms_username", this.param.username);
                  localStorage.setItem("ms_displayname",res.data.data.displayName);
                  localStorage.setItem("zero_token", res.data.data.token);

                  //   const getRoutes= ass.initsss()
                  //   console.log("1")
                  //   router.options.routes=getRoutes
                  // router.addRoutes(getRoutes);
                  //  console.log("2")
                  //     console.log(getRoutes)
                  //      console.log(router)
                if (this.$route.query.redirect) {
                  this.$router.push(this.$route.query.redirect);
                  return;
                }
                this.$router.push("/");
              } else {
                   this.close();
                this.$message.error(res.data.message);
              }
            })
            .catch(err => {
                 this.close();
                //console.log(err.message)
            });
        } else {
             this.close();
          this.$message.error("请输入账号和密码");
          console.log("error submit!!");
          return false;
        }
      });
   
    },
    close(){
        this.loading=false;
    }
  }
};
</script>

<style scoped>
.login-wrap {
  position: relative;
  width: 100%;
  height: 100%;
  background-image: url(../../assets/img/login-bg.jpg);
  background-size: 100%;
}
.ms-title {
  width: 100%;
  line-height: 50px;
  text-align: center;
  font-size: 20px;
  color: #fff;
  border-bottom: 1px solid #ddd;
}
.ms-login {
  position: absolute;
  left: 50%;
  top: 50%;
  width: 350px;
  margin: -190px 0 0 -175px;
  border-radius: 5px;
  background: rgba(255, 255, 255, 0.3);
  overflow: hidden;
}
.ms-content {
  padding: 30px 30px;
}
.login-btn {
  text-align: center;
}
.login-btn button {
  width: 100%;
  height: 36px;
  margin-bottom: 10px;
}
.login-tips {
  font-size: 12px;
  line-height: 30px;
  color: #fff;
}
</style>