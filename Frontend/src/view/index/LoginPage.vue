<template>
    <div class="login">
        <div class="relative">
            <div class="left">
                <el-row>
                    <el-col :span="24">
                        <div class="homepageLogo">
                            <ul>
                                <li>
                                    <el-image style="width: 50px; height: 50px" :src="url" fit="fit" />
                                </li>
                                <li><span>Daniel LU</span></li>
                            </ul>
                        </div>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-image class="boxbg" :src="boxbg" fit="fit" />
                        <p class="p1">WELCOME</p>
                        <p class="p2">Administration Panel</p>
                    </el-col>
                </el-row>
            </div>
            <div class="right">
                <el-row>
                    <el-col :span="24">
                        <h2>LOG IN</h2>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-form :model="form" label-width="120px" label-position="top" size="large" class="form"
                            :rules="rules" ref="ruleFormRef">
                            <el-form-item label="Username" prop="userName">
                                <el-input v-model="form.userName" />
                            </el-form-item>
                            <el-form-item label="Password" prop="passWord">
                                <el-input v-model="form.passWord" type="password" show-password />
                            </el-form-item>
                            <el-form-item>
                                <el-button class="submitBtn" type="primary" @click="onSubmit(ruleFormRef)">Login
                                </el-button>
                            </el-form-item>
                        </el-form>
                    </el-col>
                </el-row>
            </div>
        </div>
    </div>
</template>
<script lang="ts" setup>
import { ref, reactive } from 'vue'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'
import { getToken } from '../../http/index'
import Tool from '../../global'
import { UserInfo } from './class/UserInfo'
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
const store=useStore()
const router = useRouter()
const url = ref('/images/1000051423.jpg')
const boxbg = ref('/images/svgs/login-box-bg.svg')

const form = reactive({
    userName: '',
    passWord: ''
})
const rules = reactive<FormRules>({
    //用户名
    userName: [{ required: true, message: "请输入用户名", trigger: 'blur' }],
    //密码
    passWord: [{ required: true, message: "请输入密码", trigger: 'blur' }]
})
const ruleFormRef = ref<FormInstance>()
const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return;
    await ruleFormRef.validate(async (valid, fields) => {
        if (valid) {
            //请求后端数据，获取token，并将token放入localStorage
            const token = await getToken(form.userName, form.passWord) as any as string
            const user: UserInfo = JSON.parse(new Tool().FormatToken(token))
            localStorage["token"] = token 
            localStorage["nickname"] = user.NickName
            store.commit("SettingNickName",user.NickName)
            store.commit("SettingToken",token)
            router.push({ path: '/desktop' });

        } else {
            console.log("校验不通过！")
            console.log(fields)
            let errors: string = "";
            fields?.userName?.forEach(element => {
                errors += element.message + ';'
            })
            fields?.passWord?.forEach(element => {
                errors += element.message + ';'
            })
            ElMessage({
                type: "warning",
                message: errors
            })
        }
    })
}


</script>
<style lang="scss" scoped>
.login {
    width: 100%;
    height: 100%;

    .relative {
        width: 100%;
        height: 100%;
        text-align: center;

        .left {
            width: 50%;
            height: 100%;
            float: left;
            background-image: url('/images/svgs/login-bg.svg');

            .boxbg {
                width: 350px;
                height: 350px;
                margin-top: 100px;
            }

            .homepageLogo {
                height: 50px;
                line-height: 50px;
                margin-top: 40px;

                span {
                    color: white;
                    font-size: 24px;
                }

                ul {
                    list-style: none;

                    li {
                        float: left;
                        margin-left: 5px;
                    }
                }
            }

            p {
                color: white;
            }

            .p1 {
                font-size: 1.875rem;
                line-height: 2.25rem;
            }

            .p2 {
                font-size: 0.875rem;
                line-height: 1.25rem;
            }
        }

        .right {
            width: 50%;
            float: left;
            padding-top: 15%;

            .form {
                width: 50%;
                margin: 0px auto;

                .submitBtn {
                    width: 100%;
                }
            }
        }
    }
}
</style>