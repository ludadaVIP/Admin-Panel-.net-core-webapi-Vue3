<template>
    <div class="cardContent">
        <el-card class="box-card" v-for="item in list">
            <CardCom :info="item"></CardCom>
        </el-card>
        <el-card class="left">

            <Pie></Pie>
        </el-card>
        <el-card class="right">

            <Histogram></Histogram>
        </el-card>
        <el-card class="lineCard">

            <Line></Line>
        </el-card>
    </div>
</template>
<script lang="ts" setup>
import { ref } from 'vue'
import CardCom from '../../components/CardCom.vue'
import Pie from '../../components/Echarts/Pie.vue'
import Histogram from '../../components/Echarts/Histogram.vue'
import Line from '../../components/Echarts/Line.vue'
const list = ref([
    {
        "Title": "New Users",
        "Icon": "CoffeeCup",
        "Count": 10291
    },
    {
        "Title": "New Messages",
        "Icon": "Apple",
        "Count": 8912
    },
    {
        "Title": "Transaction Amount",
        "Icon": "Drizzling",
        "Count": 9280
    },
    {
        "Title": "Total Volume",
        "Icon": "Headset",
        "Count": 13600
    }
])
</script>
<style lang="scss" scoped>
.cardContent {
    width: 100%;
    margin: 0px auto;

    .box-card {
        float: left;
        width: 24%;
        margin-right: 5px;
        margin-bottom: 20px;
    }

    .left,
    .right {
        float: left;
        width: 48%;
        margin-bottom: 20px;
    }

    .lineCard {
        width: 97.5%;
    }

    .right {
        margin-left: 20px;
    }

}
</style>