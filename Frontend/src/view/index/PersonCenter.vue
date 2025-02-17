<template>
    <el-row>
        <el-col :span="6">
            <el-form :model="form" label-width="120px">
                <el-form-item label="Username">
                    <el-input v-model="form.name" />
                </el-form-item>
                <el-form-item label="Password">
                    <el-input v-model="form.password" />
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="onSubmit">Modify</el-button>
                    <el-button>Cancel</el-button>
                </el-form-item>
            </el-form>
        </el-col>
    </el-row>
</template>

<script lang="ts" setup>
import { reactive } from 'vue'
import { useStore } from 'vuex'
import { editNickNameOrPassword } from '../../http';
import { ElMessage } from 'element-plus';
import Tool from '../../global'
import { useRouter } from 'vue-router';
const router = useRouter()
const form = reactive({
    name: useStore().state.NickName,
    password: '',
})

const onSubmit = async () => {
    console.log('submit!')
    let res: Boolean = await editNickNameOrPassword(form.name, form.password) as any as boolean
    if (res) {
        ElMessage.success("Setting successful! You are about to log out, please log in again...")
        setTimeout(function () {
            new Tool().ClearLocalStorage()
            router.push({ path: "/login" })
        }, 2000)
    }else{
        ElMessage.success("Setup failed! Please contact your system administrator")
    }
}
</script>